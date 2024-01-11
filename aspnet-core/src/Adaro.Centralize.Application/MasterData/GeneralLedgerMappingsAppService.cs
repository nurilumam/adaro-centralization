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
    [AbpAuthorize(AppPermissions.Pages_GeneralLedgerMappings)]
    public class GeneralLedgerMappingsAppService : CentralizeAppServiceBase, IGeneralLedgerMappingsAppService
    {
        private readonly IRepository<GeneralLedgerMapping, Guid> _generalLedgerMappingRepository;
        private readonly IGeneralLedgerMappingsExcelExporter _generalLedgerMappingsExcelExporter;

        public GeneralLedgerMappingsAppService(IRepository<GeneralLedgerMapping, Guid> generalLedgerMappingRepository, IGeneralLedgerMappingsExcelExporter generalLedgerMappingsExcelExporter)
        {
            _generalLedgerMappingRepository = generalLedgerMappingRepository;
            _generalLedgerMappingsExcelExporter = generalLedgerMappingsExcelExporter;

        }

        public virtual async Task<PagedResultDto<GetGeneralLedgerMappingForViewDto>> GetAll(GetAllGeneralLedgerMappingsInput input)
        {

            var filteredGeneralLedgerMappings = _generalLedgerMappingRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.GLAccount.Contains(input.Filter) || e.GLAccountDescription.Contains(input.Filter) || e.MappingType.Contains(input.Filter) || e.ValuationClass.Contains(input.Filter) || e.ValuationClassDescription.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GLAccountFilter), e => e.GLAccount.Contains(input.GLAccountFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GLAccountDescriptionFilter), e => e.GLAccountDescription.Contains(input.GLAccountDescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MappingTypeFilter), e => e.MappingType.Contains(input.MappingTypeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ValuationClassFilter), e => e.ValuationClass.Contains(input.ValuationClassFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ValuationClassDescriptionFilter), e => e.ValuationClassDescription.Contains(input.ValuationClassDescriptionFilter));

            var pagedAndFilteredGeneralLedgerMappings = filteredGeneralLedgerMappings
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var generalLedgerMappings = from o in pagedAndFilteredGeneralLedgerMappings
                                        select new
                                        {

                                            o.GLAccount,
                                            o.GLAccountDescription,
                                            o.MappingType,
                                            o.ValuationClass,
                                            o.ValuationClassDescription,
                                            Id = o.Id
                                        };

            var totalCount = await filteredGeneralLedgerMappings.CountAsync();

            var dbList = await generalLedgerMappings.ToListAsync();
            var results = new List<GetGeneralLedgerMappingForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetGeneralLedgerMappingForViewDto()
                {
                    GeneralLedgerMapping = new GeneralLedgerMappingDto
                    {

                        GLAccount = o.GLAccount,
                        GLAccountDescription = o.GLAccountDescription,
                        MappingType = o.MappingType,
                        ValuationClass = o.ValuationClass,
                        ValuationClassDescription = o.ValuationClassDescription,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetGeneralLedgerMappingForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetGeneralLedgerMappingForViewDto> GetGeneralLedgerMappingForView(Guid id)
        {
            var generalLedgerMapping = await _generalLedgerMappingRepository.GetAsync(id);

            var output = new GetGeneralLedgerMappingForViewDto { GeneralLedgerMapping = ObjectMapper.Map<GeneralLedgerMappingDto>(generalLedgerMapping) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_GeneralLedgerMappings_Edit)]
        public virtual async Task<GetGeneralLedgerMappingForEditOutput> GetGeneralLedgerMappingForEdit(EntityDto<Guid> input)
        {
            var generalLedgerMapping = await _generalLedgerMappingRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetGeneralLedgerMappingForEditOutput { GeneralLedgerMapping = ObjectMapper.Map<CreateOrEditGeneralLedgerMappingDto>(generalLedgerMapping) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditGeneralLedgerMappingDto input)
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

        [AbpAuthorize(AppPermissions.Pages_GeneralLedgerMappings_Create)]
        protected virtual async Task Create(CreateOrEditGeneralLedgerMappingDto input)
        {
            var generalLedgerMapping = ObjectMapper.Map<GeneralLedgerMapping>(input);

            if (AbpSession.TenantId != null)
            {
                generalLedgerMapping.TenantId = (int?)AbpSession.TenantId;
            }

            await _generalLedgerMappingRepository.InsertAsync(generalLedgerMapping);

        }

        [AbpAuthorize(AppPermissions.Pages_GeneralLedgerMappings_Edit)]
        protected virtual async Task Update(CreateOrEditGeneralLedgerMappingDto input)
        {
            var generalLedgerMapping = await _generalLedgerMappingRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, generalLedgerMapping);

        }

        [AbpAuthorize(AppPermissions.Pages_GeneralLedgerMappings_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _generalLedgerMappingRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetGeneralLedgerMappingsToExcel(GetAllGeneralLedgerMappingsForExcelInput input)
        {

            var filteredGeneralLedgerMappings = _generalLedgerMappingRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.GLAccount.Contains(input.Filter) || e.GLAccountDescription.Contains(input.Filter) || e.MappingType.Contains(input.Filter) || e.ValuationClass.Contains(input.Filter) || e.ValuationClassDescription.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GLAccountFilter), e => e.GLAccount.Contains(input.GLAccountFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GLAccountDescriptionFilter), e => e.GLAccountDescription.Contains(input.GLAccountDescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MappingTypeFilter), e => e.MappingType.Contains(input.MappingTypeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ValuationClassFilter), e => e.ValuationClass.Contains(input.ValuationClassFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ValuationClassDescriptionFilter), e => e.ValuationClassDescription.Contains(input.ValuationClassDescriptionFilter));

            var query = (from o in filteredGeneralLedgerMappings
                         select new GetGeneralLedgerMappingForViewDto()
                         {
                             GeneralLedgerMapping = new GeneralLedgerMappingDto
                             {
                                 GLAccount = o.GLAccount,
                                 GLAccountDescription = o.GLAccountDescription,
                                 MappingType = o.MappingType,
                                 ValuationClass = o.ValuationClass,
                                 ValuationClassDescription = o.ValuationClassDescription,
                                 Id = o.Id
                             }
                         });

            var generalLedgerMappingListDtos = await query.ToListAsync();

            return _generalLedgerMappingsExcelExporter.ExportToFile(generalLedgerMappingListDtos);
        }

    }
}