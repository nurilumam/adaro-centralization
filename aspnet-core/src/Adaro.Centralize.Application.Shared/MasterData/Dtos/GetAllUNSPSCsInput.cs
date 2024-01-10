using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.MasterData.Dtos
{
    public class GetAllUNSPSCsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string UNSPSC_CodeFilter { get; set; }

        public string DescriptionFilter { get; set; }

        public string AccountCodeFilter { get; set; }

    }
}