using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class ZMM021RDto : EntityDto<Guid>
    {
        public string PurchasingDocument { get; set; }

        public string PurchasingDocType { get; set; }

        public string PurchasingDocTypeDescription { get; set; }

        public string Item { get; set; }

        public string LineNumber { get; set; }

        public string DeletionIndicator { get; set; }

        public DateTime DocumentDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public string PurchaseRequisition { get; set; }

        public string ItemPR { get; set; }

        public string SupplierCode { get; set; }

        public string SupplierName { get; set; }

        public string Address { get; set; }

        public string ItemNo { get; set; }

        public string MaterialGroup { get; set; }

        public string ShortText { get; set; }

        public double? OrderQuantity { get; set; }

        public string OrderUnit { get; set; }

        public string Currency { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public double? NetPrice { get; set; }

        public double? NetOrderValue { get; set; }

        public double? Demurrage { get; set; }

        public double? GrossPrice { get; set; }

        public double? TotalDiscount { get; set; }

        public double? FreightCost { get; set; }

        public string ReleaseIndicator { get; set; }

        public string Plant { get; set; }

        public string PurchasingGroup { get; set; }

        public string TaxCode { get; set; }

        public string CollectiveNumber { get; set; }

        public string ItemCategory { get; set; }

        public string AccountAssignment { get; set; }

        public string OutlineAgreement { get; set; }

        public string RFQNo { get; set; }

        public double? QtyPending { get; set; }

        public string MaterialService { get; set; }

        public string ApprovalStatus { get; set; }

        public string POStatus { get; set; }

        public string Period { get; set; }

        public string CommentVendor { get; set; }

        public string ItemText { get; set; }

        public string LongText { get; set; }

        public string OurReference { get; set; }

        public DateTime? PRFinalFirstApprovalDate { get; set; }

        public DateTime? PRFinalLastApprovalDate { get; set; }

        public DateTime? POFirstApprovalDate { get; set; }

        public DateTime? POLastApprovalDate { get; set; }

        public string POApprovalName { get; set; }

        public string BuyerCode { get; set; }

        public string BuyerName { get; set; }

        public string PICDept { get; set; }

        public string PICSect { get; set; }

        public string FuelAllocation { get; set; }

        public string CostCenter { get; set; }

        public string CostCenterDescription { get; set; }

        public string WBSElement { get; set; }

        public string AssetNo { get; set; }

        public string FundCenter { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

    }
}