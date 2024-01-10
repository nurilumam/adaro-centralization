using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.MasterData.Dtos
{
    public class CreateOrEditMaterialGroupDto : EntityDto<Guid?>
    {

        [Required]
        [StringLength(MaterialGroupConsts.MaxMaterialGroupCodeLength, MinimumLength = MaterialGroupConsts.MinMaterialGroupCodeLength)]
        public string MaterialGroupCode { get; set; }

        [Required]
        [StringLength(MaterialGroupConsts.MaxMaterialGroupNameLength, MinimumLength = MaterialGroupConsts.MinMaterialGroupNameLength)]
        public string MaterialGroupName { get; set; }

        [Required]
        [StringLength(MaterialGroupConsts.MaxMaterialGroupDescriptionLength, MinimumLength = MaterialGroupConsts.MinMaterialGroupDescriptionLength)]
        public string MaterialGroupDescription { get; set; }

        [Required]
        [StringLength(MaterialGroupConsts.MaxLanguageLength, MinimumLength = MaterialGroupConsts.MinLanguageLength)]
        public string Language { get; set; }

    }
}