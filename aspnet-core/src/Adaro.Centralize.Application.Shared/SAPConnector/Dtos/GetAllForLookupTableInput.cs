using Abp.Application.Services.Dto;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}