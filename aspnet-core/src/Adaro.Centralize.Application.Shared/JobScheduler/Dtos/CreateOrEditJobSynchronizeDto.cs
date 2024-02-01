using Adaro.Centralize.JobScheduler;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.JobScheduler.Dtos
{
    public class CreateOrEditJobSynchronizeDto : EntityDto<Guid?>
    {

        [Required]
        public string JobName { get; set; }

        public JobSchedulerType JobType { get; set; }

        [Required]
        public string DataSource { get; set; }

        public JobSchedulerStatus LastStatus { get; set; }

        public string Uri { get; set; }

        public DateTime LastUpdate { get; set; }

        public int TotalInsert { get; set; }

        public int TotalUpdate { get; set; }

        public int TotalDelete { get; set; }

    }
}