using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddBenhAnAndBacSiTenToLichKham : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BenhAns_LichKhamId",
                table: "BenhAns");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "LichKhams");

            migrationBuilder.AlterColumn<string>(
                name: "TrangThai",
                table: "LichKhams",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_BenhAns_LichKhamId",
                table: "BenhAns",
                column: "LichKhamId",
                unique: true,
                filter: "[LichKhamId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BenhAns_LichKhamId",
                table: "BenhAns");

            migrationBuilder.AlterColumn<string>(
                name: "TrangThai",
                table: "LichKhams",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "LichKhams",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BenhAns_LichKhamId",
                table: "BenhAns",
                column: "LichKhamId");
        }
    }
}
