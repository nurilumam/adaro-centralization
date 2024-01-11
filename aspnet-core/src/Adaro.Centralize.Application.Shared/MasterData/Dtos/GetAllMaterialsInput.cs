using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.MasterData.Dtos
{
    public class GetAllMaterialsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string MaterialNoFilter { get; set; }

        public string MaterialNameFilter { get; set; }

        public string MaterialNameSAPFilter { get; set; }

        public string DescriptionFilter { get; set; }

        public string SizeFilter { get; set; }

        public string UoMFilter { get; set; }

        public string BrandFilter { get; set; }

        public string MaterialGroupDisplayPropertyFilter { get; set; }

        public string UNSPSCDisplayPropertyFilter { get; set; }

        public string GeneralLedgerMappingDisplayPropertyFilter { get; set; }

    }
}