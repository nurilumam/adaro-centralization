using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class DataProductionDto : EntityDto<Guid>
    {
        public string MaterialDocument { get; set; }

        public int MaterialDocYear { get; set; }

        public int MaterialDocItem { get; set; }

        public string Order { get; set; }

        public int Reservation { get; set; }

        public string PurchaseOrder { get; set; }

        public string MovementType { get; set; }

        public string MovementTypeText { get; set; }

        public string Plant { get; set; }

        public string StorageLocation { get; set; }

        public string Material { get; set; }

        public string MaterialDescription { get; set; }

        public decimal Quantity { get; set; }

        public decimal QtyInOrderUnit { get; set; }

        public DateTime PostingDate { get; set; }

        public DateTime EntryDate { get; set; }

        public DateTime DocumentDate { get; set; }

        public string Batch { get; set; }

    }
}