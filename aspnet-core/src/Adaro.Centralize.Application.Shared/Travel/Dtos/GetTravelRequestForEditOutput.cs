using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.Travel.Dtos
{
    public class GetTravelRequestForEditOutput
    {
        public CreateOrEditTravelRequestDto TravelRequest { get; set; }

        public string UserName { get; set; }

        public string AirportDisplayProperty { get; set; }

        public string AirportDisplayProperty2 { get; set; }

        public string UserName2 { get; set; }

    }
}