using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.Finance.Dtos
{
    public class CreateOrEditTransferBudgetDetailDto : EntityDto<Guid?>
    {

        [Required]
        [StringLength(TransferBudgetDetailConsts.MaxPeriodLength, MinimumLength = TransferBudgetDetailConsts.MinPeriodLength)]
        public string Period { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(TransferBudgetDetailConsts.MaxTransferTypeLength, MinimumLength = TransferBudgetDetailConsts.MinTransferTypeLength)]
        public string TransferType { get; set; }

        public Guid CostCenterId { get; set; }

        public Guid? GeneralLedgerMappingId { get; set; }

    }
}