using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace pos_api_app.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_product",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barcode_id = table.Column<string>(type: "varchar(255)", nullable: false),
                    title = table.Column<string>(type: "varchar(150)", nullable: false),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_role",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_unit",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_unit", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_account",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "varchar(100)", nullable: false),
                    password = table.Column<string>(type: "varchar(200)", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: true),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_account", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_m_account_tb_m_role_role_id",
                        column: x => x.role_id,
                        principalTable: "tb_m_role",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_price",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_id = table.Column<int>(type: "integer", nullable: true),
                    unit_id = table.Column<int>(type: "integer", nullable: true),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_price", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_tr_price_tb_m_product_product_id",
                        column: x => x.product_id,
                        principalTable: "tb_m_product",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tb_tr_price_tb_m_unit_unit_id",
                        column: x => x.unit_id,
                        principalTable: "tb_m_unit",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tb_m_employee",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(type: "varchar(200)", nullable: false),
                    lastname = table.Column<string>(type: "varchar(200)", nullable: true),
                    account_id = table.Column<int>(type: "integer", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: true),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_employee", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_m_employee_tb_m_account_account_id",
                        column: x => x.account_id,
                        principalTable: "tb_m_account",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tb_m_employee_tb_m_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tb_m_role",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_transaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    account_id = table.Column<int>(type: "integer", nullable: true),
                    transaction_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    total_ammount = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    EmployeeId = table.Column<int>(type: "integer", nullable: true),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_transaction", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_tr_transaction_tb_m_account_account_id",
                        column: x => x.account_id,
                        principalTable: "tb_m_account",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tb_tr_transaction_tb_m_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "tb_m_employee",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tb_m_transaction_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    transaction_id = table.Column<int>(type: "integer", nullable: true),
                    product_id = table.Column<int>(type: "integer", nullable: true),
                    price_id = table.Column<int>(type: "integer", nullable: true),
                    quantity = table.Column<float>(type: "real", nullable: false),
                    subtotal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_transaction_item", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_m_transaction_item_tb_m_product_product_id",
                        column: x => x.product_id,
                        principalTable: "tb_m_product",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tb_m_transaction_item_tb_tr_price_price_id",
                        column: x => x.price_id,
                        principalTable: "tb_tr_price",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tb_m_transaction_item_tb_tr_transaction_transaction_id",
                        column: x => x.transaction_id,
                        principalTable: "tb_tr_transaction",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_account_role_id",
                table: "tb_m_account",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_account_username",
                table: "tb_m_account",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_account_id",
                table: "tb_m_employee",
                column: "account_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_RoleId",
                table: "tb_m_employee",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_transaction_item_price_id",
                table: "tb_m_transaction_item",
                column: "price_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_transaction_item_product_id",
                table: "tb_m_transaction_item",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_transaction_item_transaction_id",
                table: "tb_m_transaction_item",
                column: "transaction_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_price_product_id",
                table: "tb_tr_price",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_price_unit_id",
                table: "tb_tr_price",
                column: "unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_transaction_account_id",
                table: "tb_tr_transaction",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_transaction_EmployeeId",
                table: "tb_tr_transaction",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_transaction_item");

            migrationBuilder.DropTable(
                name: "tb_tr_price");

            migrationBuilder.DropTable(
                name: "tb_tr_transaction");

            migrationBuilder.DropTable(
                name: "tb_m_product");

            migrationBuilder.DropTable(
                name: "tb_m_unit");

            migrationBuilder.DropTable(
                name: "tb_m_employee");

            migrationBuilder.DropTable(
                name: "tb_m_account");

            migrationBuilder.DropTable(
                name: "tb_m_role");
        }
    }
}
