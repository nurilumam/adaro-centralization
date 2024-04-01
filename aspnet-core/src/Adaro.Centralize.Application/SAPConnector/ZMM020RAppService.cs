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
    [AbpAuthorize(AppPermissions.Pages_ZMM020R)]
    public class ZMM020RAppService : CentralizeAppServiceBase, IZMM020RAppService
    {
        private readonly IRepository<ZMM020R, Guid> _zmM020RRepository;
        private readonly IZMM020RExcelExporter _zmM020RExcelExporter;

        public ZMM020RAppService(IRepository<ZMM020R, Guid> zmM020RRepository, IZMM020RExcelExporter zmM020RExcelExporter)
        {
            _zmM020RRepository = zmM020RRepository;
            _zmM020RExcelExporter = zmM020RExcelExporter;

        }

        public virtual async Task<PagedResultDto<GetZMM020RForViewDto>> GetAll(GetAllZMM020RInput input)
        {

            var filteredZMM020R = _zmM020RRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.PurchaseRequisition.Contains(input.Filter) || e.DocumentType.Contains(input.Filter) || e.DocumentTypeText.Contains(input.Filter) || e.ItemRequisition.Contains(input.Filter) || e.ProcessingStatusCode.Contains(input.Filter) || e.ProcessingStatus.Contains(input.Filter) || e.DeletionIndicator.Contains(input.Filter) || e.ItemCategory.Contains(input.Filter) || e.AccountAssignment.Contains(input.Filter) || e.Material.Contains(input.Filter) || e.ShortText.Contains(input.Filter) || e.UnitOfMeasure.Contains(input.Filter) || e.ServiceItem.Contains(input.Filter) || e.Service.Contains(input.Filter) || e.ServiceShortText.Contains(input.Filter) || e.UnitOfMeasureService.Contains(input.Filter) || e.MaterialGroup.Contains(input.Filter) || e.Plant.Contains(input.Filter) || e.StorageLocation.Contains(input.Filter) || e.PurchaseGroup.Contains(input.Filter) || e.Requisitioner.Contains(input.Filter) || e.RequisitionerName.Contains(input.Filter) || e.PurchasingDocument.Contains(input.Filter) || e.OutlineAgreement.Contains(input.Filter) || e.PrincAgreementItem.Contains(input.Filter) || e.PurchasingInfoRec.Contains(input.Filter) || e.Status.Contains(input.Filter) || e.CreatedBy.Contains(input.Filter) || e.Currency.Contains(input.Filter) || e.EntrySheet.Contains(input.Filter) || e.GoodsReceipt.Contains(input.Filter) || e.SupplierCode.Contains(input.Filter) || e.SupplierName.Contains(input.Filter) || e.ReleaseIndicator.Contains(input.Filter) || e.ItemText.Contains(input.Filter) || e.LongText.Contains(input.Filter) || e.FirstApprovalName.Contains(input.Filter) || e.LastApprovalName.Contains(input.Filter) || e.CostCenter.Contains(input.Filter) || e.CostCenterDescription.Contains(input.Filter) || e.WBSElement.Contains(input.Filter) || e.Asset.Contains(input.Filter) || e.FundsCenter.Contains(input.Filter) || e.DocumentId.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchaseRequisitionFilter), e => e.PurchaseRequisition.Contains(input.PurchaseRequisitionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DocumentTypeFilter), e => e.DocumentType.Contains(input.DocumentTypeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DocumentTypeTextFilter), e => e.DocumentTypeText.Contains(input.DocumentTypeTextFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ItemRequisitionFilter), e => e.ItemRequisition.Contains(input.ItemRequisitionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ProcessingStatusCodeFilter), e => e.ProcessingStatusCode.Contains(input.ProcessingStatusCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ProcessingStatusFilter), e => e.ProcessingStatus.Contains(input.ProcessingStatusFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DeletionIndicatorFilter), e => e.DeletionIndicator.Contains(input.DeletionIndicatorFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ItemCategoryFilter), e => e.ItemCategory.Contains(input.ItemCategoryFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AccountAssignmentFilter), e => e.AccountAssignment.Contains(input.AccountAssignmentFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialFilter), e => e.Material.Contains(input.MaterialFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ShortTextFilter), e => e.ShortText.Contains(input.ShortTextFilter))
                        .WhereIf(input.MinQuantityRequestedFilter != null, e => e.QuantityRequested >= input.MinQuantityRequestedFilter)
                        .WhereIf(input.MaxQuantityRequestedFilter != null, e => e.QuantityRequested <= input.MaxQuantityRequestedFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UnitOfMeasureFilter), e => e.UnitOfMeasure.Contains(input.UnitOfMeasureFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ServiceItemFilter), e => e.ServiceItem.Contains(input.ServiceItemFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ServiceFilter), e => e.Service.Contains(input.ServiceFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ServiceShortTextFilter), e => e.ServiceShortText.Contains(input.ServiceShortTextFilter))
                        .WhereIf(input.MinQuantityServiceFilter != null, e => e.QuantityService >= input.MinQuantityServiceFilter)
                        .WhereIf(input.MaxQuantityServiceFilter != null, e => e.QuantityService <= input.MaxQuantityServiceFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UnitOfMeasureServiceFilter), e => e.UnitOfMeasureService.Contains(input.UnitOfMeasureServiceFilter))
                        .WhereIf(input.MinDeliveryDateFilter != null, e => e.DeliveryDate >= input.MinDeliveryDateFilter)
                        .WhereIf(input.MaxDeliveryDateFilter != null, e => e.DeliveryDate <= input.MaxDeliveryDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialGroupFilter), e => e.MaterialGroup.Contains(input.MaterialGroupFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PlantFilter), e => e.Plant.Contains(input.PlantFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StorageLocationFilter), e => e.StorageLocation.Contains(input.StorageLocationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchaseGroupFilter), e => e.PurchaseGroup.Contains(input.PurchaseGroupFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RequisitionerFilter), e => e.Requisitioner.Contains(input.RequisitionerFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RequisitionerNameFilter), e => e.RequisitionerName.Contains(input.RequisitionerNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchasingDocumentFilter), e => e.PurchasingDocument.Contains(input.PurchasingDocumentFilter))
                        .WhereIf(input.MinPurchaseOrderDateFilter != null, e => e.PurchaseOrderDate >= input.MinPurchaseOrderDateFilter)
                        .WhereIf(input.MaxPurchaseOrderDateFilter != null, e => e.PurchaseOrderDate <= input.MaxPurchaseOrderDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OutlineAgreementFilter), e => e.OutlineAgreement.Contains(input.OutlineAgreementFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PrincAgreementItemFilter), e => e.PrincAgreementItem.Contains(input.PrincAgreementItemFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchasingInfoRecFilter), e => e.PurchasingInfoRec.Contains(input.PurchasingInfoRecFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StatusFilter), e => e.Status.Contains(input.StatusFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CreatedByFilter), e => e.CreatedBy.Contains(input.CreatedByFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CurrencyFilter), e => e.Currency.Contains(input.CurrencyFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EntrySheetFilter), e => e.EntrySheet.Contains(input.EntrySheetFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GoodsReceiptFilter), e => e.GoodsReceipt.Contains(input.GoodsReceiptFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SupplierCodeFilter), e => e.SupplierCode.Contains(input.SupplierCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SupplierNameFilter), e => e.SupplierName.Contains(input.SupplierNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ReleaseIndicatorFilter), e => e.ReleaseIndicator.Contains(input.ReleaseIndicatorFilter))
                        .WhereIf(input.MinUnitPriceFilter != null, e => e.UnitPrice >= input.MinUnitPriceFilter)
                        .WhereIf(input.MaxUnitPriceFilter != null, e => e.UnitPrice <= input.MaxUnitPriceFilter)
                        .WhereIf(input.MinValuationPriceFilter != null, e => e.ValuationPrice >= input.MinValuationPriceFilter)
                        .WhereIf(input.MaxValuationPriceFilter != null, e => e.ValuationPrice <= input.MaxValuationPriceFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ItemTextFilter), e => e.ItemText.Contains(input.ItemTextFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LongTextFilter), e => e.LongText.Contains(input.LongTextFilter))
                        .WhereIf(input.MinFirstApprovalDateFilter != null, e => e.FirstApprovalDate >= input.MinFirstApprovalDateFilter)
                        .WhereIf(input.MaxFirstApprovalDateFilter != null, e => e.FirstApprovalDate <= input.MaxFirstApprovalDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FirstApprovalNameFilter), e => e.FirstApprovalName.Contains(input.FirstApprovalNameFilter))
                        .WhereIf(input.MinLastApprovalDateFilter != null, e => e.LastApprovalDate >= input.MinLastApprovalDateFilter)
                        .WhereIf(input.MaxLastApprovalDateFilter != null, e => e.LastApprovalDate <= input.MaxLastApprovalDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LastApprovalNameFilter), e => e.LastApprovalName.Contains(input.LastApprovalNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterFilter), e => e.CostCenter.Contains(input.CostCenterFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterDescriptionFilter), e => e.CostCenterDescription.Contains(input.CostCenterDescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WBSElementFilter), e => e.WBSElement.Contains(input.WBSElementFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AssetFilter), e => e.Asset.Contains(input.AssetFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FundsCenterFilter), e => e.FundsCenter.Contains(input.FundsCenterFilter))
                        .WhereIf(input.MinRemainQuantityFilter != null, e => e.RemainQuantity >= input.MinRemainQuantityFilter)
                        .WhereIf(input.MaxRemainQuantityFilter != null, e => e.RemainQuantity <= input.MaxRemainQuantityFilter)
                        .WhereIf(input.MinCreatedDateFilter != null, e => e.CreatedDate >= input.MinCreatedDateFilter)
                        .WhereIf(input.MaxCreatedDateFilter != null, e => e.CreatedDate <= input.MaxCreatedDateFilter)
                        .WhereIf(input.MinUpdatedDateFilter != null, e => e.UpdatedDate >= input.MinUpdatedDateFilter)
                        .WhereIf(input.MaxUpdatedDateFilter != null, e => e.UpdatedDate <= input.MaxUpdatedDateFilter);

            var pagedAndFilteredZMM020R = filteredZMM020R
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var zmM020R = from o in pagedAndFilteredZMM020R
                          select new
                          {

                              o.PurchaseRequisition,
                              o.DocumentType,
                              o.DocumentTypeText,
                              o.ItemRequisition,
                              o.ProcessingStatusCode,
                              o.ProcessingStatus,
                              o.DeletionIndicator,
                              o.ItemCategory,
                              o.AccountAssignment,
                              o.Material,
                              o.ShortText,
                              o.QuantityRequested,
                              o.UnitOfMeasure,
                              o.ServiceItem,
                              o.Service,
                              o.ServiceShortText,
                              o.QuantityService,
                              o.UnitOfMeasureService,
                              o.DeliveryDate,
                              o.MaterialGroup,
                              o.Plant,
                              o.StorageLocation,
                              o.PurchaseGroup,
                              o.Requisitioner,
                              o.RequisitionerName,
                              o.PurchasingDocument,
                              o.PurchaseOrderDate,
                              o.OutlineAgreement,
                              o.PrincAgreementItem,
                              o.PurchasingInfoRec,
                              o.Status,
                              o.CreatedBy,
                              o.Currency,
                              o.EntrySheet,
                              o.GoodsReceipt,
                              o.SupplierCode,
                              o.SupplierName,
                              o.ReleaseIndicator,
                              o.UnitPrice,
                              o.ValuationPrice,
                              o.ItemText,
                              o.LongText,
                              o.FirstApprovalDate,
                              o.FirstApprovalName,
                              o.LastApprovalDate,
                              o.LastApprovalName,
                              o.CostCenter,
                              o.CostCenterDescription,
                              o.WBSElement,
                              o.Asset,
                              o.FundsCenter,
                              o.RemainQuantity,
                              o.CreatedDate,
                              o.UpdatedDate,
                              Id = o.Id
                          };

            var totalCount = await filteredZMM020R.CountAsync();

            var dbList = await zmM020R.ToListAsync();
            var results = new List<GetZMM020RForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetZMM020RForViewDto()
                {
                    ZMM020R = new ZMM020RDto
                    {

                        PurchaseRequisition = o.PurchaseRequisition,
                        DocumentType = o.DocumentType,
                        DocumentTypeText = o.DocumentTypeText,
                        ItemRequisition = o.ItemRequisition,
                        ProcessingStatusCode = o.ProcessingStatusCode,
                        ProcessingStatus = o.ProcessingStatus,
                        DeletionIndicator = o.DeletionIndicator,
                        ItemCategory = o.ItemCategory,
                        AccountAssignment = o.AccountAssignment,
                        Material = o.Material,
                        ShortText = o.ShortText,
                        QuantityRequested = o.QuantityRequested,
                        UnitOfMeasure = o.UnitOfMeasure,
                        ServiceItem = o.ServiceItem,
                        Service = o.Service,
                        ServiceShortText = o.ServiceShortText,
                        QuantityService = o.QuantityService,
                        UnitOfMeasureService = o.UnitOfMeasureService,
                        DeliveryDate = o.DeliveryDate,
                        MaterialGroup = o.MaterialGroup,
                        Plant = o.Plant,
                        StorageLocation = o.StorageLocation,
                        PurchaseGroup = o.PurchaseGroup,
                        Requisitioner = o.Requisitioner,
                        RequisitionerName = o.RequisitionerName,
                        PurchasingDocument = o.PurchasingDocument,
                        PurchaseOrderDate = o.PurchaseOrderDate,
                        OutlineAgreement = o.OutlineAgreement,
                        PrincAgreementItem = o.PrincAgreementItem,
                        PurchasingInfoRec = o.PurchasingInfoRec,
                        Status = o.Status,
                        CreatedBy = o.CreatedBy,
                        Currency = o.Currency,
                        EntrySheet = o.EntrySheet,
                        GoodsReceipt = o.GoodsReceipt,
                        SupplierCode = o.SupplierCode,
                        SupplierName = o.SupplierName,
                        ReleaseIndicator = o.ReleaseIndicator,
                        UnitPrice = o.UnitPrice,
                        ValuationPrice = o.ValuationPrice,
                        ItemText = o.ItemText,
                        LongText = o.LongText,
                        FirstApprovalDate = o.FirstApprovalDate,
                        FirstApprovalName = o.FirstApprovalName,
                        LastApprovalDate = o.LastApprovalDate,
                        LastApprovalName = o.LastApprovalName,
                        CostCenter = o.CostCenter,
                        CostCenterDescription = o.CostCenterDescription,
                        WBSElement = o.WBSElement,
                        Asset = o.Asset,
                        FundsCenter = o.FundsCenter,
                        RemainQuantity = o.RemainQuantity,
                        CreatedDate = o.CreatedDate,
                        UpdatedDate = o.UpdatedDate,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetZMM020RForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetZMM020RForViewDto> GetZMM020RForView(Guid id)
        {
            var zmM020R = await _zmM020RRepository.GetAsync(id);

            var output = new GetZMM020RForViewDto { ZMM020R = ObjectMapper.Map<ZMM020RDto>(zmM020R) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ZMM020R_Edit)]
        public virtual async Task<GetZMM020RForEditOutput> GetZMM020RForEdit(EntityDto<Guid> input)
        {
            var zmM020R = await _zmM020RRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetZMM020RForEditOutput { ZMM020R = ObjectMapper.Map<CreateOrEditZMM020RDto>(zmM020R) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditZMM020RDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ZMM020R_Create)]
        protected virtual async Task Create(CreateOrEditZMM020RDto input)
        {
            var zmM020R = ObjectMapper.Map<ZMM020R>(input);

            if (AbpSession.TenantId != null)
            {
                zmM020R.TenantId = (int?)AbpSession.TenantId;
            }

            await _zmM020RRepository.InsertAsync(zmM020R);

        }

        [AbpAuthorize(AppPermissions.Pages_ZMM020R_Edit)]
        protected virtual async Task Update(CreateOrEditZMM020RDto input)
        {
            var zmM020R = await _zmM020RRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, zmM020R);

        }

        [AbpAuthorize(AppPermissions.Pages_ZMM020R_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _zmM020RRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetZMM020RToExcel(GetAllZMM020RForExcelInput input)
        {

            var filteredZMM020R = _zmM020RRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.PurchaseRequisition.Contains(input.Filter) || e.DocumentType.Contains(input.Filter) || e.DocumentTypeText.Contains(input.Filter) || e.ItemRequisition.Contains(input.Filter) || e.ProcessingStatusCode.Contains(input.Filter) || e.ProcessingStatus.Contains(input.Filter) || e.DeletionIndicator.Contains(input.Filter) || e.ItemCategory.Contains(input.Filter) || e.AccountAssignment.Contains(input.Filter) || e.Material.Contains(input.Filter) || e.ShortText.Contains(input.Filter) || e.UnitOfMeasure.Contains(input.Filter) || e.ServiceItem.Contains(input.Filter) || e.Service.Contains(input.Filter) || e.ServiceShortText.Contains(input.Filter) || e.UnitOfMeasureService.Contains(input.Filter) || e.MaterialGroup.Contains(input.Filter) || e.Plant.Contains(input.Filter) || e.StorageLocation.Contains(input.Filter) || e.PurchaseGroup.Contains(input.Filter) || e.Requisitioner.Contains(input.Filter) || e.RequisitionerName.Contains(input.Filter) || e.PurchasingDocument.Contains(input.Filter) || e.OutlineAgreement.Contains(input.Filter) || e.PrincAgreementItem.Contains(input.Filter) || e.PurchasingInfoRec.Contains(input.Filter) || e.Status.Contains(input.Filter) || e.CreatedBy.Contains(input.Filter) || e.Currency.Contains(input.Filter) || e.EntrySheet.Contains(input.Filter) || e.GoodsReceipt.Contains(input.Filter) || e.SupplierCode.Contains(input.Filter) || e.SupplierName.Contains(input.Filter) || e.ReleaseIndicator.Contains(input.Filter) || e.ItemText.Contains(input.Filter) || e.LongText.Contains(input.Filter) || e.FirstApprovalName.Contains(input.Filter) || e.LastApprovalName.Contains(input.Filter) || e.CostCenter.Contains(input.Filter) || e.CostCenterDescription.Contains(input.Filter) || e.WBSElement.Contains(input.Filter) || e.Asset.Contains(input.Filter) || e.FundsCenter.Contains(input.Filter) || e.DocumentId.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchaseRequisitionFilter), e => e.PurchaseRequisition.Contains(input.PurchaseRequisitionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DocumentTypeFilter), e => e.DocumentType.Contains(input.DocumentTypeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DocumentTypeTextFilter), e => e.DocumentTypeText.Contains(input.DocumentTypeTextFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ItemRequisitionFilter), e => e.ItemRequisition.Contains(input.ItemRequisitionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ProcessingStatusCodeFilter), e => e.ProcessingStatusCode.Contains(input.ProcessingStatusCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ProcessingStatusFilter), e => e.ProcessingStatus.Contains(input.ProcessingStatusFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DeletionIndicatorFilter), e => e.DeletionIndicator.Contains(input.DeletionIndicatorFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ItemCategoryFilter), e => e.ItemCategory.Contains(input.ItemCategoryFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AccountAssignmentFilter), e => e.AccountAssignment.Contains(input.AccountAssignmentFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialFilter), e => e.Material.Contains(input.MaterialFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ShortTextFilter), e => e.ShortText.Contains(input.ShortTextFilter))
                        .WhereIf(input.MinQuantityRequestedFilter != null, e => e.QuantityRequested >= input.MinQuantityRequestedFilter)
                        .WhereIf(input.MaxQuantityRequestedFilter != null, e => e.QuantityRequested <= input.MaxQuantityRequestedFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UnitOfMeasureFilter), e => e.UnitOfMeasure.Contains(input.UnitOfMeasureFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ServiceItemFilter), e => e.ServiceItem.Contains(input.ServiceItemFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ServiceFilter), e => e.Service.Contains(input.ServiceFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ServiceShortTextFilter), e => e.ServiceShortText.Contains(input.ServiceShortTextFilter))
                        .WhereIf(input.MinQuantityServiceFilter != null, e => e.QuantityService >= input.MinQuantityServiceFilter)
                        .WhereIf(input.MaxQuantityServiceFilter != null, e => e.QuantityService <= input.MaxQuantityServiceFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UnitOfMeasureServiceFilter), e => e.UnitOfMeasureService.Contains(input.UnitOfMeasureServiceFilter))
                        .WhereIf(input.MinDeliveryDateFilter != null, e => e.DeliveryDate >= input.MinDeliveryDateFilter)
                        .WhereIf(input.MaxDeliveryDateFilter != null, e => e.DeliveryDate <= input.MaxDeliveryDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialGroupFilter), e => e.MaterialGroup.Contains(input.MaterialGroupFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PlantFilter), e => e.Plant.Contains(input.PlantFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StorageLocationFilter), e => e.StorageLocation.Contains(input.StorageLocationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchaseGroupFilter), e => e.PurchaseGroup.Contains(input.PurchaseGroupFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RequisitionerFilter), e => e.Requisitioner.Contains(input.RequisitionerFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RequisitionerNameFilter), e => e.RequisitionerName.Contains(input.RequisitionerNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchasingDocumentFilter), e => e.PurchasingDocument.Contains(input.PurchasingDocumentFilter))
                        .WhereIf(input.MinPurchaseOrderDateFilter != null, e => e.PurchaseOrderDate >= input.MinPurchaseOrderDateFilter)
                        .WhereIf(input.MaxPurchaseOrderDateFilter != null, e => e.PurchaseOrderDate <= input.MaxPurchaseOrderDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OutlineAgreementFilter), e => e.OutlineAgreement.Contains(input.OutlineAgreementFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PrincAgreementItemFilter), e => e.PrincAgreementItem.Contains(input.PrincAgreementItemFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchasingInfoRecFilter), e => e.PurchasingInfoRec.Contains(input.PurchasingInfoRecFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StatusFilter), e => e.Status.Contains(input.StatusFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CreatedByFilter), e => e.CreatedBy.Contains(input.CreatedByFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CurrencyFilter), e => e.Currency.Contains(input.CurrencyFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EntrySheetFilter), e => e.EntrySheet.Contains(input.EntrySheetFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GoodsReceiptFilter), e => e.GoodsReceipt.Contains(input.GoodsReceiptFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SupplierCodeFilter), e => e.SupplierCode.Contains(input.SupplierCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SupplierNameFilter), e => e.SupplierName.Contains(input.SupplierNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ReleaseIndicatorFilter), e => e.ReleaseIndicator.Contains(input.ReleaseIndicatorFilter))
                        .WhereIf(input.MinUnitPriceFilter != null, e => e.UnitPrice >= input.MinUnitPriceFilter)
                        .WhereIf(input.MaxUnitPriceFilter != null, e => e.UnitPrice <= input.MaxUnitPriceFilter)
                        .WhereIf(input.MinValuationPriceFilter != null, e => e.ValuationPrice >= input.MinValuationPriceFilter)
                        .WhereIf(input.MaxValuationPriceFilter != null, e => e.ValuationPrice <= input.MaxValuationPriceFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ItemTextFilter), e => e.ItemText.Contains(input.ItemTextFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LongTextFilter), e => e.LongText.Contains(input.LongTextFilter))
                        .WhereIf(input.MinFirstApprovalDateFilter != null, e => e.FirstApprovalDate >= input.MinFirstApprovalDateFilter)
                        .WhereIf(input.MaxFirstApprovalDateFilter != null, e => e.FirstApprovalDate <= input.MaxFirstApprovalDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FirstApprovalNameFilter), e => e.FirstApprovalName.Contains(input.FirstApprovalNameFilter))
                        .WhereIf(input.MinLastApprovalDateFilter != null, e => e.LastApprovalDate >= input.MinLastApprovalDateFilter)
                        .WhereIf(input.MaxLastApprovalDateFilter != null, e => e.LastApprovalDate <= input.MaxLastApprovalDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LastApprovalNameFilter), e => e.LastApprovalName.Contains(input.LastApprovalNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterFilter), e => e.CostCenter.Contains(input.CostCenterFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterDescriptionFilter), e => e.CostCenterDescription.Contains(input.CostCenterDescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WBSElementFilter), e => e.WBSElement.Contains(input.WBSElementFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AssetFilter), e => e.Asset.Contains(input.AssetFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FundsCenterFilter), e => e.FundsCenter.Contains(input.FundsCenterFilter))
                        .WhereIf(input.MinRemainQuantityFilter != null, e => e.RemainQuantity >= input.MinRemainQuantityFilter)
                        .WhereIf(input.MaxRemainQuantityFilter != null, e => e.RemainQuantity <= input.MaxRemainQuantityFilter)
                        .WhereIf(input.MinCreatedDateFilter != null, e => e.CreatedDate >= input.MinCreatedDateFilter)
                        .WhereIf(input.MaxCreatedDateFilter != null, e => e.CreatedDate <= input.MaxCreatedDateFilter)
                        .WhereIf(input.MinUpdatedDateFilter != null, e => e.UpdatedDate >= input.MinUpdatedDateFilter)
                        .WhereIf(input.MaxUpdatedDateFilter != null, e => e.UpdatedDate <= input.MaxUpdatedDateFilter);

            var query = (from o in filteredZMM020R
                         select new GetZMM020RForViewDto()
                         {
                             ZMM020R = new ZMM020RDto
                             {
                                 PurchaseRequisition = o.PurchaseRequisition,
                                 DocumentType = o.DocumentType,
                                 DocumentTypeText = o.DocumentTypeText,
                                 ItemRequisition = o.ItemRequisition,
                                 ProcessingStatusCode = o.ProcessingStatusCode,
                                 ProcessingStatus = o.ProcessingStatus,
                                 DeletionIndicator = o.DeletionIndicator,
                                 ItemCategory = o.ItemCategory,
                                 AccountAssignment = o.AccountAssignment,
                                 Material = o.Material,
                                 ShortText = o.ShortText,
                                 QuantityRequested = o.QuantityRequested,
                                 UnitOfMeasure = o.UnitOfMeasure,
                                 ServiceItem = o.ServiceItem,
                                 Service = o.Service,
                                 ServiceShortText = o.ServiceShortText,
                                 QuantityService = o.QuantityService,
                                 UnitOfMeasureService = o.UnitOfMeasureService,
                                 DeliveryDate = o.DeliveryDate,
                                 MaterialGroup = o.MaterialGroup,
                                 Plant = o.Plant,
                                 StorageLocation = o.StorageLocation,
                                 PurchaseGroup = o.PurchaseGroup,
                                 Requisitioner = o.Requisitioner,
                                 RequisitionerName = o.RequisitionerName,
                                 PurchasingDocument = o.PurchasingDocument,
                                 PurchaseOrderDate = o.PurchaseOrderDate,
                                 OutlineAgreement = o.OutlineAgreement,
                                 PrincAgreementItem = o.PrincAgreementItem,
                                 PurchasingInfoRec = o.PurchasingInfoRec,
                                 Status = o.Status,
                                 CreatedBy = o.CreatedBy,
                                 Currency = o.Currency,
                                 EntrySheet = o.EntrySheet,
                                 GoodsReceipt = o.GoodsReceipt,
                                 SupplierCode = o.SupplierCode,
                                 SupplierName = o.SupplierName,
                                 ReleaseIndicator = o.ReleaseIndicator,
                                 UnitPrice = o.UnitPrice,
                                 ValuationPrice = o.ValuationPrice,
                                 ItemText = o.ItemText,
                                 LongText = o.LongText,
                                 FirstApprovalDate = o.FirstApprovalDate,
                                 FirstApprovalName = o.FirstApprovalName,
                                 LastApprovalDate = o.LastApprovalDate,
                                 LastApprovalName = o.LastApprovalName,
                                 CostCenter = o.CostCenter,
                                 CostCenterDescription = o.CostCenterDescription,
                                 WBSElement = o.WBSElement,
                                 Asset = o.Asset,
                                 FundsCenter = o.FundsCenter,
                                 RemainQuantity = o.RemainQuantity,
                                 CreatedDate = o.CreatedDate,
                                 UpdatedDate = o.UpdatedDate,
                                 Id = o.Id
                             }
                         });

            var zmM020RListDtos = await query.ToListAsync();

            return _zmM020RExcelExporter.ExportToFile(zmM020RListDtos);
        }

    }
}