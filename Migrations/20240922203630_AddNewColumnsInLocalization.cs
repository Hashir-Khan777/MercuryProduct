﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnsInLocalization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tax_1_label",
                table: "Localization",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "tax_1_value",
                table: "Localization",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "tax_2_label",
                table: "Localization",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "tax_2_value",
                table: "Localization",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "tax_3_label",
                table: "Localization",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "tax_3_value",
                table: "Localization",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "tax_4_label",
                table: "Localization",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "tax_4_value",
                table: "Localization",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tax_1_label",
                table: "Localization");

            migrationBuilder.DropColumn(
                name: "tax_1_value",
                table: "Localization");

            migrationBuilder.DropColumn(
                name: "tax_2_label",
                table: "Localization");

            migrationBuilder.DropColumn(
                name: "tax_2_value",
                table: "Localization");

            migrationBuilder.DropColumn(
                name: "tax_3_label",
                table: "Localization");

            migrationBuilder.DropColumn(
                name: "tax_3_value",
                table: "Localization");

            migrationBuilder.DropColumn(
                name: "tax_4_label",
                table: "Localization");

            migrationBuilder.DropColumn(
                name: "tax_4_value",
                table: "Localization");
        }
    }
}
