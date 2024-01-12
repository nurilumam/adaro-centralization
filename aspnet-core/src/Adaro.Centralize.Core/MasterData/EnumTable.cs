using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Adaro.Centralize.MasterData
{
    [Table("EnumTables")]
    public class EnumTable : CreationAuditedEntity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        [StringLength(EnumTableConsts.MaxEnumCodeLength, MinimumLength = EnumTableConsts.MinEnumCodeLength)]
        public virtual string EnumCode { get; set; }

        [Required]
        [StringLength(EnumTableConsts.MaxEnumValueLength, MinimumLength = EnumTableConsts.MinEnumValueLength)]
        public virtual string EnumValue { get; set; }

        [Required]
        [StringLength(EnumTableConsts.MaxEnumLabelLength, MinimumLength = EnumTableConsts.MinEnumLabelLength)]
        public virtual string EnumLabel { get; set; }

    }
}