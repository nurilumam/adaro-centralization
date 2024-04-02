using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.Finance.Dtos
{
    public class GetTransferBudgetDetailForEditOutput
    {
        public CreateOrEditTransferBudgetDetailDto TransferBudgetDetail { get; set; }

        public string CostCenterCostCenterName { get; set; }

        public string GeneralLedgerMappingGLAccount { get; set; }

    }
}