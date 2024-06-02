using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCustomerDuplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "cphone_number",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_cphone_number",
                table: "Customers",
                column: "cphone_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_cphone_number",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "cphone_number",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
