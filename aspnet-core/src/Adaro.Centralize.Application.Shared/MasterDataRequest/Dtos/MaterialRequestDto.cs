using Adaro.Centralize.MasterDataRequest;

using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.MasterDataRequest.Dtos
{
    public class MaterialRequestDto : EntityDto<Guid>
    {
        public string RequestNo { get; set; }

        public MaterialRequestStatus RequestStatus { get; set; }

        public string MaterialName { get; set; }

        public string Description { get; set; }

        public string GeneralLedger { get; set; }

        public Guid? Picture { get; set; }

        public string PictureFileName { get; set; }

        public Guid? UNSPSCId { get; set; }

        public Guid? ValuationClassId { get; set; }

    }
}