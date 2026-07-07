# POS Minimarket — Backend API Documentation

## Overview

REST API for a Point-of-Sale minimarket system. Handles authentication, product catalog, unit/price management, employee management, and transactional invoice saving with full history retrieval.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 9 |
| ORM | Entity Framework Core (EF Core) |
| Raw SQL / Pagination | Dapper |
| Database | PostgreSQL 14+ |
| Authentication | JWT Bearer (configured, not yet enforced on endpoints) |
| Password Hashing | BCrypt.Net (12 rounds) |
| API Docs | Swagger / OpenAPI |

---

## Project Structure

```
pos_api_app/
├── Controllers/       — HTTP endpoints (thin layer, delegates to Services)
├── Services/          — Business logic
├── Repositories/      — Data access (EF Core + Dapper)
│   └── Entities/      — Entity-specific repositories
├── Contracts/         — Interfaces for repositories and utilities
├── Models/
│   ├── BaseEntity.cs  — Shared base (id, created_time, updated_time, is_deleted)
│   └── Entities/      — Table-mapped models
├── DTOs/              — Request/response shapes per feature
├── Data/
│   └── PosDbContext.cs
├── Utilities/
│   ├── Handlers/      — TokenHandler, ResponseHandler, SQLGeneralHandler
│   ├── Hashing.cs     — BCrypt wrapper
│   ├── StaticValue.cs — Response message constants
│   └── GetConfig.cs   — Static IConfiguration accessor
└── Migrations/
```

---

## Setup & Running

### Prerequisites
- .NET 9 SDK
- PostgreSQL running on `localhost:5432`

### Configuration (`appsettings.json`)

```json
{
  "URL_HOST": "http://localhost:5000",
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=posdb;Username=postgres;Password=root"
  },
  "JWTService": {
    "Key": "KLHNRMRNZIPVGMVG6",
    "Issuer": "UrlIssuer",
    "Audience": "UrlAudience"
  }
}
```

### Run

```bash
cd pos_api_app_net
dotnet run --project pos_api_app
```

API is available at: `http://localhost:5000`  
Swagger UI: `https://localhost:7173/swagger` (development)

### Apply Migrations

```bash
dotnet ef database update --project pos_api_app
```

---

## Database Schema

All tables have a shared base: `id` (serial PK), `created_time`, `updated_time`, `is_deleted`.

### `tb_m_role`
| Column | Type | Notes |
|---|---|---|
| `id` | int (PK) | Auto-increment |
| `name` | varchar(50) | Role name |

### `tb_m_account`
| Column | Type | Notes |
|---|---|---|
| `id` | int (PK) | |
| `username` | varchar(100) | Unique |
| `email` | varchar(100) | |
| `password` | varchar(200) | BCrypt hash |
| `role_id` | int (FK → tb_m_role) | |

### `tb_m_employee`
| Column | Type | Notes |
|---|---|---|
| `id` | int (PK) | |
| `firstname` | varchar(200) | |
| `lastname` | varchar(200) | |
| `account_id` | int (FK → tb_m_account) | |

### `tb_m_product`
| Column | Type | Notes |
|---|---|---|
| `id` | int (PK) | |
| `barcode_id` | varchar(255) | Unique |
| `title` | varchar(150) | |

### `tb_m_unit`
| Column | Type | Notes |
|---|---|---|
| `id` | int (PK) | |
| `name` | varchar(200) | Unique, stored lowercase |

### `tb_tr_price`
| Column | Type | Notes |
|---|---|---|
| `id` | int (PK) | |
| `product_id` | int (FK → tb_m_product) | |
| `unit_id` | int (FK → tb_m_unit) | |
| `amount` | decimal(18,2) | Price per unit |

### `tb_tr_transaction`
| Column | Type | Notes |
|---|---|---|
| `id` | int (PK) | |
| `account_id` | int? (FK → tb_m_account) | Nullable (cashier) |
| `transaction_date` | timestamp | UTC |
| `total_price_amount` | decimal(18,2) | Sum of all items |
| `pay_amount` | decimal(18,2) | Cash tendered |

