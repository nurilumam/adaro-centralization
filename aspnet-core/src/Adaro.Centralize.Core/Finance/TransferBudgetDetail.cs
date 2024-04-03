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
    [Table("TransferBudgetDetails")]
    public class TransferBudgetDetail : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string Period { get; set; }

        public virtual decimal Amount { get; set; }

        [Required]
        public virtual string TransferType { get; set; }

        public virtual Guid? TransferBudgetId { get; set; }

        [ForeignKey("TransferBudgetId")]
        public TransferBudget TransferBudgetFk { get; set; }

        public virtual Guid CostCenterId { get; set; }

        [ForeignKey("CostCenterId")]
        public CostCenter CostCenterFk { get; set; }

        public virtual Guid? GeneralLedgerAccountId { get; set; }

        [ForeignKey("GeneralLedgerAccountId")]
        public GeneralLedgerAccount GeneralLedgerAccountFk { get; set; }

    }
}