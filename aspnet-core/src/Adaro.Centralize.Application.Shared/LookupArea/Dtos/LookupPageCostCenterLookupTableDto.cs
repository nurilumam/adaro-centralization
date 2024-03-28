using Abp.Application.Services.Dto;

namespace Adaro.Centralize.LookupArea.Dtos
{
    public class LookupPageCostCenterLookupTableDto
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }
        public string CostCenterCode { get; set; }
        public string CostCenterName { get; set; }
        public string DepartmentName { get; set; }
    }
}