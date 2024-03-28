using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Adaro.Centralize.Finance.Dtos
{
    public class CreateOrEditTransferBudgetDto : EntityDto<Guid?>
    {

        [Required]
        public string DocumentNo { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Division { get; set; }

        [Required]
        public string Purpose { get; set; }

        public string ProjectActivities { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public string ScopeofWork { get; set; }

        [Required]
        public string Location { get; set; }

        public List<CreateOrEditTransferBudgetItemDto> TransferBudgetItemFromDtos { get; set; }
        public List<CreateOrEditTransferBudgetItemDto> TransferBudgetItemToDtos { get; set; }

    }
}