using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsInProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "created_by_id",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Products_created_by_id",
                table: "Products",
                column: "created_by_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_created_by_id",
                table: "Products",
                column: "created_by_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_created_by_id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_created_by_id",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "created_by_id",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "Products");
        }
    }
}
