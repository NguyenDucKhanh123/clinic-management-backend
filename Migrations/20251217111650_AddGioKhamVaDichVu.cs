using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddGioKhamVaDichVu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DonGiaGioKham",
                table: "BenhAns",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "SoGioKham",
                table: "BenhAns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DichVus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDichVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DichVus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BenhAnDichVus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BenhAnId = table.Column<int>(type: "int", nullable: false),
                    DichVuId = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenhAnDichVus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BenhAnDichVus_BenhAns_BenhAnId",
                        column: x => x.BenhAnId,
                        principalTable: "BenhAns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BenhAnDichVus_DichVus_DichVuId",
                        column: x => x.DichVuId,
                        principalTable: "DichVus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BenhAnDichVus_BenhAnId",
                table: "BenhAnDichVus",
                column: "BenhAnId");

            migrationBuilder.CreateIndex(
                name: "IX_BenhAnDichVus_DichVuId",
                table: "BenhAnDichVus",
                column: "DichVuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BenhAnDichVus");

            migrationBuilder.DropTable(
                name: "DichVus");

            migrationBuilder.DropColumn(
                name: "DonGiaGioKham",
                table: "BenhAns");

            migrationBuilder.DropColumn(
                name: "SoGioKham",
                table: "BenhAns");
        }
    }
}
