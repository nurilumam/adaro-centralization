using Adaro.Centralize.JobScheduler;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Adaro.Centralize.JobScheduler
{
    [Table("JobSynchronizes")]
    public class JobSynchronize : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string JobName { get; set; }

        public virtual JobSchedulerType JobType { get; set; }

        [Required]
        public virtual string DataSource { get; set; }

        public virtual JobSchedulerStatus LastStatus { get; set; }

        public virtual string Uri { get; set; }

        public virtual DateTime LastUpdate { get; set; }

        public virtual int TotalInsert { get; set; }

        public virtual int TotalUpdate { get; set; }

        public virtual int TotalDelete { get; set; }

    }
}