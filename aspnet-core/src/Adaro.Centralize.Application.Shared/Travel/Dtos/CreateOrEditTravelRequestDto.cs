using Adaro.Centralize.Travel;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.Travel.Dtos
{
    public class CreateOrEditTravelRequestDto : EntityDto<Guid?>
    {

        [Required]
        public string RequestNo { get; set; }

        public TravelStatus TravelStatus { get; set; }

        public TravelType TravelType { get; set; }

        public DateTime RequestDate { get; set; }

        public DateTime RequestPlanDate { get; set; }

        public string Camp { get; set; }

        public string TransportBus { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? UserTravel { get; set; }

        public Guid? AirportFrom { get; set; }

        public Guid? AirportTo { get; set; }

        public long? CreatedBy { get; set; }

    }
}