using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adaro.Centralize.Migrations
{
    /// <inheritdoc />
    public partial class Added_EKPO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ekko",
                table: "ekko");

            migrationBuilder.RenameTable(
                name: "ekko",
                newName: "EKKO");

            migrationBuilder.RenameIndex(
                name: "IX_ekko_TenantId",
                table: "EKKO",
                newName: "IX_EKKO_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EKKO",
                table: "EKKO",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EKPO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    MANDT = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EBELN = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EBELP = table.Column<long>(type: "bigint", nullable: true),
                    UNIQUEID = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LOEKZ = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    STATU = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AEDAT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TXZ01 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MATNR = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EMATN = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BUKRS = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    WERKS = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LGORT = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BEDNR = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MATKL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    INFNR = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IDNLF = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KTMNG = table.Column<long>(type: "bigint", nullable: true),
                    MENGE = table.Column<long>(type: "bigint", nullable: true),
                    MEINS = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BPRME = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BPUMZ = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BPUMN = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UMREZ = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UMREN = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NETPR = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PEINH = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NETWR = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BRTWR = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AGDAT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WEBAZ = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MWSKZ = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BONUS = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    INSMK = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SPINF = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PRSDR = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BWTAR = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BWTTY = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ABSKZ = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PSTYP = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KNTTP = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KONNR = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KTPNR = table.Column<long>(type: "bigint", nullable: true),
                    PACKNO = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ANFNR = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BANFN = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BNFPO = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EKPO", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EKPO_TenantId",
                table: "EKPO",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EKPO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EKKO",
                table: "EKKO");

            migrationBuilder.RenameTable(
                name: "EKKO",
                newName: "ekko");

            migrationBuilder.RenameIndex(
                name: "IX_EKKO_TenantId",
                table: "ekko",
                newName: "IX_ekko_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ekko",
                table: "ekko",
                column: "Id");
        }
    }
}
