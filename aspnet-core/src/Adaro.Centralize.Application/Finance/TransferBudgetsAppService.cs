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
    [AbpAuthorize(AppPermissions.Pages_TransferBudgets)]
    public class TransferBudgetsAppService : CentralizeAppServiceBase, ITransferBudgetsAppService
    {
        private readonly IRepository<TransferBudget, Guid> _transferBudgetRepository;
        private readonly ITransferBudgetsExcelExporter _transferBudgetsExcelExporter;

        public TransferBudgetsAppService(IRepository<TransferBudget, Guid> transferBudgetRepository, ITransferBudgetsExcelExporter transferBudgetsExcelExporter)
        {
            _transferBudgetRepository = transferBudgetRepository;
            _transferBudgetsExcelExporter = transferBudgetsExcelExporter;

        }

        public virtual async Task<PagedResultDto<GetTransferBudgetForViewDto>> GetAll(GetAllTransferBudgetsInput input)
        {

            var filteredTransferBudgets = _transferBudgetRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.DocumentNo.Contains(input.Filter) || e.Department.Contains(input.Filter) || e.Division.Contains(input.Filter) || e.Purpose.Contains(input.Filter) || e.ProjectActivities.Contains(input.Filter) || e.Reason.Contains(input.Filter) || e.ScopeofWork.Contains(input.Filter) || e.Location.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DocumentNoFilter), e => e.DocumentNo.Contains(input.DocumentNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DepartmentFilter), e => e.Department.Contains(input.DepartmentFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DivisionFilter), e => e.Division.Contains(input.DivisionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurposeFilter), e => e.Purpose.Contains(input.PurposeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ProjectActivitiesFilter), e => e.ProjectActivities.Contains(input.ProjectActivitiesFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ReasonFilter), e => e.Reason.Contains(input.ReasonFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ScopeofWorkFilter), e => e.ScopeofWork.Contains(input.ScopeofWorkFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LocationFilter), e => e.Location.Contains(input.LocationFilter));

            var pagedAndFilteredTransferBudgets = filteredTransferBudgets
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var transferBudgets = from o in pagedAndFilteredTransferBudgets
                                  select new
                                  {

                                      o.DocumentNo,
                                      o.Department,
                                      o.Division,
                                      o.Reason,
                                      o.Location,
                                      Id = o.Id
                                  };

            var totalCount = await filteredTransferBudgets.CountAsync();

            var dbList = await transferBudgets.ToListAsync();
            var results = new List<GetTransferBudgetForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetTransferBudgetForViewDto()
                {
                    TransferBudget = new TransferBudgetDto
                    {

                        DocumentNo = o.DocumentNo,
                        Department = o.Department,
                        Division = o.Division,
                        Reason = o.Reason,
                        Location = o.Location,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetTransferBudgetForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetTransferBudgetForViewDto> GetTransferBudgetForView(Guid id)
        {
            var transferBudget = await _transferBudgetRepository.GetAsync(id);

            var output = new GetTransferBudgetForViewDto { TransferBudget = ObjectMapper.Map<TransferBudgetDto>(transferBudget) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_TransferBudgets_Edit)]
        public virtual async Task<GetTransferBudgetForEditOutput> GetTransferBudgetForEdit(EntityDto<Guid> input)
        {
            var transferBudget = await _transferBudgetRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetTransferBudgetForEditOutput { TransferBudget = ObjectMapper.Map<CreateOrEditTransferBudgetDto>(transferBudget) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditTransferBudgetDto input)
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

        [AbpAuthorize(AppPermissions.Pages_TransferBudgets_Create)]
        protected virtual async Task Create(CreateOrEditTransferBudgetDto input)
        {
            var transferBudget = ObjectMapper.Map<TransferBudget>(input);

            if (AbpSession.TenantId != null)
            {
                transferBudget.TenantId = (int?)AbpSession.TenantId;
            }

            await _transferBudgetRepository.InsertAsync(transferBudget);

        }

        [AbpAuthorize(AppPermissions.Pages_TransferBudgets_Edit)]
        protected virtual async Task Update(CreateOrEditTransferBudgetDto input)
        {
            var transferBudget = await _transferBudgetRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, transferBudget);

        }

        [AbpAuthorize(AppPermissions.Pages_TransferBudgets_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _transferBudgetRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetTransferBudgetsToExcel(GetAllTransferBudgetsForExcelInput input)
        {

            var filteredTransferBudgets = _transferBudgetRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.DocumentNo.Contains(input.Filter) || e.Department.Contains(input.Filter) || e.Division.Contains(input.Filter) || e.Purpose.Contains(input.Filter) || e.ProjectActivities.Contains(input.Filter) || e.Reason.Contains(input.Filter) || e.ScopeofWork.Contains(input.Filter) || e.Location.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DocumentNoFilter), e => e.DocumentNo.Contains(input.DocumentNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DepartmentFilter), e => e.Department.Contains(input.DepartmentFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DivisionFilter), e => e.Division.Contains(input.DivisionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurposeFilter), e => e.Purpose.Contains(input.PurposeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ProjectActivitiesFilter), e => e.ProjectActivities.Contains(input.ProjectActivitiesFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ReasonFilter), e => e.Reason.Contains(input.ReasonFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ScopeofWorkFilter), e => e.ScopeofWork.Contains(input.ScopeofWorkFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LocationFilter), e => e.Location.Contains(input.LocationFilter));

            var query = (from o in filteredTransferBudgets
                         select new GetTransferBudgetForViewDto()
                         {
                             TransferBudget = new TransferBudgetDto
                             {
                                 DocumentNo = o.DocumentNo,
                                 Department = o.Department,
                                 Division = o.Division,
                                 Reason = o.Reason,
                                 Location = o.Location,
                                 Id = o.Id
                             }
                         });

            var transferBudgetListDtos = await query.ToListAsync();

            return _transferBudgetsExcelExporter.ExportToFile(transferBudgetListDtos);
        }

    }
}