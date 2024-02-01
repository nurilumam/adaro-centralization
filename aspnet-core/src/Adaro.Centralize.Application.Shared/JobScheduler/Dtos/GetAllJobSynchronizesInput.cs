using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.JobScheduler.Dtos
{
    public class GetAllJobSynchronizesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string JobNameFilter { get; set; }

        public int? JobTypeFilter { get; set; }

        public string DataSourceFilter { get; set; }

        public int? LastStatusFilter { get; set; }

        public string UriFilter { get; set; }

        public DateTime? MaxLastUpdateFilter { get; set; }
        public DateTime? MinLastUpdateFilter { get; set; }

        public int? MaxTotalInsertFilter { get; set; }
        public int? MinTotalInsertFilter { get; set; }

        public int? MaxTotalUpdateFilter { get; set; }
        public int? MinTotalUpdateFilter { get; set; }

        public int? MaxTotalDeleteFilter { get; set; }
        public int? MinTotalDeleteFilter { get; set; }

    }
}