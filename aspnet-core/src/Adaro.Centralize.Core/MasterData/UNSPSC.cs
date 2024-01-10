using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Adaro.Centralize.MasterData
{
    [Table("UNSPSCs")]
    public class UNSPSC : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(UNSPSCConsts.MaxUNSPSC_CodeLength, MinimumLength = UNSPSCConsts.MinUNSPSC_CodeLength)]
        public virtual string UNSPSC_Code { get; set; }

        [StringLength(UNSPSCConsts.MaxDescriptionLength, MinimumLength = UNSPSCConsts.MinDescriptionLength)]
        public virtual string Description { get; set; }

        [StringLength(UNSPSCConsts.MaxAccountCodeLength, MinimumLength = UNSPSCConsts.MinAccountCodeLength)]
        public virtual string AccountCode { get; set; }

    }
}