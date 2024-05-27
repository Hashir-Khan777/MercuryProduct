using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notes_doc_id",
                table: "Notes");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_doc_id",
                table: "Notes",
                column: "doc_id",
                unique: true,
                filter: "[doc_id] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notes_doc_id",
                table: "Notes");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_doc_id",
                table: "Notes",
                column: "doc_id");
        }
    }
}
