using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class GetAllDataProductionsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string IntfIdFilter { get; set; }

        public string IntfSiteFilter { get; set; }

        public string IntfSessionFilter { get; set; }

        public string ObjectIdFilter { get; set; }

        public string MessageTypeFilter { get; set; }

        public string MaterialDocumentFilter { get; set; }

        public int? MaxMaterialDocYearFilter { get; set; }
        public int? MinMaterialDocYearFilter { get; set; }

        public int? MaxMaterialDocItemFilter { get; set; }
        public int? MinMaterialDocItemFilter { get; set; }

        public string OrderFilter { get; set; }

        public int? MaxReservationFilter { get; set; }
        public int? MinReservationFilter { get; set; }

        public string PurchaseOrderFilter { get; set; }

        public int? MaxPurchaseOrderItemFilter { get; set; }
        public int? MinPurchaseOrderItemFilter { get; set; }

        public string MovementTypeFilter { get; set; }

        public string MovementTypeTextFilter { get; set; }

        public string PlantFilter { get; set; }

        public string StorageLocationFilter { get; set; }

        public string MaterialFilter { get; set; }

        public string MaterialDescriptionFilter { get; set; }

        public string VendorFilter { get; set; }

        public decimal? MaxQuantityFilter { get; set; }
        public decimal? MinQuantityFilter { get; set; }

        public decimal? MaxQtyInOrderUnitFilter { get; set; }
        public decimal? MinQtyInOrderUnitFilter { get; set; }

        public string UnitOfEntryFilter { get; set; }

        public DateTime? MaxPostingDateFilter { get; set; }
        public DateTime? MinPostingDateFilter { get; set; }

        public DateTime? MaxEntryDateFilter { get; set; }
        public DateTime? MinEntryDateFilter { get; set; }

        public string TimeOfEntryFilter { get; set; }

        public string UserNameFilter { get; set; }

        public string DocumentHeaderTextFilter { get; set; }

        public DateTime? MaxDocumentDateFilter { get; set; }
        public DateTime? MinDocumentDateFilter { get; set; }

        public string BatchFilter { get; set; }

        public string CostCenterFilter { get; set; }

        public string ReferenceFilter { get; set; }

        public DateTime? MaxInterfaceCreatedDateFilter { get; set; }
        public DateTime? MinInterfaceCreatedDateFilter { get; set; }

        public string InterfaceCreatedByFilter { get; set; }

    }
}