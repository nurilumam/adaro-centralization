using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.Finance.Dtos
{
    public class CreateOrEditTransferBudgetDetailDto : EntityDto<Guid?>
    {

        [Required]
        public string Period { get; set; }

        public decimal Amount { get; set; }

        [Required]
        public string TransferType { get; set; }

        public Guid CostCenterId { get; set; }

        public Guid? GeneralLedgerAccountId { get; set; }

    }
}