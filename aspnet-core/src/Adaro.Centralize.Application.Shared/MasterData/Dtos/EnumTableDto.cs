using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.MasterData.Dtos
{
    public class EnumTableDto : EntityDto<Guid>
    {
        public string EnumCode { get; set; }

        public string EnumValue { get; set; }

        public string EnumLabel { get; set; }

    }
}