using Adaro.Centralize.SAPConnector;
using Adaro.Centralize.MasterData;
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
        [StringLength(TransferBudgetDetailConsts.MaxPeriodLength, MinimumLength = TransferBudgetDetailConsts.MinPeriodLength)]
        public virtual string Period { get; set; }

        [Required]
        public virtual decimal Amount { get; set; }

        [Required]
        [StringLength(TransferBudgetDetailConsts.MaxTransferTypeLength, MinimumLength = TransferBudgetDetailConsts.MinTransferTypeLength)]
        public virtual string TransferType { get; set; }

        public virtual Guid CostCenterId { get; set; }

        [ForeignKey("CostCenterId")]
        public CostCenter CostCenterFk { get; set; }

        public virtual Guid? GeneralLedgerMappingId { get; set; }

        [ForeignKey("GeneralLedgerMappingId")]
        public GeneralLedgerMapping GeneralLedgerMappingFk { get; set; }

    }
}