using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adaro.Centralize.Migrations
{
    /// <inheritdoc />
    public partial class Added_DataProduction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataProductions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    IntfId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntfSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntfSession = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObjectId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialDocYear = table.Column<int>(type: "int", nullable: false),
                    MaterialDocItem = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reservation = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseOrderItem = table.Column<int>(type: "int", nullable: false),
                    MovementType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovementTypeText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StorageLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vendor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QtyInOrderUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfEntry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeOfEntry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentHeaderText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Batch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostCenter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterfaceCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InterfaceCreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProductions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataProductions_TenantId",
                table: "DataProductions",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataProductions");
        }
    }
}
