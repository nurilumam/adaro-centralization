using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.Finance.Dtos
{
    public class GetTransferBudgetItemForEditOutput
    {
        public CreateOrEditTransferBudgetItemDto TransferBudgetItem { get; set; }

        public string TransferBudgetDisplayProperty { get; set; }

        public string CostCenterDisplayProperty { get; set; }

        public string CostCenterDisplayProperty2 { get; set; }

    }
}