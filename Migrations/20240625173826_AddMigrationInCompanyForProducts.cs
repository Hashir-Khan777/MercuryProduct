using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrationInCompanyForProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Products_company_id",
                table: "Products",
                column: "company_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Companies_company_id",
                table: "Products",
                column: "company_id",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Companies_company_id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_company_id",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "companyId",
                table: "Products",
                type: "int",
                nullable: true);

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
    }
}
