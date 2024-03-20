using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Adaro.Centralize.SAPConnector
{
    [Table("CostCenters")]
    public class CostCenter : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string ControllingArea { get; set; }

        [Required]
        public virtual string CostCenterName { get; set; }

        public virtual string Description { get; set; }

        public virtual string ActState { get; set; }

        public virtual bool IsActive { get; set; }

        [Required]
        public virtual string CostCenterCode { get; set; }

        public virtual string CostCenterShort { get; set; }

        public virtual string DepartmentName { get; set; }

        [Required]
        public virtual string Period { get; set; }

    }
}