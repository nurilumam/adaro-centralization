using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.SAPConnector
{
    public interface IEKPOsAppService : IApplicationService
    {
        Task<PagedResultDto<GetEKPOForViewDto>> GetAll(GetAllEKPOsInput input);

        Task<GetEKPOForViewDto> GetEKPOForView(Guid id);

        Task<GetEKPOForEditOutput> GetEKPOForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditEKPODto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetEKPOsToExcel(GetAllEKPOsForExcelInput input);

    }
}