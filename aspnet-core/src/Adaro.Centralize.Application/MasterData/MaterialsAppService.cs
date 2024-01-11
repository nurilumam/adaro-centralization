using Adaro.Centralize.MasterData;
using Adaro.Centralize.MasterData;
using Adaro.Centralize.MasterData;

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
    [AbpAuthorize(AppPermissions.Pages_Materials)]
    public class MaterialsAppService : CentralizeAppServiceBase, IMaterialsAppService
    {
        private readonly IRepository<Material, Guid> _materialRepository;
        private readonly IMaterialsExcelExporter _materialsExcelExporter;
        private readonly IRepository<MaterialGroup, Guid> _lookup_materialGroupRepository;
        private readonly IRepository<UNSPSC, Guid> _lookup_unspscRepository;
        private readonly IRepository<GeneralLedgerMapping, Guid> _lookup_generalLedgerMappingRepository;

        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;

        public MaterialsAppService(IRepository<Material, Guid> materialRepository, IMaterialsExcelExporter materialsExcelExporter, IRepository<MaterialGroup, Guid> lookup_materialGroupRepository, IRepository<UNSPSC, Guid> lookup_unspscRepository, IRepository<GeneralLedgerMapping, Guid> lookup_generalLedgerMappingRepository, ITempFileCacheManager tempFileCacheManager, IBinaryObjectManager binaryObjectManager)
        {
            _materialRepository = materialRepository;
            _materialsExcelExporter = materialsExcelExporter;
            _lookup_materialGroupRepository = lookup_materialGroupRepository;
            _lookup_unspscRepository = lookup_unspscRepository;
            _lookup_generalLedgerMappingRepository = lookup_generalLedgerMappingRepository;

            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;

        }

        public virtual async Task<PagedResultDto<GetMaterialForViewDto>> GetAll(GetAllMaterialsInput input)
        {

            var filteredMaterials = _materialRepository.GetAll()
                        .Include(e => e.MaterialGroupFk)
                        .Include(e => e.UNSPSCFk)
                        .Include(e => e.GeneralLedgerMappingFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.MaterialNo.Contains(input.Filter) || e.MaterialName.Contains(input.Filter) || e.MaterialNameSAP.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.Size.Contains(input.Filter) || e.UoM.Contains(input.Filter) || e.Brand.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialNoFilter), e => e.MaterialNo.Contains(input.MaterialNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialNameFilter), e => e.MaterialName.Contains(input.MaterialNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialNameSAPFilter), e => e.MaterialNameSAP.Contains(input.MaterialNameSAPFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SizeFilter), e => e.Size.Contains(input.SizeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UoMFilter), e => e.UoM.Contains(input.UoMFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BrandFilter), e => e.Brand.Contains(input.BrandFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialGroupDisplayPropertyFilter), e => string.Format("{0} {1}", e.MaterialGroupFk == null || e.MaterialGroupFk.MaterialGroupCode == null ? "" : e.MaterialGroupFk.MaterialGroupCode.ToString()
, e.MaterialGroupFk == null || e.MaterialGroupFk.MaterialGroupDescription == null ? "" : e.MaterialGroupFk.MaterialGroupDescription.ToString()
) == input.MaterialGroupDisplayPropertyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UNSPSCDisplayPropertyFilter), e => string.Format("{0} {1}", e.UNSPSCFk == null || e.UNSPSCFk.UNSPSC_Code == null ? "" : e.UNSPSCFk.UNSPSC_Code.ToString()
, e.UNSPSCFk == null || e.UNSPSCFk.Description == null ? "" : e.UNSPSCFk.Description.ToString()
) == input.UNSPSCDisplayPropertyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GeneralLedgerMappingDisplayPropertyFilter), e => string.Format("{0} {1} {2} {3}", e.GeneralLedgerMappingFk == null || e.GeneralLedgerMappingFk.GLAccount == null ? "" : e.GeneralLedgerMappingFk.GLAccount.ToString()
, e.GeneralLedgerMappingFk == null || e.GeneralLedgerMappingFk.GLAccountDescription == null ? "" : e.GeneralLedgerMappingFk.GLAccountDescription.ToString()
, e.GeneralLedgerMappingFk == null || e.GeneralLedgerMappingFk.ValuationClass == null ? "" : e.GeneralLedgerMappingFk.ValuationClass.ToString()
, e.GeneralLedgerMappingFk == null || e.GeneralLedgerMappingFk.ValuationClassDescription == null ? "" : e.GeneralLedgerMappingFk.ValuationClassDescription.ToString()
) == input.GeneralLedgerMappingDisplayPropertyFilter);

            var pagedAndFilteredMaterials = filteredMaterials
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var materials = from o in pagedAndFilteredMaterials
                            join o1 in _lookup_materialGroupRepository.GetAll() on o.MaterialGroupId equals o1.Id into j1
                            from s1 in j1.DefaultIfEmpty()

                            join o2 in _lookup_unspscRepository.GetAll() on o.UNSPSCId equals o2.Id into j2
                            from s2 in j2.DefaultIfEmpty()

                            join o3 in _lookup_generalLedgerMappingRepository.GetAll() on o.GeneralLedgerMappingId equals o3.Id into j3
                            from s3 in j3.DefaultIfEmpty()

                            select new
                            {

                                o.MaterialNo,
                                o.MaterialName,
                                o.Description,
                                o.UoM,
                                o.ImageMain,
                                Id = o.Id,
                                MaterialGroupDisplayProperty = string.Format("{0} {1}", s1 == null || s1.MaterialGroupCode == null ? "" : s1.MaterialGroupCode.ToString()
            , s1 == null || s1.MaterialGroupDescription == null ? "" : s1.MaterialGroupDescription.ToString()
            ),
                                UNSPSCDisplayProperty = string.Format("{0} {1}", s2 == null || s2.UNSPSC_Code == null ? "" : s2.UNSPSC_Code.ToString()
            , s2 == null || s2.Description == null ? "" : s2.Description.ToString()
            ),
                                GeneralLedgerMappingDisplayProperty = string.Format("{0} {1} {2} {3}", s3 == null || s3.GLAccount == null ? "" : s3.GLAccount.ToString()
            , s3 == null || s3.GLAccountDescription == null ? "" : s3.GLAccountDescription.ToString()
            , s3 == null || s3.ValuationClass == null ? "" : s3.ValuationClass.ToString()
            , s3 == null || s3.ValuationClassDescription == null ? "" : s3.ValuationClassDescription.ToString()
            )
                            };

            var totalCount = await filteredMaterials.CountAsync();

            var dbList = await materials.ToListAsync();
            var results = new List<GetMaterialForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetMaterialForViewDto()
                {
                    Material = new MaterialDto
                    {

                        MaterialNo = o.MaterialNo,
                        MaterialName = o.MaterialName,
                        Description = o.Description,
                        UoM = o.UoM,
                        ImageMain = o.ImageMain,
                        Id = o.Id,
                    },
                    MaterialGroupDisplayProperty = o.MaterialGroupDisplayProperty,
                    UNSPSCDisplayProperty = o.UNSPSCDisplayProperty,
                    GeneralLedgerMappingDisplayProperty = o.GeneralLedgerMappingDisplayProperty
                };
                res.Material.ImageMainFileName = await GetBinaryFileName(o.ImageMain);

                results.Add(res);
            }

            return new PagedResultDto<GetMaterialForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetMaterialForViewDto> GetMaterialForView(Guid id)
        {
            var material = await _materialRepository.GetAsync(id);

            var output = new GetMaterialForViewDto { Material = ObjectMapper.Map<MaterialDto>(material) };

            if (output.Material.MaterialGroupId != null)
            {
                var _lookupMaterialGroup = await _lookup_materialGroupRepository.FirstOrDefaultAsync((Guid)output.Material.MaterialGroupId);
                output.MaterialGroupDisplayProperty = string.Format("{0} {1}", _lookupMaterialGroup.MaterialGroupCode, _lookupMaterialGroup.MaterialGroupDescription);
            }

            if (output.Material.UNSPSCId != null)
            {
                var _lookupUNSPSC = await _lookup_unspscRepository.FirstOrDefaultAsync((Guid)output.Material.UNSPSCId);
                output.UNSPSCDisplayProperty = string.Format("{0} {1}", _lookupUNSPSC.UNSPSC_Code, _lookupUNSPSC.Description);
            }

            if (output.Material.GeneralLedgerMappingId != null)
            {
                var _lookupGeneralLedgerMapping = await _lookup_generalLedgerMappingRepository.FirstOrDefaultAsync((Guid)output.Material.GeneralLedgerMappingId);
                output.GeneralLedgerMappingDisplayProperty = string.Format("{0} {1} {2} {3}", _lookupGeneralLedgerMapping.GLAccount, _lookupGeneralLedgerMapping.GLAccountDescription, _lookupGeneralLedgerMapping.ValuationClass, _lookupGeneralLedgerMapping.ValuationClassDescription);
            }

            output.Material.ImageMainFileName = await GetBinaryFileName(material.ImageMain);

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Materials_Edit)]
        public virtual async Task<GetMaterialForEditOutput> GetMaterialForEdit(EntityDto<Guid> input)
        {
            var material = await _materialRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetMaterialForEditOutput { Material = ObjectMapper.Map<CreateOrEditMaterialDto>(material) };

            if (output.Material.MaterialGroupId != null)
            {
                var _lookupMaterialGroup = await _lookup_materialGroupRepository.FirstOrDefaultAsync((Guid)output.Material.MaterialGroupId);
                output.MaterialGroupDisplayProperty = string.Format("{0} {1}", _lookupMaterialGroup.MaterialGroupCode, _lookupMaterialGroup.MaterialGroupDescription);
            }

            if (output.Material.UNSPSCId != null)
            {
                var _lookupUNSPSC = await _lookup_unspscRepository.FirstOrDefaultAsync((Guid)output.Material.UNSPSCId);
                output.UNSPSCDisplayProperty = string.Format("{0} {1}", _lookupUNSPSC.UNSPSC_Code, _lookupUNSPSC.Description);
            }

            if (output.Material.GeneralLedgerMappingId != null)
            {
                var _lookupGeneralLedgerMapping = await _lookup_generalLedgerMappingRepository.FirstOrDefaultAsync((Guid)output.Material.GeneralLedgerMappingId);
                output.GeneralLedgerMappingDisplayProperty = string.Format("{0} {1} {2} {3}", _lookupGeneralLedgerMapping.GLAccount, _lookupGeneralLedgerMapping.GLAccountDescription, _lookupGeneralLedgerMapping.ValuationClass, _lookupGeneralLedgerMapping.ValuationClassDescription);
            }

            output.ImageMainFileName = await GetBinaryFileName(material.ImageMain);

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditMaterialDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Materials_Create)]
        protected virtual async Task Create(CreateOrEditMaterialDto input)
        {
            var material = ObjectMapper.Map<Material>(input);

            if (AbpSession.TenantId != null)
            {
                material.TenantId = (int?)AbpSession.TenantId;
            }

            await _materialRepository.InsertAsync(material);
            material.ImageMain = await GetBinaryObjectFromCache(input.ImageMainToken);

        }

        [AbpAuthorize(AppPermissions.Pages_Materials_Edit)]
        protected virtual async Task Update(CreateOrEditMaterialDto input)
        {
            var material = await _materialRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, material);
            material.ImageMain = await GetBinaryObjectFromCache(input.ImageMainToken);

        }

        [AbpAuthorize(AppPermissions.Pages_Materials_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _materialRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetMaterialsToExcel(GetAllMaterialsForExcelInput input)
        {

            var filteredMaterials = _materialRepository.GetAll()
                        .Include(e => e.MaterialGroupFk)
                        .Include(e => e.UNSPSCFk)
                        .Include(e => e.GeneralLedgerMappingFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.MaterialNo.Contains(input.Filter) || e.MaterialName.Contains(input.Filter) || e.MaterialNameSAP.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.Size.Contains(input.Filter) || e.UoM.Contains(input.Filter) || e.Brand.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialNoFilter), e => e.MaterialNo.Contains(input.MaterialNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialNameFilter), e => e.MaterialName.Contains(input.MaterialNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialNameSAPFilter), e => e.MaterialNameSAP.Contains(input.MaterialNameSAPFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SizeFilter), e => e.Size.Contains(input.SizeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UoMFilter), e => e.UoM.Contains(input.UoMFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BrandFilter), e => e.Brand.Contains(input.BrandFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialGroupDisplayPropertyFilter), e => string.Format("{0} {1}", e.MaterialGroupFk == null || e.MaterialGroupFk.MaterialGroupCode == null ? "" : e.MaterialGroupFk.MaterialGroupCode.ToString()
, e.MaterialGroupFk == null || e.MaterialGroupFk.MaterialGroupDescription == null ? "" : e.MaterialGroupFk.MaterialGroupDescription.ToString()
) == input.MaterialGroupDisplayPropertyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UNSPSCDisplayPropertyFilter), e => string.Format("{0} {1}", e.UNSPSCFk == null || e.UNSPSCFk.UNSPSC_Code == null ? "" : e.UNSPSCFk.UNSPSC_Code.ToString()
, e.UNSPSCFk == null || e.UNSPSCFk.Description == null ? "" : e.UNSPSCFk.Description.ToString()
) == input.UNSPSCDisplayPropertyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GeneralLedgerMappingDisplayPropertyFilter), e => string.Format("{0} {1} {2} {3}", e.GeneralLedgerMappingFk == null || e.GeneralLedgerMappingFk.GLAccount == null ? "" : e.GeneralLedgerMappingFk.GLAccount.ToString()
, e.GeneralLedgerMappingFk == null || e.GeneralLedgerMappingFk.GLAccountDescription == null ? "" : e.GeneralLedgerMappingFk.GLAccountDescription.ToString()
, e.GeneralLedgerMappingFk == null || e.GeneralLedgerMappingFk.ValuationClass == null ? "" : e.GeneralLedgerMappingFk.ValuationClass.ToString()
, e.GeneralLedgerMappingFk == null || e.GeneralLedgerMappingFk.ValuationClassDescription == null ? "" : e.GeneralLedgerMappingFk.ValuationClassDescription.ToString()
) == input.GeneralLedgerMappingDisplayPropertyFilter);

            var query = (from o in filteredMaterials
                         join o1 in _lookup_materialGroupRepository.GetAll() on o.MaterialGroupId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_unspscRepository.GetAll() on o.UNSPSCId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         join o3 in _lookup_generalLedgerMappingRepository.GetAll() on o.GeneralLedgerMappingId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()

                         select new GetMaterialForViewDto()
                         {
                             Material = new MaterialDto
                             {
                                 MaterialNo = o.MaterialNo,
                                 MaterialName = o.MaterialName,
                                 Description = o.Description,
                                 UoM = o.UoM,
                                 ImageMain = o.ImageMain,
                                 Id = o.Id
                             },
                             MaterialGroupDisplayProperty = string.Format("{0} {1}", s1 == null || s1.MaterialGroupCode == null ? "" : s1.MaterialGroupCode.ToString()
, s1 == null || s1.MaterialGroupDescription == null ? "" : s1.MaterialGroupDescription.ToString()
),
                             UNSPSCDisplayProperty = string.Format("{0} {1}", s2 == null || s2.UNSPSC_Code == null ? "" : s2.UNSPSC_Code.ToString()
, s2 == null || s2.Description == null ? "" : s2.Description.ToString()
),
                             GeneralLedgerMappingDisplayProperty = string.Format("{0} {1} {2} {3}", s3 == null || s3.GLAccount == null ? "" : s3.GLAccount.ToString()
, s3 == null || s3.GLAccountDescription == null ? "" : s3.GLAccountDescription.ToString()
, s3 == null || s3.ValuationClass == null ? "" : s3.ValuationClass.ToString()
, s3 == null || s3.ValuationClassDescription == null ? "" : s3.ValuationClassDescription.ToString()
)
                         });

            var materialListDtos = await query.ToListAsync();

            return _materialsExcelExporter.ExportToFile(materialListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Materials)]
        public async Task<PagedResultDto<MaterialMaterialGroupLookupTableDto>> GetAllMaterialGroupForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_materialGroupRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => string.Format("{0} {1}", e.MaterialGroupCode, e.MaterialGroupDescription).Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var materialGroupList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<MaterialMaterialGroupLookupTableDto>();
            foreach (var materialGroup in materialGroupList)
            {
                lookupTableDtoList.Add(new MaterialMaterialGroupLookupTableDto
                {
                    Id = materialGroup.Id.ToString(),
                    DisplayName = string.Format("{0} {1}", materialGroup.MaterialGroupCode, materialGroup.MaterialGroupDescription)
                });
            }

            return new PagedResultDto<MaterialMaterialGroupLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_Materials)]
        public async Task<PagedResultDto<MaterialUNSPSCLookupTableDto>> GetAllUNSPSCForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_unspscRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => string.Format("{0} {1}", e.UNSPSC_Code, e.Description).Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var unspscList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<MaterialUNSPSCLookupTableDto>();
            foreach (var unspsc in unspscList)
            {
                lookupTableDtoList.Add(new MaterialUNSPSCLookupTableDto
                {
                    Id = unspsc.Id.ToString(),
                    DisplayName = string.Format("{0} {1}", unspsc.UNSPSC_Code, unspsc.Description)
                });
            }

            return new PagedResultDto<MaterialUNSPSCLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_Materials)]
        public async Task<PagedResultDto<MaterialGeneralLedgerMappingLookupTableDto>> GetAllGeneralLedgerMappingForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_generalLedgerMappingRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => string.Format("{0} {1} {2} {3}", e.GLAccount, e.GLAccountDescription, e.ValuationClass, e.ValuationClassDescription).Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var generalLedgerMappingList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<MaterialGeneralLedgerMappingLookupTableDto>();
            foreach (var generalLedgerMapping in generalLedgerMappingList)
            {
                lookupTableDtoList.Add(new MaterialGeneralLedgerMappingLookupTableDto
                {
                    Id = generalLedgerMapping.Id.ToString(),
                    DisplayName = string.Format("{0} {1} {2} {3}", generalLedgerMapping.GLAccount, generalLedgerMapping.GLAccountDescription, generalLedgerMapping.ValuationClass, generalLedgerMapping.ValuationClassDescription)
                });
            }

            return new PagedResultDto<MaterialGeneralLedgerMappingLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        protected virtual async Task<Guid?> GetBinaryObjectFromCache(string fileToken)
        {
            if (fileToken.IsNullOrWhiteSpace())
            {
                return null;
            }

            var fileCache = _tempFileCacheManager.GetFileInfo(fileToken);

            if (fileCache == null)
            {
                throw new UserFriendlyException("There is no such file with the token: " + fileToken);
            }

            var storedFile = new BinaryObject(AbpSession.TenantId, fileCache.File, fileCache.FileName);
            await _binaryObjectManager.SaveAsync(storedFile);

            return storedFile.Id;
        }

        protected virtual async Task<string> GetBinaryFileName(Guid? fileId)
        {
            if (!fileId.HasValue)
            {
                return null;
            }

            var file = await _binaryObjectManager.GetOrNullAsync(fileId.Value);
            return file?.Description;
        }

        [AbpAuthorize(AppPermissions.Pages_Materials_Edit)]
        public virtual async Task RemoveImageMainFile(EntityDto<Guid> input)
        {
            var material = await _materialRepository.FirstOrDefaultAsync(input.Id);
            if (material == null)
            {
                throw new UserFriendlyException(L("EntityNotFound"));
            }

            if (!material.ImageMain.HasValue)
            {
                throw new UserFriendlyException(L("FileNotFound"));
            }

            await _binaryObjectManager.DeleteAsync(material.ImageMain.Value);
            material.ImageMain = null;
        }

    }
}