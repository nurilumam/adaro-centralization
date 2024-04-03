using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;
using System.Collections.Generic;

namespace Adaro.Centralize.SAPConnector
{
    public interface IGeneralLedgerAccountsAppService : IApplicationService
    {
        Task<PagedResultDto<GetGeneralLedgerAccountForViewDto>> GetAll(GetAllGeneralLedgerAccountsInput input);

        Task<GetGeneralLedgerAccountForViewDto> GetGeneralLedgerAccountForView(Guid id);

        Task<GetGeneralLedgerAccountForEditOutput> GetGeneralLedgerAccountForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditGeneralLedgerAccountDto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetGeneralLedgerAccountsToExcel(GetAllGeneralLedgerAccountsForExcelInput input);

        Task<PagedResultDto<GeneralLedgerAccountCostCenterLookupTableDto>> GetAllCostCenterForLookupTable(GetAllForLookupTableInput input);

        Task<List<GetGeneralLedgerAccountForViewDto>> GetByCostCenterCode(string CostCenterCode);

    }
}