using Adaro.Centralize.Travel.Dtos;

using Abp.Extensions;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.Airports
{
    public class CreateOrEditAirportModalViewModel
    {
        public CreateOrEditAirportDto Airport { get; set; }

        public bool IsEditMode => Airport.Id.HasValue;
    }
}