using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adaro.Centralize.Migrations
{
    /// <inheritdoc />
    public partial class Added_CostCenter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CostCenters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    ControllingArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostCenterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActState = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CostCenters_TenantId",
                table: "CostCenters",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostCenters");
        }
    }
}
