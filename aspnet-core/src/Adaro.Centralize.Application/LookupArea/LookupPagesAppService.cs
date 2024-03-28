using Adaro.Centralize.SAPConnector;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Adaro.Centralize.LookupArea.Exporting;
using Adaro.Centralize.LookupArea.Dtos;
using Adaro.Centralize.Dto;
using Abp.Application.Services.Dto;
using Adaro.Centralize.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.LookupArea
{
    [AbpAuthorize(AppPermissions.Pages_LookupPages)]
    public class LookupPagesAppService : CentralizeAppServiceBase, ILookupPagesAppService
    {
        private readonly IRepository<LookupPage, Guid> _lookupPageRepository;
        private readonly ILookupPagesExcelExporter _lookupPagesExcelExporter;
        private readonly IRepository<CostCenter, Guid> _lookup_costCenterRepository;

        public LookupPagesAppService(IRepository<LookupPage, Guid> lookupPageRepository, ILookupPagesExcelExporter lookupPagesExcelExporter, IRepository<CostCenter, Guid> lookup_costCenterRepository)
        {
            _lookupPageRepository = lookupPageRepository;
            _lookupPagesExcelExporter = lookupPagesExcelExporter;
            _lookup_costCenterRepository = lookup_costCenterRepository;

        }

        public virtual async Task<PagedResultDto<GetLookupPageForViewDto>> GetAll(GetAllLookupPagesInput input)
        {

            var filteredLookupPages = _lookupPageRepository.GetAll()
                        .Include(e => e.CostCenterFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.LookupName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LookupNameFilter), e => e.LookupName.Contains(input.LookupNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterDisplayPropertyFilter), e => string.Format("{0} {1} {2}", e.CostCenterFk == null || e.CostCenterFk.ControllingArea == null ? "" : e.CostCenterFk.ControllingArea.ToString()
, e.CostCenterFk == null || e.CostCenterFk.CostCenterName == null ? "" : e.CostCenterFk.CostCenterName.ToString()
, e.CostCenterFk == null || e.CostCenterFk.Description == null ? "" : e.CostCenterFk.Description.ToString()
) == input.CostCenterDisplayPropertyFilter);

            var pagedAndFilteredLookupPages = filteredLookupPages
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var lookupPages = from o in pagedAndFilteredLookupPages
                              join o1 in _lookup_costCenterRepository.GetAll() on o.CostCenterId equals o1.Id into j1
                              from s1 in j1.DefaultIfEmpty()

                              select new
                              {

                                  o.LookupName,
                                  Id = o.Id,
                                  CostCenterDisplayProperty = string.Format("{0} {1} {2}", s1 == null || s1.ControllingArea == null ? "" : s1.ControllingArea.ToString()
              , s1 == null || s1.CostCenterName == null ? "" : s1.CostCenterName.ToString()
              , s1 == null || s1.Description == null ? "" : s1.Description.ToString()
              )
                              };

            var totalCount = await filteredLookupPages.CountAsync();

            var dbList = await lookupPages.ToListAsync();
            var results = new List<GetLookupPageForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetLookupPageForViewDto()
                {
                    LookupPage = new LookupPageDto
                    {

                        LookupName = o.LookupName,
                        Id = o.Id,
                    },
                    CostCenterDisplayProperty = o.CostCenterDisplayProperty
                };

                results.Add(res);
            }

            return new PagedResultDto<GetLookupPageForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetLookupPageForViewDto> GetLookupPageForView(Guid id)
        {
            var lookupPage = await _lookupPageRepository.GetAsync(id);

            var output = new GetLookupPageForViewDto { LookupPage = ObjectMapper.Map<LookupPageDto>(lookupPage) };

            if (output.LookupPage.CostCenterId != null)
            {
                var _lookupCostCenter = await _lookup_costCenterRepository.FirstOrDefaultAsync((Guid)output.LookupPage.CostCenterId);
                output.CostCenterDisplayProperty = string.Format("{0} {1} {2}", _lookupCostCenter.ControllingArea, _lookupCostCenter.CostCenterName, _lookupCostCenter.Description);
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_LookupPages_Edit)]
        public virtual async Task<GetLookupPageForEditOutput> GetLookupPageForEdit(EntityDto<Guid> input)
        {
            var lookupPage = await _lookupPageRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetLookupPageForEditOutput { LookupPage = ObjectMapper.Map<CreateOrEditLookupPageDto>(lookupPage) };

            if (output.LookupPage.CostCenterId != null)
            {
                var _lookupCostCenter = await _lookup_costCenterRepository.FirstOrDefaultAsync((Guid)output.LookupPage.CostCenterId);
                output.CostCenterDisplayProperty = string.Format("{0} {1} {2}", _lookupCostCenter.ControllingArea, _lookupCostCenter.CostCenterName, _lookupCostCenter.Description);
            }

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditLookupPageDto input)
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

        [AbpAuthorize(AppPermissions.Pages_LookupPages_Create)]
        protected virtual async Task Create(CreateOrEditLookupPageDto input)
        {
            var lookupPage = ObjectMapper.Map<LookupPage>(input);

            if (AbpSession.TenantId != null)
            {
                lookupPage.TenantId = (int?)AbpSession.TenantId;
            }

            await _lookupPageRepository.InsertAsync(lookupPage);

        }

        [AbpAuthorize(AppPermissions.Pages_LookupPages_Edit)]
        protected virtual async Task Update(CreateOrEditLookupPageDto input)
        {
            var lookupPage = await _lookupPageRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, lookupPage);

        }

        [AbpAuthorize(AppPermissions.Pages_LookupPages_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _lookupPageRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetLookupPagesToExcel(GetAllLookupPagesForExcelInput input)
        {

            var filteredLookupPages = _lookupPageRepository.GetAll()
                        .Include(e => e.CostCenterFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.LookupName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LookupNameFilter), e => e.LookupName.Contains(input.LookupNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterDisplayPropertyFilter), e => string.Format("{0} {1} {2}", e.CostCenterFk == null || e.CostCenterFk.ControllingArea == null ? "" : e.CostCenterFk.ControllingArea.ToString()
, e.CostCenterFk == null || e.CostCenterFk.CostCenterName == null ? "" : e.CostCenterFk.CostCenterName.ToString()
, e.CostCenterFk == null || e.CostCenterFk.Description == null ? "" : e.CostCenterFk.Description.ToString()
) == input.CostCenterDisplayPropertyFilter);

            var query = (from o in filteredLookupPages
                         join o1 in _lookup_costCenterRepository.GetAll() on o.CostCenterId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new GetLookupPageForViewDto()
                         {
                             LookupPage = new LookupPageDto
                             {
                                 LookupName = o.LookupName,
                                 Id = o.Id
                             },
                             CostCenterDisplayProperty = string.Format("{0} {1} {2}", s1 == null || s1.ControllingArea == null ? "" : s1.ControllingArea.ToString()
, s1 == null || s1.CostCenterName == null ? "" : s1.CostCenterName.ToString()
, s1 == null || s1.Description == null ? "" : s1.Description.ToString()
)
                         });

            var lookupPageListDtos = await query.ToListAsync();

            return _lookupPagesExcelExporter.ExportToFile(lookupPageListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_LookupPages)]
        public async Task<PagedResultDto<LookupPageCostCenterLookupTableDto>> GetAllCostCenterForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_costCenterRepository.GetAll();

            if (!string.IsNullOrEmpty(input.Filter))
            {
                query = query.Where( e => 
                e.CostCenterCode.Contains(input.Filter) ||
                e.CostCenterName.Contains(input.Filter) ||
                e.DepartmentName.Contains(input.Filter)
               );
            }

            switch (input.Sorting)
            {
                case "costCenterCode ASC":
                    query = query.OrderBy(e => e.CostCenterCode);
                    break;
                case "costCenterCode DESC":
                    query = query.OrderByDescending(e => e.CostCenterCode);
                    break;
                case "costCenterName ASC":
                    query = query.OrderBy(e => e.CostCenterName);
                    break;
                case "costCenterName DESC":
                    query = query.OrderByDescending(e => e.CostCenterName);
                    break;
                case "departmentName ASC":
                    query = query.OrderBy(e => e.DepartmentName);
                    break;
                case "departmentName DESC":
                    query = query.OrderByDescending(e => e.DepartmentName);
                    break;
                default:
                    query = query.OrderBy(e => e.CostCenterCode);
                    break;
            }

            var totalCount = await query.CountAsync();

            var costCenterList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<LookupPageCostCenterLookupTableDto>();
            foreach (var costCenter in costCenterList)
            {
                lookupTableDtoList.Add(new LookupPageCostCenterLookupTableDto
                {
                    Id = costCenter.Id.ToString(),
                    DisplayName = string.Format("{0}-{1}", costCenter.CostCenterCode, costCenter.CostCenterName),
                    CostCenterCode = costCenter.CostCenterCode,
                    CostCenterName = costCenter.CostCenterName,
                    DepartmentName = costCenter.DepartmentName
                });
            }

            return new PagedResultDto<LookupPageCostCenterLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}