using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.Finance.Dtos
{
    public class GetAllTransferBudgetDetailsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string PeriodFilter { get; set; }

        public decimal? MaxAmountFilter { get; set; }
        public decimal? MinAmountFilter { get; set; }

        public string TransferTypeFilter { get; set; }

        public string CostCenterCostCenterNameFilter { get; set; }

        public string GeneralLedgerMappingGLAccountFilter { get; set; }

    }
}