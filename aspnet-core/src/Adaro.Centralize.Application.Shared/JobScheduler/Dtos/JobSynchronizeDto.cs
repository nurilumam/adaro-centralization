using Adaro.Centralize.JobScheduler;

using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.JobScheduler.Dtos
{
    public class JobSynchronizeDto : EntityDto<Guid>
    {
        public string JobName { get; set; }

        public JobSchedulerType JobType { get; set; }

        public string DataSource { get; set; }

        public JobSchedulerStatus LastStatus { get; set; }

        public DateTime LastUpdate { get; set; }

    }
}