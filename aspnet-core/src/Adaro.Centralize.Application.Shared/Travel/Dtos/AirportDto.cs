using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.Travel.Dtos
{
    public class AirportDto : EntityDto<Guid>
    {
        public string AirportName { get; set; }

        public string IATA { get; set; }

        public string City { get; set; }

        public string Category { get; set; }

    }
}