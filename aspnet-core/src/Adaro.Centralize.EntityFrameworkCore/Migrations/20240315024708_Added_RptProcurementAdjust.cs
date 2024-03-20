using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adaro.Centralize.Migrations
{
    /// <inheritdoc />
    public partial class Added_RptProcurementAdjust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RptProcurementAdjusts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    PurchasingDocument = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsContract = table.Column<bool>(type: "bit", nullable: false),
                    IsAdjust = table.Column<bool>(type: "bit", nullable: false),
                    DayAdjust = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RptProcurementAdjusts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RptProcurementAdjusts_TenantId",
                table: "RptProcurementAdjusts",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RptProcurementAdjusts");
        }
    }
}
