using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class ThemTrangThaiPhatThuocVaSoLuongTon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoLuongTon",
                table: "Thuocs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiPhatThuoc",
                table: "BenhAns",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoLuongTon",
                table: "Thuocs");

            migrationBuilder.DropColumn(
                name: "TrangThaiPhatThuoc",
                table: "BenhAns");
        }
    }
}
