using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.MasterData.Dtos
{
    public class GetAllGeneralLedgerMappingsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string GLAccountFilter { get; set; }

        public string GLAccountDescriptionFilter { get; set; }

        public string MappingTypeFilter { get; set; }

        public string ValuationClassFilter { get; set; }

        public string ValuationClassDescriptionFilter { get; set; }

    }
}