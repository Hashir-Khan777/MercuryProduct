using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "product_id",
                table: "Docs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    department_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    product_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    product_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    regular_price = table.Column<int>(type: "int", nullable: false),
                    custom_price_1 = table.Column<int>(type: "int", nullable: false),
                    custom_price_2 = table.Column<int>(type: "int", nullable: false),
                    custom_price_3 = table.Column<int>(type: "int", nullable: false),
                    custom_price_4 = table.Column<int>(type: "int", nullable: false),
                    product_grade = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Docs_product_id",
                table: "Docs",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Docs_Products_product_id",
                table: "Docs",
                column: "product_id",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Docs_Products_product_id",
                table: "Docs");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Docs_product_id",
                table: "Docs");

            migrationBuilder.DropColumn(
                name: "product_id",
                table: "Docs");
        }
    }
}
