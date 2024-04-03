using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using Adaro.Centralize.SAPConnector.Dtos;

namespace Adaro.Centralize.Finance.Dtos
{
    public class CreateOrEditTransferBudgetDetailDto : EntityDto<Guid?>
    {

        [Required]
        public string Period { get; set; }

        public decimal Amount { get; set; }

        [Required]
        public string TransferType { get; set; }

        public Guid? TransferBudgetId { get; set; }

        public Guid CostCenterId { get; set; }
        public string CostCenterCode { get; set; }
        public string CostCenterName { get; set; }
        public string DepartmentName { get; set; }

        public Guid? GeneralLedgerAccountId { get; set; }
        public string FundsCenter { get; set; }
        public string FundsCenterDescription { get; set; }
    }
}