using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adaro.Centralize.Migrations
{
    /// <inheritdoc />
    public partial class Added_MaterialRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    RequestNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UoM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackageSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneralLedger = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImageColletion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UNSPSCId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ValuationClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialRequests_GeneralLedgerMappings_ValuationClassId",
                        column: x => x.ValuationClassId,
                        principalTable: "GeneralLedgerMappings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialRequests_UNSPSCs_UNSPSCId",
                        column: x => x.UNSPSCId,
                        principalTable: "UNSPSCs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRequests_TenantId",
                table: "MaterialRequests",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRequests_UNSPSCId",
                table: "MaterialRequests",
                column: "UNSPSCId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRequests_ValuationClassId",
                table: "MaterialRequests",
                column: "ValuationClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialRequests");
        }
    }
}
