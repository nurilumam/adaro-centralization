using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adaro.Centralize.Migrations
{
    /// <inheritdoc />
    public partial class Regenerated_ZMM020R9231 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZMM020R",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    PurchaseRequisition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DocumentTypeText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ItemRequisition = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    ProcessingStatusCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProcessingStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DeletionIndicator = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    ItemCategory = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    AccountAssignment = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Material = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShortText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    QuantityRequested = table.Column<double>(type: "float", nullable: true),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ServiceItem = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Service = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ServiceShortText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    QuantityService = table.Column<double>(type: "float", nullable: true),
                    UnitOfMeasureService = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaterialGroup = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Plant = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    StorageLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PurchaseGroup = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Requisitioner = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequisitionerName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PurchasingDocument = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PurchaseOrderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OutlineAgreement = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PrincAgreementItem = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PurchasingInfoRec = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    EntrySheet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GoodsReceipt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SupplierCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SupplierName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReleaseIndicator = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UnitPrice = table.Column<double>(type: "float", nullable: true),
                    ValuationPrice = table.Column<double>(type: "float", nullable: true),
                    ItemText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LongText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstApprovalName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LastApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastApprovalName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CostCenter = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CostCenterDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    WBSElement = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Asset = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FundsCenter = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RemainQuantity = table.Column<double>(type: "float", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZMM020R", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZMM020R_TenantId",
                table: "ZMM020R",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZMM020R");
        }
    }
}
