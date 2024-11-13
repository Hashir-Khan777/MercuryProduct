using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class AddTaxes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "bentonCountyTax",
                table: "Localization",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "cityOfRogersTax",
                table: "Localization",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "epaFeesTax",
                table: "Localization",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "stateOdArkansaTax",
                table: "Localization",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bentonCountyTax",
                table: "Localization");

            migrationBuilder.DropColumn(
                name: "cityOfRogersTax",
                table: "Localization");

            migrationBuilder.DropColumn(
                name: "epaFeesTax",
                table: "Localization");

            migrationBuilder.DropColumn(
                name: "stateOdArkansaTax",
                table: "Localization");
        }
    }
}
