using Abp.Application.Services.Dto;

namespace Adaro.Centralize.MasterData.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}