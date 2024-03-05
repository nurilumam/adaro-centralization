using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Adaro.Centralize.SAPConnector
{
    [Table("ZMM021R")]
    public class ZMM021R : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        [StringLength(ZMM021RConsts.MaxPurchasingDocumentLength, MinimumLength = ZMM021RConsts.MinPurchasingDocumentLength)]
        public virtual string PurchasingDocument { get; set; }

        [Required]
        [StringLength(ZMM021RConsts.MaxPurchasingDocTypeLength, MinimumLength = ZMM021RConsts.MinPurchasingDocTypeLength)]
        public virtual string PurchasingDocType { get; set; }

        [StringLength(ZMM021RConsts.MaxPurchasingDocTypeDescriptionLength, MinimumLength = ZMM021RConsts.MinPurchasingDocTypeDescriptionLength)]
        public virtual string PurchasingDocTypeDescription { get; set; }

        [Required]
        [StringLength(ZMM021RConsts.MaxItemLength, MinimumLength = ZMM021RConsts.MinItemLength)]
        public virtual string Item { get; set; }

        [StringLength(ZMM021RConsts.MaxLineNumberLength, MinimumLength = ZMM021RConsts.MinLineNumberLength)]
        public virtual string LineNumber { get; set; }

        [StringLength(ZMM021RConsts.MaxDeletionIndicatorLength, MinimumLength = ZMM021RConsts.MinDeletionIndicatorLength)]
        public virtual string DeletionIndicator { get; set; }

        [Required]
        public virtual DateTime DocumentDate { get; set; }

        [Required]
        public virtual DateTime CreatedOn { get; set; }

        [StringLength(ZMM021RConsts.MaxPurchaseRequisitionLength, MinimumLength = ZMM021RConsts.MinPurchaseRequisitionLength)]
        public virtual string PurchaseRequisition { get; set; }

        [StringLength(ZMM021RConsts.MaxItemPRLength, MinimumLength = ZMM021RConsts.MinItemPRLength)]
        public virtual string ItemPR { get; set; }

        [StringLength(ZMM021RConsts.MaxSupplierCodeLength, MinimumLength = ZMM021RConsts.MinSupplierCodeLength)]
        public virtual string SupplierCode { get; set; }

        [StringLength(ZMM021RConsts.MaxSupplierNameLength, MinimumLength = ZMM021RConsts.MinSupplierNameLength)]
        public virtual string SupplierName { get; set; }

        [StringLength(ZMM021RConsts.MaxAddressLength, MinimumLength = ZMM021RConsts.MinAddressLength)]
        public virtual string Address { get; set; }

        [StringLength(ZMM021RConsts.MaxItemNoLength, MinimumLength = ZMM021RConsts.MinItemNoLength)]
        public virtual string ItemNo { get; set; }

        [StringLength(ZMM021RConsts.MaxMaterialGroupLength, MinimumLength = ZMM021RConsts.MinMaterialGroupLength)]
        public virtual string MaterialGroup { get; set; }

        [StringLength(ZMM021RConsts.MaxShortTextLength, MinimumLength = ZMM021RConsts.MinShortTextLength)]
        public virtual string ShortText { get; set; }

        public virtual double? OrderQuantity { get; set; }

        [StringLength(ZMM021RConsts.MaxOrderUnitLength, MinimumLength = ZMM021RConsts.MinOrderUnitLength)]
        public virtual string OrderUnit { get; set; }

        [StringLength(ZMM021RConsts.MaxCurrencyLength, MinimumLength = ZMM021RConsts.MinCurrencyLength)]
        public virtual string Currency { get; set; }

        public virtual DateTime? DeliveryDate { get; set; }

        public virtual double? NetPrice { get; set; }

        public virtual double? NetOrderValue { get; set; }

        public virtual double? Demurrage { get; set; }

        public virtual double? GrossPrice { get; set; }

        public virtual double? TotalDiscount { get; set; }

        public virtual double? FreightCost { get; set; }

        [StringLength(ZMM021RConsts.MaxReleaseIndicatorLength, MinimumLength = ZMM021RConsts.MinReleaseIndicatorLength)]
        public virtual string ReleaseIndicator { get; set; }

        [StringLength(ZMM021RConsts.MaxPlantLength, MinimumLength = ZMM021RConsts.MinPlantLength)]
        public virtual string Plant { get; set; }

        [StringLength(ZMM021RConsts.MaxPurchasingGroupLength, MinimumLength = ZMM021RConsts.MinPurchasingGroupLength)]
        public virtual string PurchasingGroup { get; set; }

        [StringLength(ZMM021RConsts.MaxTaxCodeLength, MinimumLength = ZMM021RConsts.MinTaxCodeLength)]
        public virtual string TaxCode { get; set; }

        [StringLength(ZMM021RConsts.MaxCollectiveNumberLength, MinimumLength = ZMM021RConsts.MinCollectiveNumberLength)]
        public virtual string CollectiveNumber { get; set; }

        [StringLength(ZMM021RConsts.MaxItemCategoryLength, MinimumLength = ZMM021RConsts.MinItemCategoryLength)]
        public virtual string ItemCategory { get; set; }

        [StringLength(ZMM021RConsts.MaxAccountAssignmentLength, MinimumLength = ZMM021RConsts.MinAccountAssignmentLength)]
        public virtual string AccountAssignment { get; set; }

        [StringLength(ZMM021RConsts.MaxOutlineAgreementLength, MinimumLength = ZMM021RConsts.MinOutlineAgreementLength)]
        public virtual string OutlineAgreement { get; set; }

        [StringLength(ZMM021RConsts.MaxRFQNoLength, MinimumLength = ZMM021RConsts.MinRFQNoLength)]
        public virtual string RFQNo { get; set; }

        public virtual double? QtyPending { get; set; }

        [StringLength(ZMM021RConsts.MaxMaterialServiceLength, MinimumLength = ZMM021RConsts.MinMaterialServiceLength)]
        public virtual string MaterialService { get; set; }

        [StringLength(ZMM021RConsts.MaxApprovalStatusLength, MinimumLength = ZMM021RConsts.MinApprovalStatusLength)]
        public virtual string ApprovalStatus { get; set; }

        [StringLength(ZMM021RConsts.MaxPOStatusLength, MinimumLength = ZMM021RConsts.MinPOStatusLength)]
        public virtual string POStatus { get; set; }

        [StringLength(ZMM021RConsts.MaxPeriodLength, MinimumLength = ZMM021RConsts.MinPeriodLength)]
        public virtual string Period { get; set; }

        public virtual string CommentVendor { get; set; }

        public virtual string ItemText { get; set; }

        public virtual string LongText { get; set; }

        [StringLength(ZMM021RConsts.MaxOurReferenceLength, MinimumLength = ZMM021RConsts.MinOurReferenceLength)]
        public virtual string OurReference { get; set; }

        public virtual DateTime? PRFinalFirstApprovalDate { get; set; }

        public virtual DateTime? PRFinalLastApprovalDate { get; set; }

        public virtual DateTime? POFirstApprovalDate { get; set; }

        public virtual DateTime? POLastApprovalDate { get; set; }

        [StringLength(ZMM021RConsts.MaxPOApprovalNameLength, MinimumLength = ZMM021RConsts.MinPOApprovalNameLength)]
        public virtual string POApprovalName { get; set; }

        [StringLength(ZMM021RConsts.MaxBuyerCodeLength, MinimumLength = ZMM021RConsts.MinBuyerCodeLength)]
        public virtual string BuyerCode { get; set; }

        [StringLength(ZMM021RConsts.MaxBuyerNameLength, MinimumLength = ZMM021RConsts.MinBuyerNameLength)]
        public virtual string BuyerName { get; set; }

        [StringLength(ZMM021RConsts.MaxPICDeptLength, MinimumLength = ZMM021RConsts.MinPICDeptLength)]
        public virtual string PICDept { get; set; }

        [StringLength(ZMM021RConsts.MaxPICSectLength, MinimumLength = ZMM021RConsts.MinPICSectLength)]
        public virtual string PICSect { get; set; }

        [StringLength(ZMM021RConsts.MaxFuelAllocationLength, MinimumLength = ZMM021RConsts.MinFuelAllocationLength)]
        public virtual string FuelAllocation { get; set; }

        [StringLength(ZMM021RConsts.MaxCostCenterLength, MinimumLength = ZMM021RConsts.MinCostCenterLength)]
        public virtual string CostCenter { get; set; }

        [StringLength(ZMM021RConsts.MaxCostCenterDescriptionLength, MinimumLength = ZMM021RConsts.MinCostCenterDescriptionLength)]
        public virtual string CostCenterDescription { get; set; }

        [StringLength(ZMM021RConsts.MaxWBSElementLength, MinimumLength = ZMM021RConsts.MinWBSElementLength)]
        public virtual string WBSElement { get; set; }

        [StringLength(ZMM021RConsts.MaxAssetNoLength, MinimumLength = ZMM021RConsts.MinAssetNoLength)]
        public virtual string AssetNo { get; set; }

        [StringLength(ZMM021RConsts.MaxFundCenterLength, MinimumLength = ZMM021RConsts.MinFundCenterLength)]
        public virtual string FundCenter { get; set; }

        [Required]
        public virtual DateTime CreatedDate { get; set; }

        [Required]
        public virtual DateTime UpdatedDate { get; set; }

        [Required]
        public virtual string DocumentId { get; set; }

    }
}