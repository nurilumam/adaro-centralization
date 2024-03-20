using Abp.Application.Services.Dto;

namespace Adaro.Centralize.ReportArea.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}