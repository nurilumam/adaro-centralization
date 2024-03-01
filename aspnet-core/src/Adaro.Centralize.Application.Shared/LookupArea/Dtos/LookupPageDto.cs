using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.LookupArea.Dtos
{
    public class LookupPageDto : EntityDto<Guid>
    {
        public string LookupName { get; set; }

        public Guid? CostCenterId { get; set; }

    }
}