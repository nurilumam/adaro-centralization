using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.Finance.Dtos
{
    public class TransferBudgetDto : EntityDto<Guid>
    {
        public string DocumentNo { get; set; }

        public string Department { get; set; }

        public string Division { get; set; }

        public string Reason { get; set; }

        public string Location { get; set; }

    }
}