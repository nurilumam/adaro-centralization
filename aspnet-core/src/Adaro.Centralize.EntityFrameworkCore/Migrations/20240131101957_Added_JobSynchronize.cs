using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adaro.Centralize.Migrations
{
    /// <inheritdoc />
    public partial class Added_JobSynchronize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobSynchronizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobType = table.Column<int>(type: "int", nullable: false),
                    DataSource = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastStatus = table.Column<int>(type: "int", nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalInsert = table.Column<int>(type: "int", nullable: false),
                    TotalUpdate = table.Column<int>(type: "int", nullable: false),
                    TotalDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSynchronizes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobSynchronizes_TenantId",
                table: "JobSynchronizes",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobSynchronizes");
        }
    }
}
