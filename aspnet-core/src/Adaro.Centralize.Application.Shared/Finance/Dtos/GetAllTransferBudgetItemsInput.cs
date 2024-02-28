using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.Finance.Dtos
{
    public class GetAllTransferBudgetItemsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string PeriodFromFilter { get; set; }

        public decimal? MaxAmountFromFilter { get; set; }
        public decimal? MinAmountFromFilter { get; set; }

        public string PeriodToFilter { get; set; }

        public decimal? MaxAmountToFilter { get; set; }
        public decimal? MinAmountToFilter { get; set; }

        public string TransferBudgetDisplayPropertyFilter { get; set; }

        public string CostCenterDisplayPropertyFilter { get; set; }

        public string CostCenterDisplayProperty2Filter { get; set; }

    }
}