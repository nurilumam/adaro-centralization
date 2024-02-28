using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.Finance.Dtos
{
    public class TransferBudgetItemDto : EntityDto<Guid>
    {
        public string PeriodFrom { get; set; }

        public decimal AmountFrom { get; set; }

        public string PeriodTo { get; set; }

        public decimal AmountTo { get; set; }

        public Guid? TransferBudgetId { get; set; }

        public Guid? CostCenterIdFrom { get; set; }

        public Guid? CostCenterIdTo { get; set; }

    }
}