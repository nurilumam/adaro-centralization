using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.Finance.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.Finance
{
    public interface ITransferBudgetsAppService : IApplicationService
    {
        Task<PagedResultDto<GetTransferBudgetForViewDto>> GetAll(GetAllTransferBudgetsInput input);

        Task<GetTransferBudgetForViewDto> GetTransferBudgetForView(Guid id);

        Task<GetTransferBudgetForEditOutput> GetTransferBudgetForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditTransferBudgetDto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetTransferBudgetsToExcel(GetAllTransferBudgetsForExcelInput input);

    }
}