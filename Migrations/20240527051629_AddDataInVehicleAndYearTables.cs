using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MecuryProduct.Migrations
{
    /// <inheritdoc />
    public partial class AddDataInVehicleAndYearTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 293);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 294);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 295);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 296);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 297);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 298);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 299);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 311);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 312);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 316);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 317);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 318);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 319);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 320);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 321);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 322);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 323);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 324);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 325);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 326);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 327);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 328);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 329);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 330);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 331);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 332);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 333);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 334);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 335);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 336);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 337);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 338);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 339);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 340);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 341);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 342);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 343);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 344);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 345);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 346);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 347);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 348);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 349);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 350);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 351);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 352);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 353);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 354);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 355);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 356);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 357);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 358);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 359);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 360);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 361);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 362);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 363);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 364);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 365);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 366);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 367);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 368);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 369);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 370);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 371);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 372);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 373);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 374);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 375);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 376);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 377);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 378);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 379);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 380);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 381);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 382);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 383);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 384);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 385);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 386);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 387);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 388);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 389);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 390);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 391);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 392);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 393);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 394);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 395);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 396);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 397);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 398);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 399);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 400);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 401);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 402);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 403);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 404);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 405);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 406);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 407);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 408);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 409);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 410);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 411);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 412);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 413);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 414);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 415);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 416);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 417);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 418);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 419);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 420);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 421);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 422);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 423);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 424);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 425);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 426);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 427);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 428);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 429);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 430);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 431);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 432);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 433);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 434);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 435);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 436);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 437);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 438);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 439);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 440);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 441);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 442);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 443);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 444);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 445);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 446);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 447);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 448);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 449);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 450);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 451);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 452);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 453);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 454);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 455);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 456);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 457);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 458);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 459);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 460);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 461);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 462);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 463);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 464);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 465);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 466);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 467);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 468);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 469);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 470);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 471);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 472);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 473);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 474);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 475);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 476);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 477);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 478);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 479);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 480);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 481);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 482);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 483);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 484);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 485);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 486);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 487);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 488);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 489);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 490);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 491);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 492);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 493);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 494);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 495);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 496);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 497);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 498);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 499);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 500);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 501);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 502);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 503);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 504);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 505);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 506);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 507);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 508);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 509);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 510);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 511);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 512);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 513);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 514);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 515);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 516);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 517);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 518);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 519);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 520);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 521);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 522);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 523);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 524);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 525);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 526);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 527);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 528);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 529);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 530);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 531);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 532);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 533);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 534);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 535);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 536);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 537);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 538);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 539);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 540);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 541);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 542);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 543);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 544);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 545);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 546);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 547);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 548);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 549);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 550);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 551);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 552);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 553);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 554);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 555);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 556);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 557);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 558);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 559);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 560);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 561);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 562);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 563);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 564);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 565);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 566);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 567);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 568);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 569);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 570);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 571);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 572);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 573);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 574);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 575);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 576);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 577);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 578);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 579);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 580);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 581);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 582);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 583);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 584);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 585);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 586);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 587);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 588);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 589);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 590);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 591);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 592);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 593);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 594);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 595);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 596);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 597);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 598);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 599);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 600);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 601);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 602);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 603);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 604);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 605);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 606);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 607);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 608);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 609);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 610);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 611);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 612);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 613);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 614);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 615);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 616);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 617);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 618);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 619);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 620);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 621);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 622);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 623);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 624);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 625);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 626);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 627);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 628);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 629);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 630);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 631);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 632);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 633);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 634);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 635);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 636);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 637);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 638);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 639);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 640);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 641);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 642);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 643);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 644);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 645);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 646);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 647);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 648);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 649);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 650);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 651);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 652);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 653);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 654);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 655);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 656);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 657);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 658);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 659);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 660);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 661);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 662);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 663);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 664);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 665);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 666);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 667);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 668);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 669);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 670);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 671);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 672);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 673);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 674);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 675);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 676);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 677);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 678);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 679);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 680);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 681);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 682);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 683);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 684);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 685);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 686);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 687);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 688);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 689);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 690);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 691);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 692);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 693);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 694);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 695);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 696);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 697);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 698);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 699);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 700);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 701);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 702);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 703);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 704);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 705);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 706);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 707);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 708);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 709);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 710);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 711);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 712);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 713);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 714);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 715);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 716);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 717);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 718);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 719);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 720);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 721);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 722);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 723);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 724);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 725);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 726);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 727);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 728);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 729);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 730);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 731);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 732);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 733);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 734);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 735);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 736);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 737);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 738);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 739);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 740);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 741);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 742);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 743);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 744);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 745);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 746);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 747);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 748);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 749);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 750);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 751);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 752);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 753);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 754);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 755);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 756);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 757);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 758);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 759);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 760);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 761);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 762);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 763);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 764);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 765);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 766);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 767);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 768);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 769);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 770);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 771);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 772);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 773);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 774);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 775);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 776);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 777);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 778);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 779);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 780);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 781);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 782);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 783);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 784);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 785);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 786);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 787);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 788);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 789);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 790);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 791);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 792);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 793);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 794);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 795);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 796);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 797);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 798);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 799);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 800);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 801);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 802);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 803);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 804);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 805);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 806);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 807);

            migrationBuilder.DeleteData(
                table: "MasterVehicleTable",
                keyColumn: "Id",
                keyValue: 808);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "MasterYearTable",
                keyColumn: "Id",
                keyValue: 90);
        }
    }
}
