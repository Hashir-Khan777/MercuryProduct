using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationsAndPermissionSettingsForEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_ManagerId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ManagerId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "permissions",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyDrivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    driver_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDrivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDrivers_AspNetUsers_driver_id",
                        column: x => x.driver_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyDrivers_Companies_company_id",
                        column: x => x.company_id,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyEmployees",
                columns: table => new
                {
                    employee_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyEmployees", x => new { x.employee_id, x.company_id });
                    table.ForeignKey(
                        name: "FK_CompanyEmployees_AspNetUsers_employee_id",
                        column: x => x.employee_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyEmployees_Companies_company_id",
                        column: x => x.company_id,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyManagers",
                columns: table => new
                {
                    manager_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyManagers", x => new { x.manager_id, x.company_id });
                    table.ForeignKey(
                        name: "FK_CompanyManagers_AspNetUsers_manager_id",
                        column: x => x.manager_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyManagers_Companies_company_id",
                        column: x => x.company_id,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDrivers_company_id",
                table: "CompanyDrivers",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDrivers_driver_id",
                table: "CompanyDrivers",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmployees_company_id",
                table: "CompanyEmployees",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyManagers_company_id",
                table: "CompanyManagers",
                column: "company_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyDrivers");

            migrationBuilder.DropTable(
                name: "CompanyEmployees");

            migrationBuilder.DropTable(
                name: "CompanyManagers");

            migrationBuilder.DropColumn(
                name: "permissions",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ManagerId",
                table: "Companies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ManagerId",
                table: "Companies",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_ManagerId",
                table: "Companies",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
