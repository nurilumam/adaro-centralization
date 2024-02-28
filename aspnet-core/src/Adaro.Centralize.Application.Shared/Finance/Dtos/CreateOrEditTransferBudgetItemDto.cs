using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.Finance.Dtos
{
    public class CreateOrEditTransferBudgetItemDto : EntityDto<Guid?>
    {

        [Required]
        public string PeriodFrom { get; set; }

        public decimal AmountFrom { get; set; }

        [Required]
        public string PeriodTo { get; set; }

        public decimal AmountTo { get; set; }

        public Guid? TransferBudgetId { get; set; }

        public Guid? CostCenterIdFrom { get; set; }

        public Guid? CostCenterIdTo { get; set; }

    }
}