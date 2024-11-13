using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "returned",
                table: "Products",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "returned_at",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "taxAmount",
                table: "Products",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "returned",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "returned_at",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "taxAmount",
                table: "Products");
        }
    }
}
