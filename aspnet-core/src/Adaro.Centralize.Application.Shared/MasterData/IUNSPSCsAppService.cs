using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.MasterData.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.MasterData
{
    public interface IUNSPSCsAppService : IApplicationService
    {
        Task<PagedResultDto<GetUNSPSCForViewDto>> GetAll(GetAllUNSPSCsInput input);

        Task<GetUNSPSCForViewDto> GetUNSPSCForView(Guid id);

        Task<GetUNSPSCForEditOutput> GetUNSPSCForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditUNSPSCDto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetUNSPSCsToExcel(GetAllUNSPSCsForExcelInput input);

    }
}