using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.MasterData.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.MasterData
{
    public interface IMaterialGroupsAppService : IApplicationService
    {
        Task<PagedResultDto<GetMaterialGroupForViewDto>> GetAll(GetAllMaterialGroupsInput input);

        Task<GetMaterialGroupForViewDto> GetMaterialGroupForView(Guid id);

        Task<GetMaterialGroupForEditOutput> GetMaterialGroupForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditMaterialGroupDto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetMaterialGroupsToExcel(GetAllMaterialGroupsForExcelInput input);

    }
}