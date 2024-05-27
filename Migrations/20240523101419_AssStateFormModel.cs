using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class AssStateFormModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sf_id",
                table: "Docs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StateForm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateForm", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Docs_sf_id",
                table: "Docs",
                column: "sf_id",
                unique: true,
                filter: "[sf_id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Docs_StateForm_sf_id",
                table: "Docs",
                column: "sf_id",
                principalTable: "StateForm",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Docs_StateForm_sf_id",
                table: "Docs");

            migrationBuilder.DropTable(
                name: "StateForm");

            migrationBuilder.DropIndex(
                name: "IX_Docs_sf_id",
                table: "Docs");

            migrationBuilder.DropColumn(
                name: "sf_id",
                table: "Docs");
        }
    }
}
