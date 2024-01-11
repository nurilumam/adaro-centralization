using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.MasterData.Dtos
{
    public class CreateOrEditGeneralLedgerMappingDto : EntityDto<Guid?>
    {

        [Required]
        [StringLength(GeneralLedgerMappingConsts.MaxGLAccountLength, MinimumLength = GeneralLedgerMappingConsts.MinGLAccountLength)]
        public string GLAccount { get; set; }

        [StringLength(GeneralLedgerMappingConsts.MaxGLAccountDescriptionLength, MinimumLength = GeneralLedgerMappingConsts.MinGLAccountDescriptionLength)]
        public string GLAccountDescription { get; set; }

        [Required]
        [StringLength(GeneralLedgerMappingConsts.MaxMappingTypeLength, MinimumLength = GeneralLedgerMappingConsts.MinMappingTypeLength)]
        public string MappingType { get; set; }

        [Required]
        [StringLength(GeneralLedgerMappingConsts.MaxValuationClassLength, MinimumLength = GeneralLedgerMappingConsts.MinValuationClassLength)]
        public string ValuationClass { get; set; }

        [StringLength(GeneralLedgerMappingConsts.MaxValuationClassDescriptionLength, MinimumLength = GeneralLedgerMappingConsts.MinValuationClassDescriptionLength)]
        public string ValuationClassDescription { get; set; }

    }
}