using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.Travel.Dtos
{
    public class GetAllAirportsForExcelInput
    {
        public string Filter { get; set; }

        public string AirportNameFilter { get; set; }

        public string IATAFilter { get; set; }

        public string CityFilter { get; set; }

        public string CategoryFilter { get; set; }

    }
}