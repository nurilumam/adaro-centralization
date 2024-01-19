using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.MasterDataRequest.Dtos
{
    public class GetAllMaterialRequestsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string RequestNoFilter { get; set; }

        public int? RequestStatusFilter { get; set; }

        public string MaterialNameFilter { get; set; }

        public string DescriptionFilter { get; set; }

        public string PurposeFilter { get; set; }

        public string MaterialTypeFilter { get; set; }

        public string CategoryFilter { get; set; }

        public string SizeFilter { get; set; }

        public string UoMFilter { get; set; }

        public string PackageSizeFilter { get; set; }

        public string GeneralLedgerFilter { get; set; }

        public string BrandFilter { get; set; }

        public string WeightFilter { get; set; }

        public string ImageColletionFilter { get; set; }

        public string UNSPSCDisplayPropertyFilter { get; set; }

        public string GeneralLedgerMappingDisplayPropertyFilter { get; set; }

    }
}