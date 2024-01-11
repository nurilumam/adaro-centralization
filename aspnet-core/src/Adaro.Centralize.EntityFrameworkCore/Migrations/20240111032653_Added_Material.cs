using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adaro.Centralize.Migrations
{
    /// <inheritdoc />
    public partial class Added_Material : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    MaterialNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialNameSAP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UoM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageMain = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaterialGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UNSPSCId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GeneralLedgerMappingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_GeneralLedgerMappings_GeneralLedgerMappingId",
                        column: x => x.GeneralLedgerMappingId,
                        principalTable: "GeneralLedgerMappings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Materials_MaterialGroups_MaterialGroupId",
                        column: x => x.MaterialGroupId,
                        principalTable: "MaterialGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Materials_UNSPSCs_UNSPSCId",
                        column: x => x.UNSPSCId,
                        principalTable: "UNSPSCs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_GeneralLedgerMappingId",
                table: "Materials",
                column: "GeneralLedgerMappingId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_MaterialGroupId",
                table: "Materials",
                column: "MaterialGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_TenantId",
                table: "Materials",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_UNSPSCId",
                table: "Materials",
                column: "UNSPSCId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Materials");
        }
    }
}
