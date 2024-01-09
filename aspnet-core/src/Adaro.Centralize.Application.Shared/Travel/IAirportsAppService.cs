using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.Travel.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.Travel
{
    public interface IAirportsAppService : IApplicationService
    {
        Task<PagedResultDto<GetAirportForViewDto>> GetAll(GetAllAirportsInput input);

        Task<GetAirportForViewDto> GetAirportForView(Guid id);

        Task<GetAirportForEditOutput> GetAirportForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditAirportDto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetAirportsToExcel(GetAllAirportsForExcelInput input);

    }
}