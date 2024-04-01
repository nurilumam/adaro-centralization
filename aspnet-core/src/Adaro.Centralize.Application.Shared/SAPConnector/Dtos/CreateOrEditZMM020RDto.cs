using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class CreateOrEditZMM020RDto : EntityDto<Guid?>
    {

        [Required]
        [StringLength(ZMM020RConsts.MaxPurchaseRequisitionLength, MinimumLength = ZMM020RConsts.MinPurchaseRequisitionLength)]
        public string PurchaseRequisition { get; set; }

        [Required]
        [StringLength(ZMM020RConsts.MaxDocumentTypeLength, MinimumLength = ZMM020RConsts.MinDocumentTypeLength)]
        public string DocumentType { get; set; }

        [Required]
        [StringLength(ZMM020RConsts.MaxDocumentTypeTextLength, MinimumLength = ZMM020RConsts.MinDocumentTypeTextLength)]
        public string DocumentTypeText { get; set; }

        [Required]
        [StringLength(ZMM020RConsts.MaxItemRequisitionLength, MinimumLength = ZMM020RConsts.MinItemRequisitionLength)]
        public string ItemRequisition { get; set; }

        [StringLength(ZMM020RConsts.MaxProcessingStatusCodeLength, MinimumLength = ZMM020RConsts.MinProcessingStatusCodeLength)]
        public string ProcessingStatusCode { get; set; }

        [StringLength(ZMM020RConsts.MaxProcessingStatusLength, MinimumLength = ZMM020RConsts.MinProcessingStatusLength)]
        public string ProcessingStatus { get; set; }

        [StringLength(ZMM020RConsts.MaxDeletionIndicatorLength, MinimumLength = ZMM020RConsts.MinDeletionIndicatorLength)]
        public string DeletionIndicator { get; set; }

        [StringLength(ZMM020RConsts.MaxItemCategoryLength, MinimumLength = ZMM020RConsts.MinItemCategoryLength)]
        public string ItemCategory { get; set; }

        [StringLength(ZMM020RConsts.MaxAccountAssignmentLength, MinimumLength = ZMM020RConsts.MinAccountAssignmentLength)]
        public string AccountAssignment { get; set; }

        [StringLength(ZMM020RConsts.MaxMaterialLength, MinimumLength = ZMM020RConsts.MinMaterialLength)]
        public string Material { get; set; }

        [StringLength(ZMM020RConsts.MaxShortTextLength, MinimumLength = ZMM020RConsts.MinShortTextLength)]
        public string ShortText { get; set; }

        public double? QuantityRequested { get; set; }

        [StringLength(ZMM020RConsts.MaxUnitOfMeasureLength, MinimumLength = ZMM020RConsts.MinUnitOfMeasureLength)]
        public string UnitOfMeasure { get; set; }

        [StringLength(ZMM020RConsts.MaxServiceItemLength, MinimumLength = ZMM020RConsts.MinServiceItemLength)]
        public string ServiceItem { get; set; }

        [StringLength(ZMM020RConsts.MaxServiceLength, MinimumLength = ZMM020RConsts.MinServiceLength)]
        public string Service { get; set; }

        [StringLength(ZMM020RConsts.MaxServiceShortTextLength, MinimumLength = ZMM020RConsts.MinServiceShortTextLength)]
        public string ServiceShortText { get; set; }

        public double? QuantityService { get; set; }

        [StringLength(ZMM020RConsts.MaxUnitOfMeasureServiceLength, MinimumLength = ZMM020RConsts.MinUnitOfMeasureServiceLength)]
        public string UnitOfMeasureService { get; set; }

        public DateTime? DeliveryDate { get; set; }

        [StringLength(ZMM020RConsts.MaxMaterialGroupLength, MinimumLength = ZMM020RConsts.MinMaterialGroupLength)]
        public string MaterialGroup { get; set; }

        [StringLength(ZMM020RConsts.MaxPlantLength, MinimumLength = ZMM020RConsts.MinPlantLength)]
        public string Plant { get; set; }

        [StringLength(ZMM020RConsts.MaxStorageLocationLength, MinimumLength = ZMM020RConsts.MinStorageLocationLength)]
        public string StorageLocation { get; set; }

        [StringLength(ZMM020RConsts.MaxPurchaseGroupLength, MinimumLength = ZMM020RConsts.MinPurchaseGroupLength)]
        public string PurchaseGroup { get; set; }

        [StringLength(ZMM020RConsts.MaxRequisitionerLength, MinimumLength = ZMM020RConsts.MinRequisitionerLength)]
        public string Requisitioner { get; set; }

        [StringLength(ZMM020RConsts.MaxRequisitionerNameLength, MinimumLength = ZMM020RConsts.MinRequisitionerNameLength)]
        public string RequisitionerName { get; set; }

        [StringLength(ZMM020RConsts.MaxPurchasingDocumentLength, MinimumLength = ZMM020RConsts.MinPurchasingDocumentLength)]
        public string PurchasingDocument { get; set; }

        public DateTime? PurchaseOrderDate { get; set; }

        [StringLength(ZMM020RConsts.MaxOutlineAgreementLength, MinimumLength = ZMM020RConsts.MinOutlineAgreementLength)]
        public string OutlineAgreement { get; set; }

        [StringLength(ZMM020RConsts.MaxPrincAgreementItemLength, MinimumLength = ZMM020RConsts.MinPrincAgreementItemLength)]
        public string PrincAgreementItem { get; set; }

        [StringLength(ZMM020RConsts.MaxPurchasingInfoRecLength, MinimumLength = ZMM020RConsts.MinPurchasingInfoRecLength)]
        public string PurchasingInfoRec { get; set; }

        [StringLength(ZMM020RConsts.MaxStatusLength, MinimumLength = ZMM020RConsts.MinStatusLength)]
        public string Status { get; set; }

        [StringLength(ZMM020RConsts.MaxCreatedByLength, MinimumLength = ZMM020RConsts.MinCreatedByLength)]
        public string CreatedBy { get; set; }

        [StringLength(ZMM020RConsts.MaxCurrencyLength, MinimumLength = ZMM020RConsts.MinCurrencyLength)]
        public string Currency { get; set; }

        [StringLength(ZMM020RConsts.MaxEntrySheetLength, MinimumLength = ZMM020RConsts.MinEntrySheetLength)]
        public string EntrySheet { get; set; }

        [StringLength(ZMM020RConsts.MaxGoodsReceiptLength, MinimumLength = ZMM020RConsts.MinGoodsReceiptLength)]
        public string GoodsReceipt { get; set; }

        [StringLength(ZMM020RConsts.MaxSupplierCodeLength, MinimumLength = ZMM020RConsts.MinSupplierCodeLength)]
        public string SupplierCode { get; set; }

        [StringLength(ZMM020RConsts.MaxSupplierNameLength, MinimumLength = ZMM020RConsts.MinSupplierNameLength)]
        public string SupplierName { get; set; }

        [StringLength(ZMM020RConsts.MaxReleaseIndicatorLength, MinimumLength = ZMM020RConsts.MinReleaseIndicatorLength)]
        public string ReleaseIndicator { get; set; }

        public double? UnitPrice { get; set; }

        public double? ValuationPrice { get; set; }

        public string ItemText { get; set; }

        public string LongText { get; set; }

        public DateTime? FirstApprovalDate { get; set; }

        [StringLength(ZMM020RConsts.MaxFirstApprovalNameLength, MinimumLength = ZMM020RConsts.MinFirstApprovalNameLength)]
        public string FirstApprovalName { get; set; }

        public DateTime? LastApprovalDate { get; set; }

        [StringLength(ZMM020RConsts.MaxLastApprovalNameLength, MinimumLength = ZMM020RConsts.MinLastApprovalNameLength)]
        public string LastApprovalName { get; set; }

        [StringLength(ZMM020RConsts.MaxCostCenterLength, MinimumLength = ZMM020RConsts.MinCostCenterLength)]
        public string CostCenter { get; set; }

        [StringLength(ZMM020RConsts.MaxCostCenterDescriptionLength, MinimumLength = ZMM020RConsts.MinCostCenterDescriptionLength)]
        public string CostCenterDescription { get; set; }

        [StringLength(ZMM020RConsts.MaxWBSElementLength, MinimumLength = ZMM020RConsts.MinWBSElementLength)]
        public string WBSElement { get; set; }

        [StringLength(ZMM020RConsts.MaxAssetLength, MinimumLength = ZMM020RConsts.MinAssetLength)]
        public string Asset { get; set; }

        [StringLength(ZMM020RConsts.MaxFundsCenterLength, MinimumLength = ZMM020RConsts.MinFundsCenterLength)]
        public string FundsCenter { get; set; }

        public double? RemainQuantity { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public string DocumentId { get; set; }

    }
}