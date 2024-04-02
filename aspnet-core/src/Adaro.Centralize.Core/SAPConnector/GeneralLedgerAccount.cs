using Adaro.Centralize.SAPConnector;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Adaro.Centralize.SAPConnector
{
    [Table("GeneralLedgerAccounts")]
    [Audited]
    public class GeneralLedgerAccount : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(GeneralLedgerAccountConsts.MaxFundsCenterLength, MinimumLength = GeneralLedgerAccountConsts.MinFundsCenterLength)]
        public virtual string FundsCenter { get; set; }

        public virtual double? ConsumableBudget { get; set; }

        public virtual double? ConsumedBudget { get; set; }

        public virtual double? AvailableAmount { get; set; }

        public virtual double? CurrentBudget { get; set; }

        public virtual double? CommitmentActuals { get; set; }

        public virtual string FundsCenterDescription { get; set; }

        public virtual Guid? CostCenterId { get; set; }

        [ForeignKey("CostCenterId")]
        public CostCenter CostCenterFk { get; set; }

        [Required]
        public virtual DateTime CreatedDate { get; set; }

        [Required]
        public virtual DateTime UpdatedDate { get; set; }

    }
}