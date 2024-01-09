using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.Travel.Dtos
{
    public class CreateOrEditAirportDto : EntityDto<Guid?>
    {

        [Required]
        [StringLength(AirportConsts.MaxAirportNameLength, MinimumLength = AirportConsts.MinAirportNameLength)]
        public string AirportName { get; set; }

        [Required]
        [StringLength(AirportConsts.MaxIATALength, MinimumLength = AirportConsts.MinIATALength)]
        public string IATA { get; set; }

        [StringLength(AirportConsts.MaxCityLength, MinimumLength = AirportConsts.MinCityLength)]
        public string City { get; set; }

        [StringLength(AirportConsts.MaxCategoryLength, MinimumLength = AirportConsts.MinCategoryLength)]
        public string Category { get; set; }

    }
}