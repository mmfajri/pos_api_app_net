using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pos_api_app.Migrations
{
    /// <inheritdoc />
    public partial class AddPayAmountInTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "pay_amount",
                table: "tb_tr_transaction",
                type: "numeric(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pay_amount",
                table: "tb_tr_transaction");
        }
    }
}
