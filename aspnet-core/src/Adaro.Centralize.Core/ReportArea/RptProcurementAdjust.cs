using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Adaro.Centralize.ReportArea
{
    [Table("RptProcurementAdjusts")]
    public class RptProcurementAdjust : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string PurchasingDocument { get; set; }

        public virtual bool IsContract { get; set; }

        public virtual bool IsAdjust { get; set; }

        public virtual int DayAdjust { get; set; }

        public virtual string Remark { get; set; }

    }
}