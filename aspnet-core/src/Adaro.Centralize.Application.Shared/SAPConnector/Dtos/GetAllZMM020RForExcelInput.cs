using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class GetAllZMM020RForExcelInput
    {
        public string Filter { get; set; }

        public string PurchaseRequisitionFilter { get; set; }

        public string DocumentTypeFilter { get; set; }

        public string DocumentTypeTextFilter { get; set; }

        public string ItemRequisitionFilter { get; set; }

        public string ProcessingStatusCodeFilter { get; set; }

        public string ProcessingStatusFilter { get; set; }

        public string DeletionIndicatorFilter { get; set; }

        public string ItemCategoryFilter { get; set; }

        public string AccountAssignmentFilter { get; set; }

        public string MaterialFilter { get; set; }

        public string ShortTextFilter { get; set; }

        public double? MaxQuantityRequestedFilter { get; set; }
        public double? MinQuantityRequestedFilter { get; set; }

        public string UnitOfMeasureFilter { get; set; }

        public string ServiceItemFilter { get; set; }

        public string ServiceFilter { get; set; }

        public string ServiceShortTextFilter { get; set; }

        public double? MaxQuantityServiceFilter { get; set; }
        public double? MinQuantityServiceFilter { get; set; }

        public string UnitOfMeasureServiceFilter { get; set; }

        public DateTime? MaxDeliveryDateFilter { get; set; }
        public DateTime? MinDeliveryDateFilter { get; set; }

        public string MaterialGroupFilter { get; set; }

        public string PlantFilter { get; set; }

        public string StorageLocationFilter { get; set; }

        public string PurchaseGroupFilter { get; set; }

        public string RequisitionerFilter { get; set; }

        public string RequisitionerNameFilter { get; set; }

        public string PurchasingDocumentFilter { get; set; }

        public DateTime? MaxPurchaseOrderDateFilter { get; set; }
        public DateTime? MinPurchaseOrderDateFilter { get; set; }

        public string OutlineAgreementFilter { get; set; }

        public string PrincAgreementItemFilter { get; set; }

        public string PurchasingInfoRecFilter { get; set; }

        public string StatusFilter { get; set; }

        public string CreatedByFilter { get; set; }

        public string CurrencyFilter { get; set; }

        public string EntrySheetFilter { get; set; }

        public string GoodsReceiptFilter { get; set; }

        public string SupplierCodeFilter { get; set; }

        public string SupplierNameFilter { get; set; }

        public string ReleaseIndicatorFilter { get; set; }

        public double? MaxUnitPriceFilter { get; set; }
        public double? MinUnitPriceFilter { get; set; }

        public double? MaxValuationPriceFilter { get; set; }
        public double? MinValuationPriceFilter { get; set; }

        public string ItemTextFilter { get; set; }

        public string LongTextFilter { get; set; }

        public DateTime? MaxFirstApprovalDateFilter { get; set; }
        public DateTime? MinFirstApprovalDateFilter { get; set; }

        public string FirstApprovalNameFilter { get; set; }

        public DateTime? MaxLastApprovalDateFilter { get; set; }
        public DateTime? MinLastApprovalDateFilter { get; set; }

        public string LastApprovalNameFilter { get; set; }

        public string CostCenterFilter { get; set; }

        public string CostCenterDescriptionFilter { get; set; }

        public string WBSElementFilter { get; set; }

        public string AssetFilter { get; set; }

        public string FundsCenterFilter { get; set; }

        public double? MaxRemainQuantityFilter { get; set; }
        public double? MinRemainQuantityFilter { get; set; }

        public DateTime? MaxCreatedDateFilter { get; set; }
        public DateTime? MinCreatedDateFilter { get; set; }

        public DateTime? MaxUpdatedDateFilter { get; set; }
        public DateTime? MinUpdatedDateFilter { get; set; }

    }
}