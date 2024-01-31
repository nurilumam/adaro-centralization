using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Adaro.Centralize.SAPConnector
{
    [Table("DataProductions")]
    public class DataProduction : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public virtual string IntfId { get; set; }

        public virtual string IntfSite { get; set; }

        public virtual string IntfSession { get; set; }

        public virtual string ObjectId { get; set; }

        public virtual string MessageType { get; set; }

        public virtual string MaterialDocument { get; set; }

        public virtual int MaterialDocYear { get; set; }

        public virtual int MaterialDocItem { get; set; }

        public virtual string Order { get; set; }

        public virtual int Reservation { get; set; }

        public virtual string PurchaseOrder { get; set; }

        public virtual int PurchaseOrderItem { get; set; }

        public virtual string MovementType { get; set; }

        public virtual string MovementTypeText { get; set; }

        public virtual string Plant { get; set; }

        public virtual string StorageLocation { get; set; }

        public virtual string Material { get; set; }

        public virtual string MaterialDescription { get; set; }

        public virtual string Vendor { get; set; }

        public virtual decimal Quantity { get; set; }

        public virtual decimal QtyInOrderUnit { get; set; }

        public virtual string UnitOfEntry { get; set; }

        public virtual DateTime PostingDate { get; set; }

        public virtual DateTime EntryDate { get; set; }

        public virtual string TimeOfEntry { get; set; }

        public virtual string UserName { get; set; }

        public virtual string DocumentHeaderText { get; set; }

        public virtual DateTime DocumentDate { get; set; }

        public virtual string Batch { get; set; }

        public virtual string CostCenter { get; set; }

        public virtual string Reference { get; set; }

        public virtual DateTime InterfaceCreatedDate { get; set; }

        public virtual string InterfaceCreatedBy { get; set; }

    }
}