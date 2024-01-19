using Adaro.Centralize.MasterData;
using Adaro.Centralize.MasterData;

using Adaro.Centralize.MasterDataRequest;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Adaro.Centralize.MasterDataRequest.Exporting;
using Adaro.Centralize.MasterDataRequest.Dtos;
using Adaro.Centralize.Dto;
using Abp.Application.Services.Dto;
using Adaro.Centralize.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.MasterDataRequest
{
    [AbpAuthorize(AppPermissions.Pages_MaterialRequests)]
    public class MaterialRequestsAppService : CentralizeAppServiceBase, IMaterialRequestsAppService
    {
        private readonly IRepository<MaterialRequest, Guid> _materialRequestRepository;
        private readonly IMaterialRequestsExcelExporter _materialRequestsExcelExporter;
        private readonly IRepository<UNSPSC, Guid> _lookup_unspscRepository;
        private readonly IRepository<GeneralLedgerMapping, Guid> _lookup_generalLedgerMappingRepository;

        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;

        public MaterialRequestsAppService(IRepository<MaterialRequest, Guid> materialRequestRepository, IMaterialRequestsExcelExporter materialRequestsExcelExporter, IRepository<UNSPSC, Guid> lookup_unspscRepository, IRepository<GeneralLedgerMapping, Guid> lookup_generalLedgerMappingRepository, ITempFileCacheManager tempFileCacheManager, IBinaryObjectManager binaryObjectManager)
        {
            _materialRequestRepository = materialRequestRepository;
            _materialRequestsExcelExporter = materialRequestsExcelExporter;
            _lookup_unspscRepository = lookup_unspscRepository;
            _lookup_generalLedgerMappingRepository = lookup_generalLedgerMappingRepository;

            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;

        }

        public virtual async Task<PagedResultDto<GetMaterialRequestForViewDto>> GetAll(GetAllMaterialRequestsInput input)
        {
            var requestStatusFilter = input.RequestStatusFilter.HasValue
                        ? (MaterialRequestStatus)input.RequestStatusFilter
                        : default;

            var filteredMaterialRequests = _materialRequestRepository.GetAll()
                        .Include(e => e.UNSPSCFk)
                        .Include(e => e.ValuationClassFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.RequestNo.Contains(input.Filter) || e.MaterialName.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.Purpose.Contains(input.Filter) || e.MaterialType.Contains(input.Filter) || e.Category.Contains(input.Filter) || e.Size.Contains(input.Filter) || e.UoM.Contains(input.Filter) || e.PackageSize.Contains(input.Filter) || e.GeneralLedger.Contains(input.Filter) || e.Brand.Contains(input.Filter) || e.Weight.Contains(input.Filter) || e.ImageColletion.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RequestNoFilter), e => e.RequestNo.Contains(input.RequestNoFilter))
                        .WhereIf(input.RequestStatusFilter.HasValue && input.RequestStatusFilter > -1, e => e.RequestStatus == requestStatusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialNameFilter), e => e.MaterialName.Contains(input.MaterialNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurposeFilter), e => e.Purpose.Contains(input.PurposeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialTypeFilter), e => e.MaterialType.Contains(input.MaterialTypeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CategoryFilter), e => e.Category.Contains(input.CategoryFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SizeFilter), e => e.Size.Contains(input.SizeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UoMFilter), e => e.UoM.Contains(input.UoMFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PackageSizeFilter), e => e.PackageSize.Contains(input.PackageSizeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GeneralLedgerFilter), e => e.GeneralLedger.Contains(input.GeneralLedgerFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BrandFilter), e => e.Brand.Contains(input.BrandFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WeightFilter), e => e.Weight.Contains(input.WeightFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ImageColletionFilter), e => e.ImageColletion.Contains(input.ImageColletionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UNSPSCDisplayPropertyFilter), e => string.Format("{0} - {1}", e.UNSPSCFk == null || e.UNSPSCFk.UNSPSC_Code == null ? "" : e.UNSPSCFk.UNSPSC_Code.ToString()
, e.UNSPSCFk == null || e.UNSPSCFk.Description == null ? "" : e.UNSPSCFk.Description.ToString()
) == input.UNSPSCDisplayPropertyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GeneralLedgerMappingDisplayPropertyFilter), e => string.Format("{0} - {1}", e.ValuationClassFk == null || e.ValuationClassFk.ValuationClass == null ? "" : e.ValuationClassFk.ValuationClass.ToString()
, e.ValuationClassFk == null || e.ValuationClassFk.ValuationClassDescription == null ? "" : e.ValuationClassFk.ValuationClassDescription.ToString()
) == input.GeneralLedgerMappingDisplayPropertyFilter);

            var pagedAndFilteredMaterialRequests = filteredMaterialRequests
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var materialRequests = from o in pagedAndFilteredMaterialRequests
                                   join o1 in _lookup_unspscRepository.GetAll() on o.UNSPSCId equals o1.Id into j1
                                   from s1 in j1.DefaultIfEmpty()

                                   join o2 in _lookup_generalLedgerMappingRepository.GetAll() on o.ValuationClassId equals o2.Id into j2
                                   from s2 in j2.DefaultIfEmpty()

                                   select new
                                   {

                                       o.RequestNo,
                                       o.RequestStatus,
                                       o.MaterialName,
                                       o.Description,
                                       o.GeneralLedger,
                                       o.Picture,
                                       Id = o.Id,
                                       UNSPSCDisplayProperty = string.Format("{0} - {1}", s1 == null || s1.UNSPSC_Code == null ? "" : s1.UNSPSC_Code.ToString()
                   , s1 == null || s1.Description == null ? "" : s1.Description.ToString()
                   ),
                                       GeneralLedgerMappingDisplayProperty = string.Format("{0} - {1}", s2 == null || s2.ValuationClass == null ? "" : s2.ValuationClass.ToString()
                   , s2 == null || s2.ValuationClassDescription == null ? "" : s2.ValuationClassDescription.ToString()
                   )
                                   };

            var totalCount = await filteredMaterialRequests.CountAsync();

            var dbList = await materialRequests.ToListAsync();
            var results = new List<GetMaterialRequestForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetMaterialRequestForViewDto()
                {
                    MaterialRequest = new MaterialRequestDto
                    {

                        RequestNo = o.RequestNo,
                        RequestStatus = o.RequestStatus,
                        MaterialName = o.MaterialName,
                        Description = o.Description,
                        GeneralLedger = o.GeneralLedger,
                        Picture = o.Picture,
                        Id = o.Id,
                    },
                    UNSPSCDisplayProperty = o.UNSPSCDisplayProperty,
                    GeneralLedgerMappingDisplayProperty = o.GeneralLedgerMappingDisplayProperty
                };
                res.MaterialRequest.PictureFileName = await GetBinaryFileName(o.Picture);

                results.Add(res);
            }

            return new PagedResultDto<GetMaterialRequestForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetMaterialRequestForViewDto> GetMaterialRequestForView(Guid id)
        {
            var materialRequest = await _materialRequestRepository.GetAsync(id);

            var output = new GetMaterialRequestForViewDto { MaterialRequest = ObjectMapper.Map<MaterialRequestDto>(materialRequest) };

            if (output.MaterialRequest.UNSPSCId != null)
            {
                var _lookupUNSPSC = await _lookup_unspscRepository.FirstOrDefaultAsync((Guid)output.MaterialRequest.UNSPSCId);
                output.UNSPSCDisplayProperty = string.Format("{0} - {1}", _lookupUNSPSC.UNSPSC_Code, _lookupUNSPSC.Description);
            }

            if (output.MaterialRequest.ValuationClassId != null)
            {
                var _lookupGeneralLedgerMapping = await _lookup_generalLedgerMappingRepository.FirstOrDefaultAsync((Guid)output.MaterialRequest.ValuationClassId);
                output.GeneralLedgerMappingDisplayProperty = string.Format("{0} - {1}", _lookupGeneralLedgerMapping.ValuationClass, _lookupGeneralLedgerMapping.ValuationClassDescription);
            }

            output.MaterialRequest.PictureFileName = await GetBinaryFileName(materialRequest.Picture);

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_MaterialRequests_Edit)]
        public virtual async Task<GetMaterialRequestForEditOutput> GetMaterialRequestForEdit(EntityDto<Guid> input)
        {
            var materialRequest = await _materialRequestRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetMaterialRequestForEditOutput { MaterialRequest = ObjectMapper.Map<CreateOrEditMaterialRequestDto>(materialRequest) };

            if (output.MaterialRequest.UNSPSCId != null)
            {
                var _lookupUNSPSC = await _lookup_unspscRepository.FirstOrDefaultAsync((Guid)output.MaterialRequest.UNSPSCId);
                output.UNSPSCDisplayProperty = string.Format("{0} - {1}", _lookupUNSPSC.UNSPSC_Code, _lookupUNSPSC.Description);
            }

            if (output.MaterialRequest.ValuationClassId != null)
            {
                var _lookupGeneralLedgerMapping = await _lookup_generalLedgerMappingRepository.FirstOrDefaultAsync((Guid)output.MaterialRequest.ValuationClassId);
                output.GeneralLedgerMappingDisplayProperty = string.Format("{0} - {1}", _lookupGeneralLedgerMapping.ValuationClass, _lookupGeneralLedgerMapping.ValuationClassDescription);
            }

            output.PictureFileName = await GetBinaryFileName(materialRequest.Picture);

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditMaterialRequestDto input)
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

        [AbpAuthorize(AppPermissions.Pages_MaterialRequests_Create)]
        protected virtual async Task Create(CreateOrEditMaterialRequestDto input)
        {
            var materialRequest = ObjectMapper.Map<MaterialRequest>(input);

            if (AbpSession.TenantId != null)
            {
                materialRequest.TenantId = (int?)AbpSession.TenantId;
            }

            await _materialRequestRepository.InsertAsync(materialRequest);
            materialRequest.Picture = await GetBinaryObjectFromCache(input.PictureToken);

        }

        [AbpAuthorize(AppPermissions.Pages_MaterialRequests_Edit)]
        protected virtual async Task Update(CreateOrEditMaterialRequestDto input)
        {
            var materialRequest = await _materialRequestRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, materialRequest);
            materialRequest.Picture = await GetBinaryObjectFromCache(input.PictureToken);

        }

        [AbpAuthorize(AppPermissions.Pages_MaterialRequests_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _materialRequestRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetMaterialRequestsToExcel(GetAllMaterialRequestsForExcelInput input)
        {
            var requestStatusFilter = input.RequestStatusFilter.HasValue
                        ? (MaterialRequestStatus)input.RequestStatusFilter
                        : default;

            var filteredMaterialRequests = _materialRequestRepository.GetAll()
                        .Include(e => e.UNSPSCFk)
                        .Include(e => e.ValuationClassFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.RequestNo.Contains(input.Filter) || e.MaterialName.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.Purpose.Contains(input.Filter) || e.MaterialType.Contains(input.Filter) || e.Category.Contains(input.Filter) || e.Size.Contains(input.Filter) || e.UoM.Contains(input.Filter) || e.PackageSize.Contains(input.Filter) || e.GeneralLedger.Contains(input.Filter) || e.Brand.Contains(input.Filter) || e.Weight.Contains(input.Filter) || e.ImageColletion.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RequestNoFilter), e => e.RequestNo.Contains(input.RequestNoFilter))
                        .WhereIf(input.RequestStatusFilter.HasValue && input.RequestStatusFilter > -1, e => e.RequestStatus == requestStatusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialNameFilter), e => e.MaterialName.Contains(input.MaterialNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurposeFilter), e => e.Purpose.Contains(input.PurposeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialTypeFilter), e => e.MaterialType.Contains(input.MaterialTypeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CategoryFilter), e => e.Category.Contains(input.CategoryFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SizeFilter), e => e.Size.Contains(input.SizeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UoMFilter), e => e.UoM.Contains(input.UoMFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PackageSizeFilter), e => e.PackageSize.Contains(input.PackageSizeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GeneralLedgerFilter), e => e.GeneralLedger.Contains(input.GeneralLedgerFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BrandFilter), e => e.Brand.Contains(input.BrandFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WeightFilter), e => e.Weight.Contains(input.WeightFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ImageColletionFilter), e => e.ImageColletion.Contains(input.ImageColletionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UNSPSCDisplayPropertyFilter), e => string.Format("{0} - {1}", e.UNSPSCFk == null || e.UNSPSCFk.UNSPSC_Code == null ? "" : e.UNSPSCFk.UNSPSC_Code.ToString()
, e.UNSPSCFk == null || e.UNSPSCFk.Description == null ? "" : e.UNSPSCFk.Description.ToString()
) == input.UNSPSCDisplayPropertyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GeneralLedgerMappingDisplayPropertyFilter), e => string.Format("{0} - {1}", e.ValuationClassFk == null || e.ValuationClassFk.ValuationClass == null ? "" : e.ValuationClassFk.ValuationClass.ToString()
, e.ValuationClassFk == null || e.ValuationClassFk.ValuationClassDescription == null ? "" : e.ValuationClassFk.ValuationClassDescription.ToString()
) == input.GeneralLedgerMappingDisplayPropertyFilter);

            var query = (from o in filteredMaterialRequests
                         join o1 in _lookup_unspscRepository.GetAll() on o.UNSPSCId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_generalLedgerMappingRepository.GetAll() on o.ValuationClassId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         select new GetMaterialRequestForViewDto()
                         {
                             MaterialRequest = new MaterialRequestDto
                             {
                                 RequestNo = o.RequestNo,
                                 RequestStatus = o.RequestStatus,
                                 MaterialName = o.MaterialName,
                                 Description = o.Description,
                                 GeneralLedger = o.GeneralLedger,
                                 Picture = o.Picture,
                                 Id = o.Id
                             },
                             UNSPSCDisplayProperty = string.Format("{0} - {1}", s1 == null || s1.UNSPSC_Code == null ? "" : s1.UNSPSC_Code.ToString()
, s1 == null || s1.Description == null ? "" : s1.Description.ToString()
),
                             GeneralLedgerMappingDisplayProperty = string.Format("{0} - {1}", s2 == null || s2.ValuationClass == null ? "" : s2.ValuationClass.ToString()
, s2 == null || s2.ValuationClassDescription == null ? "" : s2.ValuationClassDescription.ToString()
)
                         });

            var materialRequestListDtos = await query.ToListAsync();

            return _materialRequestsExcelExporter.ExportToFile(materialRequestListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_MaterialRequests)]
        public async Task<PagedResultDto<MaterialRequestUNSPSCLookupTableDto>> GetAllUNSPSCForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_unspscRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => string.Format("{0} - {1}", e.UNSPSC_Code, e.Description).Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var unspscList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<MaterialRequestUNSPSCLookupTableDto>();
            foreach (var unspsc in unspscList)
            {
                lookupTableDtoList.Add(new MaterialRequestUNSPSCLookupTableDto
                {
                    Id = unspsc.Id.ToString(),
                    DisplayName = string.Format("{0} - {1}", unspsc.UNSPSC_Code, unspsc.Description)
                });
            }

            return new PagedResultDto<MaterialRequestUNSPSCLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_MaterialRequests)]
        public async Task<PagedResultDto<MaterialRequestGeneralLedgerMappingLookupTableDto>> GetAllGeneralLedgerMappingForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_generalLedgerMappingRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => string.Format("{0} - {1}", e.ValuationClass, e.ValuationClassDescription).Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var generalLedgerMappingList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<MaterialRequestGeneralLedgerMappingLookupTableDto>();
            foreach (var generalLedgerMapping in generalLedgerMappingList)
            {
                lookupTableDtoList.Add(new MaterialRequestGeneralLedgerMappingLookupTableDto
                {
                    Id = generalLedgerMapping.Id.ToString(),
                    DisplayName = string.Format("{0} - {1}", generalLedgerMapping.ValuationClass, generalLedgerMapping.ValuationClassDescription)
                });
            }

            return new PagedResultDto<MaterialRequestGeneralLedgerMappingLookupTableDto>(
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

        [AbpAuthorize(AppPermissions.Pages_MaterialRequests_Edit)]
        public virtual async Task RemovePictureFile(EntityDto<Guid> input)
        {
            var materialRequest = await _materialRequestRepository.FirstOrDefaultAsync(input.Id);
            if (materialRequest == null)
            {
                throw new UserFriendlyException(L("EntityNotFound"));
            }

            if (!materialRequest.Picture.HasValue)
            {
                throw new UserFriendlyException(L("FileNotFound"));
            }

            await _binaryObjectManager.DeleteAsync(materialRequest.Picture.Value);
            materialRequest.Picture = null;
        }

    }
}