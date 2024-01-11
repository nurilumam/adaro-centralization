using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.MasterData.Dtos
{
    public class GeneralLedgerMappingDto : EntityDto<Guid>
    {
        public string GLAccount { get; set; }

        public string GLAccountDescription { get; set; }

        public string MappingType { get; set; }

        public string ValuationClass { get; set; }

        public string ValuationClassDescription { get; set; }

    }
}