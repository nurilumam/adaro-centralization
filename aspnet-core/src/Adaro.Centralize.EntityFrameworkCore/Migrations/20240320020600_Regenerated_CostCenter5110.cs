using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adaro.Centralize.Migrations
{
    /// <inheritdoc />
    public partial class Regenerated_CostCenter5110 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CostCenterCode",
                table: "CostCenters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CostCenterShort",
                table: "CostCenters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentName",
                table: "CostCenters",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostCenterCode",
                table: "CostCenters");

            migrationBuilder.DropColumn(
                name: "CostCenterShort",
                table: "CostCenters");

            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "CostCenters");
        }
    }
}
