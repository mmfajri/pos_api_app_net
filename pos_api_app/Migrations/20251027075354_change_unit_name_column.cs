using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pos_api_app.Migrations
{
    /// <inheritdoc />
    public partial class change_unit_name_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tb_m_unit",
                newName: "name");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "tb_m_unit",
                type: "varchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "tb_m_unit",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tb_m_unit",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)");
        }
    }
}
