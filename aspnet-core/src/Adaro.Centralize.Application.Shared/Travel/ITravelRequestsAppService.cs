using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.Travel.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.Travel
{
    public interface ITravelRequestsAppService : IApplicationService
    {
        Task<PagedResultDto<GetTravelRequestForViewDto>> GetAll(GetAllTravelRequestsInput input);

        Task<GetTravelRequestForViewDto> GetTravelRequestForView(Guid id);

        Task<GetTravelRequestForEditOutput> GetTravelRequestForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditTravelRequestDto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetTravelRequestsToExcel(GetAllTravelRequestsForExcelInput input);

        Task<PagedResultDto<TravelRequestUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<TravelRequestAirportLookupTableDto>> GetAllAirportForLookupTable(GetAllForLookupTableInput input);

    }
}