using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddBenhAnDonThuoc_Relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ThuocId",
                table: "DonThuocs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "DonThuocs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LieuDung",
                table: "DonThuocs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoNgay",
                table: "DonThuocs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenThuoc",
                table: "DonThuocs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "TrieuChung",
                table: "BenhAns",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "GhiChu",
                table: "BenhAns",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "LichKhamId",
                table: "BenhAns",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonThuocs_BenhAnId",
                table: "DonThuocs",
                column: "BenhAnId");

            migrationBuilder.CreateIndex(
                name: "IX_DonThuocs_ThuocId",
                table: "DonThuocs",
                column: "ThuocId");

            migrationBuilder.CreateIndex(
                name: "IX_BenhAns_LichKhamId",
                table: "BenhAns",
                column: "LichKhamId");

            migrationBuilder.AddForeignKey(
                name: "FK_BenhAns_LichKhams_LichKhamId",
                table: "BenhAns",
                column: "LichKhamId",
                principalTable: "LichKhams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DonThuocs_BenhAns_BenhAnId",
                table: "DonThuocs",
                column: "BenhAnId",
                principalTable: "BenhAns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DonThuocs_Thuocs_ThuocId",
                table: "DonThuocs",
                column: "ThuocId",
                principalTable: "Thuocs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BenhAns_LichKhams_LichKhamId",
                table: "BenhAns");

            migrationBuilder.DropForeignKey(
                name: "FK_DonThuocs_BenhAns_BenhAnId",
                table: "DonThuocs");

            migrationBuilder.DropForeignKey(
                name: "FK_DonThuocs_Thuocs_ThuocId",
                table: "DonThuocs");

            migrationBuilder.DropIndex(
                name: "IX_DonThuocs_BenhAnId",
                table: "DonThuocs");

            migrationBuilder.DropIndex(
                name: "IX_DonThuocs_ThuocId",
                table: "DonThuocs");

            migrationBuilder.DropIndex(
                name: "IX_BenhAns_LichKhamId",
                table: "BenhAns");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "DonThuocs");

            migrationBuilder.DropColumn(
                name: "LieuDung",
                table: "DonThuocs");

            migrationBuilder.DropColumn(
                name: "SoNgay",
                table: "DonThuocs");

            migrationBuilder.DropColumn(
                name: "TenThuoc",
                table: "DonThuocs");

            migrationBuilder.DropColumn(
                name: "LichKhamId",
                table: "BenhAns");

            migrationBuilder.AlterColumn<int>(
                name: "ThuocId",
                table: "DonThuocs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TrieuChung",
                table: "BenhAns",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GhiChu",
                table: "BenhAns",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
