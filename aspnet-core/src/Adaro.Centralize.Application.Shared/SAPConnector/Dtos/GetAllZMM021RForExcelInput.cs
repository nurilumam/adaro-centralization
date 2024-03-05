using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class GetAllZMM021RForExcelInput
    {
        public string Filter { get; set; }

        public string PurchasingDocumentFilter { get; set; }

        public string PurchasingDocTypeFilter { get; set; }

        public string PurchasingDocTypeDescriptionFilter { get; set; }

        public string ItemFilter { get; set; }

        public string LineNumberFilter { get; set; }

        public string DeletionIndicatorFilter { get; set; }

        public DateTime? MaxDocumentDateFilter { get; set; }
        public DateTime? MinDocumentDateFilter { get; set; }

        public DateTime? MaxCreatedOnFilter { get; set; }
        public DateTime? MinCreatedOnFilter { get; set; }

        public string PurchaseRequisitionFilter { get; set; }

        public string ItemPRFilter { get; set; }

        public string SupplierCodeFilter { get; set; }

        public string SupplierNameFilter { get; set; }

        public string AddressFilter { get; set; }

        public string ItemNoFilter { get; set; }

        public string MaterialGroupFilter { get; set; }

        public string ShortTextFilter { get; set; }

        public double? MaxOrderQuantityFilter { get; set; }
        public double? MinOrderQuantityFilter { get; set; }

        public string OrderUnitFilter { get; set; }

        public string CurrencyFilter { get; set; }

        public DateTime? MaxDeliveryDateFilter { get; set; }
        public DateTime? MinDeliveryDateFilter { get; set; }

        public double? MaxNetPriceFilter { get; set; }
        public double? MinNetPriceFilter { get; set; }

        public double? MaxNetOrderValueFilter { get; set; }
        public double? MinNetOrderValueFilter { get; set; }

        public double? MaxDemurrageFilter { get; set; }
        public double? MinDemurrageFilter { get; set; }

        public double? MaxGrossPriceFilter { get; set; }
        public double? MinGrossPriceFilter { get; set; }

        public double? MaxTotalDiscountFilter { get; set; }
        public double? MinTotalDiscountFilter { get; set; }

        public double? MaxFreightCostFilter { get; set; }
        public double? MinFreightCostFilter { get; set; }

        public string ReleaseIndicatorFilter { get; set; }

        public string PlantFilter { get; set; }

        public string PurchasingGroupFilter { get; set; }

        public string TaxCodeFilter { get; set; }

        public string CollectiveNumberFilter { get; set; }

        public string ItemCategoryFilter { get; set; }

        public string AccountAssignmentFilter { get; set; }

        public string OutlineAgreementFilter { get; set; }

        public string RFQNoFilter { get; set; }

        public double? MaxQtyPendingFilter { get; set; }
        public double? MinQtyPendingFilter { get; set; }

        public string MaterialServiceFilter { get; set; }

        public string ApprovalStatusFilter { get; set; }

        public string POStatusFilter { get; set; }

        public string PeriodFilter { get; set; }

        public string CommentVendorFilter { get; set; }

        public string ItemTextFilter { get; set; }

        public string LongTextFilter { get; set; }

        public string OurReferenceFilter { get; set; }

        public DateTime? MaxPRFinalFirstApprovalDateFilter { get; set; }
        public DateTime? MinPRFinalFirstApprovalDateFilter { get; set; }

        public DateTime? MaxPRFinalLastApprovalDateFilter { get; set; }
        public DateTime? MinPRFinalLastApprovalDateFilter { get; set; }

        public DateTime? MaxPOFirstApprovalDateFilter { get; set; }
        public DateTime? MinPOFirstApprovalDateFilter { get; set; }

        public DateTime? MaxPOLastApprovalDateFilter { get; set; }
        public DateTime? MinPOLastApprovalDateFilter { get; set; }

        public string POApprovalNameFilter { get; set; }

        public string BuyerCodeFilter { get; set; }

        public string BuyerNameFilter { get; set; }

        public string PICDeptFilter { get; set; }

        public string PICSectFilter { get; set; }

        public string FuelAllocationFilter { get; set; }

        public string CostCenterFilter { get; set; }

        public string CostCenterDescriptionFilter { get; set; }

        public string WBSElementFilter { get; set; }

        public string AssetNoFilter { get; set; }

        public string FundCenterFilter { get; set; }

        public DateTime? MaxCreatedDateFilter { get; set; }
        public DateTime? MinCreatedDateFilter { get; set; }

        public DateTime? MaxUpdatedDateFilter { get; set; }
        public DateTime? MinUpdatedDateFilter { get; set; }

        public string DocumentIdFilter { get; set; }

    }
}