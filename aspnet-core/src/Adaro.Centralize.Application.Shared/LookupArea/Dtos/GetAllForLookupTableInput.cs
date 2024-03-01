using Abp.Application.Services.Dto;

namespace Adaro.Centralize.LookupArea.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}