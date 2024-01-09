using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.Travel.Dtos
{
    public class GetAirportForEditOutput
    {
        public CreateOrEditAirportDto Airport { get; set; }

    }
}