using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBenhNhan_AddMoreFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CCCD",
                table: "BenhNhans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GioiTinh",
                table: "BenhNhans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HoTen",
                table: "BenhNhans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgaySinh",
                table: "BenhNhans",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CCCD",
                table: "BenhNhans");

            migrationBuilder.DropColumn(
                name: "GioiTinh",
                table: "BenhNhans");

            migrationBuilder.DropColumn(
                name: "HoTen",
                table: "BenhNhans");

            migrationBuilder.DropColumn(
                name: "NgaySinh",
                table: "BenhNhans");
        }
    }
}
