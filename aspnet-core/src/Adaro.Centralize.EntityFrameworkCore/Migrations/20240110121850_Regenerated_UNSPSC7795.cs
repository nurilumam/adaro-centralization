using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adaro.Centralize.Migrations
{
    /// <inheritdoc />
    public partial class Regenerated_UNSPSC7795 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionId",
                table: "UNSPSCs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionId",
                table: "UNSPSCs");
        }
    }
}
