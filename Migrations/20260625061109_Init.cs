using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 

namespace WorkAd.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkAdvert",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    HourlyRate = table.Column<long>(type: "INTEGER", nullable: false),
                    ContractType = table.Column<int>(type: "INTEGER", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    WorkType = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkAdvert", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WorkAdvert",
                columns: new[] { "Id", "ContractType", "CreatedDate", "Description", "HourlyRate", "Location", "Title", "WorkType" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 6, 20, 6, 11, 9, 569, DateTimeKind.Utc).AddTicks(7954), "We are looking for a salesperson to work in our drugstore. The candidate should have experience in sales and customer service.", 3200L, "Warsaw", "Drugstore salesperson", 1 },
                    { 2, 3, new DateTime(2026, 6, 13, 6, 11, 9, 569, DateTimeKind.Utc).AddTicks(7959), "We are looking for a junior web developer to join our remote development team. Basic knowledge of HTML, CSS, and C# is required.", 4550L, "Gdańsk", "Junior Web Developer", 0 },
                    { 3, 2, new DateTime(2026, 6, 23, 6, 11, 9, 569, DateTimeKind.Utc).AddTicks(7962), "A warehouse assistant is needed for packaging, sorting and preparing shipments. No experience required, training provided.", 3750L, "Poznań", "Warehouse Assistant", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkAdvert_CreatedDate",
                table: "WorkAdvert",
                column: "CreatedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkAdvert");
        }
    }
}
