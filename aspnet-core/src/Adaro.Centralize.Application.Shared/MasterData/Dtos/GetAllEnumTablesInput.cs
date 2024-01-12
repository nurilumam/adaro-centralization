using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.MasterData.Dtos
{
    public class GetAllEnumTablesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string EnumCodeFilter { get; set; }

        public string EnumValueFilter { get; set; }

        public string EnumLabelFilter { get; set; }

    }
}