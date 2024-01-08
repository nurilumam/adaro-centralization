using Abp.Application.Services.Dto;

namespace Adaro.Centralize.Notifications.Dto
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}