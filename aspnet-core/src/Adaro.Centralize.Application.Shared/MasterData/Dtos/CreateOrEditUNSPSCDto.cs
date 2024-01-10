using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.MasterData.Dtos
{
    public class CreateOrEditUNSPSCDto : EntityDto<Guid?>
    {

        [StringLength(UNSPSCConsts.MaxUNSPSC_CodeLength, MinimumLength = UNSPSCConsts.MinUNSPSC_CodeLength)]
        public string UNSPSC_Code { get; set; }

        [StringLength(UNSPSCConsts.MaxDescriptionLength, MinimumLength = UNSPSCConsts.MinDescriptionLength)]
        public string Description { get; set; }

        [StringLength(UNSPSCConsts.MaxAccountCodeLength, MinimumLength = UNSPSCConsts.MinAccountCodeLength)]
        public string AccountCode { get; set; }

    }
}