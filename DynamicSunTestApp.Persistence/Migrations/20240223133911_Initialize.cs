using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicSunTestApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherArchiveEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherArchiveEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherArchiveRecordEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Temperature = table.Column<double>(type: "double precision", nullable: false),
                    Humidity = table.Column<double>(type: "double precision", nullable: false),
                    DewPoint = table.Column<double>(type: "double precision", nullable: false),
                    AtmosphericPressure = table.Column<int>(type: "integer", nullable: false),
                    WindDirection = table.Column<string>(type: "text", nullable: true),
                    WindSpeed = table.Column<int>(type: "integer", nullable: true),
                    Cloudiness = table.Column<int>(type: "integer", nullable: true),
                    LowerBoundaryOfCloudiness = table.Column<int>(type: "integer", nullable: true),
                    HorizontalVisibility = table.Column<int>(type: "integer", nullable: true),
                    WeatherEvents = table.Column<string>(type: "text", nullable: true),
                    WeatherArchiveId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherArchiveRecordEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherArchiveRecordEntities_WeatherArchiveEntities_Weather~",
                        column: x => x.WeatherArchiveId,
                        principalTable: "WeatherArchiveEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherArchiveRecordEntities_WeatherArchiveId",
                table: "WeatherArchiveRecordEntities",
                column: "WeatherArchiveId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherArchiveRecordEntities");

            migrationBuilder.DropTable(
                name: "WeatherArchiveEntities");
        }
    }
}
