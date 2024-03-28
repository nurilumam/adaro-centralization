using Abp.Application.Services.Dto;

namespace Adaro.Centralize.Finance.Dtos
{
    public class TransferBudgetItemCostCenterLookupTableDto
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public string CostCenterCode { get; set; }

        public string CostCenterName { get; set; }

        public string DepartmentName { get; set; }
    }
}