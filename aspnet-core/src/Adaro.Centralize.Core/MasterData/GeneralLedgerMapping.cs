using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Adaro.Centralize.MasterData
{
    [Table("GeneralLedgerMappings")]
    public class GeneralLedgerMapping : CreationAuditedEntity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        [StringLength(GeneralLedgerMappingConsts.MaxGLAccountLength, MinimumLength = GeneralLedgerMappingConsts.MinGLAccountLength)]
        public virtual string GLAccount { get; set; }

        [StringLength(GeneralLedgerMappingConsts.MaxGLAccountDescriptionLength, MinimumLength = GeneralLedgerMappingConsts.MinGLAccountDescriptionLength)]
        public virtual string GLAccountDescription { get; set; }

        [Required]
        [StringLength(GeneralLedgerMappingConsts.MaxMappingTypeLength, MinimumLength = GeneralLedgerMappingConsts.MinMappingTypeLength)]
        public virtual string MappingType { get; set; }

        [Required]
        [StringLength(GeneralLedgerMappingConsts.MaxValuationClassLength, MinimumLength = GeneralLedgerMappingConsts.MinValuationClassLength)]
        public virtual string ValuationClass { get; set; }

        [StringLength(GeneralLedgerMappingConsts.MaxValuationClassDescriptionLength, MinimumLength = GeneralLedgerMappingConsts.MinValuationClassDescriptionLength)]
        public virtual string ValuationClassDescription { get; set; }

    }
}