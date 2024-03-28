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
using Abp.Organizations;

namespace Adaro.Centralize.SAPConnector
{
    [AbpAuthorize(AppPermissions.Pages_CostCenters)]
    public class CostCentersAppService : CentralizeAppServiceBase, ICostCentersAppService
    {
        private readonly IRepository<CostCenter, Guid> _costCenterRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly ICostCentersExcelExporter _costCentersExcelExporter;

        public CostCentersAppService(
            IRepository<CostCenter, Guid> costCenterRepository,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            ICostCentersExcelExporter costCentersExcelExporter)
        {
            _costCenterRepository = costCenterRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _costCentersExcelExporter = costCentersExcelExporter;

        }

        public virtual async Task<PagedResultDto<GetCostCenterForViewDto>> GetAll(GetAllCostCentersInput input)
        {

            var filteredCostCenters = _costCenterRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ControllingArea.Contains(input.Filter) || e.CostCenterName.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.ActState.Contains(input.Filter) || e.CostCenterCode.Contains(input.Filter) || e.CostCenterShort.Contains(input.Filter) || e.DepartmentName.Contains(input.Filter) || e.Period.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ControllingAreaFilter), e => e.ControllingArea.Contains(input.ControllingAreaFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterNameFilter), e => e.CostCenterName.Contains(input.CostCenterNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ActStateFilter), e => e.ActState.Contains(input.ActStateFilter))
                        .WhereIf(input.IsActiveFilter.HasValue && input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterCodeFilter), e => e.CostCenterCode.Contains(input.CostCenterCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterShortFilter), e => e.CostCenterShort.Contains(input.CostCenterShortFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DepartmentNameFilter), e => e.DepartmentName.Contains(input.DepartmentNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PeriodFilter), e => e.Period.Contains(input.PeriodFilter));

            var pagedAndFilteredCostCenters = filteredCostCenters
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var costCenters = from o in pagedAndFilteredCostCenters
                              select new
                              {

                                  o.ControllingArea,
                                  o.CostCenterName,
                                  o.Description,
                                  o.IsActive,
                                  o.CostCenterCode,
                                  o.DepartmentName,
                                  o.Period,
                                  Id = o.Id
                              };

            var totalCount = await filteredCostCenters.CountAsync();

            var dbList = await costCenters.ToListAsync();
            var results = new List<GetCostCenterForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetCostCenterForViewDto()
                {
                    CostCenter = new CostCenterDto
                    {

                        ControllingArea = o.ControllingArea,
                        CostCenterName = o.CostCenterName,
                        Description = o.Description,
                        IsActive = o.IsActive,
                        CostCenterCode = o.CostCenterCode,
                        DepartmentName = o.DepartmentName,
                        Period = o.Period,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetCostCenterForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetCostCenterForViewDto> GetCostCenterForView(Guid id)
        {
            var costCenter = await _costCenterRepository.GetAsync(id);

            var output = new GetCostCenterForViewDto { CostCenter = ObjectMapper.Map<CostCenterDto>(costCenter) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_CostCenters_Edit)]
        public virtual async Task<GetCostCenterForEditOutput> GetCostCenterForEdit(EntityDto<Guid> input)
        {
            var costCenter = await _costCenterRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetCostCenterForEditOutput { CostCenter = ObjectMapper.Map<CreateOrEditCostCenterDto>(costCenter) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditCostCenterDto input)
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

        [AbpAuthorize(AppPermissions.Pages_CostCenters_Create)]
        protected virtual async Task Create(CreateOrEditCostCenterDto input)
        {
            var costCenter = ObjectMapper.Map<CostCenter>(input);

            if (AbpSession.TenantId != null)
            {
                costCenter.TenantId = (int?)AbpSession.TenantId;
            }

            await _costCenterRepository.InsertAsync(costCenter);

        }

        [AbpAuthorize(AppPermissions.Pages_CostCenters_Edit)]
        protected virtual async Task Update(CreateOrEditCostCenterDto input)
        {
            var costCenter = await _costCenterRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, costCenter);

        }

        [AbpAuthorize(AppPermissions.Pages_CostCenters_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _costCenterRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetCostCentersToExcel(GetAllCostCentersForExcelInput input)
        {

            var filteredCostCenters = _costCenterRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ControllingArea.Contains(input.Filter) || e.CostCenterName.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.ActState.Contains(input.Filter) || e.CostCenterCode.Contains(input.Filter) || e.CostCenterShort.Contains(input.Filter) || e.DepartmentName.Contains(input.Filter) || e.Period.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ControllingAreaFilter), e => e.ControllingArea.Contains(input.ControllingAreaFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterNameFilter), e => e.CostCenterName.Contains(input.CostCenterNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ActStateFilter), e => e.ActState.Contains(input.ActStateFilter))
                        .WhereIf(input.IsActiveFilter.HasValue && input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterCodeFilter), e => e.CostCenterCode.Contains(input.CostCenterCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterShortFilter), e => e.CostCenterShort.Contains(input.CostCenterShortFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DepartmentNameFilter), e => e.DepartmentName.Contains(input.DepartmentNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PeriodFilter), e => e.Period.Contains(input.PeriodFilter));

            var query = (from o in filteredCostCenters
                         select new GetCostCenterForViewDto()
                         {
                             CostCenter = new CostCenterDto
                             {
                                 ControllingArea = o.ControllingArea,
                                 CostCenterName = o.CostCenterName,
                                 Description = o.Description,
                                 IsActive = o.IsActive,
                                 CostCenterCode = o.CostCenterCode,
                                 DepartmentName = o.DepartmentName,
                                 Period = o.Period,
                                 Id = o.Id
                             }
                         });

            var costCenterListDtos = await query.ToListAsync();

            return _costCentersExcelExporter.ExportToFile(costCenterListDtos);
        }

        public virtual async Task<GetCostCenterForViewDto> GetCostCenterFromDepartmentId(long id)
        {
            var organizationUnit = await _organizationUnitRepository.GetAsync(id);

            var costCenter = await _costCenterRepository.FirstOrDefaultAsync(x => x.DepartmentName == organizationUnit.DisplayName);

            var output = new GetCostCenterForViewDto { CostCenter = ObjectMapper.Map<CostCenterDto>(costCenter) };

            return output;
        }

    }
}