### `tb_tr_transaction_item`
| Column | Type | Notes |
|---|---|---|
| `id` | int (PK) | |
| `transaction_id` | int (FK → tb_tr_transaction) | |
| `barcode_id` | varchar(255) | Snapshot at time of sale |
| `title_product` | varchar(255) | Snapshot |
| `quantity_type` | varchar(200) | Unit name snapshot (e.g. "kg") |
| `quantity` | decimal | |
| `price_product` | decimal(18,2) | Unit price at time of sale |
| `total_price_product` | decimal(18,2) | quantity × price |
| `unit_List` | varchar(255) | Semicolon-joined available unit names |

---

## Architecture

```
HTTP Request
    ↓
Controller        — validates HTTP, maps to service call, returns IActionResult
    ↓
Service           — business logic, DB transaction management (BeginTransaction / Commit / Rollback)
    ↓
Repository        — data access; GeneralRepository<T> provides base CRUD;
                    entity-specific repos extend it with custom queries (Dapper for pagination)
    ↓
PosDbContext      — EF Core DbContext (Npgsql provider)
    ↓
PostgreSQL
```

### Response Envelope

All endpoints return `ResponseDTO<T>`:
```json
{
  "statusCode": 200,
  "message": "Success",
  "data": { ... }
}
```

Paginated list endpoints wrap data in `ResponseTableDTO<T>`:
```json
{
  "statusCode": 200,
  "message": "Success",
  "data": {
    "totalRecord": 50,
    "currentPage": 1,
    "totalPage": 5,
    "dataTable": [ ... ]
  }
}
```

---

## API Endpoints

Base path: `/pos_api_app`

---

### Auth — `/pos_api_app/Auth`

#### `POST /Register`
Register a new account.

**Request body:**
```json
{
  "username": "cashier01",
  "email": "cashier@example.com",
  "password": "secret123"
}
```

**Response:** `ResponseDTO<bool>` — `data: true` on success.

---

#### `POST /Login`
Authenticate and get user info.

**Request body:**
```json
{
  "username": "cashier01",
  "password": "secret123"
}
```

**Response:** `ResponseDTO<AuthDTO>`
```json
{
  "statusCode": 200,
  "message": "Success",
  "data": {
    "username": "cashier01",
    "role": null,
    "token": null
  }
}
```
> Note: JWT token generation is configured but not yet wired into the login flow.

---

### Invoice — `/pos_api_app/Invoice`

#### `POST /SaveInvoiceTransaction`
Save a complete invoice (transaction header + all line items) atomically.

**Request body:**
```json
{
  "transactionDate": "2026-04-22T10:00:00.000Z",
  "accountPos": null,
  "totalTransaction": 25.50,
  "payAmount": 25.50,
  "listTransactionItems": [
    {
      "barcodeId": "8991234567890",
      "title": "Rice 1kg",
      "quantityType": "kg",
      "quantity": 2,
      "price": 10.00,
      "totalPrice": 20.00,
      "unitList": "kg;gram"
    }
  ]
}
```

**Response:** `ResponseDTO<bool>` — `data: true` on success.

---

#### `GET /GetProductPriceByBarcode`
Look up product info and all available units/prices by barcode. Used when adding items to an invoice.

**Query params:** `BarcodeId`, `Unit` (unit name, optional)

**Response:** `ResponseDTO<InvoiceProductDTO>`
```json
{
  "statusCode": 200,
  "data": {
    "priceId": 1,
    "barcodeId": "8991234567890",
    "title": "Rice",
    "quantityType": "kg",
    "amount": 10.00,
    "unitList": [
      { "id": 1, "name": "kg" },
      { "id": 2, "name": "gram" }
    ]
  }
}
```

---

### Transaction — `/pos_api_app/Transaction`

#### `GET /`
Paginated list of transactions with optional date filter.

