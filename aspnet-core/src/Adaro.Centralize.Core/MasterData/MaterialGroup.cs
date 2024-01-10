using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Adaro.Centralize.MasterData
{
    [Table("MaterialGroups")]
    public class MaterialGroup : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        [StringLength(MaterialGroupConsts.MaxMaterialGroupCodeLength, MinimumLength = MaterialGroupConsts.MinMaterialGroupCodeLength)]
        public virtual string MaterialGroupCode { get; set; }

        [Required]
        [StringLength(MaterialGroupConsts.MaxMaterialGroupNameLength, MinimumLength = MaterialGroupConsts.MinMaterialGroupNameLength)]
        public virtual string MaterialGroupName { get; set; }

        [Required]
        [StringLength(MaterialGroupConsts.MaxMaterialGroupDescriptionLength, MinimumLength = MaterialGroupConsts.MinMaterialGroupDescriptionLength)]
        public virtual string MaterialGroupDescription { get; set; }

        [Required]
        [StringLength(MaterialGroupConsts.MaxLanguageLength, MinimumLength = MaterialGroupConsts.MinLanguageLength)]
        public virtual string Language { get; set; }

    }
}