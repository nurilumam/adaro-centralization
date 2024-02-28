using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Adaro.Centralize.Finance
{
    [Table("TransferBudgets")]
    public class TransferBudget : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string DocumentNo { get; set; }

        [Required]
        public virtual string Department { get; set; }

        [Required]
        public virtual string Division { get; set; }

        [Required]
        public virtual string Purpose { get; set; }

        public virtual string ProjectActivities { get; set; }

        [Required]
        public virtual string Reason { get; set; }

        [Required]
        public virtual string ScopeofWork { get; set; }

        [Required]
        public virtual string Location { get; set; }

    }
}