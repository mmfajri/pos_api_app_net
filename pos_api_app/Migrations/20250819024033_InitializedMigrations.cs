using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pos_api_app.Migrations
{
    /// <inheritdoc />
    public partial class InitializedMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_product",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    barcode_id = table.Column<string>(type: "varchar(255)", nullable: false),
                    title = table.Column<string>(type: "varchar(150)", nullable: false),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_product", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_role",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_role", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_unit",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_unit", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_account",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "varchar(100)", nullable: false),
                    password = table.Column<string>(type: "varchar(200)", nullable: false),
                    role_guid = table.Column<Guid>(type: "uuid", nullable: true),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_account", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_account_tb_m_role_role_guid",
                        column: x => x.role_guid,
                        principalTable: "tb_m_role",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_price",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    product_guid = table.Column<Guid>(type: "uuid", nullable: true),
                    price_guid = table.Column<Guid>(type: "uuid", nullable: true),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_price", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_tr_price_tb_m_product_product_guid",
                        column: x => x.product_guid,
                        principalTable: "tb_m_product",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_tb_tr_price_tb_m_unit_price_guid",
                        column: x => x.price_guid,
                        principalTable: "tb_m_unit",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateTable(
                name: "tb_m_employee",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    firstname = table.Column<string>(type: "varchar(200)", nullable: false),
                    lastname = table.Column<string>(type: "varchar(200)", nullable: true),
                    account_guid = table.Column<Guid>(type: "uuid", nullable: true),
                    RoleGuid = table.Column<Guid>(type: "uuid", nullable: true),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_employee", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_employee_tb_m_account_account_guid",
                        column: x => x.account_guid,
                        principalTable: "tb_m_account",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_tb_m_employee_tb_m_role_RoleGuid",
                        column: x => x.RoleGuid,
                        principalTable: "tb_m_role",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_transaction",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    employee_guid = table.Column<Guid>(type: "uuid", nullable: true),
                    transaction_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    total_ammount = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    AccountGuid = table.Column<Guid>(type: "uuid", nullable: true),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_transaction", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_tr_transaction_tb_m_account_AccountGuid",
                        column: x => x.AccountGuid,
                        principalTable: "tb_m_account",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_tb_tr_transaction_tb_m_employee_employee_guid",
                        column: x => x.employee_guid,
                        principalTable: "tb_m_employee",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateTable(
                name: "tb_m_transaction_item",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    transaction_guid = table.Column<Guid>(type: "uuid", nullable: true),
                    product_guid = table.Column<Guid>(type: "uuid", nullable: true),
                    price_guid = table.Column<Guid>(type: "uuid", nullable: true),
                    quantity = table.Column<float>(type: "real", nullable: false),
                    subtotal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_transaction_item", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_transaction_item_tb_m_product_product_guid",
                        column: x => x.product_guid,
                        principalTable: "tb_m_product",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_tb_m_transaction_item_tb_tr_price_price_guid",
                        column: x => x.price_guid,
                        principalTable: "tb_tr_price",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_tb_m_transaction_item_tb_tr_transaction_transaction_guid",
                        column: x => x.transaction_guid,
                        principalTable: "tb_tr_transaction",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_account_role_guid",
                table: "tb_m_account",
                column: "role_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_account_username",
                table: "tb_m_account",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_account_guid",
                table: "tb_m_employee",
                column: "account_guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_RoleGuid",
                table: "tb_m_employee",
                column: "RoleGuid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_transaction_item_price_guid",
                table: "tb_m_transaction_item",
                column: "price_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_transaction_item_product_guid",
                table: "tb_m_transaction_item",
                column: "product_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_transaction_item_transaction_guid",
                table: "tb_m_transaction_item",
                column: "transaction_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_price_price_guid",
                table: "tb_tr_price",
                column: "price_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_price_product_guid",
                table: "tb_tr_price",
                column: "product_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_transaction_AccountGuid",
                table: "tb_tr_transaction",
                column: "AccountGuid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_transaction_employee_guid",
                table: "tb_tr_transaction",
                column: "employee_guid");
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
