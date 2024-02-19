using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adaro.Centralize.Migrations
{
    /// <inheritdoc />
    public partial class Added_Ekko : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ekko",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    MANDT = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EBELN = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BUKRS = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BSTYP = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BSART = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BSAKZ = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LOEKZ = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    STATU = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AEDAT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ERNAM = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PINCR = table.Column<long>(type: "bigint", nullable: true),
                    LPONR = table.Column<long>(type: "bigint", nullable: true),
                    LIFNR = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ZTERM = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ZBD1T = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ZBD2T = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ZBD3T = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ZBD1P = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ZBD2P = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EKORG = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EKGRP = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    WAERS = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    WKURS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    KUFIX = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BEDAT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KDATB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KDATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BWBDT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GWLDT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IHRAN = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KUNNR = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KONNR = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ABGRU = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AUTLF = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    WEAKT = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RESWK = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LBLIF = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    INCO1 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    INCO2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SUBMI = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KNUMV = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KALSM = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PROCSTAT = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UNSEZ = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FRGGR = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FRGSX = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FRGKE = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FRGZU = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ADRNR = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ekko", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ekko_TenantId",
                table: "ekko",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ekko");
        }
    }
}
