using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.MasterData.Dtos
{
    public class GetAllMaterialGroupsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string MaterialGroupCodeFilter { get; set; }

        public string MaterialGroupNameFilter { get; set; }

        public string MaterialGroupDescriptionFilter { get; set; }

        public string LanguageFilter { get; set; }

    }
}