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

namespace Adaro.Centralize.SAPConnector
{
    [AbpAuthorize(AppPermissions.Pages_DataProductions)]
    public class DataProductionsAppService : CentralizeAppServiceBase, IDataProductionsAppService
    {
        private readonly IRepository<DataProduction, Guid> _dataProductionRepository;
        private readonly IDataProductionsExcelExporter _dataProductionsExcelExporter;

        public DataProductionsAppService(IRepository<DataProduction, Guid> dataProductionRepository, IDataProductionsExcelExporter dataProductionsExcelExporter)
        {
            _dataProductionRepository = dataProductionRepository;
            _dataProductionsExcelExporter = dataProductionsExcelExporter;

        }

        public virtual async Task<PagedResultDto<GetDataProductionForViewDto>> GetAll(GetAllDataProductionsInput input)
        {

            var filteredDataProductions = _dataProductionRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.IntfId.Contains(input.Filter) || e.IntfSite.Contains(input.Filter) || e.IntfSession.Contains(input.Filter) || e.ObjectId.Contains(input.Filter) || e.MessageType.Contains(input.Filter) || e.MaterialDocument.Contains(input.Filter) || e.Order.Contains(input.Filter) || e.PurchaseOrder.Contains(input.Filter) || e.MovementType.Contains(input.Filter) || e.MovementTypeText.Contains(input.Filter) || e.Plant.Contains(input.Filter) || e.StorageLocation.Contains(input.Filter) || e.Material.Contains(input.Filter) || e.MaterialDescription.Contains(input.Filter) || e.Vendor.Contains(input.Filter) || e.UnitOfEntry.Contains(input.Filter) || e.TimeOfEntry.Contains(input.Filter) || e.UserName.Contains(input.Filter) || e.DocumentHeaderText.Contains(input.Filter) || e.Batch.Contains(input.Filter) || e.CostCenter.Contains(input.Filter) || e.Reference.Contains(input.Filter) || e.InterfaceCreatedBy.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.IntfIdFilter), e => e.IntfId.Contains(input.IntfIdFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.IntfSiteFilter), e => e.IntfSite.Contains(input.IntfSiteFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.IntfSessionFilter), e => e.IntfSession.Contains(input.IntfSessionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ObjectIdFilter), e => e.ObjectId.Contains(input.ObjectIdFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MessageTypeFilter), e => e.MessageType.Contains(input.MessageTypeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialDocumentFilter), e => e.MaterialDocument.Contains(input.MaterialDocumentFilter))
                        .WhereIf(input.MinMaterialDocYearFilter != null, e => e.MaterialDocYear >= input.MinMaterialDocYearFilter)
                        .WhereIf(input.MaxMaterialDocYearFilter != null, e => e.MaterialDocYear <= input.MaxMaterialDocYearFilter)
                        .WhereIf(input.MinMaterialDocItemFilter != null, e => e.MaterialDocItem >= input.MinMaterialDocItemFilter)
                        .WhereIf(input.MaxMaterialDocItemFilter != null, e => e.MaterialDocItem <= input.MaxMaterialDocItemFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OrderFilter), e => e.Order.Contains(input.OrderFilter))
                        .WhereIf(input.MinReservationFilter != null, e => e.Reservation >= input.MinReservationFilter)
                        .WhereIf(input.MaxReservationFilter != null, e => e.Reservation <= input.MaxReservationFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchaseOrderFilter), e => e.PurchaseOrder.Contains(input.PurchaseOrderFilter))
                        .WhereIf(input.MinPurchaseOrderItemFilter != null, e => e.PurchaseOrderItem >= input.MinPurchaseOrderItemFilter)
                        .WhereIf(input.MaxPurchaseOrderItemFilter != null, e => e.PurchaseOrderItem <= input.MaxPurchaseOrderItemFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MovementTypeFilter), e => e.MovementType.Contains(input.MovementTypeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MovementTypeTextFilter), e => e.MovementTypeText.Contains(input.MovementTypeTextFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PlantFilter), e => e.Plant.Contains(input.PlantFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StorageLocationFilter), e => e.StorageLocation.Contains(input.StorageLocationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialFilter), e => e.Material.Contains(input.MaterialFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialDescriptionFilter), e => e.MaterialDescription.Contains(input.MaterialDescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.VendorFilter), e => e.Vendor.Contains(input.VendorFilter))
                        .WhereIf(input.MinQuantityFilter != null, e => e.Quantity >= input.MinQuantityFilter)
                        .WhereIf(input.MaxQuantityFilter != null, e => e.Quantity <= input.MaxQuantityFilter)
                        .WhereIf(input.MinQtyInOrderUnitFilter != null, e => e.QtyInOrderUnit >= input.MinQtyInOrderUnitFilter)
                        .WhereIf(input.MaxQtyInOrderUnitFilter != null, e => e.QtyInOrderUnit <= input.MaxQtyInOrderUnitFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UnitOfEntryFilter), e => e.UnitOfEntry.Contains(input.UnitOfEntryFilter))
                        .WhereIf(input.MinPostingDateFilter != null, e => e.PostingDate >= input.MinPostingDateFilter)
                        .WhereIf(input.MaxPostingDateFilter != null, e => e.PostingDate <= input.MaxPostingDateFilter)
                        .WhereIf(input.MinEntryDateFilter != null, e => e.EntryDate >= input.MinEntryDateFilter)
                        .WhereIf(input.MaxEntryDateFilter != null, e => e.EntryDate <= input.MaxEntryDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TimeOfEntryFilter), e => e.TimeOfEntry.Contains(input.TimeOfEntryFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.UserName.Contains(input.UserNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DocumentHeaderTextFilter), e => e.DocumentHeaderText.Contains(input.DocumentHeaderTextFilter))
                        .WhereIf(input.MinDocumentDateFilter != null, e => e.DocumentDate >= input.MinDocumentDateFilter)
                        .WhereIf(input.MaxDocumentDateFilter != null, e => e.DocumentDate <= input.MaxDocumentDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BatchFilter), e => e.Batch.Contains(input.BatchFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterFilter), e => e.CostCenter.Contains(input.CostCenterFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ReferenceFilter), e => e.Reference.Contains(input.ReferenceFilter))
                        .WhereIf(input.MinInterfaceCreatedDateFilter != null, e => e.InterfaceCreatedDate >= input.MinInterfaceCreatedDateFilter)
                        .WhereIf(input.MaxInterfaceCreatedDateFilter != null, e => e.InterfaceCreatedDate <= input.MaxInterfaceCreatedDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.InterfaceCreatedByFilter), e => e.InterfaceCreatedBy.Contains(input.InterfaceCreatedByFilter));

            var pagedAndFilteredDataProductions = filteredDataProductions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var dataProductions = from o in pagedAndFilteredDataProductions
                                  select new
                                  {

                                      o.MaterialDocument,
                                      o.MaterialDocYear,
                                      o.MaterialDocItem,
                                      o.Order,
                                      o.Reservation,
                                      o.PurchaseOrder,
                                      o.MovementType,
                                      o.MovementTypeText,
                                      o.Plant,
                                      o.StorageLocation,
                                      o.Material,
                                      o.MaterialDescription,
                                      o.Quantity,
                                      o.QtyInOrderUnit,
                                      o.PostingDate,
                                      o.EntryDate,
                                      o.DocumentDate,
                                      o.Batch,
                                      Id = o.Id
                                  };

            var totalCount = await filteredDataProductions.CountAsync();

            var dbList = await dataProductions.ToListAsync();
            var results = new List<GetDataProductionForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetDataProductionForViewDto()
                {
                    DataProduction = new DataProductionDto
                    {

                        MaterialDocument = o.MaterialDocument,
                        MaterialDocYear = o.MaterialDocYear,
                        MaterialDocItem = o.MaterialDocItem,
                        Order = o.Order,
                        Reservation = o.Reservation,
                        PurchaseOrder = o.PurchaseOrder,
                        MovementType = o.MovementType,
                        MovementTypeText = o.MovementTypeText,
                        Plant = o.Plant,
                        StorageLocation = o.StorageLocation,
                        Material = o.Material,
                        MaterialDescription = o.MaterialDescription,
                        Quantity = o.Quantity,
                        QtyInOrderUnit = o.QtyInOrderUnit,
                        PostingDate = o.PostingDate,
                        EntryDate = o.EntryDate,
                        DocumentDate = o.DocumentDate,
                        Batch = o.Batch,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetDataProductionForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetDataProductionForViewDto> GetDataProductionForView(Guid id)
        {
            var dataProduction = await _dataProductionRepository.GetAsync(id);

            var output = new GetDataProductionForViewDto { DataProduction = ObjectMapper.Map<DataProductionDto>(dataProduction) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_DataProductions_Edit)]
        public virtual async Task<GetDataProductionForEditOutput> GetDataProductionForEdit(EntityDto<Guid> input)
        {
            var dataProduction = await _dataProductionRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetDataProductionForEditOutput { DataProduction = ObjectMapper.Map<CreateOrEditDataProductionDto>(dataProduction) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditDataProductionDto input)
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

        [AbpAuthorize(AppPermissions.Pages_DataProductions_Create)]
        protected virtual async Task Create(CreateOrEditDataProductionDto input)
        {
            var dataProduction = ObjectMapper.Map<DataProduction>(input);

            if (AbpSession.TenantId != null)
            {
                dataProduction.TenantId = (int?)AbpSession.TenantId;
            }

            await _dataProductionRepository.InsertAsync(dataProduction);

        }

        [AbpAuthorize(AppPermissions.Pages_DataProductions_Edit)]
        protected virtual async Task Update(CreateOrEditDataProductionDto input)
        {
            var dataProduction = await _dataProductionRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, dataProduction);

        }

        [AbpAuthorize(AppPermissions.Pages_DataProductions_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _dataProductionRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetDataProductionsToExcel(GetAllDataProductionsForExcelInput input)
        {

            var filteredDataProductions = _dataProductionRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.IntfId.Contains(input.Filter) || e.IntfSite.Contains(input.Filter) || e.IntfSession.Contains(input.Filter) || e.ObjectId.Contains(input.Filter) || e.MessageType.Contains(input.Filter) || e.MaterialDocument.Contains(input.Filter) || e.Order.Contains(input.Filter) || e.PurchaseOrder.Contains(input.Filter) || e.MovementType.Contains(input.Filter) || e.MovementTypeText.Contains(input.Filter) || e.Plant.Contains(input.Filter) || e.StorageLocation.Contains(input.Filter) || e.Material.Contains(input.Filter) || e.MaterialDescription.Contains(input.Filter) || e.Vendor.Contains(input.Filter) || e.UnitOfEntry.Contains(input.Filter) || e.TimeOfEntry.Contains(input.Filter) || e.UserName.Contains(input.Filter) || e.DocumentHeaderText.Contains(input.Filter) || e.Batch.Contains(input.Filter) || e.CostCenter.Contains(input.Filter) || e.Reference.Contains(input.Filter) || e.InterfaceCreatedBy.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.IntfIdFilter), e => e.IntfId.Contains(input.IntfIdFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.IntfSiteFilter), e => e.IntfSite.Contains(input.IntfSiteFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.IntfSessionFilter), e => e.IntfSession.Contains(input.IntfSessionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ObjectIdFilter), e => e.ObjectId.Contains(input.ObjectIdFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MessageTypeFilter), e => e.MessageType.Contains(input.MessageTypeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialDocumentFilter), e => e.MaterialDocument.Contains(input.MaterialDocumentFilter))
                        .WhereIf(input.MinMaterialDocYearFilter != null, e => e.MaterialDocYear >= input.MinMaterialDocYearFilter)
                        .WhereIf(input.MaxMaterialDocYearFilter != null, e => e.MaterialDocYear <= input.MaxMaterialDocYearFilter)
                        .WhereIf(input.MinMaterialDocItemFilter != null, e => e.MaterialDocItem >= input.MinMaterialDocItemFilter)
                        .WhereIf(input.MaxMaterialDocItemFilter != null, e => e.MaterialDocItem <= input.MaxMaterialDocItemFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OrderFilter), e => e.Order.Contains(input.OrderFilter))
                        .WhereIf(input.MinReservationFilter != null, e => e.Reservation >= input.MinReservationFilter)
                        .WhereIf(input.MaxReservationFilter != null, e => e.Reservation <= input.MaxReservationFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchaseOrderFilter), e => e.PurchaseOrder.Contains(input.PurchaseOrderFilter))
                        .WhereIf(input.MinPurchaseOrderItemFilter != null, e => e.PurchaseOrderItem >= input.MinPurchaseOrderItemFilter)
                        .WhereIf(input.MaxPurchaseOrderItemFilter != null, e => e.PurchaseOrderItem <= input.MaxPurchaseOrderItemFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MovementTypeFilter), e => e.MovementType.Contains(input.MovementTypeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MovementTypeTextFilter), e => e.MovementTypeText.Contains(input.MovementTypeTextFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PlantFilter), e => e.Plant.Contains(input.PlantFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StorageLocationFilter), e => e.StorageLocation.Contains(input.StorageLocationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialFilter), e => e.Material.Contains(input.MaterialFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialDescriptionFilter), e => e.MaterialDescription.Contains(input.MaterialDescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.VendorFilter), e => e.Vendor.Contains(input.VendorFilter))
                        .WhereIf(input.MinQuantityFilter != null, e => e.Quantity >= input.MinQuantityFilter)
                        .WhereIf(input.MaxQuantityFilter != null, e => e.Quantity <= input.MaxQuantityFilter)
                        .WhereIf(input.MinQtyInOrderUnitFilter != null, e => e.QtyInOrderUnit >= input.MinQtyInOrderUnitFilter)
                        .WhereIf(input.MaxQtyInOrderUnitFilter != null, e => e.QtyInOrderUnit <= input.MaxQtyInOrderUnitFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UnitOfEntryFilter), e => e.UnitOfEntry.Contains(input.UnitOfEntryFilter))
                        .WhereIf(input.MinPostingDateFilter != null, e => e.PostingDate >= input.MinPostingDateFilter)
                        .WhereIf(input.MaxPostingDateFilter != null, e => e.PostingDate <= input.MaxPostingDateFilter)
                        .WhereIf(input.MinEntryDateFilter != null, e => e.EntryDate >= input.MinEntryDateFilter)
                        .WhereIf(input.MaxEntryDateFilter != null, e => e.EntryDate <= input.MaxEntryDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TimeOfEntryFilter), e => e.TimeOfEntry.Contains(input.TimeOfEntryFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.UserName.Contains(input.UserNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DocumentHeaderTextFilter), e => e.DocumentHeaderText.Contains(input.DocumentHeaderTextFilter))
                        .WhereIf(input.MinDocumentDateFilter != null, e => e.DocumentDate >= input.MinDocumentDateFilter)
                        .WhereIf(input.MaxDocumentDateFilter != null, e => e.DocumentDate <= input.MaxDocumentDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BatchFilter), e => e.Batch.Contains(input.BatchFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterFilter), e => e.CostCenter.Contains(input.CostCenterFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ReferenceFilter), e => e.Reference.Contains(input.ReferenceFilter))
                        .WhereIf(input.MinInterfaceCreatedDateFilter != null, e => e.InterfaceCreatedDate >= input.MinInterfaceCreatedDateFilter)
                        .WhereIf(input.MaxInterfaceCreatedDateFilter != null, e => e.InterfaceCreatedDate <= input.MaxInterfaceCreatedDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.InterfaceCreatedByFilter), e => e.InterfaceCreatedBy.Contains(input.InterfaceCreatedByFilter));

            var query = (from o in filteredDataProductions
                         select new GetDataProductionForViewDto()
                         {
                             DataProduction = new DataProductionDto
                             {
                                 MaterialDocument = o.MaterialDocument,
                                 MaterialDocYear = o.MaterialDocYear,
                                 MaterialDocItem = o.MaterialDocItem,
                                 Order = o.Order,
                                 Reservation = o.Reservation,
                                 PurchaseOrder = o.PurchaseOrder,
                                 MovementType = o.MovementType,
                                 MovementTypeText = o.MovementTypeText,
                                 Plant = o.Plant,
                                 StorageLocation = o.StorageLocation,
                                 Material = o.Material,
                                 MaterialDescription = o.MaterialDescription,
                                 Quantity = o.Quantity,
                                 QtyInOrderUnit = o.QtyInOrderUnit,
                                 PostingDate = o.PostingDate,
                                 EntryDate = o.EntryDate,
                                 DocumentDate = o.DocumentDate,
                                 Batch = o.Batch,
                                 Id = o.Id
                             }
                         });

            var dataProductionListDtos = await query.ToListAsync();

            return _dataProductionsExcelExporter.ExportToFile(dataProductionListDtos);
        }

    }
}