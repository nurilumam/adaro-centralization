using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class CreateOrEditDataProductionDto : EntityDto<Guid?>
    {

        public string IntfId { get; set; }

        public string IntfSite { get; set; }

        public string IntfSession { get; set; }

        public string ObjectId { get; set; }

        public string MessageType { get; set; }

        public string MaterialDocument { get; set; }

        public int MaterialDocYear { get; set; }

        public int MaterialDocItem { get; set; }

        public string Order { get; set; }

        public int Reservation { get; set; }

        public string PurchaseOrder { get; set; }

        public int PurchaseOrderItem { get; set; }

        public string MovementType { get; set; }

        public string MovementTypeText { get; set; }

        public string Plant { get; set; }

        public string StorageLocation { get; set; }

        public string Material { get; set; }

        public string MaterialDescription { get; set; }

        public string Vendor { get; set; }

        public decimal Quantity { get; set; }

        public decimal QtyInOrderUnit { get; set; }

        public string UnitOfEntry { get; set; }

        public DateTime PostingDate { get; set; }

        public DateTime EntryDate { get; set; }

        public string TimeOfEntry { get; set; }

        public string UserName { get; set; }

        public string DocumentHeaderText { get; set; }

        public DateTime DocumentDate { get; set; }

        public string Batch { get; set; }

        public string CostCenter { get; set; }

        public string Reference { get; set; }

        public DateTime InterfaceCreatedDate { get; set; }

        public string InterfaceCreatedBy { get; set; }

    }
}