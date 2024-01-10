using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.MasterData.Dtos
{
    public class MaterialGroupDto : EntityDto<Guid>
    {
        public string MaterialGroupCode { get; set; }

        public string MaterialGroupName { get; set; }

        public string MaterialGroupDescription { get; set; }

        public string Language { get; set; }

    }
}