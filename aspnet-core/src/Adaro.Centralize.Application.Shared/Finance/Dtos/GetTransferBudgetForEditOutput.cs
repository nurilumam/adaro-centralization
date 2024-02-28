using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.Finance.Dtos
{
    public class GetTransferBudgetForEditOutput
    {
        public CreateOrEditTransferBudgetDto TransferBudget { get; set; }

    }
}