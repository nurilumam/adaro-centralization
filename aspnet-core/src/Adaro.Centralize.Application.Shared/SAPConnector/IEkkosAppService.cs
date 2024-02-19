using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.SAPConnector
{
    public interface IEkkosAppService : IApplicationService
    {
        Task<PagedResultDto<GetEkkoForViewDto>> GetAll(GetAllEkkosInput input);

        Task<GetEkkoForViewDto> GetEkkoForView(Guid id);

        Task<GetEkkoForEditOutput> GetEkkoForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditEkkoDto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetEkkosToExcel(GetAllEkkosForExcelInput input);

    }
}