**Query params:**
| Param | Type | Default | Notes |
|---|---|---|---|
| `sortColumn` | string | `""` | Column name to sort by |
| `sortColumnDir` | string | `""` | `asc` or `desc` |
| `rowsPerPage` | int | `0` | Page size |
| `pageNumber` | int | `0` | 1-based |
| `transactionDate` | string | `""` | Filter by exact date (`yyyy-MM-dd`) |

**Response:** `ResponseDTO<ResponseTableDTO<TransactionDTO>>`

---

#### `GET /Detail`
Get all line items for a specific transaction.

**Query param:** `req` (transaction ID, long)

**Response:** `ResponseDTO<ResponseTableDTO<TransactionItemDTO>>`
```json
{
  "data": {
    "totalRecord": 3,
    "currentPage": 1,
    "totalPage": 1,
    "dataTable": [
      {
        "transactionId": 5,
        "barcodeId": "8991234567890",
        "titleProduct": "Rice",
        "quantityType": "kg",
        "quantity": 2,
        "priceProduct": 10.00,
        "totalPrice": 20.00
      }
    ]
  }
}
```

---

#### `PUT /UpdateTransactionItems`
Replace all line items for a transaction (delete-all + re-insert).

**Request body:**
```json
{
  "transactionId": 5,
  "listTransactionItem": [
    {
      "barcodeId": "8991234567890",
      "titleProduct": "Rice",
      "quantityType": "kg",
      "quantity": 3,
      "priceProduct": 10.00,
      "totalPrice": 30.00
    }
  ]
}
```

**Response:** `ResponseDTO<bool>`

---

#### `PUT /Delete`
Delete a transaction and all its line items.

**Request body:** transaction ID (int, raw)

**Response:** `ResponseDTO<bool>`

---

#### `POST /AddTransaction/`
Create a transaction with items via the legacy transaction flow (use `/Invoice/SaveInvoiceTransaction` for the POS invoice flow instead).

---

### Product — `/pos_api_app/Product`

| Method | Route | Description |
|---|---|---|
| `GET` | `/` | Paginated product list (`TableDTO` + `BarcodeId` filter) |
| `GET` | `/GetAllProductDropdown` | All products as dropdown options |
| `GET` | `/GetProductByBarcodeIdDropdown?barcodeId=` | Single product by barcode for dropdown |
| `POST` | `/Create` | Create product + unit + price atomically |
| `PUT` | `/Update` | Update product + price + unit |
| `DELETE` | `/Delete?id=` | Soft-delete a price record |

---

### Unit — `/pos_api_app/Unit`

| Method | Route | Description |
|---|---|---|
| `GET` | `/GetAllUnitDropdown` | All units |
| `GET` | `/GetUnitByNameDropdown?name=` | Unit by name |

---

### Role — `/pos_api_app/Role`

| Method | Route | Description |
|---|---|---|
| `GET` | `/` | All roles |
| `POST` | `/Create` | Create role |
| `DELETE` | `/Delete?id=` | Delete role |

---

### Employee — `/pos_api_app/Employee/`

| Method | Route | Description |
|---|---|---|
| `GET` | `/` | All employees |
| `GET` | `/{id}/` | Employee by ID |
| `POST` | `/AddEmployee/` | Create employee |
| `PUT` | `/UpdatePrice/` | Update employee (route name is a typo) |
| `DELETE` | `/Delete/{id}` | Delete employee |

---

## Known Limitations

- **No `[Authorize]`** on any endpoint — JWT authentication is configured but not enforced. All endpoints are publicly accessible.
- **Login returns no token** — `AuthService.Login()` validates credentials correctly but does not call `ITokenHandler.GenerateToken()`.
- **`app.UseHttpsRedirection()` called twice** in `Program.cs` (harmless but redundant).
- **`UpdateTransaction`** endpoint is entirely commented out.
- **No soft-delete on transactions** — deletion is a hard `RemoveRange`.
- **No account/cashier filter** on transaction list — only date filter available.
