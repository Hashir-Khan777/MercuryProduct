using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreFieldsInProductModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "companyId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "company_id",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_companyId",
                table: "Products",
                column: "companyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Companies_companyId",
                table: "Products",
                column: "companyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Companies_companyId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_companyId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "companyId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "company_id",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "Products");
        }
    }
}
