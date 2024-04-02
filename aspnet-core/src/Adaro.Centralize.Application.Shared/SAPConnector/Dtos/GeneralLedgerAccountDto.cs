using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class GeneralLedgerAccountDto : EntityDto<Guid>
    {
        public string FundsCenter { get; set; }

        public double? ConsumableBudget { get; set; }

        public double? ConsumedBudget { get; set; }

        public double? AvailableAmount { get; set; }

        public double? CurrentBudget { get; set; }

        public double? CommitmentActuals { get; set; }

        public string FundsCenterDescription { get; set; }

        public Guid? CostCenterId { get; set; }

    }
}