using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace pos_api_app.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
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
                    name = table.Column<string>(type: "varchar(200)", nullable: false),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_unit", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_transaction_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    transaction_id = table.Column<int>(type: "integer", nullable: true),
                    barcode_id = table.Column<string>(type: "varchar(255)", nullable: true),
                    title_product = table.Column<string>(type: "varchar(255)", nullable: true),
                    quantity_type = table.Column<string>(type: "varchar(200)", nullable: true),
                    quantity = table.Column<decimal>(type: "numeric", nullable: true),
                    price_product = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    total_price_product = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    unit_List = table.Column<string>(type: "varchar(255)", nullable: true),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_transaction_item", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_account",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "varchar(100)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: true),
                    password = table.Column<string>(type: "varchar(200)", nullable: true),
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
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
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
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_tb_tr_price_tb_m_unit_unit_id",
                        column: x => x.unit_id,
                        principalTable: "tb_m_unit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
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
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_transaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    account_id = table.Column<int>(type: "integer", nullable: true),
                    transaction_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    total_price_amount = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    pay_amount = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
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
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
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
                name: "IX_tb_m_product_barcode_id",
                table: "tb_m_product",
                column: "barcode_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_unit_name",
                table: "tb_m_unit",
                column: "name",
                unique: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_employee");

            migrationBuilder.DropTable(
                name: "tb_tr_price");

            migrationBuilder.DropTable(
                name: "tb_tr_transaction");

            migrationBuilder.DropTable(
                name: "tb_tr_transaction_item");

            migrationBuilder.DropTable(
                name: "tb_m_product");

            migrationBuilder.DropTable(
                name: "tb_m_unit");

            migrationBuilder.DropTable(
                name: "tb_m_account");

            migrationBuilder.DropTable(
                name: "tb_m_role");
        }
    }
}
