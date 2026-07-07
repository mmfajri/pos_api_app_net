using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pos_api_app.Migrations
{
    /// <inheritdoc />
    public partial class remove_unit_list_on_transasction_item : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "unit_List",
                table: "tb_tr_transaction_item");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "unit_List",
                table: "tb_tr_transaction_item",
                type: "varchar(255)",
                nullable: true);
        }
    }
}
