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
    [AbpAuthorize(AppPermissions.Pages_MaterialGroups)]
    public class MaterialGroupsAppService : CentralizeAppServiceBase, IMaterialGroupsAppService
    {
        private readonly IRepository<MaterialGroup, Guid> _materialGroupRepository;
        private readonly IMaterialGroupsExcelExporter _materialGroupsExcelExporter;

        public MaterialGroupsAppService(IRepository<MaterialGroup, Guid> materialGroupRepository, IMaterialGroupsExcelExporter materialGroupsExcelExporter)
        {
            _materialGroupRepository = materialGroupRepository;
            _materialGroupsExcelExporter = materialGroupsExcelExporter;

        }

        public virtual async Task<PagedResultDto<GetMaterialGroupForViewDto>> GetAll(GetAllMaterialGroupsInput input)
        {

            var filteredMaterialGroups = _materialGroupRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.MaterialGroupCode.Contains(input.Filter) || e.MaterialGroupName.Contains(input.Filter) || e.MaterialGroupDescription.Contains(input.Filter) || e.Language.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialGroupCodeFilter), e => e.MaterialGroupCode.Contains(input.MaterialGroupCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialGroupNameFilter), e => e.MaterialGroupName.Contains(input.MaterialGroupNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialGroupDescriptionFilter), e => e.MaterialGroupDescription.Contains(input.MaterialGroupDescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LanguageFilter), e => e.Language.Contains(input.LanguageFilter));

            var pagedAndFilteredMaterialGroups = filteredMaterialGroups
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var materialGroups = from o in pagedAndFilteredMaterialGroups
                                 select new
                                 {

                                     o.MaterialGroupCode,
                                     o.MaterialGroupName,
                                     o.MaterialGroupDescription,
                                     o.Language,
                                     Id = o.Id
                                 };

            var totalCount = await filteredMaterialGroups.CountAsync();

            var dbList = await materialGroups.ToListAsync();
            var results = new List<GetMaterialGroupForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetMaterialGroupForViewDto()
                {
                    MaterialGroup = new MaterialGroupDto
                    {

                        MaterialGroupCode = o.MaterialGroupCode,
                        MaterialGroupName = o.MaterialGroupName,
                        MaterialGroupDescription = o.MaterialGroupDescription,
                        Language = o.Language,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetMaterialGroupForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetMaterialGroupForViewDto> GetMaterialGroupForView(Guid id)
        {
            var materialGroup = await _materialGroupRepository.GetAsync(id);

            var output = new GetMaterialGroupForViewDto { MaterialGroup = ObjectMapper.Map<MaterialGroupDto>(materialGroup) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_MaterialGroups_Edit)]
        public virtual async Task<GetMaterialGroupForEditOutput> GetMaterialGroupForEdit(EntityDto<Guid> input)
        {
            var materialGroup = await _materialGroupRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetMaterialGroupForEditOutput { MaterialGroup = ObjectMapper.Map<CreateOrEditMaterialGroupDto>(materialGroup) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditMaterialGroupDto input)
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

        [AbpAuthorize(AppPermissions.Pages_MaterialGroups_Create)]
        protected virtual async Task Create(CreateOrEditMaterialGroupDto input)
        {
            var materialGroup = ObjectMapper.Map<MaterialGroup>(input);

            if (AbpSession.TenantId != null)
            {
                materialGroup.TenantId = (int?)AbpSession.TenantId;
            }

            await _materialGroupRepository.InsertAsync(materialGroup);

        }

        [AbpAuthorize(AppPermissions.Pages_MaterialGroups_Edit)]
        protected virtual async Task Update(CreateOrEditMaterialGroupDto input)
        {
            var materialGroup = await _materialGroupRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, materialGroup);

        }

        [AbpAuthorize(AppPermissions.Pages_MaterialGroups_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _materialGroupRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetMaterialGroupsToExcel(GetAllMaterialGroupsForExcelInput input)
        {

            var filteredMaterialGroups = _materialGroupRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.MaterialGroupCode.Contains(input.Filter) || e.MaterialGroupName.Contains(input.Filter) || e.MaterialGroupDescription.Contains(input.Filter) || e.Language.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialGroupCodeFilter), e => e.MaterialGroupCode.Contains(input.MaterialGroupCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialGroupNameFilter), e => e.MaterialGroupName.Contains(input.MaterialGroupNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialGroupDescriptionFilter), e => e.MaterialGroupDescription.Contains(input.MaterialGroupDescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LanguageFilter), e => e.Language.Contains(input.LanguageFilter));

            var query = (from o in filteredMaterialGroups
                         select new GetMaterialGroupForViewDto()
                         {
                             MaterialGroup = new MaterialGroupDto
                             {
                                 MaterialGroupCode = o.MaterialGroupCode,
                                 MaterialGroupName = o.MaterialGroupName,
                                 MaterialGroupDescription = o.MaterialGroupDescription,
                                 Language = o.Language,
                                 Id = o.Id
                             }
                         });

            var materialGroupListDtos = await query.ToListAsync();

            return _materialGroupsExcelExporter.ExportToFile(materialGroupListDtos);
        }

    }
}