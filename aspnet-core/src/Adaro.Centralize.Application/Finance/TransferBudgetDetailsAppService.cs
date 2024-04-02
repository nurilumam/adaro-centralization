using Adaro.Centralize.SAPConnector;
using Adaro.Centralize.MasterData;

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
        private readonly IRepository<CostCenter, Guid> _lookup_costCenterRepository;
        private readonly IRepository<GeneralLedgerMapping, Guid> _lookup_generalLedgerMappingRepository;

        public TransferBudgetDetailsAppService(IRepository<TransferBudgetDetail, Guid> transferBudgetDetailRepository, IRepository<CostCenter, Guid> lookup_costCenterRepository, IRepository<GeneralLedgerMapping, Guid> lookup_generalLedgerMappingRepository)
        {
            _transferBudgetDetailRepository = transferBudgetDetailRepository;
            _lookup_costCenterRepository = lookup_costCenterRepository;
            _lookup_generalLedgerMappingRepository = lookup_generalLedgerMappingRepository;

        }

        public virtual async Task<PagedResultDto<GetTransferBudgetDetailForViewDto>> GetAll(GetAllTransferBudgetDetailsInput input)
        {

            var filteredTransferBudgetDetails = _transferBudgetDetailRepository.GetAll()
                        .Include(e => e.CostCenterFk)
                        .Include(e => e.GeneralLedgerMappingFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Period.Contains(input.Filter) || e.TransferType.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PeriodFilter), e => e.Period.Contains(input.PeriodFilter))
                        .WhereIf(input.MinAmountFilter != null, e => e.Amount >= input.MinAmountFilter)
                        .WhereIf(input.MaxAmountFilter != null, e => e.Amount <= input.MaxAmountFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TransferTypeFilter), e => e.TransferType.Contains(input.TransferTypeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterCostCenterNameFilter), e => e.CostCenterFk != null && e.CostCenterFk.CostCenterName == input.CostCenterCostCenterNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GeneralLedgerMappingGLAccountFilter), e => e.GeneralLedgerMappingFk != null && e.GeneralLedgerMappingFk.GLAccount == input.GeneralLedgerMappingGLAccountFilter);

            var pagedAndFilteredTransferBudgetDetails = filteredTransferBudgetDetails
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var transferBudgetDetails = from o in pagedAndFilteredTransferBudgetDetails
                                        join o1 in _lookup_costCenterRepository.GetAll() on o.CostCenterId equals o1.Id into j1
                                        from s1 in j1.DefaultIfEmpty()

                                        join o2 in _lookup_generalLedgerMappingRepository.GetAll() on o.GeneralLedgerMappingId equals o2.Id into j2
                                        from s2 in j2.DefaultIfEmpty()

                                        select new
                                        {

                                            o.Period,
                                            o.Amount,
                                            o.TransferType,
                                            Id = o.Id,
                                            CostCenterCostCenterName = s1 == null || s1.CostCenterName == null ? "" : s1.CostCenterName.ToString(),
                                            GeneralLedgerMappingGLAccount = s2 == null || s2.GLAccount == null ? "" : s2.GLAccount.ToString()
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
                    CostCenterCostCenterName = o.CostCenterCostCenterName,
                    GeneralLedgerMappingGLAccount = o.GeneralLedgerMappingGLAccount
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

            if (output.TransferBudgetDetail.CostCenterId != null)
            {
                var _lookupCostCenter = await _lookup_costCenterRepository.FirstOrDefaultAsync((Guid)output.TransferBudgetDetail.CostCenterId);
                output.CostCenterCostCenterName = _lookupCostCenter?.CostCenterName?.ToString();
            }

            if (output.TransferBudgetDetail.GeneralLedgerMappingId != null)
            {
                var _lookupGeneralLedgerMapping = await _lookup_generalLedgerMappingRepository.FirstOrDefaultAsync((Guid)output.TransferBudgetDetail.GeneralLedgerMappingId);
                output.GeneralLedgerMappingGLAccount = _lookupGeneralLedgerMapping?.GLAccount?.ToString();
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
        public async Task<PagedResultDto<TransferBudgetDetailCostCenterLookupTableDto>> GetAllCostCenterForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_costCenterRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.CostCenterName != null && e.CostCenterName.Contains(input.Filter)
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
                    DisplayName = costCenter.CostCenterName?.ToString()
                });
            }

            return new PagedResultDto<TransferBudgetDetailCostCenterLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_TransferBudgetDetails)]
        public async Task<PagedResultDto<TransferBudgetDetailGeneralLedgerMappingLookupTableDto>> GetAllGeneralLedgerMappingForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_generalLedgerMappingRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.GLAccount != null && e.GLAccount.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var generalLedgerMappingList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<TransferBudgetDetailGeneralLedgerMappingLookupTableDto>();
            foreach (var generalLedgerMapping in generalLedgerMappingList)
            {
                lookupTableDtoList.Add(new TransferBudgetDetailGeneralLedgerMappingLookupTableDto
                {
                    Id = generalLedgerMapping.Id.ToString(),
                    DisplayName = generalLedgerMapping.GLAccount?.ToString()
                });
            }

            return new PagedResultDto<TransferBudgetDetailGeneralLedgerMappingLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}