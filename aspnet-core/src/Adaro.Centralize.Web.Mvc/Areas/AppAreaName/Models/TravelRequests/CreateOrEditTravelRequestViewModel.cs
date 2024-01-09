using Adaro.Centralize.Travel.Dtos;

using Abp.Extensions;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.TravelRequests
{
    public class CreateOrEditTravelRequestViewModel
    {
        public CreateOrEditTravelRequestDto TravelRequest { get; set; }

        public string UserName { get; set; }

        public string AirportDisplayProperty { get; set; }

        public string AirportDisplayProperty2 { get; set; }

        public string UserName2 { get; set; }

        public bool IsEditMode => TravelRequest.Id.HasValue;
    }
}