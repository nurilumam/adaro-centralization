using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adaro.Centralize.Migrations
{
    /// <inheritdoc />
    public partial class Added_ZMM021R : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZMM021R",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    PurchasingDocument = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PurchasingDocType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PurchasingDocTypeDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Item = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LineNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DeletionIndicator = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchaseRequisition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ItemPR = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SupplierCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SupplierName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ItemNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaterialGroup = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ShortText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OrderQuantity = table.Column<double>(type: "float", nullable: true),
                    OrderUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NetPrice = table.Column<double>(type: "float", nullable: true),
                    NetOrderValue = table.Column<double>(type: "float", nullable: true),
                    Demurrage = table.Column<double>(type: "float", nullable: true),
                    GrossPrice = table.Column<double>(type: "float", nullable: true),
                    TotalDiscount = table.Column<double>(type: "float", nullable: true),
                    FreightCost = table.Column<double>(type: "float", nullable: true),
                    ReleaseIndicator = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Plant = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PurchasingGroup = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TaxCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CollectiveNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ItemCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AccountAssignment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OutlineAgreement = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RFQNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    QtyPending = table.Column<double>(type: "float", nullable: true),
                    MaterialService = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    POStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Period = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CommentVendor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LongText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OurReference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PRFinalFirstApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PRFinalLastApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    POFirstApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    POLastApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    POApprovalName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BuyerCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BuyerName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PICDept = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PICSect = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FuelAllocation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CostCenter = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CostCenterDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    WBSElement = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AssetNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FundCenter = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZMM021R", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZMM021R_TenantId",
                table: "ZMM021R",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZMM021R");
        }
    }
}
