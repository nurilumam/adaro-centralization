using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.MasterData.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.MasterData
{
    public interface IMaterialsAppService : IApplicationService
    {
        Task<PagedResultDto<GetMaterialForViewDto>> GetAll(GetAllMaterialsInput input);

        Task<GetMaterialForViewDto> GetMaterialForView(int id);

        Task<GetMaterialForEditOutput> GetMaterialForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditMaterialDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetMaterialsToExcel(GetAllMaterialsForExcelInput input);

        Task<PagedResultDto<MaterialMaterialGroupLookupTableDto>> GetAllMaterialGroupForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<MaterialUNSPSCLookupTableDto>> GetAllUNSPSCForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<MaterialGeneralLedgerMappingLookupTableDto>> GetAllGeneralLedgerMappingForLookupTable(GetAllForLookupTableInput input);

        Task RemoveImageMainFile(EntityDto input);

    }
}