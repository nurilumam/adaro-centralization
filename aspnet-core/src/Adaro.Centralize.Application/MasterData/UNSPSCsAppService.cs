using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Adaro.Centralize.MasterData.Exporting;
using Adaro.Centralize.MasterData.Dtos;
using Adaro.Centralize.Dto;
using Abp.Application.Services.Dto;
using Adaro.Centralize.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.MasterData
{
    [AbpAuthorize(AppPermissions.Pages_UNSPSCs)]
    public class UNSPSCsAppService : CentralizeAppServiceBase, IUNSPSCsAppService
    {
        private readonly IRepository<UNSPSC, Guid> _unspscRepository;
        private readonly IUNSPSCsExcelExporter _unspsCsExcelExporter;

        public UNSPSCsAppService(IRepository<UNSPSC, Guid> unspscRepository, IUNSPSCsExcelExporter unspsCsExcelExporter)
        {
            _unspscRepository = unspscRepository;
            _unspsCsExcelExporter = unspsCsExcelExporter;

        }

        public virtual async Task<PagedResultDto<GetUNSPSCForViewDto>> GetAll(GetAllUNSPSCsInput input)
        {

            var filteredUNSPSCs = _unspscRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.UNSPSC_Code.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.AccountCode.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UNSPSC_CodeFilter), e => e.UNSPSC_Code.Contains(input.UNSPSC_CodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AccountCodeFilter), e => e.AccountCode.Contains(input.AccountCodeFilter));

            var pagedAndFilteredUNSPSCs = filteredUNSPSCs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var unspsCs = from o in pagedAndFilteredUNSPSCs
                          select new
                          {

                              o.UNSPSC_Code,
                              o.Description,
                              o.AccountCode,
                              Id = o.Id
                          };

            var totalCount = await filteredUNSPSCs.CountAsync();

            var dbList = await unspsCs.ToListAsync();
            var results = new List<GetUNSPSCForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetUNSPSCForViewDto()
                {
                    UNSPSC = new UNSPSCDto
                    {

                        UNSPSC_Code = o.UNSPSC_Code,
                        Description = o.Description,
                        AccountCode = o.AccountCode,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetUNSPSCForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetUNSPSCForViewDto> GetUNSPSCForView(Guid id)
        {
            var unspsc = await _unspscRepository.GetAsync(id);

            var output = new GetUNSPSCForViewDto { UNSPSC = ObjectMapper.Map<UNSPSCDto>(unspsc) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_UNSPSCs_Edit)]
        public virtual async Task<GetUNSPSCForEditOutput> GetUNSPSCForEdit(EntityDto<Guid> input)
        {
            var unspsc = await _unspscRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetUNSPSCForEditOutput { UNSPSC = ObjectMapper.Map<CreateOrEditUNSPSCDto>(unspsc) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditUNSPSCDto input)
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

        [AbpAuthorize(AppPermissions.Pages_UNSPSCs_Create)]
        protected virtual async Task Create(CreateOrEditUNSPSCDto input)
        {
            var unspsc = ObjectMapper.Map<UNSPSC>(input);

            if (AbpSession.TenantId != null)
            {
                unspsc.TenantId = (int?)AbpSession.TenantId;
            }

            await _unspscRepository.InsertAsync(unspsc);

        }

        [AbpAuthorize(AppPermissions.Pages_UNSPSCs_Edit)]
        protected virtual async Task Update(CreateOrEditUNSPSCDto input)
        {
            var unspsc = await _unspscRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, unspsc);

        }

        [AbpAuthorize(AppPermissions.Pages_UNSPSCs_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _unspscRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetUNSPSCsToExcel(GetAllUNSPSCsForExcelInput input)
        {

            var filteredUNSPSCs = _unspscRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.UNSPSC_Code.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.AccountCode.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UNSPSC_CodeFilter), e => e.UNSPSC_Code.Contains(input.UNSPSC_CodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AccountCodeFilter), e => e.AccountCode.Contains(input.AccountCodeFilter));

            var query = (from o in filteredUNSPSCs
                         select new GetUNSPSCForViewDto()
                         {
                             UNSPSC = new UNSPSCDto
                             {
                                 UNSPSC_Code = o.UNSPSC_Code,
                                 Description = o.Description,
                                 AccountCode = o.AccountCode,
                                 Id = o.Id
                             }
                         });

            var unspscListDtos = await query.ToListAsync();

            return _unspsCsExcelExporter.ExportToFile(unspscListDtos);
        }

    }
}