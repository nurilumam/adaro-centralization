using Adaro.Centralize.Finance;
using Adaro.Centralize.SAPConnector;
using Adaro.Centralize.SAPConnector;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Adaro.Centralize.Finance
{
    [Table("TransferBudgetItems")]
    public class TransferBudgetItem : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string PeriodFrom { get; set; }

        public virtual decimal AmountFrom { get; set; }

        [Required]
        public virtual string PeriodTo { get; set; }

        public virtual decimal AmountTo { get; set; }

        public virtual Guid? TransferBudgetId { get; set; }

        [ForeignKey("TransferBudgetId")]
        public TransferBudget TransferBudgetFk { get; set; }

        public virtual Guid? CostCenterIdFrom { get; set; }

        [ForeignKey("CostCenterIdFrom")]
        public CostCenter CostCenterIdFromFk { get; set; }

        public virtual Guid? CostCenterIdTo { get; set; }

        [ForeignKey("CostCenterIdTo")]
        public CostCenter CostCenterIdToFk { get; set; }

    }
}