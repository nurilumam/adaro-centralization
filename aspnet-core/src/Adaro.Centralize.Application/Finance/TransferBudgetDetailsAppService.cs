using Adaro.Centralize.Finance;
using Adaro.Centralize.SAPConnector;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Adaro.Centralize.Finance.Dtos;
using Adaro.Centralize.Dto;
using Abp.Application.Services.Dto;
using Adaro.Centralize.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.Finance
{
    [AbpAuthorize(AppPermissions.Pages_TransferBudgetDetails)]
    public class TransferBudgetDetailsAppService : CentralizeAppServiceBase, ITransferBudgetDetailsAppService
    {
        private readonly IRepository<TransferBudgetDetail, Guid> _transferBudgetDetailRepository;
        private readonly IRepository<TransferBudget, Guid> _lookup_transferBudgetRepository;
        private readonly IRepository<CostCenter, Guid> _lookup_costCenterRepository;
        private readonly IRepository<GeneralLedgerAccount, Guid> _lookup_generalLedgerAccountRepository;

        public TransferBudgetDetailsAppService(IRepository<TransferBudgetDetail, Guid> transferBudgetDetailRepository, IRepository<TransferBudget, Guid> lookup_transferBudgetRepository, IRepository<CostCenter, Guid> lookup_costCenterRepository, IRepository<GeneralLedgerAccount, Guid> lookup_generalLedgerAccountRepository)
        {
            _transferBudgetDetailRepository = transferBudgetDetailRepository;
            _lookup_transferBudgetRepository = lookup_transferBudgetRepository;
            _lookup_costCenterRepository = lookup_costCenterRepository;
            _lookup_generalLedgerAccountRepository = lookup_generalLedgerAccountRepository;

        }

        public virtual async Task<PagedResultDto<GetTransferBudgetDetailForViewDto>> GetAll(GetAllTransferBudgetDetailsInput input)
        {

            var filteredTransferBudgetDetails = _transferBudgetDetailRepository.GetAll()
                        .Include(e => e.TransferBudgetFk)
                        .Include(e => e.CostCenterFk)
                        .Include(e => e.GeneralLedgerAccountFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Period.Contains(input.Filter) || e.TransferType.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PeriodFilter), e => e.Period.Contains(input.PeriodFilter))
                        .WhereIf(input.MinAmountFilter != null, e => e.Amount >= input.MinAmountFilter)
                        .WhereIf(input.MaxAmountFilter != null, e => e.Amount <= input.MaxAmountFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TransferTypeFilter), e => e.TransferType.Contains(input.TransferTypeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TransferBudgetDocumentNoFilter), e => e.TransferBudgetFk != null && e.TransferBudgetFk.DocumentNo == input.TransferBudgetDocumentNoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterDisplayPropertyFilter), e => string.Format("{1} {0}", e.CostCenterFk == null || e.CostCenterFk.CostCenterName == null ? "" : e.CostCenterFk.CostCenterName.ToString()
, e.CostCenterFk == null || e.CostCenterFk.CostCenterCode == null ? "" : e.CostCenterFk.CostCenterCode.ToString()
) == input.CostCenterDisplayPropertyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GeneralLedgerAccountFundsCenterFilter), e => e.GeneralLedgerAccountFk != null && e.GeneralLedgerAccountFk.FundsCenter == input.GeneralLedgerAccountFundsCenterFilter);

            var pagedAndFilteredTransferBudgetDetails = filteredTransferBudgetDetails
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var transferBudgetDetails = from o in pagedAndFilteredTransferBudgetDetails
                                        join o1 in _lookup_transferBudgetRepository.GetAll() on o.TransferBudgetId equals o1.Id into j1
                                        from s1 in j1.DefaultIfEmpty()

                                        join o2 in _lookup_costCenterRepository.GetAll() on o.CostCenterId equals o2.Id into j2
                                        from s2 in j2.DefaultIfEmpty()

                                        join o3 in _lookup_generalLedgerAccountRepository.GetAll() on o.GeneralLedgerAccountId equals o3.Id into j3
                                        from s3 in j3.DefaultIfEmpty()

                                        select new
                                        {

                                            o.Period,
                                            o.Amount,
                                            o.TransferType,
                                            Id = o.Id,
                                            TransferBudgetDocumentNo = s1 == null || s1.DocumentNo == null ? "" : s1.DocumentNo.ToString(),
                                            CostCenterDisplayProperty = string.Format("{1} {0}", s2 == null || s2.CostCenterName == null ? "" : s2.CostCenterName.ToString()
                        , s2 == null || s2.CostCenterCode == null ? "" : s2.CostCenterCode.ToString()
                        ),
                                            GeneralLedgerAccountFundsCenter = s3 == null || s3.FundsCenter == null ? "" : s3.FundsCenter.ToString()
                                        };

            var totalCount = await filteredTransferBudgetDetails.CountAsync();

            var dbList = await transferBudgetDetails.ToListAsync();
            var results = new List<GetTransferBudgetDetailForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetTransferBudgetDetailForViewDto()
                {
                    TransferBudgetDetail = new TransferBudgetDetailDto
                    {

                        Period = o.Period,
                        Amount = o.Amount,
                        TransferType = o.TransferType,
                        Id = o.Id,
                    },
                    TransferBudgetDocumentNo = o.TransferBudgetDocumentNo,
                    CostCenterDisplayProperty = o.CostCenterDisplayProperty,
                    GeneralLedgerAccountFundsCenter = o.GeneralLedgerAccountFundsCenter
                };

                results.Add(res);
            }

            return new PagedResultDto<GetTransferBudgetDetailForViewDto>(
                totalCount,
                results
            );

        }

        [AbpAuthorize(AppPermissions.Pages_TransferBudgetDetails_Edit)]
        public virtual async Task<GetTransferBudgetDetailForEditOutput> GetTransferBudgetDetailForEdit(EntityDto<Guid> input)
        {
            var transferBudgetDetail = await _transferBudgetDetailRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetTransferBudgetDetailForEditOutput { TransferBudgetDetail = ObjectMapper.Map<CreateOrEditTransferBudgetDetailDto>(transferBudgetDetail) };

            if (output.TransferBudgetDetail.TransferBudgetId != null)
            {
                var _lookupTransferBudget = await _lookup_transferBudgetRepository.FirstOrDefaultAsync((Guid)output.TransferBudgetDetail.TransferBudgetId);
                output.TransferBudgetDocumentNo = _lookupTransferBudget?.DocumentNo?.ToString();
            }

            if (output.TransferBudgetDetail.CostCenterId != null)
            {
                var _lookupCostCenter = await _lookup_costCenterRepository.FirstOrDefaultAsync((Guid)output.TransferBudgetDetail.CostCenterId);
                output.CostCenterDisplayProperty = string.Format("{1} {0}", _lookupCostCenter.CostCenterName, _lookupCostCenter.CostCenterCode);
            }

            if (output.TransferBudgetDetail.GeneralLedgerAccountId != null)
            {
                var _lookupGeneralLedgerAccount = await _lookup_generalLedgerAccountRepository.FirstOrDefaultAsync((Guid)output.TransferBudgetDetail.GeneralLedgerAccountId);
                output.GeneralLedgerAccountFundsCenter = _lookupGeneralLedgerAccount?.FundsCenter?.ToString();
            }

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditTransferBudgetDetailDto input)
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

        [AbpAuthorize(AppPermissions.Pages_TransferBudgetDetails_Create)]
        protected virtual async Task Create(CreateOrEditTransferBudgetDetailDto input)
        {
            var transferBudgetDetail = ObjectMapper.Map<TransferBudgetDetail>(input);

            if (AbpSession.TenantId != null)
            {
                transferBudgetDetail.TenantId = (int?)AbpSession.TenantId;
            }

            await _transferBudgetDetailRepository.InsertAsync(transferBudgetDetail);

        }

        [AbpAuthorize(AppPermissions.Pages_TransferBudgetDetails_Edit)]
        protected virtual async Task Update(CreateOrEditTransferBudgetDetailDto input)
        {
            var transferBudgetDetail = await _transferBudgetDetailRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, transferBudgetDetail);

        }

        [AbpAuthorize(AppPermissions.Pages_TransferBudgetDetails_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _transferBudgetDetailRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_TransferBudgetDetails)]
        public async Task<List<TransferBudgetDetailTransferBudgetLookupTableDto>> GetAllTransferBudgetForTableDropdown()
        {
            return await _lookup_transferBudgetRepository.GetAll()
                .Select(transferBudget => new TransferBudgetDetailTransferBudgetLookupTableDto
                {
                    Id = transferBudget.Id.ToString(),
                    DisplayName = transferBudget == null || transferBudget.DocumentNo == null ? "" : transferBudget.DocumentNo.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_TransferBudgetDetails)]
        public async Task<PagedResultDto<TransferBudgetDetailCostCenterLookupTableDto>> GetAllCostCenterForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_costCenterRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => string.Format("{1} {0}", e.CostCenterName, e.CostCenterCode).Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var costCenterList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<TransferBudgetDetailCostCenterLookupTableDto>();
            foreach (var costCenter in costCenterList)
            {
                lookupTableDtoList.Add(new TransferBudgetDetailCostCenterLookupTableDto
                {
                    Id = costCenter.Id.ToString(),
                    DisplayName = string.Format("{1} {0}", costCenter.CostCenterName, costCenter.CostCenterCode)
                });
            }

            return new PagedResultDto<TransferBudgetDetailCostCenterLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
        [AbpAuthorize(AppPermissions.Pages_TransferBudgetDetails)]
        public async Task<List<TransferBudgetDetailGeneralLedgerAccountLookupTableDto>> GetAllGeneralLedgerAccountForTableDropdown()
        {
            return await _lookup_generalLedgerAccountRepository.GetAll()
                .Select(generalLedgerAccount => new TransferBudgetDetailGeneralLedgerAccountLookupTableDto
                {
                    Id = generalLedgerAccount.Id.ToString(),
                    DisplayName = generalLedgerAccount == null || generalLedgerAccount.FundsCenter == null ? "" : generalLedgerAccount.FundsCenter.ToString()
                }).ToListAsync();
        }

    }
}