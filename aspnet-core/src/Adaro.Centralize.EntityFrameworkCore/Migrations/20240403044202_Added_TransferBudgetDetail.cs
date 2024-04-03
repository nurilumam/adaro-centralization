using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adaro.Centralize.Migrations
{
    /// <inheritdoc />
    public partial class Added_TransferBudgetDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransferBudgetDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransferType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransferBudgetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CostCenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeneralLedgerAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferBudgetDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferBudgetDetails_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferBudgetDetails_GeneralLedgerAccounts_GeneralLedgerAccountId",
                        column: x => x.GeneralLedgerAccountId,
                        principalTable: "GeneralLedgerAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransferBudgetDetails_TransferBudgets_TransferBudgetId",
                        column: x => x.TransferBudgetId,
                        principalTable: "TransferBudgets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferBudgetDetails_CostCenterId",
                table: "TransferBudgetDetails",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferBudgetDetails_GeneralLedgerAccountId",
                table: "TransferBudgetDetails",
                column: "GeneralLedgerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferBudgetDetails_TenantId",
                table: "TransferBudgetDetails",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferBudgetDetails_TransferBudgetId",
                table: "TransferBudgetDetails",
                column: "TransferBudgetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferBudgetDetails");
        }
    }
}
