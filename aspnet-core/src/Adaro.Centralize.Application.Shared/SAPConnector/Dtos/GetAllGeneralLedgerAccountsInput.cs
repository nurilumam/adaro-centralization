using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class GetAllGeneralLedgerAccountsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string FundsCenterFilter { get; set; }

        public double? MaxConsumableBudgetFilter { get; set; }
        public double? MinConsumableBudgetFilter { get; set; }

        public double? MaxConsumedBudgetFilter { get; set; }
        public double? MinConsumedBudgetFilter { get; set; }

        public double? MaxAvailableAmountFilter { get; set; }
        public double? MinAvailableAmountFilter { get; set; }

        public double? MaxCurrentBudgetFilter { get; set; }
        public double? MinCurrentBudgetFilter { get; set; }

        public double? MaxCommitmentActualsFilter { get; set; }
        public double? MinCommitmentActualsFilter { get; set; }

        public string FundsCenterDescriptionFilter { get; set; }

        public string CostCenterCostCenterNameFilter { get; set; }

    }
}