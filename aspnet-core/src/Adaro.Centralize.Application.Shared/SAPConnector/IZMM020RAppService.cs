using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.SAPConnector
{
    public interface IZMM020RAppService : IApplicationService
    {
        Task<PagedResultDto<GetZMM020RForViewDto>> GetAll(GetAllZMM020RInput input);

        Task<GetZMM020RForViewDto> GetZMM020RForView(Guid id);

        Task<GetZMM020RForEditOutput> GetZMM020RForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditZMM020RDto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetZMM020RToExcel(GetAllZMM020RForExcelInput input);

    }
}