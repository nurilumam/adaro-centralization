using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.MasterDataRequest.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.MasterDataRequest
{
    public interface IMaterialRequestsAppService : IApplicationService
    {
        Task<PagedResultDto<GetMaterialRequestForViewDto>> GetAll(GetAllMaterialRequestsInput input);

        Task<GetMaterialRequestForViewDto> GetMaterialRequestForView(Guid id);

        Task<GetMaterialRequestForEditOutput> GetMaterialRequestForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditMaterialRequestDto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetMaterialRequestsToExcel(GetAllMaterialRequestsForExcelInput input);

        Task<PagedResultDto<MaterialRequestUNSPSCLookupTableDto>> GetAllUNSPSCForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<MaterialRequestGeneralLedgerMappingLookupTableDto>> GetAllGeneralLedgerMappingForLookupTable(GetAllForLookupTableInput input);

        Task RemovePictureFile(EntityDto<Guid> input);

    }
}