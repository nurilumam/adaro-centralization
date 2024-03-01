using Adaro.Centralize.SAPConnector;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Adaro.Centralize.LookupArea
{
    [Table("LookupPages")]
    public class LookupPage : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string LookupName { get; set; }

        public virtual Guid? CostCenterId { get; set; }

        [ForeignKey("CostCenterId")]
        public CostCenter CostCenterFk { get; set; }

    }
}