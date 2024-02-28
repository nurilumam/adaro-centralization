using Adaro.Centralize.Finance;
using Adaro.Centralize.SAPConnector;
using Adaro.Centralize.SAPConnector;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Adaro.Centralize.Finance.Exporting;
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
    [AbpAuthorize(AppPermissions.Pages_TransferBudgetItems)]
    public class TransferBudgetItemsAppService : CentralizeAppServiceBase, ITransferBudgetItemsAppService
    {
        private readonly IRepository<TransferBudgetItem, Guid> _transferBudgetItemRepository;
        private readonly ITransferBudgetItemsExcelExporter _transferBudgetItemsExcelExporter;
        private readonly IRepository<TransferBudget, Guid> _lookup_transferBudgetRepository;
        private readonly IRepository<CostCenter, Guid> _lookup_costCenterRepository;

        public TransferBudgetItemsAppService(IRepository<TransferBudgetItem, Guid> transferBudgetItemRepository, ITransferBudgetItemsExcelExporter transferBudgetItemsExcelExporter, IRepository<TransferBudget, Guid> lookup_transferBudgetRepository, IRepository<CostCenter, Guid> lookup_costCenterRepository)
        {
            _transferBudgetItemRepository = transferBudgetItemRepository;
            _transferBudgetItemsExcelExporter = transferBudgetItemsExcelExporter;
            _lookup_transferBudgetRepository = lookup_transferBudgetRepository;
            _lookup_costCenterRepository = lookup_costCenterRepository;

        }

        public virtual async Task<PagedResultDto<GetTransferBudgetItemForViewDto>> GetAll(GetAllTransferBudgetItemsInput input)
        {

            var filteredTransferBudgetItems = _transferBudgetItemRepository.GetAll()
                        .Include(e => e.TransferBudgetFk)
                        .Include(e => e.CostCenterIdFromFk)
                        .Include(e => e.CostCenterIdToFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.PeriodFrom.Contains(input.Filter) || e.PeriodTo.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PeriodFromFilter), e => e.PeriodFrom.Contains(input.PeriodFromFilter))
                        .WhereIf(input.MinAmountFromFilter != null, e => e.AmountFrom >= input.MinAmountFromFilter)
                        .WhereIf(input.MaxAmountFromFilter != null, e => e.AmountFrom <= input.MaxAmountFromFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PeriodToFilter), e => e.PeriodTo.Contains(input.PeriodToFilter))
                        .WhereIf(input.MinAmountToFilter != null, e => e.AmountTo >= input.MinAmountToFilter)
                        .WhereIf(input.MaxAmountToFilter != null, e => e.AmountTo <= input.MaxAmountToFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TransferBudgetDisplayPropertyFilter), e => string.Format("{0}-{1}", e.TransferBudgetFk == null || e.TransferBudgetFk.DocumentNo == null ? "" : e.TransferBudgetFk.DocumentNo.ToString()
, e.TransferBudgetFk == null || e.TransferBudgetFk.Department == null ? "" : e.TransferBudgetFk.Department.ToString()
) == input.TransferBudgetDisplayPropertyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterDisplayPropertyFilter), e => string.Format("{0}-{1}", e.CostCenterIdFromFk == null || e.CostCenterIdFromFk.ControllingArea == null ? "" : e.CostCenterIdFromFk.ControllingArea.ToString()
, e.CostCenterIdFromFk == null || e.CostCenterIdFromFk.CostCenterName == null ? "" : e.CostCenterIdFromFk.CostCenterName.ToString()
) == input.CostCenterDisplayPropertyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterDisplayProperty2Filter), e => string.Format("{0}-{1}", e.CostCenterIdToFk == null || e.CostCenterIdToFk.ControllingArea == null ? "" : e.CostCenterIdToFk.ControllingArea.ToString()
, e.CostCenterIdToFk == null || e.CostCenterIdToFk.CostCenterName == null ? "" : e.CostCenterIdToFk.CostCenterName.ToString()
) == input.CostCenterDisplayProperty2Filter);

            var pagedAndFilteredTransferBudgetItems = filteredTransferBudgetItems
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var transferBudgetItems = from o in pagedAndFilteredTransferBudgetItems
                                      join o1 in _lookup_transferBudgetRepository.GetAll() on o.TransferBudgetId equals o1.Id into j1
                                      from s1 in j1.DefaultIfEmpty()

                                      join o2 in _lookup_costCenterRepository.GetAll() on o.CostCenterIdFrom equals o2.Id into j2
                                      from s2 in j2.DefaultIfEmpty()

                                      join o3 in _lookup_costCenterRepository.GetAll() on o.CostCenterIdTo equals o3.Id into j3
                                      from s3 in j3.DefaultIfEmpty()

                                      select new
                                      {

                                          o.PeriodFrom,
                                          o.AmountFrom,
                                          o.PeriodTo,
                                          o.AmountTo,
                                          Id = o.Id,
                                          TransferBudgetDisplayProperty = string.Format("{0}-{1}", s1 == null || s1.DocumentNo == null ? "" : s1.DocumentNo.ToString()
                      , s1 == null || s1.Department == null ? "" : s1.Department.ToString()
                      ),
                                          CostCenterDisplayProperty = string.Format("{0}-{1}", s2 == null || s2.ControllingArea == null ? "" : s2.ControllingArea.ToString()
                      , s2 == null || s2.CostCenterName == null ? "" : s2.CostCenterName.ToString()
                      ),
                                          CostCenterDisplayProperty2 = string.Format("{0}-{1}", s3 == null || s3.ControllingArea == null ? "" : s3.ControllingArea.ToString()
                      , s3 == null || s3.CostCenterName == null ? "" : s3.CostCenterName.ToString()
                      )
                                      };

            var totalCount = await filteredTransferBudgetItems.CountAsync();

            var dbList = await transferBudgetItems.ToListAsync();
            var results = new List<GetTransferBudgetItemForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetTransferBudgetItemForViewDto()
                {
                    TransferBudgetItem = new TransferBudgetItemDto
                    {

                        PeriodFrom = o.PeriodFrom,
                        AmountFrom = o.AmountFrom,
                        PeriodTo = o.PeriodTo,
                        AmountTo = o.AmountTo,
                        Id = o.Id,
                    },
                    TransferBudgetDisplayProperty = o.TransferBudgetDisplayProperty,
                    CostCenterDisplayProperty = o.CostCenterDisplayProperty,
                    CostCenterDisplayProperty2 = o.CostCenterDisplayProperty2
                };

                results.Add(res);
            }

            return new PagedResultDto<GetTransferBudgetItemForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetTransferBudgetItemForViewDto> GetTransferBudgetItemForView(Guid id)
        {
            var transferBudgetItem = await _transferBudgetItemRepository.GetAsync(id);

            var output = new GetTransferBudgetItemForViewDto { TransferBudgetItem = ObjectMapper.Map<TransferBudgetItemDto>(transferBudgetItem) };

            if (output.TransferBudgetItem.TransferBudgetId != null)
            {
                var _lookupTransferBudget = await _lookup_transferBudgetRepository.FirstOrDefaultAsync((Guid)output.TransferBudgetItem.TransferBudgetId);
                output.TransferBudgetDisplayProperty = string.Format("{0}-{1}", _lookupTransferBudget.DocumentNo, _lookupTransferBudget.Department);
            }

            if (output.TransferBudgetItem.CostCenterIdFrom != null)
            {
                var _lookupCostCenter = await _lookup_costCenterRepository.FirstOrDefaultAsync((Guid)output.TransferBudgetItem.CostCenterIdFrom);
                output.CostCenterDisplayProperty = string.Format("{0}-{1}", _lookupCostCenter.ControllingArea, _lookupCostCenter.CostCenterName);
            }

            if (output.TransferBudgetItem.CostCenterIdTo != null)
            {
                var _lookupCostCenter = await _lookup_costCenterRepository.FirstOrDefaultAsync((Guid)output.TransferBudgetItem.CostCenterIdTo);
                output.CostCenterDisplayProperty2 = string.Format("{0}-{1}", _lookupCostCenter.ControllingArea, _lookupCostCenter.CostCenterName);
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_TransferBudgetItems_Edit)]
        public virtual async Task<GetTransferBudgetItemForEditOutput> GetTransferBudgetItemForEdit(EntityDto<Guid> input)
        {
            var transferBudgetItem = await _transferBudgetItemRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetTransferBudgetItemForEditOutput { TransferBudgetItem = ObjectMapper.Map<CreateOrEditTransferBudgetItemDto>(transferBudgetItem) };

            if (output.TransferBudgetItem.TransferBudgetId != null)
            {
                var _lookupTransferBudget = await _lookup_transferBudgetRepository.FirstOrDefaultAsync((Guid)output.TransferBudgetItem.TransferBudgetId);
                output.TransferBudgetDisplayProperty = string.Format("{0}-{1}", _lookupTransferBudget.DocumentNo, _lookupTransferBudget.Department);
            }

            if (output.TransferBudgetItem.CostCenterIdFrom != null)
            {
                var _lookupCostCenter = await _lookup_costCenterRepository.FirstOrDefaultAsync((Guid)output.TransferBudgetItem.CostCenterIdFrom);
                output.CostCenterDisplayProperty = string.Format("{0}-{1}", _lookupCostCenter.ControllingArea, _lookupCostCenter.CostCenterName);
            }

            if (output.TransferBudgetItem.CostCenterIdTo != null)
            {
                var _lookupCostCenter = await _lookup_costCenterRepository.FirstOrDefaultAsync((Guid)output.TransferBudgetItem.CostCenterIdTo);
                output.CostCenterDisplayProperty2 = string.Format("{0}-{1}", _lookupCostCenter.ControllingArea, _lookupCostCenter.CostCenterName);
            }

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditTransferBudgetItemDto input)
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

        [AbpAuthorize(AppPermissions.Pages_TransferBudgetItems_Create)]
        protected virtual async Task Create(CreateOrEditTransferBudgetItemDto input)
        {
            var transferBudgetItem = ObjectMapper.Map<TransferBudgetItem>(input);

            if (AbpSession.TenantId != null)
            {
                transferBudgetItem.TenantId = (int?)AbpSession.TenantId;
            }

            await _transferBudgetItemRepository.InsertAsync(transferBudgetItem);

        }

        [AbpAuthorize(AppPermissions.Pages_TransferBudgetItems_Edit)]
        protected virtual async Task Update(CreateOrEditTransferBudgetItemDto input)
        {
            var transferBudgetItem = await _transferBudgetItemRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, transferBudgetItem);

        }

        [AbpAuthorize(AppPermissions.Pages_TransferBudgetItems_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _transferBudgetItemRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetTransferBudgetItemsToExcel(GetAllTransferBudgetItemsForExcelInput input)
        {

            var filteredTransferBudgetItems = _transferBudgetItemRepository.GetAll()
                        .Include(e => e.TransferBudgetFk)
                        .Include(e => e.CostCenterIdFromFk)
                        .Include(e => e.CostCenterIdToFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.PeriodFrom.Contains(input.Filter) || e.PeriodTo.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PeriodFromFilter), e => e.PeriodFrom.Contains(input.PeriodFromFilter))
                        .WhereIf(input.MinAmountFromFilter != null, e => e.AmountFrom >= input.MinAmountFromFilter)
                        .WhereIf(input.MaxAmountFromFilter != null, e => e.AmountFrom <= input.MaxAmountFromFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PeriodToFilter), e => e.PeriodTo.Contains(input.PeriodToFilter))
                        .WhereIf(input.MinAmountToFilter != null, e => e.AmountTo >= input.MinAmountToFilter)
                        .WhereIf(input.MaxAmountToFilter != null, e => e.AmountTo <= input.MaxAmountToFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TransferBudgetDisplayPropertyFilter), e => string.Format("{0}-{1}", e.TransferBudgetFk == null || e.TransferBudgetFk.DocumentNo == null ? "" : e.TransferBudgetFk.DocumentNo.ToString()
, e.TransferBudgetFk == null || e.TransferBudgetFk.Department == null ? "" : e.TransferBudgetFk.Department.ToString()
) == input.TransferBudgetDisplayPropertyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterDisplayPropertyFilter), e => string.Format("{0}-{1}", e.CostCenterIdFromFk == null || e.CostCenterIdFromFk.ControllingArea == null ? "" : e.CostCenterIdFromFk.ControllingArea.ToString()
, e.CostCenterIdFromFk == null || e.CostCenterIdFromFk.CostCenterName == null ? "" : e.CostCenterIdFromFk.CostCenterName.ToString()
) == input.CostCenterDisplayPropertyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterDisplayProperty2Filter), e => string.Format("{0}-{1}", e.CostCenterIdToFk == null || e.CostCenterIdToFk.ControllingArea == null ? "" : e.CostCenterIdToFk.ControllingArea.ToString()
, e.CostCenterIdToFk == null || e.CostCenterIdToFk.CostCenterName == null ? "" : e.CostCenterIdToFk.CostCenterName.ToString()
) == input.CostCenterDisplayProperty2Filter);

            var query = (from o in filteredTransferBudgetItems
                         join o1 in _lookup_transferBudgetRepository.GetAll() on o.TransferBudgetId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_costCenterRepository.GetAll() on o.CostCenterIdFrom equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         join o3 in _lookup_costCenterRepository.GetAll() on o.CostCenterIdTo equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()

                         select new GetTransferBudgetItemForViewDto()
                         {
                             TransferBudgetItem = new TransferBudgetItemDto
                             {
                                 PeriodFrom = o.PeriodFrom,
                                 AmountFrom = o.AmountFrom,
                                 PeriodTo = o.PeriodTo,
                                 AmountTo = o.AmountTo,
                                 Id = o.Id
                             },
                             TransferBudgetDisplayProperty = string.Format("{0}-{1}", s1 == null || s1.DocumentNo == null ? "" : s1.DocumentNo.ToString()
, s1 == null || s1.Department == null ? "" : s1.Department.ToString()
),
                             CostCenterDisplayProperty = string.Format("{0}-{1}", s2 == null || s2.ControllingArea == null ? "" : s2.ControllingArea.ToString()
, s2 == null || s2.CostCenterName == null ? "" : s2.CostCenterName.ToString()
),
                             CostCenterDisplayProperty2 = string.Format("{0}-{1}", s3 == null || s3.ControllingArea == null ? "" : s3.ControllingArea.ToString()
, s3 == null || s3.CostCenterName == null ? "" : s3.CostCenterName.ToString()
)
                         });

            var transferBudgetItemListDtos = await query.ToListAsync();

            return _transferBudgetItemsExcelExporter.ExportToFile(transferBudgetItemListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_TransferBudgetItems)]
        public async Task<PagedResultDto<TransferBudgetItemTransferBudgetLookupTableDto>> GetAllTransferBudgetForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_transferBudgetRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => string.Format("{0}-{1}", e.DocumentNo, e.Department).Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var transferBudgetList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<TransferBudgetItemTransferBudgetLookupTableDto>();
            foreach (var transferBudget in transferBudgetList)
            {
                lookupTableDtoList.Add(new TransferBudgetItemTransferBudgetLookupTableDto
                {
                    Id = transferBudget.Id.ToString(),
                    DisplayName = string.Format("{0}-{1}", transferBudget.DocumentNo, transferBudget.Department)
                });
            }

            return new PagedResultDto<TransferBudgetItemTransferBudgetLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_TransferBudgetItems)]
        public async Task<PagedResultDto<TransferBudgetItemCostCenterLookupTableDto>> GetAllCostCenterForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_costCenterRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => string.Format("{0}-{1}", e.ControllingArea, e.CostCenterName).Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var costCenterList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<TransferBudgetItemCostCenterLookupTableDto>();
            foreach (var costCenter in costCenterList)
            {
                lookupTableDtoList.Add(new TransferBudgetItemCostCenterLookupTableDto
                {
                    Id = costCenter.Id.ToString(),
                    DisplayName = string.Format("{0}-{1}", costCenter.ControllingArea, costCenter.CostCenterName)
                });
            }

            return new PagedResultDto<TransferBudgetItemCostCenterLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}