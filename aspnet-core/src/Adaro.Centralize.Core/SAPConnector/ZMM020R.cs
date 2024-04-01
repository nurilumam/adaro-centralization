using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Adaro.Centralize.SAPConnector
{
    [Table("ZMM020R")]
    public class ZMM020R : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        [StringLength(ZMM020RConsts.MaxPurchaseRequisitionLength, MinimumLength = ZMM020RConsts.MinPurchaseRequisitionLength)]
        public virtual string PurchaseRequisition { get; set; }

        [Required]
        [StringLength(ZMM020RConsts.MaxDocumentTypeLength, MinimumLength = ZMM020RConsts.MinDocumentTypeLength)]
        public virtual string DocumentType { get; set; }

        [Required]
        [StringLength(ZMM020RConsts.MaxDocumentTypeTextLength, MinimumLength = ZMM020RConsts.MinDocumentTypeTextLength)]
        public virtual string DocumentTypeText { get; set; }

        [Required]
        [StringLength(ZMM020RConsts.MaxItemRequisitionLength, MinimumLength = ZMM020RConsts.MinItemRequisitionLength)]
        public virtual string ItemRequisition { get; set; }

        [StringLength(ZMM020RConsts.MaxProcessingStatusCodeLength, MinimumLength = ZMM020RConsts.MinProcessingStatusCodeLength)]
        public virtual string ProcessingStatusCode { get; set; }

        [StringLength(ZMM020RConsts.MaxProcessingStatusLength, MinimumLength = ZMM020RConsts.MinProcessingStatusLength)]
        public virtual string ProcessingStatus { get; set; }

        [StringLength(ZMM020RConsts.MaxDeletionIndicatorLength, MinimumLength = ZMM020RConsts.MinDeletionIndicatorLength)]
        public virtual string DeletionIndicator { get; set; }

        [StringLength(ZMM020RConsts.MaxItemCategoryLength, MinimumLength = ZMM020RConsts.MinItemCategoryLength)]
        public virtual string ItemCategory { get; set; }

        [StringLength(ZMM020RConsts.MaxAccountAssignmentLength, MinimumLength = ZMM020RConsts.MinAccountAssignmentLength)]
        public virtual string AccountAssignment { get; set; }

        [StringLength(ZMM020RConsts.MaxMaterialLength, MinimumLength = ZMM020RConsts.MinMaterialLength)]
        public virtual string Material { get; set; }

        [StringLength(ZMM020RConsts.MaxShortTextLength, MinimumLength = ZMM020RConsts.MinShortTextLength)]
        public virtual string ShortText { get; set; }

        public virtual double? QuantityRequested { get; set; }

        [StringLength(ZMM020RConsts.MaxUnitOfMeasureLength, MinimumLength = ZMM020RConsts.MinUnitOfMeasureLength)]
        public virtual string UnitOfMeasure { get; set; }

        [StringLength(ZMM020RConsts.MaxServiceItemLength, MinimumLength = ZMM020RConsts.MinServiceItemLength)]
        public virtual string ServiceItem { get; set; }

        [StringLength(ZMM020RConsts.MaxServiceLength, MinimumLength = ZMM020RConsts.MinServiceLength)]
        public virtual string Service { get; set; }

        [StringLength(ZMM020RConsts.MaxServiceShortTextLength, MinimumLength = ZMM020RConsts.MinServiceShortTextLength)]
        public virtual string ServiceShortText { get; set; }

        public virtual double? QuantityService { get; set; }

        [StringLength(ZMM020RConsts.MaxUnitOfMeasureServiceLength, MinimumLength = ZMM020RConsts.MinUnitOfMeasureServiceLength)]
        public virtual string UnitOfMeasureService { get; set; }

        public virtual DateTime? DeliveryDate { get; set; }

        [StringLength(ZMM020RConsts.MaxMaterialGroupLength, MinimumLength = ZMM020RConsts.MinMaterialGroupLength)]
        public virtual string MaterialGroup { get; set; }

        [StringLength(ZMM020RConsts.MaxPlantLength, MinimumLength = ZMM020RConsts.MinPlantLength)]
        public virtual string Plant { get; set; }

        [StringLength(ZMM020RConsts.MaxStorageLocationLength, MinimumLength = ZMM020RConsts.MinStorageLocationLength)]
        public virtual string StorageLocation { get; set; }

        [StringLength(ZMM020RConsts.MaxPurchaseGroupLength, MinimumLength = ZMM020RConsts.MinPurchaseGroupLength)]
        public virtual string PurchaseGroup { get; set; }

        [StringLength(ZMM020RConsts.MaxRequisitionerLength, MinimumLength = ZMM020RConsts.MinRequisitionerLength)]
        public virtual string Requisitioner { get; set; }

        [StringLength(ZMM020RConsts.MaxRequisitionerNameLength, MinimumLength = ZMM020RConsts.MinRequisitionerNameLength)]
        public virtual string RequisitionerName { get; set; }

        [StringLength(ZMM020RConsts.MaxPurchasingDocumentLength, MinimumLength = ZMM020RConsts.MinPurchasingDocumentLength)]
        public virtual string PurchasingDocument { get; set; }

        public virtual DateTime? PurchaseOrderDate { get; set; }

        [StringLength(ZMM020RConsts.MaxOutlineAgreementLength, MinimumLength = ZMM020RConsts.MinOutlineAgreementLength)]
        public virtual string OutlineAgreement { get; set; }

        [StringLength(ZMM020RConsts.MaxPrincAgreementItemLength, MinimumLength = ZMM020RConsts.MinPrincAgreementItemLength)]
        public virtual string PrincAgreementItem { get; set; }

        [StringLength(ZMM020RConsts.MaxPurchasingInfoRecLength, MinimumLength = ZMM020RConsts.MinPurchasingInfoRecLength)]
        public virtual string PurchasingInfoRec { get; set; }

        [StringLength(ZMM020RConsts.MaxStatusLength, MinimumLength = ZMM020RConsts.MinStatusLength)]
        public virtual string Status { get; set; }

        [StringLength(ZMM020RConsts.MaxCreatedByLength, MinimumLength = ZMM020RConsts.MinCreatedByLength)]
        public virtual string CreatedBy { get; set; }

        [StringLength(ZMM020RConsts.MaxCurrencyLength, MinimumLength = ZMM020RConsts.MinCurrencyLength)]
        public virtual string Currency { get; set; }

        [StringLength(ZMM020RConsts.MaxEntrySheetLength, MinimumLength = ZMM020RConsts.MinEntrySheetLength)]
        public virtual string EntrySheet { get; set; }

        [StringLength(ZMM020RConsts.MaxGoodsReceiptLength, MinimumLength = ZMM020RConsts.MinGoodsReceiptLength)]
        public virtual string GoodsReceipt { get; set; }

        [StringLength(ZMM020RConsts.MaxSupplierCodeLength, MinimumLength = ZMM020RConsts.MinSupplierCodeLength)]
        public virtual string SupplierCode { get; set; }

        [StringLength(ZMM020RConsts.MaxSupplierNameLength, MinimumLength = ZMM020RConsts.MinSupplierNameLength)]
        public virtual string SupplierName { get; set; }

        [StringLength(ZMM020RConsts.MaxReleaseIndicatorLength, MinimumLength = ZMM020RConsts.MinReleaseIndicatorLength)]
        public virtual string ReleaseIndicator { get; set; }

        public virtual double? UnitPrice { get; set; }

        public virtual double? ValuationPrice { get; set; }

        public virtual string ItemText { get; set; }

        public virtual string LongText { get; set; }

        public virtual DateTime? FirstApprovalDate { get; set; }

        [StringLength(ZMM020RConsts.MaxFirstApprovalNameLength, MinimumLength = ZMM020RConsts.MinFirstApprovalNameLength)]
        public virtual string FirstApprovalName { get; set; }

        public virtual DateTime? LastApprovalDate { get; set; }

        [StringLength(ZMM020RConsts.MaxLastApprovalNameLength, MinimumLength = ZMM020RConsts.MinLastApprovalNameLength)]
        public virtual string LastApprovalName { get; set; }

        [StringLength(ZMM020RConsts.MaxCostCenterLength, MinimumLength = ZMM020RConsts.MinCostCenterLength)]
        public virtual string CostCenter { get; set; }

        [StringLength(ZMM020RConsts.MaxCostCenterDescriptionLength, MinimumLength = ZMM020RConsts.MinCostCenterDescriptionLength)]
        public virtual string CostCenterDescription { get; set; }

        [StringLength(ZMM020RConsts.MaxWBSElementLength, MinimumLength = ZMM020RConsts.MinWBSElementLength)]
        public virtual string WBSElement { get; set; }

        [StringLength(ZMM020RConsts.MaxAssetLength, MinimumLength = ZMM020RConsts.MinAssetLength)]
        public virtual string Asset { get; set; }

        [StringLength(ZMM020RConsts.MaxFundsCenterLength, MinimumLength = ZMM020RConsts.MinFundsCenterLength)]
        public virtual string FundsCenter { get; set; }

        public virtual double? RemainQuantity { get; set; }

        [Required]
        public virtual DateTime CreatedDate { get; set; }

        [Required]
        public virtual DateTime UpdatedDate { get; set; }

        [Required]
        public virtual string DocumentId { get; set; }

    }
}