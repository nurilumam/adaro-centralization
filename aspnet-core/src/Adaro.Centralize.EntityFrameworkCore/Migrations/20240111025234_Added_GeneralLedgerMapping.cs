using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adaro.Centralize.Migrations
{
    /// <inheritdoc />
    public partial class Added_GeneralLedgerMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneralLedgerMappings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    GLAccount = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GLAccountDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MappingType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValuationClass = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ValuationClassDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralLedgerMappings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneralLedgerMappings_TenantId",
                table: "GeneralLedgerMappings",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralLedgerMappings");
        }
    }
}
