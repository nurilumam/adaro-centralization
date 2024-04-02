using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adaro.Centralize.Migrations
{
    /// <inheritdoc />
    public partial class Added_GeneralLedgerAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProcessingStatus",
                table: "ZMM020R",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "GeneralLedgerAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    FundsCenter = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ConsumableBudget = table.Column<double>(type: "float", nullable: true),
                    ConsumedBudget = table.Column<double>(type: "float", nullable: true),
                    AvailableAmount = table.Column<double>(type: "float", nullable: true),
                    CurrentBudget = table.Column<double>(type: "float", nullable: true),
                    CommitmentActuals = table.Column<double>(type: "float", nullable: true),
                    FundsCenterDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostCenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralLedgerAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralLedgerAccounts_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneralLedgerAccounts_CostCenterId",
                table: "GeneralLedgerAccounts",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralLedgerAccounts_TenantId",
                table: "GeneralLedgerAccounts",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralLedgerAccounts");

            migrationBuilder.AlterColumn<string>(
                name: "ProcessingStatus",
                table: "ZMM020R",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
