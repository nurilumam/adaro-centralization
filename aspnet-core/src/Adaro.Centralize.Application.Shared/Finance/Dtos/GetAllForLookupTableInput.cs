using Abp.Application.Services.Dto;

namespace Adaro.Centralize.Finance.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}