using Adaro.Centralize.SAPConnector;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Adaro.Centralize.SAPConnector.Exporting;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;
using Abp.Application.Services.Dto;
using Adaro.Centralize.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.SAPConnector
{
    [AbpAuthorize(AppPermissions.Pages_GeneralLedgerAccounts)]
    public class GeneralLedgerAccountsAppService : CentralizeAppServiceBase, IGeneralLedgerAccountsAppService
    {
        private readonly IRepository<GeneralLedgerAccount, Guid> _generalLedgerAccountRepository;
        private readonly IGeneralLedgerAccountsExcelExporter _generalLedgerAccountsExcelExporter;
        private readonly IRepository<CostCenter, Guid> _lookup_costCenterRepository;

        public GeneralLedgerAccountsAppService(IRepository<GeneralLedgerAccount, Guid> generalLedgerAccountRepository, IGeneralLedgerAccountsExcelExporter generalLedgerAccountsExcelExporter, IRepository<CostCenter, Guid> lookup_costCenterRepository)
        {
            _generalLedgerAccountRepository = generalLedgerAccountRepository;
            _generalLedgerAccountsExcelExporter = generalLedgerAccountsExcelExporter;
            _lookup_costCenterRepository = lookup_costCenterRepository;

        }

        public virtual async Task<PagedResultDto<GetGeneralLedgerAccountForViewDto>> GetAll(GetAllGeneralLedgerAccountsInput input)
        {

            var filteredGeneralLedgerAccounts = _generalLedgerAccountRepository.GetAll()
                        .Include(e => e.CostCenterFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.FundsCenter.Contains(input.Filter) || e.FundsCenterDescription.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FundsCenterFilter), e => e.FundsCenter.Contains(input.FundsCenterFilter))
                        .WhereIf(input.MinConsumableBudgetFilter != null, e => e.ConsumableBudget >= input.MinConsumableBudgetFilter)
                        .WhereIf(input.MaxConsumableBudgetFilter != null, e => e.ConsumableBudget <= input.MaxConsumableBudgetFilter)
                        .WhereIf(input.MinConsumedBudgetFilter != null, e => e.ConsumedBudget >= input.MinConsumedBudgetFilter)
                        .WhereIf(input.MaxConsumedBudgetFilter != null, e => e.ConsumedBudget <= input.MaxConsumedBudgetFilter)
                        .WhereIf(input.MinAvailableAmountFilter != null, e => e.AvailableAmount >= input.MinAvailableAmountFilter)
                        .WhereIf(input.MaxAvailableAmountFilter != null, e => e.AvailableAmount <= input.MaxAvailableAmountFilter)
                        .WhereIf(input.MinCurrentBudgetFilter != null, e => e.CurrentBudget >= input.MinCurrentBudgetFilter)
                        .WhereIf(input.MaxCurrentBudgetFilter != null, e => e.CurrentBudget <= input.MaxCurrentBudgetFilter)
                        .WhereIf(input.MinCommitmentActualsFilter != null, e => e.CommitmentActuals >= input.MinCommitmentActualsFilter)
                        .WhereIf(input.MaxCommitmentActualsFilter != null, e => e.CommitmentActuals <= input.MaxCommitmentActualsFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FundsCenterDescriptionFilter), e => e.FundsCenterDescription.Contains(input.FundsCenterDescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterCostCenterNameFilter), e => e.CostCenterFk != null && e.CostCenterFk.CostCenterName == input.CostCenterCostCenterNameFilter);

            var pagedAndFilteredGeneralLedgerAccounts = filteredGeneralLedgerAccounts
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var generalLedgerAccounts = from o in pagedAndFilteredGeneralLedgerAccounts
                                        join o1 in _lookup_costCenterRepository.GetAll() on o.CostCenterId equals o1.Id into j1
                                        from s1 in j1.DefaultIfEmpty()

                                        select new
                                        {

                                            o.FundsCenter,
                                            o.ConsumableBudget,
                                            o.ConsumedBudget,
                                            o.AvailableAmount,
                                            o.CurrentBudget,
                                            o.CommitmentActuals,
                                            o.FundsCenterDescription,
                                            Id = o.Id,
                                            CostCenterCostCenterName = s1 == null || s1.CostCenterName == null ? "" : s1.CostCenterName.ToString()
                                        };

            var totalCount = await filteredGeneralLedgerAccounts.CountAsync();

            var dbList = await generalLedgerAccounts.ToListAsync();
            var results = new List<GetGeneralLedgerAccountForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetGeneralLedgerAccountForViewDto()
                {
                    GeneralLedgerAccount = new GeneralLedgerAccountDto
                    {

                        FundsCenter = o.FundsCenter,
                        ConsumableBudget = o.ConsumableBudget,
                        ConsumedBudget = o.ConsumedBudget,
                        AvailableAmount = o.AvailableAmount,
                        CurrentBudget = o.CurrentBudget,
                        CommitmentActuals = o.CommitmentActuals,
                        FundsCenterDescription = o.FundsCenterDescription,
                        Id = o.Id,
                    },
                    CostCenterCostCenterName = o.CostCenterCostCenterName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetGeneralLedgerAccountForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetGeneralLedgerAccountForViewDto> GetGeneralLedgerAccountForView(Guid id)
        {
            var generalLedgerAccount = await _generalLedgerAccountRepository.GetAsync(id);

            var output = new GetGeneralLedgerAccountForViewDto { GeneralLedgerAccount = ObjectMapper.Map<GeneralLedgerAccountDto>(generalLedgerAccount) };

            if (output.GeneralLedgerAccount.CostCenterId != null)
            {
                var _lookupCostCenter = await _lookup_costCenterRepository.FirstOrDefaultAsync((Guid)output.GeneralLedgerAccount.CostCenterId);
                output.CostCenterCostCenterName = _lookupCostCenter?.CostCenterName?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_GeneralLedgerAccounts_Edit)]
        public virtual async Task<GetGeneralLedgerAccountForEditOutput> GetGeneralLedgerAccountForEdit(EntityDto<Guid> input)
        {
            var generalLedgerAccount = await _generalLedgerAccountRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetGeneralLedgerAccountForEditOutput { GeneralLedgerAccount = ObjectMapper.Map<CreateOrEditGeneralLedgerAccountDto>(generalLedgerAccount) };

            if (output.GeneralLedgerAccount.CostCenterId != null)
            {
                var _lookupCostCenter = await _lookup_costCenterRepository.FirstOrDefaultAsync((Guid)output.GeneralLedgerAccount.CostCenterId);
                output.CostCenterCostCenterName = _lookupCostCenter?.CostCenterName?.ToString();
            }

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditGeneralLedgerAccountDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_GeneralLedgerAccounts_Create)]
        protected virtual async Task Create(CreateOrEditGeneralLedgerAccountDto input)
        {
            var generalLedgerAccount = ObjectMapper.Map<GeneralLedgerAccount>(input);

            if (AbpSession.TenantId != null)
            {
                generalLedgerAccount.TenantId = (int?)AbpSession.TenantId;
            }

            await _generalLedgerAccountRepository.InsertAsync(generalLedgerAccount);

        }

        [AbpAuthorize(AppPermissions.Pages_GeneralLedgerAccounts_Edit)]
        protected virtual async Task Update(CreateOrEditGeneralLedgerAccountDto input)
        {
            var generalLedgerAccount = await _generalLedgerAccountRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, generalLedgerAccount);

        }

        [AbpAuthorize(AppPermissions.Pages_GeneralLedgerAccounts_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _generalLedgerAccountRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetGeneralLedgerAccountsToExcel(GetAllGeneralLedgerAccountsForExcelInput input)
        {

            var filteredGeneralLedgerAccounts = _generalLedgerAccountRepository.GetAll()
                        .Include(e => e.CostCenterFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.FundsCenter.Contains(input.Filter) || e.FundsCenterDescription.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FundsCenterFilter), e => e.FundsCenter.Contains(input.FundsCenterFilter))
                        .WhereIf(input.MinConsumableBudgetFilter != null, e => e.ConsumableBudget >= input.MinConsumableBudgetFilter)
                        .WhereIf(input.MaxConsumableBudgetFilter != null, e => e.ConsumableBudget <= input.MaxConsumableBudgetFilter)
                        .WhereIf(input.MinConsumedBudgetFilter != null, e => e.ConsumedBudget >= input.MinConsumedBudgetFilter)
                        .WhereIf(input.MaxConsumedBudgetFilter != null, e => e.ConsumedBudget <= input.MaxConsumedBudgetFilter)
                        .WhereIf(input.MinAvailableAmountFilter != null, e => e.AvailableAmount >= input.MinAvailableAmountFilter)
                        .WhereIf(input.MaxAvailableAmountFilter != null, e => e.AvailableAmount <= input.MaxAvailableAmountFilter)
                        .WhereIf(input.MinCurrentBudgetFilter != null, e => e.CurrentBudget >= input.MinCurrentBudgetFilter)
                        .WhereIf(input.MaxCurrentBudgetFilter != null, e => e.CurrentBudget <= input.MaxCurrentBudgetFilter)
                        .WhereIf(input.MinCommitmentActualsFilter != null, e => e.CommitmentActuals >= input.MinCommitmentActualsFilter)
                        .WhereIf(input.MaxCommitmentActualsFilter != null, e => e.CommitmentActuals <= input.MaxCommitmentActualsFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FundsCenterDescriptionFilter), e => e.FundsCenterDescription.Contains(input.FundsCenterDescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterCostCenterNameFilter), e => e.CostCenterFk != null && e.CostCenterFk.CostCenterName == input.CostCenterCostCenterNameFilter);

            var query = (from o in filteredGeneralLedgerAccounts
                         join o1 in _lookup_costCenterRepository.GetAll() on o.CostCenterId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new GetGeneralLedgerAccountForViewDto()
                         {
                             GeneralLedgerAccount = new GeneralLedgerAccountDto
                             {
                                 FundsCenter = o.FundsCenter,
                                 ConsumableBudget = o.ConsumableBudget,
                                 ConsumedBudget = o.ConsumedBudget,
                                 AvailableAmount = o.AvailableAmount,
                                 CurrentBudget = o.CurrentBudget,
                                 CommitmentActuals = o.CommitmentActuals,
                                 FundsCenterDescription = o.FundsCenterDescription,
                                 Id = o.Id
                             },
                             CostCenterCostCenterName = s1 == null || s1.CostCenterName == null ? "" : s1.CostCenterName.ToString()
                         });

            var generalLedgerAccountListDtos = await query.ToListAsync();

            return _generalLedgerAccountsExcelExporter.ExportToFile(generalLedgerAccountListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_GeneralLedgerAccounts)]
        public async Task<PagedResultDto<GeneralLedgerAccountCostCenterLookupTableDto>> GetAllCostCenterForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_costCenterRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.CostCenterName != null && e.CostCenterName.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var costCenterList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<GeneralLedgerAccountCostCenterLookupTableDto>();
            foreach (var costCenter in costCenterList)
            {
                lookupTableDtoList.Add(new GeneralLedgerAccountCostCenterLookupTableDto
                {
                    Id = costCenter.Id.ToString(),
                    DisplayName = costCenter.CostCenterName?.ToString()
                });
            }

            return new PagedResultDto<GeneralLedgerAccountCostCenterLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}