using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.Finance.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.Finance
{
    public interface ITransferBudgetItemsAppService : IApplicationService
    {
        Task<PagedResultDto<GetTransferBudgetItemForViewDto>> GetAll(GetAllTransferBudgetItemsInput input);

        Task<GetTransferBudgetItemForViewDto> GetTransferBudgetItemForView(Guid id);

        Task<GetTransferBudgetItemForEditOutput> GetTransferBudgetItemForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditTransferBudgetItemDto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetTransferBudgetItemsToExcel(GetAllTransferBudgetItemsForExcelInput input);

        Task<PagedResultDto<TransferBudgetItemTransferBudgetLookupTableDto>> GetAllTransferBudgetForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<TransferBudgetItemCostCenterLookupTableDto>> GetAllCostCenterForLookupTable(GetAllForLookupTableInput input);

    }
}