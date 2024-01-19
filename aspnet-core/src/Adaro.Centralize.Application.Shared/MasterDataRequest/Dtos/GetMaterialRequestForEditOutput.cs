using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.MasterDataRequest.Dtos
{
    public class GetMaterialRequestForEditOutput
    {
        public CreateOrEditMaterialRequestDto MaterialRequest { get; set; }

        public string UNSPSCDisplayProperty { get; set; }

        public string GeneralLedgerMappingDisplayProperty { get; set; }

        public string PictureFileName { get; set; }

    }
}