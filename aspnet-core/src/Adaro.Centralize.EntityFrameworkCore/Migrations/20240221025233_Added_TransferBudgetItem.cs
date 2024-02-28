using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adaro.Centralize.Migrations
{
    /// <inheritdoc />
    public partial class Added_TransferBudgetItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransferBudgetItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    PeriodFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountFrom = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PeriodTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountTo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransferBudgetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CostCenterIdFrom = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CostCenterIdTo = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferBudgetItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferBudgetItems_CostCenters_CostCenterIdFrom",
                        column: x => x.CostCenterIdFrom,
                        principalTable: "CostCenters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransferBudgetItems_CostCenters_CostCenterIdTo",
                        column: x => x.CostCenterIdTo,
                        principalTable: "CostCenters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransferBudgetItems_TransferBudgets_TransferBudgetId",
                        column: x => x.TransferBudgetId,
                        principalTable: "TransferBudgets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferBudgetItems_CostCenterIdFrom",
                table: "TransferBudgetItems",
                column: "CostCenterIdFrom");

            migrationBuilder.CreateIndex(
                name: "IX_TransferBudgetItems_CostCenterIdTo",
                table: "TransferBudgetItems",
                column: "CostCenterIdTo");

            migrationBuilder.CreateIndex(
                name: "IX_TransferBudgetItems_TenantId",
                table: "TransferBudgetItems",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferBudgetItems_TransferBudgetId",
                table: "TransferBudgetItems",
                column: "TransferBudgetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferBudgetItems");
        }
    }
}
