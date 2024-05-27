using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNotFlowInStatteForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "note",
                table: "StateForm");

            migrationBuilder.AddColumn<int>(
                name: "sf_id",
                table: "Notes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "state_formId",
                table: "Notes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_state_formId",
                table: "Notes",
                column: "state_formId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_StateForm_state_formId",
                table: "Notes",
                column: "state_formId",
                principalTable: "StateForm",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_StateForm_state_formId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_state_formId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "sf_id",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "state_formId",
                table: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "note",
                table: "StateForm",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
