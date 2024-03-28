using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.Finance.Dtos
{
    public class CreateOrEditTransferBudgetItemDto : EntityDto<Guid?>
    {
        public Guid TransferBudgetId { get; set; }
        public Guid CostCenterId { get; set; }

        [Required]
        public string Period { get; set; }

        public decimal Amount { get; set; }


        [Required]
        public string CostCenterCode { get; set; }
        public string CostCenterName { get; set; }
        public virtual string DepartmentName { get; set; }
    }
}