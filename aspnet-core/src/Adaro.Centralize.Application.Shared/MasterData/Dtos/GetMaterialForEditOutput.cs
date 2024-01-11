using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.MasterData.Dtos
{
    public class GetMaterialForEditOutput
    {
        public CreateOrEditMaterialDto Material { get; set; }

        public string MaterialGroupDisplayProperty { get; set; }

        public string UNSPSCDisplayProperty { get; set; }

        public string GeneralLedgerMappingDisplayProperty { get; set; }

        public string ImageMainFileName { get; set; }

    }
}