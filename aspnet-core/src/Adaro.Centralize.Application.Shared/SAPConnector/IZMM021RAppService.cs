using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.SAPConnector
{
    public interface IZMM021RAppService : IApplicationService
    {
        Task<PagedResultDto<GetZMM021RForViewDto>> GetAll(GetAllZMM021RInput input);

        Task<GetZMM021RForViewDto> GetZMM021RForView(Guid id);

        Task<GetZMM021RForEditOutput> GetZMM021RForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditZMM021RDto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetZMM021RToExcel(GetAllZMM021RForExcelInput input);

    }
}