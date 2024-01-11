using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.MasterData.Dtos
{
    public class MaterialDto : EntityDto<Guid>
    {
        public string MaterialNo { get; set; }

        public string MaterialName { get; set; }

        public string Description { get; set; }

        public string UoM { get; set; }

        public Guid? ImageMain { get; set; }

        public string ImageMainFileName { get; set; }

        public Guid? MaterialGroupId { get; set; }

        public Guid? UNSPSCId { get; set; }

        public Guid? GeneralLedgerMappingId { get; set; }

    }
}