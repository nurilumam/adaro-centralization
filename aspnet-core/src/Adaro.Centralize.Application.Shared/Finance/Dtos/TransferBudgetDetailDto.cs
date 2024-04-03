using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.Finance.Dtos
{
    public class TransferBudgetDetailDto : EntityDto<Guid>
    {
        public string Period { get; set; }

        public decimal Amount { get; set; }

        public string TransferType { get; set; }

        public Guid TransferBudgetId { get; set; }

        public Guid CostCenterId { get; set; }
        public string CostCenterCode { get; set; }
        public string CostCenterName { get; set; }
        public string DepartmentName { get; set; }

        public Guid? GeneralLedgerAccountId { get; set; }
        public string FundsCenter { get; set; }
        public string FundsCenterDescription { get; set; }

    }
}