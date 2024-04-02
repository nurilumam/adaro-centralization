using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.Finance.Dtos
{
    public class TransferBudgetDetailDto : EntityDto<Guid>
    {
        public string Period { get; set; }

        public decimal Amount { get; set; }

        public string TransferType { get; set; }

        public Guid CostCenterId { get; set; }

        public Guid? GeneralLedgerMappingId { get; set; }

    }
}