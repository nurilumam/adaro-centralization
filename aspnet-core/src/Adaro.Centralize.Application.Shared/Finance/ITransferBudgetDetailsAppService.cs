using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.Finance.Dtos;
using Adaro.Centralize.Dto;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Adaro.Centralize.Finance
{
    public interface ITransferBudgetDetailsAppService : IApplicationService
    {
        Task<PagedResultDto<GetTransferBudgetDetailForViewDto>> GetAll(GetAllTransferBudgetDetailsInput input);

        Task<GetTransferBudgetDetailForEditOutput> GetTransferBudgetDetailForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditTransferBudgetDetailDto input);

        Task Delete(EntityDto<Guid> input);

        Task<List<TransferBudgetDetailTransferBudgetLookupTableDto>> GetAllTransferBudgetForTableDropdown();

        Task<PagedResultDto<TransferBudgetDetailCostCenterLookupTableDto>> GetAllCostCenterForLookupTable(GetAllForLookupTableInput input);

        Task<List<TransferBudgetDetailGeneralLedgerAccountLookupTableDto>> GetAllGeneralLedgerAccountForTableDropdown();

    }
}