using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.Travel.Dtos
{
    public class GetAllTravelRequestsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string RequestNoFilter { get; set; }

        public int? TravelStatusFilter { get; set; }

        public int? TravelTypeFilter { get; set; }

        public DateTime? MaxRequestDateFilter { get; set; }
        public DateTime? MinRequestDateFilter { get; set; }

        public DateTime? MaxRequestPlanDateFilter { get; set; }
        public DateTime? MinRequestPlanDateFilter { get; set; }

        public string CampFilter { get; set; }

        public string TransportBusFilter { get; set; }

        public DateTime? MaxCreatedDateFilter { get; set; }
        public DateTime? MinCreatedDateFilter { get; set; }

        public string UserNameFilter { get; set; }

        public string AirportDisplayPropertyFilter { get; set; }

        public string AirportDisplayProperty2Filter { get; set; }

        public string UserName2Filter { get; set; }

    }
}