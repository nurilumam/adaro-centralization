using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.LookupArea.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.LookupArea
{
    public interface ILookupPagesAppService : IApplicationService
    {
        Task<PagedResultDto<GetLookupPageForViewDto>> GetAll(GetAllLookupPagesInput input);

        Task<GetLookupPageForViewDto> GetLookupPageForView(Guid id);

        Task<GetLookupPageForEditOutput> GetLookupPageForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditLookupPageDto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetLookupPagesToExcel(GetAllLookupPagesForExcelInput input);

        Task<PagedResultDto<LookupPageCostCenterLookupTableDto>> GetAllCostCenterForLookupTable(GetAllForLookupTableInput input);

    }
}