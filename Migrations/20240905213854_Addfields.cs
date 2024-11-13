using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class Addfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "stateOdArkansaTax",
                table: "Localization",
                newName: "stateOfArkansaTax");

            migrationBuilder.AddColumn<double>(
                name: "bentonCountyTax",
                table: "Products",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "cityOfRogersTax",
                table: "Products",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "epaFeesTax",
                table: "Products",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "stateOfArkansaTax",
                table: "Products",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "totalTax",
                table: "Products",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bentonCountyTax",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "cityOfRogersTax",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "epaFeesTax",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "stateOfArkansaTax",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "totalTax",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "stateOfArkansaTax",
                table: "Localization",
                newName: "stateOdArkansaTax");
        }
    }
}
