using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class AddRelatioShipBetweenSTateFormAndNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_StateForm_state_formId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_state_formId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "state_formId",
                table: "Notes");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_sf_id",
                table: "Notes",
                column: "sf_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_StateForm_sf_id",
                table: "Notes",
                column: "sf_id",
                principalTable: "StateForm",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_StateForm_sf_id",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_sf_id",
                table: "Notes");

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
    }
}
