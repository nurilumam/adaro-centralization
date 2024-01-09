using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adaro.Centralize.Migrations
{
    /// <inheritdoc />
    public partial class Added_TravelRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TravelRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    RequestNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TravelStatus = table.Column<int>(type: "int", nullable: false),
                    TravelType = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestPlanDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Camp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportBus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserTravel = table.Column<long>(type: "bigint", nullable: true),
                    AirportFrom = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AirportTo = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelRequests_AbpUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TravelRequests_AbpUsers_UserTravel",
                        column: x => x.UserTravel,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TravelRequests_Airports_AirportFrom",
                        column: x => x.AirportFrom,
                        principalTable: "Airports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TravelRequests_Airports_AirportTo",
                        column: x => x.AirportTo,
                        principalTable: "Airports",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TravelRequests_AirportFrom",
                table: "TravelRequests",
                column: "AirportFrom");

            migrationBuilder.CreateIndex(
                name: "IX_TravelRequests_AirportTo",
                table: "TravelRequests",
                column: "AirportTo");

            migrationBuilder.CreateIndex(
                name: "IX_TravelRequests_CreatedBy",
                table: "TravelRequests",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TravelRequests_TenantId",
                table: "TravelRequests",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelRequests_UserTravel",
                table: "TravelRequests",
                column: "UserTravel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelRequests");
        }
    }
}
