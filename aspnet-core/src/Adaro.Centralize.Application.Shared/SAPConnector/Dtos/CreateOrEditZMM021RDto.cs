using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class CreateOrEditZMM021RDto : EntityDto<Guid?>
    {

        [Required]
        [StringLength(ZMM021RConsts.MaxPurchasingDocumentLength, MinimumLength = ZMM021RConsts.MinPurchasingDocumentLength)]
        public string PurchasingDocument { get; set; }

        [Required]
        [StringLength(ZMM021RConsts.MaxPurchasingDocTypeLength, MinimumLength = ZMM021RConsts.MinPurchasingDocTypeLength)]
        public string PurchasingDocType { get; set; }

        [StringLength(ZMM021RConsts.MaxPurchasingDocTypeDescriptionLength, MinimumLength = ZMM021RConsts.MinPurchasingDocTypeDescriptionLength)]
        public string PurchasingDocTypeDescription { get; set; }

        [Required]
        [StringLength(ZMM021RConsts.MaxItemLength, MinimumLength = ZMM021RConsts.MinItemLength)]
        public string Item { get; set; }

        [StringLength(ZMM021RConsts.MaxLineNumberLength, MinimumLength = ZMM021RConsts.MinLineNumberLength)]
        public string LineNumber { get; set; }

        [StringLength(ZMM021RConsts.MaxDeletionIndicatorLength, MinimumLength = ZMM021RConsts.MinDeletionIndicatorLength)]
        public string DeletionIndicator { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [StringLength(ZMM021RConsts.MaxPurchaseRequisitionLength, MinimumLength = ZMM021RConsts.MinPurchaseRequisitionLength)]
        public string PurchaseRequisition { get; set; }

        [StringLength(ZMM021RConsts.MaxItemPRLength, MinimumLength = ZMM021RConsts.MinItemPRLength)]
        public string ItemPR { get; set; }

        [StringLength(ZMM021RConsts.MaxSupplierCodeLength, MinimumLength = ZMM021RConsts.MinSupplierCodeLength)]
        public string SupplierCode { get; set; }

        [StringLength(ZMM021RConsts.MaxSupplierNameLength, MinimumLength = ZMM021RConsts.MinSupplierNameLength)]
        public string SupplierName { get; set; }

        [StringLength(ZMM021RConsts.MaxAddressLength, MinimumLength = ZMM021RConsts.MinAddressLength)]
        public string Address { get; set; }

        [StringLength(ZMM021RConsts.MaxItemNoLength, MinimumLength = ZMM021RConsts.MinItemNoLength)]
        public string ItemNo { get; set; }

        [StringLength(ZMM021RConsts.MaxMaterialGroupLength, MinimumLength = ZMM021RConsts.MinMaterialGroupLength)]
        public string MaterialGroup { get; set; }

        [StringLength(ZMM021RConsts.MaxShortTextLength, MinimumLength = ZMM021RConsts.MinShortTextLength)]
        public string ShortText { get; set; }

        public double? OrderQuantity { get; set; }

        [StringLength(ZMM021RConsts.MaxOrderUnitLength, MinimumLength = ZMM021RConsts.MinOrderUnitLength)]
        public string OrderUnit { get; set; }

        [StringLength(ZMM021RConsts.MaxCurrencyLength, MinimumLength = ZMM021RConsts.MinCurrencyLength)]
        public string Currency { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public double? NetPrice { get; set; }

        public double? NetOrderValue { get; set; }

        public double? Demurrage { get; set; }

        public double? GrossPrice { get; set; }

        public double? TotalDiscount { get; set; }

        public double? FreightCost { get; set; }

        [StringLength(ZMM021RConsts.MaxReleaseIndicatorLength, MinimumLength = ZMM021RConsts.MinReleaseIndicatorLength)]
        public string ReleaseIndicator { get; set; }

        [StringLength(ZMM021RConsts.MaxPlantLength, MinimumLength = ZMM021RConsts.MinPlantLength)]
        public string Plant { get; set; }

        [StringLength(ZMM021RConsts.MaxPurchasingGroupLength, MinimumLength = ZMM021RConsts.MinPurchasingGroupLength)]
        public string PurchasingGroup { get; set; }

        [StringLength(ZMM021RConsts.MaxTaxCodeLength, MinimumLength = ZMM021RConsts.MinTaxCodeLength)]
        public string TaxCode { get; set; }

        [StringLength(ZMM021RConsts.MaxCollectiveNumberLength, MinimumLength = ZMM021RConsts.MinCollectiveNumberLength)]
        public string CollectiveNumber { get; set; }

        [StringLength(ZMM021RConsts.MaxItemCategoryLength, MinimumLength = ZMM021RConsts.MinItemCategoryLength)]
        public string ItemCategory { get; set; }

        [StringLength(ZMM021RConsts.MaxAccountAssignmentLength, MinimumLength = ZMM021RConsts.MinAccountAssignmentLength)]
        public string AccountAssignment { get; set; }

        [StringLength(ZMM021RConsts.MaxOutlineAgreementLength, MinimumLength = ZMM021RConsts.MinOutlineAgreementLength)]
        public string OutlineAgreement { get; set; }

        [StringLength(ZMM021RConsts.MaxRFQNoLength, MinimumLength = ZMM021RConsts.MinRFQNoLength)]
        public string RFQNo { get; set; }

        public double? QtyPending { get; set; }

        [StringLength(ZMM021RConsts.MaxMaterialServiceLength, MinimumLength = ZMM021RConsts.MinMaterialServiceLength)]
        public string MaterialService { get; set; }

        [StringLength(ZMM021RConsts.MaxApprovalStatusLength, MinimumLength = ZMM021RConsts.MinApprovalStatusLength)]
        public string ApprovalStatus { get; set; }

        [StringLength(ZMM021RConsts.MaxPOStatusLength, MinimumLength = ZMM021RConsts.MinPOStatusLength)]
        public string POStatus { get; set; }

        [StringLength(ZMM021RConsts.MaxPeriodLength, MinimumLength = ZMM021RConsts.MinPeriodLength)]
        public string Period { get; set; }

        public string CommentVendor { get; set; }

        public string ItemText { get; set; }

        public string LongText { get; set; }

        [StringLength(ZMM021RConsts.MaxOurReferenceLength, MinimumLength = ZMM021RConsts.MinOurReferenceLength)]
        public string OurReference { get; set; }

        public DateTime? PRFinalFirstApprovalDate { get; set; }

        public DateTime? PRFinalLastApprovalDate { get; set; }

        public DateTime? POFirstApprovalDate { get; set; }

        public DateTime? POLastApprovalDate { get; set; }

        [StringLength(ZMM021RConsts.MaxPOApprovalNameLength, MinimumLength = ZMM021RConsts.MinPOApprovalNameLength)]
        public string POApprovalName { get; set; }

        [StringLength(ZMM021RConsts.MaxBuyerCodeLength, MinimumLength = ZMM021RConsts.MinBuyerCodeLength)]
        public string BuyerCode { get; set; }

        [StringLength(ZMM021RConsts.MaxBuyerNameLength, MinimumLength = ZMM021RConsts.MinBuyerNameLength)]
        public string BuyerName { get; set; }

        [StringLength(ZMM021RConsts.MaxPICDeptLength, MinimumLength = ZMM021RConsts.MinPICDeptLength)]
        public string PICDept { get; set; }

        [StringLength(ZMM021RConsts.MaxPICSectLength, MinimumLength = ZMM021RConsts.MinPICSectLength)]
        public string PICSect { get; set; }

        [StringLength(ZMM021RConsts.MaxFuelAllocationLength, MinimumLength = ZMM021RConsts.MinFuelAllocationLength)]
        public string FuelAllocation { get; set; }

        [StringLength(ZMM021RConsts.MaxCostCenterLength, MinimumLength = ZMM021RConsts.MinCostCenterLength)]
        public string CostCenter { get; set; }

        [StringLength(ZMM021RConsts.MaxCostCenterDescriptionLength, MinimumLength = ZMM021RConsts.MinCostCenterDescriptionLength)]
        public string CostCenterDescription { get; set; }

        [StringLength(ZMM021RConsts.MaxWBSElementLength, MinimumLength = ZMM021RConsts.MinWBSElementLength)]
        public string WBSElement { get; set; }

        [StringLength(ZMM021RConsts.MaxAssetNoLength, MinimumLength = ZMM021RConsts.MinAssetNoLength)]
        public string AssetNo { get; set; }

        [StringLength(ZMM021RConsts.MaxFundCenterLength, MinimumLength = ZMM021RConsts.MinFundCenterLength)]
        public string FundCenter { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public string DocumentId { get; set; }

    }
}