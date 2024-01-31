using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.SAPConnector
{
    public interface IDataProductionsAppService : IApplicationService
    {
        Task<PagedResultDto<GetDataProductionForViewDto>> GetAll(GetAllDataProductionsInput input);

        Task<GetDataProductionForViewDto> GetDataProductionForView(Guid id);

        Task<GetDataProductionForEditOutput> GetDataProductionForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditDataProductionDto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetDataProductionsToExcel(GetAllDataProductionsForExcelInput input);

    }
}