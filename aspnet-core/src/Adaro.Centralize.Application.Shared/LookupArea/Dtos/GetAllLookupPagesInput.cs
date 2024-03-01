using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.LookupArea.Dtos
{
    public class GetAllLookupPagesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string LookupNameFilter { get; set; }

        public string CostCenterDisplayPropertyFilter { get; set; }

    }
}