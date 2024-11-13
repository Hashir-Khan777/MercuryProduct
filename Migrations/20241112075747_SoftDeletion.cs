﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class SoftDeletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted",
                table: "AspNetUsers");
        }
    }
}
