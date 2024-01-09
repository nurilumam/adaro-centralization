using Abp.Application.Services.Dto;

namespace Adaro.Centralize.Travel.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}