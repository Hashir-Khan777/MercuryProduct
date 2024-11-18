using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    driverId = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    oldThreePasswords = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    permissions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    caddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clat = table.Column<double>(type: "float", nullable: false),
                    clon = table.Column<double>(type: "float", nullable: false),
                    czip_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ccity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ccountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cstate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterProductionTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    section = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    row = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterProductionTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterVehicleTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    make = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    model = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterVehicleTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterYearTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterYearTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    entity_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    action_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_AspNetUsers_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyDrivers",
                columns: table => new
                {
                    driver_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDrivers", x => new { x.driver_id, x.company_id });
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
                    Id = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    cfname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    caddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clat = table.Column<double>(type: "float", nullable: false),
                    clon = table.Column<double>(type: "float", nullable: false),
                    czip_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ccity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ccountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cstate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cphone_number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    number_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    contact_prefrence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customer_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    company_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customer_notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    search = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Localization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    salesTax = table.Column<double>(type: "float", nullable: false),
                    bentonCountyTax = table.Column<double>(type: "float", nullable: false),
                    cityOfRogersTax = table.Column<double>(type: "float", nullable: false),
                    epaFeesTax = table.Column<double>(type: "float", nullable: false),
                    stateOfArkansaTax = table.Column<double>(type: "float", nullable: false),
                    tax_1_label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tax_1_value = table.Column<double>(type: "float", nullable: false),
                    tax_2_label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tax_2_value = table.Column<double>(type: "float", nullable: false),
                    tax_3_label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tax_3_value = table.Column<double>(type: "float", nullable: false),
                    tax_4_label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tax_4_value = table.Column<double>(type: "float", nullable: false),
                    defaultProductImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    companyLogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    showPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localization_Companies_company_id",
                        column: x => x.company_id,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PosCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cfname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_by_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    incartquantity = table.Column<int>(type: "int", nullable: false),
                    discount = table.Column<int>(type: "int", nullable: true),
                    product_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    industry_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    product_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    special = table.Column<bool>(type: "bit", nullable: false),
                    special_price = table.Column<double>(type: "float", nullable: false),
                    regular_price = table.Column<double>(type: "float", nullable: false),
                    custom_price_1 = table.Column<double>(type: "float", nullable: false),
                    custom_price_2 = table.Column<double>(type: "float", nullable: false),
                    custom_price_3 = table.Column<double>(type: "float", nullable: false),
                    custom_price_4 = table.Column<double>(type: "float", nullable: false),
                    vat = table.Column<double>(type: "float", nullable: false),
                    product_grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    bentonCountyTax = table.Column<double>(type: "float", nullable: true),
                    cityOfRogersTax = table.Column<double>(type: "float", nullable: true),
                    epaFeesTax = table.Column<double>(type: "float", nullable: true),
                    stateOfArkansaTax = table.Column<double>(type: "float", nullable: true),
                    tax_1_label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tax_1_value = table.Column<double>(type: "float", nullable: false),
                    tax_2_label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tax_2_value = table.Column<double>(type: "float", nullable: false),
                    tax_3_label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tax_3_value = table.Column<double>(type: "float", nullable: false),
                    tax_4_label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tax_4_value = table.Column<double>(type: "float", nullable: false),
                    totalTax = table.Column<double>(type: "float", nullable: true),
                    show_price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    taxAmount = table.Column<double>(type: "float", nullable: true),
                    returned = table.Column<bool>(type: "bit", nullable: true),
                    returned_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_AspNetUsers_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Companies_company_id",
                        column: x => x.company_id,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StateForm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StateForm_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cid = table.Column<int>(type: "int", nullable: true),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    car_year = table.Column<int>(type: "int", nullable: false),
                    car_make = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    car_model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vin_no = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    car_run = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    car_color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    submitted_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    offered_ammount = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tires_wheel_front = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tires_wheel_rear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    car_not_running_notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pickup_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    scheduled_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    set_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    pulled_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    driver_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    created_by_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    purchase_amount = table.Column<int>(type: "int", nullable: true),
                    dnd_notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lead_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prod_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    section = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    row = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pull_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pull_type_des = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    motor_condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vehicle_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    title_status = table.Column<bool>(type: "bit", nullable: false),
                    special_instructions = table.Column<bool>(type: "bit", nullable: false),
                    checkNo = table.Column<int>(type: "int", nullable: true),
                    DL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_AspNetUsers_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cars_AspNetUsers_driver_id",
                        column: x => x.driver_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cars_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cars_Customers_cid",
                        column: x => x.cid,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cus_id = table.Column<int>(type: "int", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    created_by_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    reciept_image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payment_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<double>(type: "float", nullable: false),
                    check_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_AspNetUsers_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expenses_Companies_company_id",
                        column: x => x.company_id,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expenses_Customers_cus_id",
                        column: x => x.cus_id,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    created_by_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_AspNetUsers_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoices_Companies_company_id",
                        column: x => x.company_id,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    taxAmount = table.Column<double>(type: "float", nullable: false),
                    discount = table.Column<double>(type: "float", nullable: false),
                    cartDiscount = table.Column<int>(type: "int", nullable: false),
                    itemsAmount = table.Column<double>(type: "float", nullable: false),
                    totalAmount = table.Column<double>(type: "float", nullable: false),
                    paymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    paidAmount = table.Column<int>(type: "int", nullable: false),
                    changeAmount = table.Column<int>(type: "int", nullable: false),
                    customerId = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    products = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Companies_company_id",
                        column: x => x.company_id,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_Customers_CustomerModelId",
                        column: x => x.CustomerModelId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_PosCustomers_customerId",
                        column: x => x.customerId,
                        principalTable: "PosCustomers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Docs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    veh_id = table.Column<int>(type: "int", nullable: true),
                    sf_id = table.Column<int>(type: "int", nullable: true),
                    product_id = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    file_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    file_path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    short_path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    server_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Docs_Cars_veh_id",
                        column: x => x.veh_id,
                        principalTable: "Cars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Docs_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Docs_StateForm_sf_id",
                        column: x => x.sf_id,
                        principalTable: "StateForm",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductInvoices",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false),
                    invoice_id = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInvoices", x => new { x.invoice_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_ProductInvoices_Invoices_invoice_id",
                        column: x => x.invoice_id,
                        principalTable: "Invoices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductInvoices_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    veh_id = table.Column<int>(type: "int", nullable: true),
                    sf_id = table.Column<int>(type: "int", nullable: true),
                    doc_id = table.Column<int>(type: "int", nullable: true),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    corrected = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    corrected_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    corrected_by_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    corrected_byId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CustomerModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_AspNetUsers_corrected_byId",
                        column: x => x.corrected_byId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notes_AspNetUsers_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notes_Cars_veh_id",
                        column: x => x.veh_id,
                        principalTable: "Cars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notes_Customers_CustomerModelId",
                        column: x => x.CustomerModelId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notes_Docs_doc_id",
                        column: x => x.doc_id,
                        principalTable: "Docs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notes_StateForm_sf_id",
                        column: x => x.sf_id,
                        principalTable: "StateForm",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "created_at", "updated_at" },
                values: new object[,]
                {
                    { 1, "Battery", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Body", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Brakes", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Electrical", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Engine", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Engine Parts", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Glass", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "Handling Fees", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "Interior", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "Mechanical", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "Merchandise", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "Specials", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "Tires", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "Transmission", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "Transmission Parts", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "MasterProductionTable",
                columns: new[] { "Id", "row", "section" },
                values: new object[,]
                {
                    { 1, "1", "SP" },
                    { 2, "2", "SP" },
                    { 3, "3", "SP" },
                    { 4, "4", "SP" },
                    { 5, "5", "SP" },
                    { 6, "6", "SP" },
                    { 7, "7", "SP" },
                    { 8, "8", "T&V" },
                    { 9, "9", "T&V" },
                    { 10, "10", "T&V" },
                    { 11, "11", "T&V" },
                    { 12, "12", "T&V" },
                    { 13, "13", "T&V" },
                    { 14, "14", "T&V" },
                    { 15, "15", "T&V" },
                    { 16, "16", "T&V" },
                    { 17, "17", "T&V" },
                    { 18, "18", "IMP" },
                    { 19, "19", "IMP" },
                    { 20, "20", "IMP" },
                    { 21, "21", "IMP" },
                    { 22, "22", "IMP" },
                    { 23, "23", "IMP" },
                    { 24, "24", "IMP" },
                    { 25, "25", "IMP" },
                    { 26, "26", "CH" },
                    { 27, "27", "CH" },
                    { 28, "28", "T&V" },
                    { 29, "29", "T&V" },
                    { 30, "30", "T&V" },
                    { 31, "31", "T&V" },
                    { 32, "32", "T&V" },
                    { 33, "33", "GM" },
                    { 34, "34", "GM" },
                    { 35, "35", "GM" },
                    { 36, "36", "GM" },
                    { 37, "37", "GM" },
                    { 38, "38", "GM" },
                    { 39, "39", "GM" },
                    { 40, "40", "GM" },
                    { 41, "41", "GM" },
                    { 42, "42", "GM" },
                    { 43, "43", "FORD" },
                    { 44, "44", "FORD" },
                    { 45, "45", "FORD" },
                    { 46, "46", "FORD" },
                    { 47, "47", "FORD" },
                    { 48, "48", "FORD" },
                    { 49, "8B", "T&V" },
                    { 50, "9B", "T&V" },
                    { 51, "10B", "T&V" },
                    { 52, "11B", "T&V" },
                    { 53, "12B", "T&V" },
                    { 54, "13B", "T&V" },
                    { 55, "14B", "T&V" },
                    { 56, "15B", "T&V" },
                    { 57, "16B", "T&V" },
                    { 58, "17B", "T&V" },
                    { 59, "18B", "IMP" },
                    { 60, "19B", "IMP" },
                    { 61, "20B", "IMP" },
                    { 62, "21B", "IMP" },
                    { 63, "22B", "IMP" },
                    { 64, "23B", "IMP" },
                    { 65, "24B", "IMP" },
                    { 67, "25B", "IMP" },
                    { 68, "26B", "CH" },
                    { 69, "27B", "CH" },
                    { 70, "28B", "T&V" },
                    { 71, "29B", "T&V" },
                    { 72, "30B", "T&V" },
                    { 73, "31B", "T&V" },
                    { 74, "32B", "T&V" },
                    { 75, "33B", "GM" },
                    { 76, "34B", "GM" },
                    { 77, "35B", "GM" },
                    { 78, "36B", "GM" },
                    { 79, "37B", "GM" },
                    { 80, "38B", "GM" },
                    { 81, "39B", "GM" },
                    { 82, "40B", "GM" },
                    { 83, "41B", "GM" },
                    { 84, "42B", "GM" },
                    { 85, "43B", "FORD" },
                    { 86, "44B", "FORD" },
                    { 87, "45B", "FORD" },
                    { 88, "46B", "FORD" },
                    { 89, "47B", "FORD" },
                    { 90, "48B", "FORD" }
                });

            migrationBuilder.InsertData(
                table: "MasterVehicleTable",
                columns: new[] { "Id", "make", "model" },
                values: new object[,]
                {
                    { 1, "Other", "Other" },
                    { 2, "ACURA", "CL" },
                    { 3, "ACURA", "ILX" },
                    { 4, "ACURA", "ILX" },
                    { 5, "ACURA", "INTEGR" },
                    { 6, "ACURA", "MDX" },
                    { 7, "ACURA", "NSX" },
                    { 8, "ACURA", "RDX" },
                    { 9, "ACURA", "RLX" },
                    { 10, "ACURA", "RLX SPORTS" },
                    { 11, "ACURA", "RSX" },
                    { 12, "ACURA", "TL" },
                    { 13, "ACURA", "TLX" },
                    { 14, "ACURA", "TSX" },
                    { 15, "ACURA", "ZDX" },
                    { 16, "ALFA ROMEO", "4C" },
                    { 17, "ALFA ROMEO", "8C" },
                    { 18, "AM GENERAL", "HUMMER H1" },
                    { 19, "AM GENERAL", "HUMMER H2" },
                    { 20, "AM GENERAL", "HUMMER H3" },
                    { 21, "AUDI", "A3" },
                    { 22, "AUDI", "A4" },
                    { 23, "AUDI", "A5" },
                    { 24, "AUDI", "A6" },
                    { 25, "AUDI", "A7" },
                    { 26, "AUDI", "A8" },
                    { 27, "AUDI", "ALLROA" },
                    { 28, "AUDI", "Q3" },
                    { 29, "AUDI", "Q5" },
                    { 30, "AUDI", "Q7" },
                    { 31, "AUDI", "R8" },
                    { 32, "AUDI", "RS 4" },
                    { 33, "AUDI", "RS 5" },
                    { 34, "AUDI", "RS 7" },
                    { 35, "AUDI", "RS 6" },
                    { 36, "AUDI", "S3" },
                    { 37, "AUDI", "S4" },
                    { 38, "AUDI", "S5" },
                    { 39, "AUDI", "S6" },
                    { 40, "AUDI", "S7" },
                    { 41, "AUDI", "S8" },
                    { 42, "AUDI", "SQ" },
                    { 43, "AUDI", "TT" },
                    { 44, "AUDI", "TT RS" },
                    { 45, "BMW", "1 SERIES" },
                    { 46, "BMW", "128" },
                    { 47, "BMW", "135" },
                    { 48, "BMW", "228" },
                    { 49, "BMW", "3 SERIES" },
                    { 50, "BMW", "320" },
                    { 51, "BMW", "3232" },
                    { 52, "BMW", "325" },
                    { 53, "BMW", "328" },
                    { 54, "BMW", "328 GT" },
                    { 55, "BMW", "328D" },
                    { 56, "BMW", "330" },
                    { 57, "BMW", "335" },
                    { 58, "BMW", "335 GT" },
                    { 59, "BMW", "M3" },
                    { 60, "BMW", "4 SERIES" },
                    { 61, "BMW", "428" },
                    { 62, "BMW", "428 GT" },
                    { 63, "BMW", "435" },
                    { 64, "BMW", "435 GRAN COUP" },
                    { 65, "BMW", "M4" },
                    { 66, "BMW", "5 SERIES" },
                    { 67, "BMW", "525" },
                    { 68, "BMW", "528" },
                    { 69, "BMW", "530" },
                    { 70, "BMW", "535" },
                    { 71, "BMW", "535 GT" },
                    { 72, "BMW", "535D" },
                    { 73, "BMW", "540" },
                    { 74, "BMW", "545" },
                    { 75, "BMW", "550" },
                    { 76, "BMW", "550" },
                    { 77, "BMW", "FT" },
                    { 78, "BMW", "M5" },
                    { 79, "BMW", "6 SERIES" },
                    { 80, "BMW", "640" },
                    { 81, "BMW", "640 G COUPE" },
                    { 82, "BMW", "645" },
                    { 83, "BMW", "650" },
                    { 84, "BMW", "M6" },
                    { 85, "BMW", "7 SERIES" },
                    { 86, "BMW", "740" },
                    { 87, "BMW", "745" },
                    { 88, "BMW", "750" },
                    { 89, "BMW", "760" },
                    { 90, "BMW", "HYBRID 750" },
                    { 91, "BMW", "ALPINA B7" },
                    { 92, "BMW", "HYBRID 3" },
                    { 93, "BMW", "HYBRID 5" },
                    { 94, "BMW", "HYBRID 750" },
                    { 95, "VOLKSWAGEN", "PASSAT" },
                    { 96, "VOLKSWAGEN", "PHAETON" },
                    { 97, "VOLKSWAGEN", "R32" },
                    { 98, "VOLKSWAGEN", "RABBIT" },
                    { 99, "VOLKSWAGEN", "TIGUAN" },
                    { 100, "VOLKSWAGEN", "TOUAREG" },
                    { 101, "VOLKSWAGEN", "TOUAREG 2" },
                    { 102, "VOLKSWAGEN", "TOUAREG HYBRID" },
                    { 103, "VOLVO", "C30" },
                    { 104, "VOLVO", "C70" },
                    { 105, "VOLVO", "S40" },
                    { 106, "VOLVO", "S60" },
                    { 107, "VOLVO", "S70" },
                    { 108, "VOLVO", "S80" },
                    { 109, "VOLVO", "V40" },
                    { 110, "VOLVO", "V50" },
                    { 111, "VOLVO", "V60" },
                    { 112, "VOLVO", "V60 CROSS COUNTRY" },
                    { 113, "VOLVO", "V70" },
                    { 114, "VOLVO", "XC60" },
                    { 115, "VOLVO", "XC70" },
                    { 116, "VOLVO", "XC90" },
                    { 117, "Bulk", "Bulk" },
                    { 118, "Unlisted", "Unlisted" },
                    { 119, "VOLKSWAGEN", "ROUTAN" },
                    { 120, "EAGLE", "TALON" },
                    { 121, "OLDSMOBILE", "88" },
                    { 122, "FORD", "TEMPO" },
                    { 123, "CHRYSLER", "LEBARON" },
                    { 124, "VOLVO", "960" },
                    { 125, "FORD", "AEROSTAR" },
                    { 126, "NISSAN", "XE" },
                    { 127, "PORSCHE", "944" },
                    { 128, "DODGE", "DYNASTY" },
                    { 129, "CADILLAC", "TRAX" },
                    { 130, "SUBARU", "SVX" },
                    { 131, "SUBARU", "test" },
                    { 132, "BMW", "HYBRID 740" },
                    { 133, "BMW", "ALPINA B6" },
                    { 134, "BMW", "I3" },
                    { 135, "BMW", "M SERIES" },
                    { 136, "BMW", "M" },
                    { 137, "BMW", "M235" },
                    { 138, "BMW", "M3" },
                    { 139, "BMW", "M4" },
                    { 140, "BMW", "M5" },
                    { 141, "BMW", "M6" },
                    { 142, "BMW", "M6 COUPE" },
                    { 143, "BMW", "X5" },
                    { 144, "BMW", "X6" },
                    { 145, "BMW", "Z4" },
                    { 146, "BMW", "X" },
                    { 147, "BMW", "HYBRID X6" },
                    { 148, "BMW", "X1" },
                    { 149, "BMW", "X3" },
                    { 150, "BMW", "X5" },
                    { 151, "BMW", "X6" },
                    { 152, "BMW", "X6 M" },
                    { 153, "BMW", "Z SERIES" },
                    { 154, "BMW", "Z3" },
                    { 155, "BMW", "Z4" },
                    { 156, "BMW", "Z5" },
                    { 157, "BUICK", "Century" },
                    { 158, "BUICK", "Enclave" },
                    { 159, "BUICK", "Encore" },
                    { 160, "BUICK", "Lacrosse" },
                    { 161, "BUICK", "LeSabre" },
                    { 162, "BUICK", "Lucerne" },
                    { 163, "BUICK", "Park Avenue" },
                    { 164, "BUICK", "Rainier" },
                    { 165, "BUICK", "Regal" },
                    { 166, "BUICK", "Rendezvous" },
                    { 167, "BUICK", "Terraza" },
                    { 168, "BUICK", "Verano" },
                    { 169, "CADILLAC", "ATS" },
                    { 170, "CADILLAC", "ATS-V" },
                    { 171, "CADILLAC", "CATERA" },
                    { 172, "CADILLAC", "CTS" },
                    { 173, "CADILLAC", "DEVILLE" },
                    { 174, "CADILLAC", "DTS" },
                    { 175, "CADILLAC", "ELDORADO" },
                    { 176, "CADILLAC", "ELR" },
                    { 177, "CADILLAC", "ESCALADE" },
                    { 178, "CADILLAC", "ESCALADE ESV" },
                    { 179, "CADILLAC", "ESCALADE EXT" },
                    { 180, "CADILLAC", "ESCALADE HYBRID" },
                    { 181, "CADILLAC", "SEVILLE" },
                    { 182, "CADILLAC", "SRX" },
                    { 183, "CADILLAC", "STS" },
                    { 184, "CADILLAC", "XLR" },
                    { 185, "CADILLAC", "XTS" },
                    { 186, "CHEVROLET", "2500" },
                    { 187, "CHEVROLET", "3500" },
                    { 188, "CHEVROLET", "ASTRO" },
                    { 189, "CHEVROLET", "AVALANCE" },
                    { 190, "CHEVROLET", "AVEO" },
                    { 191, "CHEVROLET", "BLAZER" },
                    { 192, "CHEVROLET", "CAMARO" },
                    { 193, "CHEVROLET", "CAPTIVA SPORT" },
                    { 194, "CHEVROLET", "CAVALIER" },
                    { 195, "CHEVROLET", "CITY ESPRESS" },
                    { 196, "CHEVROLET", "CLASSIC" },
                    { 197, "CHEVROLET", "COBALT" },
                    { 198, "CHEVROLET", "COLORADO" },
                    { 199, "CHEVROLET", "CORVETTE" },
                    { 200, "CHEVROLET", "CRUZE" },
                    { 201, "CHEVROLET", "EQUINOX" },
                    { 202, "CHEVROLET", "EXPRESS VANS" },
                    { 203, "CHEVROLET", "EXPRESS 1500" },
                    { 204, "CHEVROLET", "EXPRESS 2500" },
                    { 205, "CHEVROLET", "EXPRESS 3500" },
                    { 206, "CHEVROLET", "HHR" },
                    { 207, "CHEVROLET", "IMPALA" },
                    { 208, "CHEVROLET", "IMPALA LIMITED" },
                    { 209, "CHEVROLET", "LUMINA" },
                    { 210, "CHEVROLET", "MALIBU" },
                    { 211, "CHEVROLET", "MALIBU CLASSIC" },
                    { 212, "CHEVROLET", "MALIBU HYBRID" },
                    { 213, "CHEVROLET", "MALIBU MAXX" },
                    { 214, "CHEVROLET", "METRO" },
                    { 215, "CHEVROLET", "MONTE CARLO" },
                    { 216, "CHEVROLET", "PICKUP" },
                    { 217, "CHEVROLET", "PRIZM" },
                    { 218, "CHEVROLET", "S -10" },
                    { 219, "CHEVROLET", "SILVERADO 1500" },
                    { 220, "CHEVROLET", "SILVERADO 1500" },
                    { 221, "CHEVROLET", "SILVERADO 2500" },
                    { 222, "CHEVROLET", "SILVERADO 3500" },
                    { 223, "CHEVROLET", "SONIC" },
                    { 224, "CHEVROLET", "SPARK" },
                    { 225, "CHEVROLET", "SPARK EV" },
                    { 226, "CHEVROLET", "SS" },
                    { 227, "CHEVROLET", "SSR" },
                    { 228, "CHEVROLET", "SUBURBAN" },
                    { 229, "CHEVROLET", "TAHOE" },
                    { 230, "CHEVROLET", "TAHOE HYBIRD" },
                    { 231, "CHEVROLET", "TRACKER" },
                    { 232, "CHEVROLET", "TRAILBLAZER" },
                    { 233, "CHEVROLET", "TRAILBLAZER EXT" },
                    { 234, "CHEVROLET", "TRAVERSE" },
                    { 235, "CHEVROLET", "UPLANDER" },
                    { 236, "CHEVROLET", "VENTURE" },
                    { 237, "CHEVROLET", "VOLT" },
                    { 238, "CHRYSLER", "200" },
                    { 239, "CHRYSLER", "300" },
                    { 240, "CHRYSLER", "300C" },
                    { 241, "CHRYSLER", "300M" },
                    { 242, "CHRYSLER", "ASPEN" },
                    { 243, "CHRYSLER", "ASPEN HYBRID" },
                    { 244, "CHRYSLER", "CIRRUS" },
                    { 245, "CHRYSLER", "CONCORDE" },
                    { 246, "CHRYSLER", "CROSSFIRE" },
                    { 247, "CHRYSLER", "GRAND VOYAGER" },
                    { 248, "CHRYSLER", "LHS" },
                    { 249, "CHRYSLER", "PACIFICA" },
                    { 250, "CHRYSLER", "PROWLER" },
                    { 251, "CHRYSLER", "PT CRUISER" },
                    { 252, "CHRYSLER", "SEBRING" },
                    { 253, "CHRYSLER", "TOWN & COUNTRY" },
                    { 254, "CHRYSLER", "VOYAGER" },
                    { 255, "DAEWOO", "LANOS" },
                    { 256, "DAEWOO", "LEGANZA" },
                    { 257, "DAEWOO", "NUBIRA" },
                    { 258, "DODGE", "AVENGER" },
                    { 259, "DODGE", "CALIBER" },
                    { 260, "DODGE", "CARAVAN" },
                    { 261, "DODGE", "CHALLENGER" },
                    { 262, "DODGE", "CHARGER" },
                    { 263, "DODGE", "DAKOTA" },
                    { 264, "DODGE", "DART" },
                    { 265, "DODGE", "DURANGO" },
                    { 266, "DODGE", "DURANGO HYB" },
                    { 267, "DODGE", "GRAND CARAVAN" },
                    { 268, "DODGE", "INTERPID" },
                    { 269, "DODGE", "JOURNEY" },
                    { 270, "DODGE", "MAGNUM" },
                    { 271, "DODGE", "NEON" },
                    { 272, "DODGE", "RAM ALL" },
                    { 273, "DODGE", "RAM PICKUP" },
                    { 274, "DODGE", "RAM 1500" },
                    { 275, "DODGE", "RAM 2500" },
                    { 276, "DODGE", "RAM 3500" },
                    { 277, "DODGE", "RAM VAN" },
                    { 278, "DODGE", "RAM WAGON" },
                    { 279, "DODGE", "SPRINTER" },
                    { 280, "DODGE", "SRT VIPER" },
                    { 281, "DODGE", "STRATUS" },
                    { 282, "DODGE", "VAN" },
                    { 283, "DODGE", "VIPER" },
                    { 284, "FIAT", "500" },
                    { 285, "FIAT", "500C" },
                    { 286, "FIAT", "500E" },
                    { 287, "FIAT", "500L" },
                    { 288, "FIAT", "500X" },
                    { 289, "FORD", "C-MAX ENERGI" },
                    { 290, "FORD", "C-MAX HYBRID" },
                    { 291, "FORD", "CONTOUR" },
                    { 292, "FORD", "CROWN VICTORIA" },
                    { 293, "FORD", "ECONOLINE VANS" },
                    { 294, "FORD", "E150" },
                    { 295, "FORD", "E250" },
                    { 296, "FORD", "E350" },
                    { 297, "FORD", "E350 SUPER DUTY" },
                    { 298, "FORD", "VAN" },
                    { 299, "FORD", "EDGE" },
                    { 300, "FORD", "ESCAPE" },
                    { 301, "FORD", "EXCAPE HYBRID" },
                    { 302, "FORD", "ESCORT" },
                    { 303, "FORD", "EXCURSION" },
                    { 304, "FORD", "EXPEDITION" },
                    { 305, "FORD", "EXPEDITION" },
                    { 306, "FORD", "EXPEDITION EL" },
                    { 307, "FORD", "EXPLORER" },
                    { 308, "FORD", "EXPLORE SPORT" },
                    { 309, "FORD", "F150" },
                    { 310, "FORD", "F250" },
                    { 311, "FORD", "F350" },
                    { 312, "FORD", "F450" },
                    { 313, "FORD", "F PICKUP" },
                    { 314, "FORD", "FIESTA" },
                    { 315, "FORD", "FIVE HUNDRED" },
                    { 316, "FORD", "FLEX" },
                    { 317, "FORD", "FOCUSE" },
                    { 318, "FORD", "FOCUSE ELECTRIC" },
                    { 319, "FORD", "FOCUSE ST" },
                    { 320, "FORD", "FREESTAR" },
                    { 321, "FORD", "FREESTYLE" },
                    { 322, "FORD", "FUSION" },
                    { 323, "FORD", "FUSION ENERGI" },
                    { 324, "FORD", "FUSION HYBRID" },
                    { 325, "FORD", "FT" },
                    { 326, "FORD", "MUSTANG" },
                    { 327, "FORD", "RANGER" },
                    { 328, "FORD", "SEDAN POLICE" },
                    { 329, "FORD", "TAURUS" },
                    { 330, "FORD", "TAURUS X" },
                    { 331, "FORD", "THUNDERBIRD" },
                    { 332, "FORD", "TRANSIT CONNECT" },
                    { 333, "FORD", "TRANSIT-150" },
                    { 334, "FORD", "TRANSIT-250" },
                    { 335, "FORD", "TRANSIT-350" },
                    { 336, "FORD", "WINDSTAR" },
                    { 337, "FORD", "ZX2" },
                    { 338, "GMC", "ACADIA" },
                    { 339, "GMC", "CANYON" },
                    { 340, "GMC", "ENVOY" },
                    { 341, "GMC", "ENVOY XL" },
                    { 342, "GMC", "ENVOY XUV" },
                    { 343, "GMC", "JIMMY" },
                    { 344, "GMC", "SAFARI" },
                    { 345, "GMC", "SAVANA VANS" },
                    { 346, "GMC", "SAVANA 1500" },
                    { 347, "GMC", "SAVANA 2500" },
                    { 348, "GMC", "SAVANA 3500" },
                    { 349, "GMC", "VAN" },
                    { 350, "GMC", "SIERRA TRUCH" },
                    { 351, "GMC", "PICKUP" },
                    { 352, "GMC", "SIERRA 1500" },
                    { 353, "GMC", "SIERRA 1500 HYB" },
                    { 354, "GMC", "SIERRA 2500" },
                    { 355, "GMC", "SIERRA 3500" },
                    { 356, "GMC", "SONOMA" },
                    { 357, "GMC", "TERRAIN" },
                    { 358, "GMC", "YUKON" },
                    { 359, "GMC", "YUKON HYBIRD" },
                    { 360, "GMC", "YUKON XL" },
                    { 361, "NISSAN", "NV 2500 HD" },
                    { 362, "NISSAN", "NV 3500 HD" },
                    { 363, "NISSAN", "NV PASSENGER" },
                    { 364, "NISSAN", "NV 200" },
                    { 365, "NISSAN", "PATHFINDER" },
                    { 366, "NISSAN", "PATHFINDER HYBR" },
                    { 367, "NISSAN", "QUEST" },
                    { 368, "NISSAN", "ROUGE" },
                    { 369, "NISSAN", "ROUGE SELECT" },
                    { 370, "NISSAN", "SENTRA" },
                    { 371, "NISSAN", "TITAN" },
                    { 372, "NISSAN", "VERSA" },
                    { 373, "NISSAN", "VERSA NOTE" },
                    { 374, "NISSAN", "XTERRA" },
                    { 375, "OLDSMOBILE", "ALERO" },
                    { 376, "OLDSMOBILE", "AURORA" },
                    { 377, "OLDSMOBILE", "BARVADA" },
                    { 378, "OLDSMOBILE", "CUTLASS" },
                    { 379, "OLDSMOBILE", "INTRIGE" },
                    { 380, "OLDSMOBILE", "SILHOUEETE" },
                    { 381, "PLYMOUTH", "BREEZE" },
                    { 382, "PLYMOUTH", "GRAND VOYAGER" },
                    { 383, "PLYMOUTH", "NEON" },
                    { 384, "PLYMOUTH", "PROWLER" },
                    { 385, "PLYMOUTH", "VOYAGER" },
                    { 386, "PONTIAC", "AZTEK" },
                    { 387, "PONTIAC", "BONNEVILLE" },
                    { 388, "PONTIAC", "FIREBIRD" },
                    { 389, "PONTIAC", "G3" },
                    { 390, "PONTIAC", "G5" },
                    { 391, "PONTIAC", "G6" },
                    { 392, "PONTIAC", "G8" },
                    { 393, "PONTIAC", "GRAND AM" },
                    { 394, "PONTIAC", "GRAND PRIX" },
                    { 395, "PONTIAC", "GTO" },
                    { 396, "PONTIAC", "MONTANA" },
                    { 397, "PONTIAC", "MONTANA SV6" },
                    { 398, "PONTIAC", "SOLSTICE" },
                    { 399, "PONTIAC", "SUNFIRE" },
                    { 400, "PONTIAC", "TORRENT" },
                    { 401, "PONTIAC", "VIBE" },
                    { 402, "RAM", "1500" },
                    { 403, "RAM", "2500" },
                    { 404, "RAM", "3500" },
                    { 405, "RAM", "CARGO" },
                    { 406, "RAM", "PROMASTER 1500" },
                    { 407, "RAM", "PROMASTER 2500" },
                    { 408, "RAM", "PROMASTER 2500 W" },
                    { 409, "RAM", "PROMASTER 3500" },
                    { 410, "RAM", "PROMASTER CITY" },
                    { 411, "SAAB", "9-2X" },
                    { 412, "SAAB", "42250" },
                    { 413, "SAAB", "9-3X" },
                    { 414, "SAAB", "9-4X" },
                    { 415, "SAAB", "42252" },
                    { 416, "SAAB", "9-7X" },
                    { 417, "SATURN", "ASTRA" },
                    { 418, "SATURN", "AURA" },
                    { 419, "SATURN", "AURA HYBIRD" },
                    { 420, "SATURN", "AURA GREEN LINE" },
                    { 421, "SATURN", "AURA HYBIRD" },
                    { 422, "SATURN", "ION" },
                    { 423, "SATURN", "L" },
                    { 424, "SATURN", "LS" },
                    { 425, "SATURN", "LW" },
                    { 426, "SATURN", "OUTLOOK" },
                    { 427, "SATURN", "RELAY" },
                    { 428, "SATURN", "SC" },
                    { 429, "SATURN", "SKY" },
                    { 430, "SATURN", "SL" },
                    { 431, "SATURN", "SW" },
                    { 432, "SATURN", "VUE" },
                    { 433, "SATURN", "VUE HYBRID" },
                    { 434, "SATURN", "VUE GREEN LINE" },
                    { 435, "SATURN", "VUE HYBIRD" },
                    { 436, "SCION", "FRS" },
                    { 437, "SCION", "IQ" },
                    { 438, "SCION", "TC" },
                    { 439, "SCION", "XA" },
                    { 440, "SCION", "XB" },
                    { 441, "SCION", "XD" },
                    { 442, "SMART", "ALL" },
                    { 443, "SUBARU", "B9 TRIBECA" },
                    { 444, "SUBARU", "BAJA" },
                    { 445, "SUBARU", "BRZ" },
                    { 446, "SUBARU", "FORESTER" },
                    { 447, "SUBARU", "IMPREZA" },
                    { 448, "SUBARU", "IMPREZA OUTBACK" },
                    { 449, "SUBARU", "IMPERZA WRX" },
                    { 450, "SUBARU", "IMPERZA WRX STI" },
                    { 451, "SUBARU", "LEGACY" },
                    { 452, "SUBARU", "OUTBACK" },
                    { 453, "SUBARU", "TRIBECA" },
                    { 454, "SUBARU", "WRX" },
                    { 455, "SUBARU", "XV CROSSTERK" },
                    { 456, "SUBARU", "XV CROSSTREK HY" },
                    { 457, "Suzuki", "AERIO" },
                    { 458, "Suzuki", "EQUATOR" },
                    { 459, "Suzuki", "ESTEEM" },
                    { 460, "Suzuki", "FORENZA" },
                    { 461, "Suzuki", "GRAND VITRARA" },
                    { 462, "Suzuki", "KIZASHI" },
                    { 463, "Suzuki", "RENO" },
                    { 464, "Suzuki", "SWIFT" },
                    { 465, "Suzuki", "SX4" },
                    { 466, "Suzuki", "VERONA" },
                    { 467, "Suzuki", "VITARA" },
                    { 468, "Suzuki", "XL7" },
                    { 469, "TOYOTA", "4RUNNER" },
                    { 470, "TOYOTA", "AVALON" },
                    { 471, "TOYOTA", "AVALON HYBRID" },
                    { 472, "TOYOTA", "CAMRY" },
                    { 473, "TOYOTA", "CAMRY HYBIRD" },
                    { 474, "TOYOTA", "CAMRY SOLARA" },
                    { 475, "TOYOTA", "CELICA" },
                    { 476, "TOYOTA", "COROLLA" },
                    { 477, "TOYOTA", "ECHO" },
                    { 478, "TOYOTA", "FJ CRUISER" },
                    { 479, "TOYOTA", "HIGHLANDER" },
                    { 480, "TOYOTA", "HIGHLANDER HYBR" },
                    { 481, "TOYOTA", "LAND CRUISER" },
                    { 482, "TOYOTA", "MATRIX" },
                    { 483, "TOYOTA", "MR2" },
                    { 484, "TOYOTA", "PRIUS" },
                    { 485, "TOYOTA", "PRIUS C" },
                    { 486, "TOYOTA", "PRIUS PLUG-IN" },
                    { 487, "TOYOTA", "PRIUS V" },
                    { 488, "TOYOTA", "RAV4" },
                    { 489, "TOYOTA", "RAV4 EV" },
                    { 490, "TOYOTA", "SEQUOIA" },
                    { 491, "TOYOTA", "SIENNA" },
                    { 492, "TOYOTA", "TACOMA" },
                    { 493, "TOYOTA", "TUNDRA" },
                    { 494, "TOYOTA", "VENXA" },
                    { 495, "TOYOTA", "YARIS" },
                    { 496, "VOLKSWAGEN", "BEETL" },
                    { 497, "VOLKSWAGEN", "CABRIO" },
                    { 498, "VOLKSWAGEN", "CC" },
                    { 499, "VOLKSWAGEN", "E-GOLD" },
                    { 500, "VOLKSWAGEN", "EOS" },
                    { 501, "VOLKSWAGEN", "EUROVAN" },
                    { 502, "VOLKSWAGEN", "GOLF" },
                    { 503, "VOLKSWAGEN", "GOLF GTI" },
                    { 504, "VOLKSWAGEN", "GOLD R" },
                    { 505, "VOLKSWAGEN", "GOLF SPORTWAGEN" },
                    { 506, "VOLKSWAGEN", "GTI" },
                    { 507, "VOLKSWAGEN", "JETTA" },
                    { 508, "VOLKSWAGEN", "JETTA HYBIRD" },
                    { 509, "VOLKSWAGEN", "JETTA SPORTWAGE" },
                    { 510, "VOLKSWAGEN", "NEW BEETLE" },
                    { 511, "HONDA", "ACCORD" },
                    { 512, "HONDA", "ACCORD CROSSTOUR" },
                    { 513, "HONDA", "ACCORD HYBIRD" },
                    { 514, "HONDA", "ACCORD PLUG IN" },
                    { 515, "HONDA", "CIVIC" },
                    { 516, "HONDA", "CIVIC HYBIRD" },
                    { 517, "HONDA", "CR-V" },
                    { 518, "HONDA", "CR-Z" },
                    { 519, "HONDA", "CROSSTOUR" },
                    { 520, "HONDA", "ELEMENT" },
                    { 521, "HONDA", "FIT" },
                    { 522, "HONDA", "FIT EV" },
                    { 523, "HONDA", "INSIGHT" },
                    { 524, "HONDA", "ODYSSEY" },
                    { 525, "HONDA", "PASSPORT" },
                    { 526, "HONDA", "PILOT" },
                    { 527, "HONDA", "PRELUDE" },
                    { 528, "HONDA", "RIDGELINE" },
                    { 529, "HONDA", "S2000" },
                    { 530, "HUMMER", "H1" },
                    { 531, "HUMMER", "H1 ALPHA" },
                    { 532, "HUMMER", "H2" },
                    { 533, "HUMMER", "H3" },
                    { 534, "HUMMER", "H3T" },
                    { 535, "HYUNDAI", "ACCENT" },
                    { 536, "HYUNDAI", "AZERA" },
                    { 537, "HYUNDAI", "ELANTRA" },
                    { 538, "HYUNDAI", "ELANTRA FT" },
                    { 539, "HYUNDAI", "ELANTRA TOURING" },
                    { 540, "HYUNDAI", "ENTOURAGE" },
                    { 541, "HYUNDAI", "EQUUS" },
                    { 542, "HYUNDAI", "GENESIS" },
                    { 543, "HYUNDAI", "GENESIS COUPE" },
                    { 544, "HYUNDAI", "SANTA FE" },
                    { 545, "HYUNDAI", "SANTA FE SPORT" },
                    { 546, "HYUNDAI", "SONATA" },
                    { 547, "HYUNDAI", "SONATA HYBRID" },
                    { 548, "HYUNDAI", "TIBURON" },
                    { 549, "HYUNDAI", "TUCSON" },
                    { 550, "HYUNDAI", "VELOSTER" },
                    { 551, "HYUNDAI", "VERACRUZ" },
                    { 552, "HYUNDAI", "XG300" },
                    { 553, "HYUNDAI", "XG350" },
                    { 554, "INFINITI", "EX35" },
                    { 555, "INFINITI", "EX37" },
                    { 556, "INFINITI", "FX35" },
                    { 557, "INFINITI", "FX37" },
                    { 558, "INFINITI", "FX45" },
                    { 559, "INFINITI", "FX50" },
                    { 560, "INFINITI", "G20" },
                    { 561, "INFINITI", "G25" },
                    { 562, "INFINITI", "G35" },
                    { 563, "INFINITI", "G37" },
                    { 564, "INFINITI", "I30" },
                    { 565, "INFINITI", "I35" },
                    { 566, "INFINITI", "IPL G" },
                    { 567, "INFINITI", "JX35" },
                    { 568, "INFINITI", "M35" },
                    { 569, "INFINITI", "M35H" },
                    { 570, "INFINITI", "M37" },
                    { 571, "INFINITI", "M45" },
                    { 572, "INFINITI", "M56" },
                    { 573, "INFINITI", "Q45" },
                    { 574, "INFINITI", "Q50" },
                    { 575, "INFINITI", "Q5" },
                    { 576, "INFINITI", "Q60" },
                    { 577, "INFINITI", "Q60IPL" },
                    { 578, "INFINITI", "Q70" },
                    { 579, "INFINITI", "Q70H" },
                    { 580, "INFINITI", "QX" },
                    { 581, "INFINITI", "QX4" },
                    { 582, "INFINITI", "QX50" },
                    { 583, "INFINITI", "QX56" },
                    { 584, "INFINITI", "QX60" },
                    { 585, "INFINITI", "QX70" },
                    { 586, "INFINITI", "QX80" },
                    { 587, "INFINITI", "QX60" },
                    { 588, "INFINITI", "QX HYBIRD" },
                    { 589, "INTERNATIONAL", "CX" },
                    { 590, "INTERNATIONAL", "MX" },
                    { 591, "INTERNATIONAL", "MXT" },
                    { 592, "INTERNATIONAL", "RXT" },
                    { 593, "ISUZU", "AMIGO" },
                    { 594, "ISUZU", "ASCENDER" },
                    { 595, "ISUZU", "AZIOM" },
                    { 596, "ISUZU", "ISUZU TRUCK" },
                    { 597, "ISUZU", "HOMBRE" },
                    { 598, "ISUZU", "I280" },
                    { 599, "ISUZU", "I290" },
                    { 600, "ISUZU", "I350" },
                    { 601, "ISUZU", "I370" },
                    { 602, "ISUZU", "RODEO" },
                    { 603, "ISUZU", "RODEO SPORT" },
                    { 604, "ISUZU", "TROOPER" },
                    { 605, "ISUZU", "VEHICROSS" },
                    { 606, "JAGUAR", "F-TYPE" },
                    { 607, "JAGUAR", "S-TYPE" },
                    { 608, "JAGUAR", "SUPER V8" },
                    { 609, "JAGUAR", "SUPER V8 PORTFOL" },
                    { 610, "JAGUAR", "VANDEN PLAS" },
                    { 611, "JAGUAR", "X-TYPE" },
                    { 612, "JAGUAR", "XF" },
                    { 613, "JAGUAR", "XJ" },
                    { 614, "JAGUAR", "XJ8" },
                    { 615, "JAGUAR", "XJR" },
                    { 616, "JAGUAR", "XK" },
                    { 617, "JAGUAR", "XK8" },
                    { 618, "JAGUAR", "XKR" },
                    { 619, "JEEP", "CHEROKEE" },
                    { 620, "JEEP", "COMMANDER" },
                    { 621, "JEEP", "COMPASS" },
                    { 622, "JEEP", "GRAND CHEROKEE" },
                    { 623, "JEEP", "LIBERTY" },
                    { 624, "JEEP", "PATRIOT" },
                    { 625, "JEEP", "WRANGLER" },
                    { 626, "JEEP", "WRANGLER UNLIMIT" },
                    { 627, "KIA", "AMANTI" },
                    { 628, "KIA", "BORREGO" },
                    { 629, "KIA", "CADENZA" },
                    { 630, "KIA", "FORTE" },
                    { 631, "KIA", "FORTE KOUP" },
                    { 632, "KIA", "K900" },
                    { 633, "KIA", "OPTIMA" },
                    { 634, "KIA", "OPTIMA HYBRID" },
                    { 635, "KIA", "RIO" },
                    { 636, "KIA", "RIO5" },
                    { 637, "KIA", "RONDO" },
                    { 638, "KIA", "SEDONA" },
                    { 639, "KIA", "SEPHIA" },
                    { 640, "KIA", "SORENTO" },
                    { 641, "KIA", "SOUL" },
                    { 642, "KIA", "SPECTRA" },
                    { 643, "KIA", "SPECTRA5" },
                    { 644, "KIA", "SPORTAGE" },
                    { 645, "LAND ROVER", "DISCOVERY" },
                    { 646, "LAND ROVER", "DISCOVERY SPORT" },
                    { 647, "LAND ROVER", "FREELANDER" },
                    { 648, "LAND ROVER", "LR2" },
                    { 649, "LAND ROVER", "LR3" },
                    { 650, "LAND ROVER", "LR4" },
                    { 651, "LAND ROVER", "RANGE ROVER" },
                    { 652, "LAND ROVER", "RANGE ROVER EVOQ" },
                    { 653, "LAND ROVER", "RANGE ROVER SPORT" },
                    { 654, "LEXUS", "CT 200H" },
                    { 655, "LEXUS", "ES 300" },
                    { 656, "LEXUS", "ES 300H" },
                    { 657, "LEXUS", "ES 330" },
                    { 658, "LEXUS", "ES 350" },
                    { 659, "LEXUS", "GS 300" },
                    { 660, "LEXUS", "GS 350" },
                    { 661, "LEXUS", "GS 400" },
                    { 662, "LEXUS", "GS 430" },
                    { 663, "LEXUS", "GS 450H" },
                    { 664, "LEXUS", "GS 460" },
                    { 665, "LEXUS", "GX 460" },
                    { 666, "LEXUS", "GX 470" },
                    { 667, "LEXUS", "HS 250H" },
                    { 668, "LEXUS", "IS 250" },
                    { 669, "LEXUS", "IS 250C" },
                    { 670, "LEXUS", "IS 300" },
                    { 671, "LEXUS", "IS 350" },
                    { 672, "LEXUS", "IS 350C" },
                    { 673, "LEXUS", "IS F" },
                    { 674, "LEXUS", "LS 400" },
                    { 675, "LEXUS", "LS 430" },
                    { 676, "LEXUS", "LS 460" },
                    { 677, "LEXUS", "LS 600HL" },
                    { 678, "LEXUS", "LX 470" },
                    { 679, "LEXUS", "LX 570" },
                    { 680, "LEXUS", "NX 200T" },
                    { 681, "LEXUS", "NX 300H" },
                    { 682, "LEXUS", "RC 350" },
                    { 683, "LEXUS", "RC F" },
                    { 684, "LEXUS", "RX 300" },
                    { 685, "LEXUS", "RX 330" },
                    { 686, "LEXUS", "RX 350" },
                    { 687, "LEXUS", "RX 400H" },
                    { 688, "LEXUS", "RX 450H" },
                    { 689, "LEXUS", "SC 300" },
                    { 690, "LEXUS", "SC 400" },
                    { 691, "LEXUS", "SC 430" },
                    { 692, "LINCOLN", "AVIATOR" },
                    { 693, "LINCOLN", "BLACKWOOD" },
                    { 694, "LINCOLN", "CONTINENTAL" },
                    { 695, "LINCOLN", "LS" },
                    { 696, "LINCOLN", "MARK LT" },
                    { 697, "LINCOLN", "MKC" },
                    { 698, "LINCOLN", "MKS" },
                    { 699, "LINCOLN", "MKT" },
                    { 700, "LINCOLN", "MKX" },
                    { 701, "LINCOLN", "MKZ" },
                    { 702, "LINCOLN", "MKZ HYBRID" },
                    { 703, "LINCOLN", "NAVIGATOR" },
                    { 704, "LINCOLN", "TOWN CAR" },
                    { 705, "LINCOLN", "ZEPHYR" },
                    { 706, "MAZDA", "628" },
                    { 707, "MAZDA", "B2300" },
                    { 708, "MAZDA", "B2500" },
                    { 709, "MAZDA", "B3000" },
                    { 710, "MAZDA", "B4000" },
                    { 711, "MAZDA", "PICKUP" },
                    { 712, "MAZDA", "CX-5" },
                    { 713, "MAZDA", "CX-7" },
                    { 714, "MAZDA", "CX-9" },
                    { 715, "MAZDA", "MAZDA2" },
                    { 716, "MAZDA", "MAZDA3" },
                    { 717, "MAZDA", "MAZDA5" },
                    { 718, "MAZDA", "MAZDA6" },
                    { 719, "MAZDA", "MAZDASPEED MIAT" },
                    { 720, "MAZDA", "MAZDASPEED PORT" },
                    { 721, "MAZDA", "MAZDASPEED3" },
                    { 722, "MAZDA", "MAZDASPEED6" },
                    { 723, "MAZDA", "MIATA MX5" },
                    { 724, "MAZDA", "MILLENIA" },
                    { 725, "MAZDA", "MPV" },
                    { 726, "MAZDA", "PROTEGE" },
                    { 727, "MAZDA", "PROTEGE5" },
                    { 728, "MAZDA", "RX-8" },
                    { 729, "MAZDA", "TRIBUTE" },
                    { 730, "MAZDA", "TRIBUTE HYBRID" },
                    { 731, "MERCEDES-BENZ", "AMG GT" },
                    { 732, "MERCEDES-BENZ", "B ELECTRIC" },
                    { 733, "MERCEDES-BENZ", "DRIVE" },
                    { 734, "MERCEDES-BENZ", "C CLASS" },
                    { 735, "MERCEDES-BENZ", "CL CLASS" },
                    { 736, "MERCEDES-BENZ", "CLA CLASS" },
                    { 737, "MERCEDES-BENZ", "CLK CLASS" },
                    { 738, "MERCEDES-BENZ", "CLS CLASS" },
                    { 739, "MERCEDES-BENZ", "E CLASS" },
                    { 740, "MERCEDES-BENZ", "G CLASS" },
                    { 741, "MERCEDES-BENZ", "GL CLASS" },
                    { 742, "MERCEDES-BENZ", "CLA CLASS" },
                    { 743, "MERCEDES-BENZ", "CLK CLASS" },
                    { 744, "MERCEDES-BENZ", "M CLASS" },
                    { 745, "MERCEDES-BENZ", "MAYBACH S" },
                    { 746, "MERCEDES-BENZ", "R CLASS" },
                    { 747, "MERCEDES-BENZ", "S CLASS" },
                    { 748, "MERCEDES-BENZ", "SL CLASS" },
                    { 749, "MERCEDES-BENZ", "CLK CLASS" },
                    { 750, "MERCEDES-BENZ", "SLR MCLAREN" },
                    { 751, "MERCEDES-BENZ", "SLS AMG" },
                    { 752, "MERCEDES-BENZ", "SLS AMG BLACK S" },
                    { 753, "MERCEDES-BENZ", "SPRINTER" },
                    { 754, "MERCURY", "COUGER" },
                    { 755, "MERCURY", "GRAND MARQUIS" },
                    { 756, "MERCURY", "MARAUDER" },
                    { 757, "MERCURY", "MARINER" },
                    { 758, "MERCURY", "MARINER HYBIRD" },
                    { 759, "MERCURY", "MILAN" },
                    { 760, "MERCURY", "MILAN HYBIRD" },
                    { 761, "MERCURY", "MONTEGO" },
                    { 762, "MERCURY", "MONTEREY" },
                    { 763, "MERCURY", "MOUNTAINEER" },
                    { 764, "MERCURY", "MYSTIQUE" },
                    { 765, "MERCURY", "SABLE" },
                    { 766, "MERCURY", "VILLAGER" },
                    { 767, "MINI", "CLUBMAN" },
                    { 768, "MINI", "COOPER CLUBMAN" },
                    { 769, "MINI", "COOPER SCLUBMAN" },
                    { 770, "MINI", "CONVERTIBLE" },
                    { 771, "MINI", "COOPER" },
                    { 772, "MINI", "COOPER S" },
                    { 773, "MINI", "COUNTRYMAN" },
                    { 774, "MINI", "COOPER COUNTRYMAN" },
                    { 775, "MINI", "COOPER S COUNTRY" },
                    { 776, "MINI", "COUNTRYMAN" },
                    { 777, "MINI", "COUPE" },
                    { 778, "MINI", "HARDTOP" },
                    { 779, "MINI", "PACEMAN" },
                    { 780, "MINI", "ROADSTER" },
                    { 781, "MITSUBISHI", "DIAMANTE" },
                    { 782, "MITSUBISHI", "ECLIPSE" },
                    { 783, "MITSUBISHI", "ENDAVOR" },
                    { 784, "MITSUBISHI", "GLANT" },
                    { 785, "MITSUBISHI", "I-MIEV" },
                    { 786, "MITSUBISHI", "LANCER" },
                    { 787, "MITSUBISHI", "LANCER EVOLUTION" },
                    { 788, "MITSUBISHI", "LANCER SPORTBACK" },
                    { 789, "MITSUBISHI", "MIRAGE" },
                    { 790, "MITSUBISHI", "MONTERO" },
                    { 791, "MITSUBISHI", "MONTERO SPORT" },
                    { 792, "MITSUBISHI", "OUTLANDER" },
                    { 793, "MITSUBISHI", "OUTLANDER SPORT" },
                    { 794, "MITSUBISHI", "RAIDER" },
                    { 795, "NISSAN", "350Z" },
                    { 796, "NISSAN", "370Z" },
                    { 797, "NISSAN", "ALTIMA" },
                    { 798, "NISSAN", "ALTIMA HYBRID" },
                    { 799, "NISSAN", "ARMADA" },
                    { 800, "NISSAN", "CUBE" },
                    { 801, "NISSAN", "FRONTIER" },
                    { 802, "NISSAN", "GT-R" },
                    { 803, "NISSAN", "JUKE" },
                    { 804, "NISSAN", "MAXIMA" },
                    { 805, "NISSAN", "MURANO" },
                    { 806, "NISSAN", "MURANO CROSSCAB" },
                    { 807, "NISSAN", "NV CARGO" },
                    { 808, "NISSAN", "NV 1500" }
                });

            migrationBuilder.InsertData(
                table: "MasterYearTable",
                columns: new[] { "Id", "Year" },
                values: new object[,]
                {
                    { 1, 2018 },
                    { 2, 2017 },
                    { 3, 2016 },
                    { 4, 2015 },
                    { 5, 2014 },
                    { 6, 2013 },
                    { 7, 2012 },
                    { 8, 2011 },
                    { 9, 2010 },
                    { 10, 2009 },
                    { 11, 2008 },
                    { 12, 2007 },
                    { 13, 2006 },
                    { 14, 2005 },
                    { 15, 2004 },
                    { 16, 2003 },
                    { 17, 2002 },
                    { 18, 2001 },
                    { 19, 2000 },
                    { 20, 1999 },
                    { 21, 1998 },
                    { 22, 1997 },
                    { 23, 1996 },
                    { 24, 1995 },
                    { 25, 1994 },
                    { 26, 1993 },
                    { 27, 1992 },
                    { 28, 1991 },
                    { 29, 1990 },
                    { 30, 1989 },
                    { 31, 1988 },
                    { 32, 1987 },
                    { 33, 1986 },
                    { 34, 1985 },
                    { 35, 1984 },
                    { 36, 1983 },
                    { 37, 1982 },
                    { 38, 1981 },
                    { 39, 1980 },
                    { 40, 1979 },
                    { 41, 1978 },
                    { 42, 1977 },
                    { 43, 1976 },
                    { 44, 1975 },
                    { 45, 1974 },
                    { 46, 1973 },
                    { 47, 1972 },
                    { 48, 1971 },
                    { 49, 1970 },
                    { 50, 1969 },
                    { 51, 1968 },
                    { 52, 1967 },
                    { 53, 1966 },
                    { 55, 1965 },
                    { 56, 1964 },
                    { 57, 1963 },
                    { 58, 1962 },
                    { 59, 1961 },
                    { 60, 1960 },
                    { 61, 1959 },
                    { 62, 1958 },
                    { 63, 1957 },
                    { 64, 1956 },
                    { 65, 1955 },
                    { 66, 1954 },
                    { 67, 1953 },
                    { 68, 1952 },
                    { 69, 1951 },
                    { 70, 1950 },
                    { 71, 1949 },
                    { 72, 1948 },
                    { 73, 1947 },
                    { 74, 1946 },
                    { 75, 1945 },
                    { 76, 1944 },
                    { 77, 1943 },
                    { 78, 1942 },
                    { 79, 1941 },
                    { 80, 1940 },
                    { 81, 1939 },
                    { 82, 1938 },
                    { 83, 1937 },
                    { 84, 1936 },
                    { 85, 1935 },
                    { 86, 1934 },
                    { 87, 1933 },
                    { 88, 1932 },
                    { 89, 1931 },
                    { 90, 1930 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_cid",
                table: "Cars",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CompanyId",
                table: "Cars",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_created_by_id",
                table: "Cars",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_driver_id",
                table: "Cars",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDrivers_company_id",
                table: "CompanyDrivers",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmployees_company_id",
                table: "CompanyEmployees",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyManagers_company_id",
                table: "CompanyManagers",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CompanyId",
                table: "Customers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_cphone_number",
                table: "Customers",
                column: "cphone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_created_by_id",
                table: "Customers",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_Docs_product_id",
                table: "Docs",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Docs_sf_id",
                table: "Docs",
                column: "sf_id",
                unique: true,
                filter: "[sf_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Docs_veh_id",
                table: "Docs",
                column: "veh_id");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_company_id",
                table: "Expenses",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_created_by_id",
                table: "Expenses",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_cus_id",
                table: "Expenses",
                column: "cus_id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_company_id",
                table: "Invoices",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_created_by_id",
                table: "Invoices",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_customer_id",
                table: "Invoices",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Localization_company_id",
                table: "Localization",
                column: "company_id",
                unique: true,
                filter: "[company_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_user_id",
                table: "Logs",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_corrected_byId",
                table: "Notes",
                column: "corrected_byId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_created_by_id",
                table: "Notes",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CustomerModelId",
                table: "Notes",
                column: "CustomerModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_doc_id",
                table: "Notes",
                column: "doc_id",
                unique: true,
                filter: "[doc_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_sf_id",
                table: "Notes",
                column: "sf_id");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_veh_id",
                table: "Notes",
                column: "veh_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_company_id",
                table: "Payments",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_customerId",
                table: "Payments",
                column: "customerId");

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductInvoices_product_id",
                table: "ProductInvoices",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_company_id",
                table: "Products",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_created_by_id",
                table: "Products",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_StateForm_CompanyId",
                table: "StateForm",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CompanyDrivers");

            migrationBuilder.DropTable(
                name: "CompanyEmployees");

            migrationBuilder.DropTable(
                name: "CompanyManagers");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Localization");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "MasterProductionTable");

            migrationBuilder.DropTable(
                name: "MasterVehicleTable");

            migrationBuilder.DropTable(
                name: "MasterYearTable");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ProductInvoices");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Docs");

            migrationBuilder.DropTable(
                name: "PosCustomers");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "StateForm");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
