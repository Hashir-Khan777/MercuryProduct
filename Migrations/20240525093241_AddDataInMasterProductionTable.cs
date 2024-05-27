using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class AddDataInMasterProductionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "MasterProductionTable",
                keyColumn: "Id",
                keyValue: 90);
        }
    }
}
