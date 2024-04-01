using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class ZMM020RDto : EntityDto<Guid>
    {
        public string PurchaseRequisition { get; set; }

        public string DocumentType { get; set; }

        public string DocumentTypeText { get; set; }

        public string ItemRequisition { get; set; }

        public string ProcessingStatusCode { get; set; }

        public string ProcessingStatus { get; set; }

        public string DeletionIndicator { get; set; }

        public string ItemCategory { get; set; }

        public string AccountAssignment { get; set; }

        public string Material { get; set; }

        public string ShortText { get; set; }

        public double? QuantityRequested { get; set; }

        public string UnitOfMeasure { get; set; }

        public string ServiceItem { get; set; }

        public string Service { get; set; }

        public string ServiceShortText { get; set; }

        public double? QuantityService { get; set; }

        public string UnitOfMeasureService { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string MaterialGroup { get; set; }

        public string Plant { get; set; }

        public string StorageLocation { get; set; }

        public string PurchaseGroup { get; set; }

        public string Requisitioner { get; set; }

        public string RequisitionerName { get; set; }

        public string PurchasingDocument { get; set; }

        public DateTime? PurchaseOrderDate { get; set; }

        public string OutlineAgreement { get; set; }

        public string PrincAgreementItem { get; set; }

        public string PurchasingInfoRec { get; set; }

        public string Status { get; set; }

        public string CreatedBy { get; set; }

        public string Currency { get; set; }

        public string EntrySheet { get; set; }

        public string GoodsReceipt { get; set; }

        public string SupplierCode { get; set; }

        public string SupplierName { get; set; }

        public string ReleaseIndicator { get; set; }

        public double? UnitPrice { get; set; }

        public double? ValuationPrice { get; set; }

        public string ItemText { get; set; }

        public string LongText { get; set; }

        public DateTime? FirstApprovalDate { get; set; }

        public string FirstApprovalName { get; set; }

        public DateTime? LastApprovalDate { get; set; }

        public string LastApprovalName { get; set; }

        public string CostCenter { get; set; }

        public string CostCenterDescription { get; set; }

        public string WBSElement { get; set; }

        public string Asset { get; set; }

        public string FundsCenter { get; set; }

        public double? RemainQuantity { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

    }
}