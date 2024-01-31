using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adaro.Centralize.Migrations
{
    /// <inheritdoc />
    public partial class Regenerated_CostCenter1513 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CostCenters",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CostCenters");
        }
    }
}
