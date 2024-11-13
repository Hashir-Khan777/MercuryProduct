using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class ResetBehaviour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyDrivers_AspNetUsers_driver_id",
                table: "CompanyDrivers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyDrivers_Companies_company_id",
                table: "CompanyDrivers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEmployees_AspNetUsers_employee_id",
                table: "CompanyEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEmployees_Companies_company_id",
                table: "CompanyEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyManagers_AspNetUsers_manager_id",
                table: "CompanyManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyManagers_Companies_company_id",
                table: "CompanyManagers");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyDrivers_AspNetUsers_driver_id",
                table: "CompanyDrivers",
                column: "driver_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyDrivers_Companies_company_id",
                table: "CompanyDrivers",
                column: "company_id",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEmployees_AspNetUsers_employee_id",
                table: "CompanyEmployees",
                column: "employee_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEmployees_Companies_company_id",
                table: "CompanyEmployees",
                column: "company_id",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyManagers_AspNetUsers_manager_id",
                table: "CompanyManagers",
                column: "manager_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyManagers_Companies_company_id",
                table: "CompanyManagers",
                column: "company_id",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyDrivers_AspNetUsers_driver_id",
                table: "CompanyDrivers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyDrivers_Companies_company_id",
                table: "CompanyDrivers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEmployees_AspNetUsers_employee_id",
                table: "CompanyEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEmployees_Companies_company_id",
                table: "CompanyEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyManagers_AspNetUsers_manager_id",
                table: "CompanyManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyManagers_Companies_company_id",
                table: "CompanyManagers");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyDrivers_AspNetUsers_driver_id",
                table: "CompanyDrivers",
                column: "driver_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyDrivers_Companies_company_id",
                table: "CompanyDrivers",
                column: "company_id",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEmployees_AspNetUsers_employee_id",
                table: "CompanyEmployees",
                column: "employee_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEmployees_Companies_company_id",
                table: "CompanyEmployees",
                column: "company_id",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyManagers_AspNetUsers_manager_id",
                table: "CompanyManagers",
                column: "manager_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyManagers_Companies_company_id",
                table: "CompanyManagers",
                column: "company_id",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
