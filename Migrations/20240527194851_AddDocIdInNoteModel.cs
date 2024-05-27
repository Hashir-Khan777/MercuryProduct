using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class AddDocIdInNoteModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "doc_id",
                table: "Notes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_doc_id",
                table: "Notes",
                column: "doc_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Docs_doc_id",
                table: "Notes",
                column: "doc_id",
                principalTable: "Docs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Docs_doc_id",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_doc_id",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "doc_id",
                table: "Notes");
        }
    }
}
