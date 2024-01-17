using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scheduler.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NationalHolidays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalHolidays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShiftPattern = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CurrentMonth = table.Column<int>(type: "int", nullable: false),
                    CurrentStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextMonthStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextMonthStartsWithNight = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    TeamRoleId = table.Column<int>(type: "int", nullable: false),
                    MonthsEmployed = table.Column<int>(type: "int", nullable: false),
                    ReligiousHoliday = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_TeamRoles_TeamRoleId",
                        column: x => x.TeamRoleId,
                        principalTable: "TeamRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Absences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    StartDay = table.Column<int>(type: "int", nullable: false),
                    EndDay = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Absences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Absences_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shifts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "NationalHolidays",
                columns: new[] { "Id", "Date", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nova Godina" },
                    { 2, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nova Godina" },
                    { 3, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bozic" },
                    { 4, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dan Drzavnosti" },
                    { 5, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dan Drzavnosti" },
                    { 6, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Praznik Rada" },
                    { 7, new DateTime(2024, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Praznik Rada" },
                    { 8, new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Veliki Petak" },
                    { 9, new DateTime(2024, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Velika Subota" },
                    { 10, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Uskrs" },
                    { 11, new DateTime(2024, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Uskrsnji ponedeljak" },
                    { 12, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dan primirja u I svetskom ratu" }
                });

            migrationBuilder.InsertData(
                table: "TeamRoles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Vođa smene", "VT" },
                    { 2, "Reanimaciona ambulanta", "RA" },
                    { 3, "Spoljna reanimacija", "SR" },
                    { 4, "Tehničar na strani", "--" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "CurrentMonth", "CurrentStartDate", "Name", "NextMonthStartDate", "NextMonthStartsWithNight", "ShiftPattern" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "TeamBn", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "DN3" },
                    { 2, 1, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "TeamČa", new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "DN3" },
                    { 3, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Teamić", new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "DN3" },
                    { 4, 1, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "TeamRn", new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "DN3" },
                    { 5, 1, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "TeamŠi", new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "DN3" },
                    { 6, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TeamPr", new DateTime(2021, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "8" },
                    { 7, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TeamBo", new DateTime(2023, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "8" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "LastName", "MonthsEmployed", "ReligiousHoliday", "TeamId", "TeamRoleId" },
                values: new object[,]
                {
                    { 1, "TestFname1", "TestLname1", 99, null, 1, 1 },
                    { 2, "TestFname2", "TestLname2", 97, null, 1, 1 },
                    { 3, "TestFname3", "TestLname3", 93, null, 1, 4 },
                    { 4, "TestFname4", "TestLname4", 92, null, 1, 4 },
                    { 5, "TestFname5", "TestLname5", 88, null, 1, 4 },
                    { 6, "TestFname6", "TestLname6", 79, null, 1, 4 },
                    { 7, "TestFname7", "TestLname7", 64, null, 1, 4 },
                    { 8, "TestFname8", "TestLname8", 61, null, 1, 2 },
                    { 9, "TestFname9", "TestLname9", 56, null, 1, 4 },
                    { 10, "TestFname10", "TestLname10", 47, null, 1, 4 },
                    { 11, "TestFname11", "TestLname11", 35, null, 1, 4 },
                    { 12, "TestFname12", "TestLname12", 32, null, 1, 2 },
                    { 13, "TestFname13", "TestLname13", 28, null, 1, 4 },
                    { 14, "TestFname14", "TestLname14", 25, null, 1, 4 },
                    { 15, "TestFname15", "TestLname15", 99, null, 2, 1 },
                    { 16, "TestFname16", "TestLname16", 97, null, 2, 1 },
                    { 17, "TestFname17", "TestLname17", 93, null, 2, 4 },
                    { 18, "TestFname18", "TestLname18", 92, null, 2, 4 },
                    { 19, "TestFname19", "TestLname19", 88, null, 2, 4 },
                    { 20, "TestFname20", "TestLname20", 79, null, 2, 4 },
                    { 21, "TestFname21", "TestLname21", 64, null, 2, 4 },
                    { 22, "TestFname22", "TestLname22", 61, null, 2, 4 },
                    { 23, "TestFname23", "TestLname23", 56, null, 2, 4 },
                    { 24, "TestFname24", "TestLname24", 47, null, 2, 3 },
                    { 25, "TestFname25", "TestLname25", 35, null, 2, 2 },
                    { 26, "TestFname26", "TestLname26", 32, null, 2, 4 },
                    { 27, "TestFname27", "TestLname27", 28, null, 2, 4 },
                    { 28, "TestFname28", "TestLname28", 25, null, 2, 4 },
                    { 29, "TestFname29", "TestLname29", 99, null, 3, 1 },
                    { 30, "TestFname30", "TestLname30", 97, null, 3, 1 },
                    { 31, "TestFname31", "TestLname31", 93, null, 3, 4 },
                    { 32, "TestFname32", "TestLname32", 92, null, 3, 4 },
                    { 33, "TestFname33", "TestLname33", 88, null, 3, 4 },
                    { 34, "TestFname34", "TestLname34", 79, null, 3, 4 },
                    { 35, "TestFname35", "TestLname35", 64, null, 3, 4 },
                    { 36, "TestFname36", "TestLname36", 61, null, 3, 4 },
                    { 37, "TestFname37", "TestLname37", 56, null, 3, 4 },
                    { 38, "TestFname38", "TestLname38", 47, null, 3, 4 },
                    { 39, "TestFname39", "TestLname39", 35, null, 3, 3 },
                    { 40, "TestFname40", "TestLname40", 32, null, 3, 4 },
                    { 41, "TestFname41", "TestLname41", 28, null, 3, 2 },
                    { 42, "TestFname42", "TestLname42", 25, null, 3, 4 },
                    { 43, "TestFname43", "TestLname43", 99, null, 4, 1 },
                    { 44, "TestFname44", "TestLname44", 97, null, 4, 1 },
                    { 45, "TestFname45", "TestLname45", 93, null, 4, 4 },
                    { 46, "TestFname46", "TestLname46", 92, null, 4, 4 },
                    { 47, "TestFname47", "TestLname47", 88, null, 4, 4 },
                    { 48, "TestFname48", "TestLname48", 79, null, 4, 4 },
                    { 49, "TestFname49", "TestLname49", 64, null, 4, 4 },
                    { 50, "TestFname50", "TestLname50", 61, null, 4, 4 },
                    { 51, "TestFname51", "TestLname51", 56, null, 4, 4 },
                    { 52, "TestFname52", "TestLname52", 47, null, 4, 4 },
                    { 53, "TestFname53", "TestLname53", 35, null, 4, 2 },
                    { 54, "TestFname54", "TestLname54", 32, null, 4, 3 },
                    { 55, "TestFname55", "TestLname55", 28, null, 4, 4 },
                    { 56, "TestFname56", "TestLname56", 25, null, 4, 4 },
                    { 57, "TestFname57", "TestLname57", 99, null, 5, 1 },
                    { 58, "TestFname58", "TestLname58", 97, null, 5, 1 },
                    { 59, "TestFname59", "TestLname59", 93, null, 5, 4 },
                    { 60, "TestFname60", "TestLname60", 92, null, 5, 4 },
                    { 61, "TestFname61", "TestLname61", 88, null, 5, 4 },
                    { 62, "TestFname62", "TestLname62", 79, null, 5, 4 },
                    { 63, "TestFname63", "TestLname63", 64, null, 5, 4 },
                    { 64, "TestFname64", "TestLname64", 61, null, 5, 4 },
                    { 65, "TestFname65", "TestLname65", 56, null, 5, 4 },
                    { 66, "TestFname66", "TestLname66", 47, null, 5, 3 },
                    { 67, "TestFname67", "TestLname67", 35, null, 5, 4 },
                    { 68, "TestFname68", "TestLname68", 32, null, 5, 4 },
                    { 69, "TestFname69", "TestLname69", 28, null, 5, 2 },
                    { 70, "TestFname70", "TestLname70", 25, null, 5, 4 },
                    { 71, "TestFname71", "TestLname71", 99, null, 6, 4 },
                    { 72, "TestFname72", "TestLname72", 97, null, 6, 4 },
                    { 73, "TestFname73", "TestLname73", 93, null, 6, 4 },
                    { 74, "TestFname74", "TestLname74", 92, null, 6, 4 },
                    { 75, "TestFname75", "TestLname75", 88, null, 6, 4 },
                    { 76, "TestFname76", "TestLname76", 79, null, 6, 4 },
                    { 77, "TestFname77", "TestLname77", 64, null, 6, 4 },
                    { 78, "TestFname78", "TestLname78", 61, null, 6, 4 },
                    { 79, "TestFname79", "TestLname79", 56, null, 6, 4 },
                    { 80, "TestFname80", "TestLname80", 47, null, 6, 4 },
                    { 81, "TestFname81", "TestLname81", 35, null, 6, 4 },
                    { 82, "TestFname82", "TestLname82", 32, null, 6, 4 },
                    { 83, "TestFname83", "TestLname83", 28, null, 6, 4 },
                    { 84, "TestFname84", "TestLname84", 25, null, 6, 4 },
                    { 85, "TestFname85", "TestLname85", 99, null, 7, 4 },
                    { 86, "TestFname86", "TestLname86", 97, null, 7, 4 },
                    { 87, "TestFname87", "TestLname87", 93, null, 7, 4 },
                    { 88, "TestFname88", "TestLname88", 92, null, 7, 4 },
                    { 89, "TestFname89", "TestLname89", 88, null, 7, 4 },
                    { 90, "TestFname90", "TestLname90", 79, null, 7, 4 },
                    { 91, "TestFname91", "TestLname91", 64, null, 7, 4 },
                    { 92, "TestFname92", "TestLname92", 61, null, 7, 4 },
                    { 93, "TestFname93", "TestLname93", 56, null, 7, 4 },
                    { 94, "TestFname94", "TestLname94", 47, null, 7, 4 },
                    { 95, "TestFname95", "TestLname95", 35, null, 7, 4 },
                    { 96, "TestFname96", "TestLname96", 32, null, 7, 4 },
                    { 97, "TestFname97", "TestLname97", 28, null, 7, 4 },
                    { 98, "TestFname98", "TestLname98", 25, null, 7, 4 }
                });

            migrationBuilder.InsertData(
                table: "Absences",
                columns: new[] { "Id", "EmployeeId", "EndDay", "Month", "StartDay", "Type", "Year" },
                values: new object[,]
                {
                    { 1, 2, 26, 1, 8, "GO", 2024 },
                    { 2, 4, 20, 1, 20, "VP", 2024 },
                    { 3, 9, 12, 1, 8, "BO", 2024 },
                    { 4, 9, 26, 1, 22, "GO", 2024 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Absences_EmployeeId",
                table: "Absences",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TeamId",
                table: "Employees",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TeamRoleId",
                table: "Employees",
                column: "TeamRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_EmployeeId",
                table: "Shifts",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Absences");

            migrationBuilder.DropTable(
                name: "NationalHolidays");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "TeamRoles");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
