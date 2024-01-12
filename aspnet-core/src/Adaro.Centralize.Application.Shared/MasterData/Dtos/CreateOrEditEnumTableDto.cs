using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.MasterData.Dtos
{
    public class CreateOrEditEnumTableDto : EntityDto<Guid?>
    {

        [Required]
        [StringLength(EnumTableConsts.MaxEnumCodeLength, MinimumLength = EnumTableConsts.MinEnumCodeLength)]
        public string EnumCode { get; set; }

        [Required]
        [StringLength(EnumTableConsts.MaxEnumValueLength, MinimumLength = EnumTableConsts.MinEnumValueLength)]
        public string EnumValue { get; set; }

        [Required]
        [StringLength(EnumTableConsts.MaxEnumLabelLength, MinimumLength = EnumTableConsts.MinEnumLabelLength)]
        public string EnumLabel { get; set; }

    }
}