using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToBenhAn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "BenhAns",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TrieuChung",
                table: "BenhAns",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "BenhAns");

            migrationBuilder.DropColumn(
                name: "TrieuChung",
                table: "BenhAns");
        }
    }
}
