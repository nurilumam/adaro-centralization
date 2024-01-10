using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.MasterData.Dtos
{
    public class UNSPSCDto : EntityDto<Guid>
    {
        public string UNSPSC_Code { get; set; }

        public string Description { get; set; }

        public string AccountCode { get; set; }

    }
}