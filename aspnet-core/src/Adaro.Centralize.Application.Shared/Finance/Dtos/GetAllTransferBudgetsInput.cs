using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.Finance.Dtos
{
    public class GetAllTransferBudgetsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string DocumentNoFilter { get; set; }

        public string DepartmentFilter { get; set; }

        public string DivisionFilter { get; set; }

        public string PurposeFilter { get; set; }

        public string ProjectActivitiesFilter { get; set; }

        public string ReasonFilter { get; set; }

        public string ScopeofWorkFilter { get; set; }

        public string LocationFilter { get; set; }

    }
}