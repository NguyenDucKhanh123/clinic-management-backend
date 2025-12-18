using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddBacSiAndBacSiIdToLichKham : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BacSi",
                table: "LichKhams",
                newName: "BacSiTen");

            migrationBuilder.AddColumn<int>(
                name: "BacSiId",
                table: "LichKhams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "BacSiId",
                table: "BenhAns",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "BacSis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChuyenKhoa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BangCap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BacSis", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LichKhams_BacSiId",
                table: "LichKhams",
                column: "BacSiId");

            migrationBuilder.CreateIndex(
                name: "IX_BenhAns_BacSiId",
                table: "BenhAns",
                column: "BacSiId");

            migrationBuilder.CreateIndex(
                name: "IX_BenhAns_BenhNhanId",
                table: "BenhAns",
                column: "BenhNhanId");

            migrationBuilder.AddForeignKey(
                name: "FK_BenhAns_BacSis_BacSiId",
                table: "BenhAns",
                column: "BacSiId",
                principalTable: "BacSis",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BenhAns_BenhNhans_BenhNhanId",
                table: "BenhAns",
                column: "BenhNhanId",
                principalTable: "BenhNhans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LichKhams_BacSis_BacSiId",
                table: "LichKhams",
                column: "BacSiId",
                principalTable: "BacSis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BenhAns_BacSis_BacSiId",
                table: "BenhAns");

            migrationBuilder.DropForeignKey(
                name: "FK_BenhAns_BenhNhans_BenhNhanId",
                table: "BenhAns");

            migrationBuilder.DropForeignKey(
                name: "FK_LichKhams_BacSis_BacSiId",
                table: "LichKhams");

            migrationBuilder.DropTable(
                name: "BacSis");

            migrationBuilder.DropIndex(
                name: "IX_LichKhams_BacSiId",
                table: "LichKhams");

            migrationBuilder.DropIndex(
                name: "IX_BenhAns_BacSiId",
                table: "BenhAns");

            migrationBuilder.DropIndex(
                name: "IX_BenhAns_BenhNhanId",
                table: "BenhAns");

            migrationBuilder.DropColumn(
                name: "BacSiId",
                table: "LichKhams");

            migrationBuilder.RenameColumn(
                name: "BacSiTen",
                table: "LichKhams",
                newName: "BacSi");

            migrationBuilder.AlterColumn<int>(
                name: "BacSiId",
                table: "BenhAns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
