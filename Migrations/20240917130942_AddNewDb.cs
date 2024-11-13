using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class AddNewDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Customers_customerId",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "CustomerModelId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PosCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cfname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    loyalty_member = table.Column<bool>(type: "bit", nullable: false),
                    caddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clat = table.Column<double>(type: "float", nullable: false),
                    clon = table.Column<double>(type: "float", nullable: false),
                    czip_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ccity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ccountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cstate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cphone_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    search = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PosCustomers_AspNetUsers_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PosCustomers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CustomerModelId",
                table: "Payments",
                column: "CustomerModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PosCustomers_CompanyId",
                table: "PosCustomers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PosCustomers_created_by_id",
                table: "PosCustomers",
                column: "created_by_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Customers_CustomerModelId",
                table: "Payments",
                column: "CustomerModelId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PosCustomers_customerId",
                table: "Payments",
                column: "customerId",
                principalTable: "PosCustomers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Customers_CustomerModelId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PosCustomers_customerId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "PosCustomers");

            migrationBuilder.DropIndex(
                name: "IX_Payments_CustomerModelId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CustomerModelId",
                table: "Payments");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Customers_customerId",
                table: "Payments",
                column: "customerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
