using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class AddSearchFieldInCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "search",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "search",
                table: "Customers");
        }
    }
}
