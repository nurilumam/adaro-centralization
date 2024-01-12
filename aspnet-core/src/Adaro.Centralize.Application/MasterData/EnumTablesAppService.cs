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
    [AbpAuthorize(AppPermissions.Pages_EnumTables)]
    public class EnumTablesAppService : CentralizeAppServiceBase, IEnumTablesAppService
    {
        private readonly IRepository<EnumTable, Guid> _enumTableRepository;
        private readonly IEnumTablesExcelExporter _enumTablesExcelExporter;

        public EnumTablesAppService(IRepository<EnumTable, Guid> enumTableRepository, IEnumTablesExcelExporter enumTablesExcelExporter)
        {
            _enumTableRepository = enumTableRepository;
            _enumTablesExcelExporter = enumTablesExcelExporter;

        }

        public virtual async Task<PagedResultDto<GetEnumTableForViewDto>> GetAll(GetAllEnumTablesInput input)
        {

            var filteredEnumTables = _enumTableRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.EnumCode.Contains(input.Filter) || e.EnumValue.Contains(input.Filter) || e.EnumLabel.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EnumCodeFilter), e => e.EnumCode.Contains(input.EnumCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EnumValueFilter), e => e.EnumValue.Contains(input.EnumValueFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EnumLabelFilter), e => e.EnumLabel.Contains(input.EnumLabelFilter));

            var pagedAndFilteredEnumTables = filteredEnumTables
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var enumTables = from o in pagedAndFilteredEnumTables
                             select new
                             {

                                 o.EnumCode,
                                 o.EnumValue,
                                 o.EnumLabel,
                                 Id = o.Id
                             };

            var totalCount = await filteredEnumTables.CountAsync();

            var dbList = await enumTables.ToListAsync();
            var results = new List<GetEnumTableForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetEnumTableForViewDto()
                {
                    EnumTable = new EnumTableDto
                    {

                        EnumCode = o.EnumCode,
                        EnumValue = o.EnumValue,
                        EnumLabel = o.EnumLabel,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetEnumTableForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetEnumTableForViewDto> GetEnumTableForView(Guid id)
        {
            var enumTable = await _enumTableRepository.GetAsync(id);

            var output = new GetEnumTableForViewDto { EnumTable = ObjectMapper.Map<EnumTableDto>(enumTable) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_EnumTables_Edit)]
        public virtual async Task<GetEnumTableForEditOutput> GetEnumTableForEdit(EntityDto<Guid> input)
        {
            var enumTable = await _enumTableRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEnumTableForEditOutput { EnumTable = ObjectMapper.Map<CreateOrEditEnumTableDto>(enumTable) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditEnumTableDto input)
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

        [AbpAuthorize(AppPermissions.Pages_EnumTables_Create)]
        protected virtual async Task Create(CreateOrEditEnumTableDto input)
        {
            var enumTable = ObjectMapper.Map<EnumTable>(input);

            if (AbpSession.TenantId != null)
            {
                enumTable.TenantId = (int?)AbpSession.TenantId;
            }

            await _enumTableRepository.InsertAsync(enumTable);

        }

        [AbpAuthorize(AppPermissions.Pages_EnumTables_Edit)]
        protected virtual async Task Update(CreateOrEditEnumTableDto input)
        {
            var enumTable = await _enumTableRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, enumTable);

        }

        [AbpAuthorize(AppPermissions.Pages_EnumTables_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _enumTableRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetEnumTablesToExcel(GetAllEnumTablesForExcelInput input)
        {

            var filteredEnumTables = _enumTableRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.EnumCode.Contains(input.Filter) || e.EnumValue.Contains(input.Filter) || e.EnumLabel.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EnumCodeFilter), e => e.EnumCode.Contains(input.EnumCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EnumValueFilter), e => e.EnumValue.Contains(input.EnumValueFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EnumLabelFilter), e => e.EnumLabel.Contains(input.EnumLabelFilter));

            var query = (from o in filteredEnumTables
                         select new GetEnumTableForViewDto()
                         {
                             EnumTable = new EnumTableDto
                             {
                                 EnumCode = o.EnumCode,
                                 EnumValue = o.EnumValue,
                                 EnumLabel = o.EnumLabel,
                                 Id = o.Id
                             }
                         });

            var enumTableListDtos = await query.ToListAsync();

            return _enumTablesExcelExporter.ExportToFile(enumTableListDtos);
        }

    }
}