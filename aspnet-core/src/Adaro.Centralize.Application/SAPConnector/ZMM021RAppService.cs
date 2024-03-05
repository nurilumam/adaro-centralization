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
    [AbpAuthorize(AppPermissions.Pages_ZMM021R)]
    public class ZMM021RAppService : CentralizeAppServiceBase, IZMM021RAppService
    {
        private readonly IRepository<ZMM021R, Guid> _zmM021RRepository;
        private readonly IZMM021RExcelExporter _zmM021RExcelExporter;

        public ZMM021RAppService(IRepository<ZMM021R, Guid> zmM021RRepository, IZMM021RExcelExporter zmM021RExcelExporter)
        {
            _zmM021RRepository = zmM021RRepository;
            _zmM021RExcelExporter = zmM021RExcelExporter;

        }

        public virtual async Task<PagedResultDto<GetZMM021RForViewDto>> GetAll(GetAllZMM021RInput input)
        {

            var filteredZMM021R = _zmM021RRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.PurchasingDocument.Contains(input.Filter) || e.PurchasingDocType.Contains(input.Filter) || e.PurchasingDocTypeDescription.Contains(input.Filter) || e.Item.Contains(input.Filter) || e.LineNumber.Contains(input.Filter) || e.DeletionIndicator.Contains(input.Filter) || e.PurchaseRequisition.Contains(input.Filter) || e.ItemPR.Contains(input.Filter) || e.SupplierCode.Contains(input.Filter) || e.SupplierName.Contains(input.Filter) || e.Address.Contains(input.Filter) || e.ItemNo.Contains(input.Filter) || e.MaterialGroup.Contains(input.Filter) || e.ShortText.Contains(input.Filter) || e.OrderUnit.Contains(input.Filter) || e.Currency.Contains(input.Filter) || e.ReleaseIndicator.Contains(input.Filter) || e.Plant.Contains(input.Filter) || e.PurchasingGroup.Contains(input.Filter) || e.TaxCode.Contains(input.Filter) || e.CollectiveNumber.Contains(input.Filter) || e.ItemCategory.Contains(input.Filter) || e.AccountAssignment.Contains(input.Filter) || e.OutlineAgreement.Contains(input.Filter) || e.RFQNo.Contains(input.Filter) || e.MaterialService.Contains(input.Filter) || e.ApprovalStatus.Contains(input.Filter) || e.POStatus.Contains(input.Filter) || e.Period.Contains(input.Filter) || e.CommentVendor.Contains(input.Filter) || e.ItemText.Contains(input.Filter) || e.LongText.Contains(input.Filter) || e.OurReference.Contains(input.Filter) || e.POApprovalName.Contains(input.Filter) || e.BuyerCode.Contains(input.Filter) || e.BuyerName.Contains(input.Filter) || e.PICDept.Contains(input.Filter) || e.PICSect.Contains(input.Filter) || e.FuelAllocation.Contains(input.Filter) || e.CostCenter.Contains(input.Filter) || e.CostCenterDescription.Contains(input.Filter) || e.WBSElement.Contains(input.Filter) || e.AssetNo.Contains(input.Filter) || e.FundCenter.Contains(input.Filter) || e.DocumentId.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchasingDocumentFilter), e => e.PurchasingDocument.Contains(input.PurchasingDocumentFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchasingDocTypeFilter), e => e.PurchasingDocType.Contains(input.PurchasingDocTypeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchasingDocTypeDescriptionFilter), e => e.PurchasingDocTypeDescription.Contains(input.PurchasingDocTypeDescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ItemFilter), e => e.Item.Contains(input.ItemFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LineNumberFilter), e => e.LineNumber.Contains(input.LineNumberFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DeletionIndicatorFilter), e => e.DeletionIndicator.Contains(input.DeletionIndicatorFilter))
                        .WhereIf(input.MinDocumentDateFilter != null, e => e.DocumentDate >= input.MinDocumentDateFilter)
                        .WhereIf(input.MaxDocumentDateFilter != null, e => e.DocumentDate <= input.MaxDocumentDateFilter)
                        .WhereIf(input.MinCreatedOnFilter != null, e => e.CreatedOn >= input.MinCreatedOnFilter)
                        .WhereIf(input.MaxCreatedOnFilter != null, e => e.CreatedOn <= input.MaxCreatedOnFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchaseRequisitionFilter), e => e.PurchaseRequisition.Contains(input.PurchaseRequisitionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ItemPRFilter), e => e.ItemPR.Contains(input.ItemPRFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SupplierCodeFilter), e => e.SupplierCode.Contains(input.SupplierCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SupplierNameFilter), e => e.SupplierName.Contains(input.SupplierNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AddressFilter), e => e.Address.Contains(input.AddressFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ItemNoFilter), e => e.ItemNo.Contains(input.ItemNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialGroupFilter), e => e.MaterialGroup.Contains(input.MaterialGroupFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ShortTextFilter), e => e.ShortText.Contains(input.ShortTextFilter))
                        .WhereIf(input.MinOrderQuantityFilter != null, e => e.OrderQuantity >= input.MinOrderQuantityFilter)
                        .WhereIf(input.MaxOrderQuantityFilter != null, e => e.OrderQuantity <= input.MaxOrderQuantityFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OrderUnitFilter), e => e.OrderUnit.Contains(input.OrderUnitFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CurrencyFilter), e => e.Currency.Contains(input.CurrencyFilter))
                        .WhereIf(input.MinDeliveryDateFilter != null, e => e.DeliveryDate >= input.MinDeliveryDateFilter)
                        .WhereIf(input.MaxDeliveryDateFilter != null, e => e.DeliveryDate <= input.MaxDeliveryDateFilter)
                        .WhereIf(input.MinNetPriceFilter != null, e => e.NetPrice >= input.MinNetPriceFilter)
                        .WhereIf(input.MaxNetPriceFilter != null, e => e.NetPrice <= input.MaxNetPriceFilter)
                        .WhereIf(input.MinNetOrderValueFilter != null, e => e.NetOrderValue >= input.MinNetOrderValueFilter)
                        .WhereIf(input.MaxNetOrderValueFilter != null, e => e.NetOrderValue <= input.MaxNetOrderValueFilter)
                        .WhereIf(input.MinDemurrageFilter != null, e => e.Demurrage >= input.MinDemurrageFilter)
                        .WhereIf(input.MaxDemurrageFilter != null, e => e.Demurrage <= input.MaxDemurrageFilter)
                        .WhereIf(input.MinGrossPriceFilter != null, e => e.GrossPrice >= input.MinGrossPriceFilter)
                        .WhereIf(input.MaxGrossPriceFilter != null, e => e.GrossPrice <= input.MaxGrossPriceFilter)
                        .WhereIf(input.MinTotalDiscountFilter != null, e => e.TotalDiscount >= input.MinTotalDiscountFilter)
                        .WhereIf(input.MaxTotalDiscountFilter != null, e => e.TotalDiscount <= input.MaxTotalDiscountFilter)
                        .WhereIf(input.MinFreightCostFilter != null, e => e.FreightCost >= input.MinFreightCostFilter)
                        .WhereIf(input.MaxFreightCostFilter != null, e => e.FreightCost <= input.MaxFreightCostFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ReleaseIndicatorFilter), e => e.ReleaseIndicator.Contains(input.ReleaseIndicatorFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PlantFilter), e => e.Plant.Contains(input.PlantFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchasingGroupFilter), e => e.PurchasingGroup.Contains(input.PurchasingGroupFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TaxCodeFilter), e => e.TaxCode.Contains(input.TaxCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CollectiveNumberFilter), e => e.CollectiveNumber.Contains(input.CollectiveNumberFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ItemCategoryFilter), e => e.ItemCategory.Contains(input.ItemCategoryFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AccountAssignmentFilter), e => e.AccountAssignment.Contains(input.AccountAssignmentFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OutlineAgreementFilter), e => e.OutlineAgreement.Contains(input.OutlineAgreementFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RFQNoFilter), e => e.RFQNo.Contains(input.RFQNoFilter))
                        .WhereIf(input.MinQtyPendingFilter != null, e => e.QtyPending >= input.MinQtyPendingFilter)
                        .WhereIf(input.MaxQtyPendingFilter != null, e => e.QtyPending <= input.MaxQtyPendingFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialServiceFilter), e => e.MaterialService.Contains(input.MaterialServiceFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ApprovalStatusFilter), e => e.ApprovalStatus.Contains(input.ApprovalStatusFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.POStatusFilter), e => e.POStatus.Contains(input.POStatusFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PeriodFilter), e => e.Period.Contains(input.PeriodFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CommentVendorFilter), e => e.CommentVendor.Contains(input.CommentVendorFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ItemTextFilter), e => e.ItemText.Contains(input.ItemTextFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LongTextFilter), e => e.LongText.Contains(input.LongTextFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OurReferenceFilter), e => e.OurReference.Contains(input.OurReferenceFilter))
                        .WhereIf(input.MinPRFinalFirstApprovalDateFilter != null, e => e.PRFinalFirstApprovalDate >= input.MinPRFinalFirstApprovalDateFilter)
                        .WhereIf(input.MaxPRFinalFirstApprovalDateFilter != null, e => e.PRFinalFirstApprovalDate <= input.MaxPRFinalFirstApprovalDateFilter)
                        .WhereIf(input.MinPRFinalLastApprovalDateFilter != null, e => e.PRFinalLastApprovalDate >= input.MinPRFinalLastApprovalDateFilter)
                        .WhereIf(input.MaxPRFinalLastApprovalDateFilter != null, e => e.PRFinalLastApprovalDate <= input.MaxPRFinalLastApprovalDateFilter)
                        .WhereIf(input.MinPOFirstApprovalDateFilter != null, e => e.POFirstApprovalDate >= input.MinPOFirstApprovalDateFilter)
                        .WhereIf(input.MaxPOFirstApprovalDateFilter != null, e => e.POFirstApprovalDate <= input.MaxPOFirstApprovalDateFilter)
                        .WhereIf(input.MinPOLastApprovalDateFilter != null, e => e.POLastApprovalDate >= input.MinPOLastApprovalDateFilter)
                        .WhereIf(input.MaxPOLastApprovalDateFilter != null, e => e.POLastApprovalDate <= input.MaxPOLastApprovalDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.POApprovalNameFilter), e => e.POApprovalName.Contains(input.POApprovalNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BuyerCodeFilter), e => e.BuyerCode.Contains(input.BuyerCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BuyerNameFilter), e => e.BuyerName.Contains(input.BuyerNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PICDeptFilter), e => e.PICDept.Contains(input.PICDeptFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PICSectFilter), e => e.PICSect.Contains(input.PICSectFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FuelAllocationFilter), e => e.FuelAllocation.Contains(input.FuelAllocationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterFilter), e => e.CostCenter.Contains(input.CostCenterFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterDescriptionFilter), e => e.CostCenterDescription.Contains(input.CostCenterDescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WBSElementFilter), e => e.WBSElement.Contains(input.WBSElementFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AssetNoFilter), e => e.AssetNo.Contains(input.AssetNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FundCenterFilter), e => e.FundCenter.Contains(input.FundCenterFilter))
                        .WhereIf(input.MinCreatedDateFilter != null, e => e.CreatedDate >= input.MinCreatedDateFilter)
                        .WhereIf(input.MaxCreatedDateFilter != null, e => e.CreatedDate <= input.MaxCreatedDateFilter)
                        .WhereIf(input.MinUpdatedDateFilter != null, e => e.UpdatedDate >= input.MinUpdatedDateFilter)
                        .WhereIf(input.MaxUpdatedDateFilter != null, e => e.UpdatedDate <= input.MaxUpdatedDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DocumentIdFilter), e => e.DocumentId.Contains(input.DocumentIdFilter));

            var pagedAndFilteredZMM021R = filteredZMM021R
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var zmM021R = from o in pagedAndFilteredZMM021R
                          select new
                          {

                              o.PurchasingDocument,
                              o.PurchasingDocType,
                              o.PurchasingDocTypeDescription,
                              o.Item,
                              o.LineNumber,
                              o.DeletionIndicator,
                              o.DocumentDate,
                              o.CreatedOn,
                              o.PurchaseRequisition,
                              o.ItemPR,
                              o.SupplierCode,
                              o.SupplierName,
                              o.Address,
                              o.ItemNo,
                              o.MaterialGroup,
                              o.ShortText,
                              o.OrderQuantity,
                              o.OrderUnit,
                              o.Currency,
                              o.DeliveryDate,
                              o.NetPrice,
                              o.NetOrderValue,
                              o.Demurrage,
                              o.GrossPrice,
                              o.TotalDiscount,
                              o.FreightCost,
                              o.ReleaseIndicator,
                              o.Plant,
                              o.PurchasingGroup,
                              o.TaxCode,
                              o.CollectiveNumber,
                              o.ItemCategory,
                              o.AccountAssignment,
                              o.OutlineAgreement,
                              o.RFQNo,
                              o.QtyPending,
                              o.MaterialService,
                              o.ApprovalStatus,
                              o.POStatus,
                              o.Period,
                              o.CommentVendor,
                              o.ItemText,
                              o.LongText,
                              o.OurReference,
                              o.PRFinalFirstApprovalDate,
                              o.PRFinalLastApprovalDate,
                              o.POFirstApprovalDate,
                              o.POLastApprovalDate,
                              o.POApprovalName,
                              o.BuyerCode,
                              o.BuyerName,
                              o.PICDept,
                              o.PICSect,
                              o.FuelAllocation,
                              o.CostCenter,
                              o.CostCenterDescription,
                              o.WBSElement,
                              o.AssetNo,
                              o.FundCenter,
                              o.CreatedDate,
                              o.UpdatedDate,
                              Id = o.Id
                          };

            var totalCount = await filteredZMM021R.CountAsync();

            var dbList = await zmM021R.ToListAsync();
            var results = new List<GetZMM021RForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetZMM021RForViewDto()
                {
                    ZMM021R = new ZMM021RDto
                    {

                        PurchasingDocument = o.PurchasingDocument,
                        PurchasingDocType = o.PurchasingDocType,
                        PurchasingDocTypeDescription = o.PurchasingDocTypeDescription,
                        Item = o.Item,
                        LineNumber = o.LineNumber,
                        DeletionIndicator = o.DeletionIndicator,
                        DocumentDate = o.DocumentDate,
                        CreatedOn = o.CreatedOn,
                        PurchaseRequisition = o.PurchaseRequisition,
                        ItemPR = o.ItemPR,
                        SupplierCode = o.SupplierCode,
                        SupplierName = o.SupplierName,
                        Address = o.Address,
                        ItemNo = o.ItemNo,
                        MaterialGroup = o.MaterialGroup,
                        ShortText = o.ShortText,
                        OrderQuantity = o.OrderQuantity,
                        OrderUnit = o.OrderUnit,
                        Currency = o.Currency,
                        DeliveryDate = o.DeliveryDate,
                        NetPrice = o.NetPrice,
                        NetOrderValue = o.NetOrderValue,
                        Demurrage = o.Demurrage,
                        GrossPrice = o.GrossPrice,
                        TotalDiscount = o.TotalDiscount,
                        FreightCost = o.FreightCost,
                        ReleaseIndicator = o.ReleaseIndicator,
                        Plant = o.Plant,
                        PurchasingGroup = o.PurchasingGroup,
                        TaxCode = o.TaxCode,
                        CollectiveNumber = o.CollectiveNumber,
                        ItemCategory = o.ItemCategory,
                        AccountAssignment = o.AccountAssignment,
                        OutlineAgreement = o.OutlineAgreement,
                        RFQNo = o.RFQNo,
                        QtyPending = o.QtyPending,
                        MaterialService = o.MaterialService,
                        ApprovalStatus = o.ApprovalStatus,
                        POStatus = o.POStatus,
                        Period = o.Period,
                        CommentVendor = o.CommentVendor,
                        ItemText = o.ItemText,
                        LongText = o.LongText,
                        OurReference = o.OurReference,
                        PRFinalFirstApprovalDate = o.PRFinalFirstApprovalDate,
                        PRFinalLastApprovalDate = o.PRFinalLastApprovalDate,
                        POFirstApprovalDate = o.POFirstApprovalDate,
                        POLastApprovalDate = o.POLastApprovalDate,
                        POApprovalName = o.POApprovalName,
                        BuyerCode = o.BuyerCode,
                        BuyerName = o.BuyerName,
                        PICDept = o.PICDept,
                        PICSect = o.PICSect,
                        FuelAllocation = o.FuelAllocation,
                        CostCenter = o.CostCenter,
                        CostCenterDescription = o.CostCenterDescription,
                        WBSElement = o.WBSElement,
                        AssetNo = o.AssetNo,
                        FundCenter = o.FundCenter,
                        CreatedDate = o.CreatedDate,
                        UpdatedDate = o.UpdatedDate,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetZMM021RForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetZMM021RForViewDto> GetZMM021RForView(Guid id)
        {
            var zmM021R = await _zmM021RRepository.GetAsync(id);

            var output = new GetZMM021RForViewDto { ZMM021R = ObjectMapper.Map<ZMM021RDto>(zmM021R) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ZMM021R_Edit)]
        public virtual async Task<GetZMM021RForEditOutput> GetZMM021RForEdit(EntityDto<Guid> input)
        {
            var zmM021R = await _zmM021RRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetZMM021RForEditOutput { ZMM021R = ObjectMapper.Map<CreateOrEditZMM021RDto>(zmM021R) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditZMM021RDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ZMM021R_Create)]
        protected virtual async Task Create(CreateOrEditZMM021RDto input)
        {
            var zmM021R = ObjectMapper.Map<ZMM021R>(input);

            if (AbpSession.TenantId != null)
            {
                zmM021R.TenantId = (int?)AbpSession.TenantId;
            }

            await _zmM021RRepository.InsertAsync(zmM021R);

        }

        [AbpAuthorize(AppPermissions.Pages_ZMM021R_Edit)]
        protected virtual async Task Update(CreateOrEditZMM021RDto input)
        {
            var zmM021R = await _zmM021RRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, zmM021R);

        }

        [AbpAuthorize(AppPermissions.Pages_ZMM021R_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _zmM021RRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetZMM021RToExcel(GetAllZMM021RForExcelInput input)
        {

            var filteredZMM021R = _zmM021RRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.PurchasingDocument.Contains(input.Filter) || e.PurchasingDocType.Contains(input.Filter) || e.PurchasingDocTypeDescription.Contains(input.Filter) || e.Item.Contains(input.Filter) || e.LineNumber.Contains(input.Filter) || e.DeletionIndicator.Contains(input.Filter) || e.PurchaseRequisition.Contains(input.Filter) || e.ItemPR.Contains(input.Filter) || e.SupplierCode.Contains(input.Filter) || e.SupplierName.Contains(input.Filter) || e.Address.Contains(input.Filter) || e.ItemNo.Contains(input.Filter) || e.MaterialGroup.Contains(input.Filter) || e.ShortText.Contains(input.Filter) || e.OrderUnit.Contains(input.Filter) || e.Currency.Contains(input.Filter) || e.ReleaseIndicator.Contains(input.Filter) || e.Plant.Contains(input.Filter) || e.PurchasingGroup.Contains(input.Filter) || e.TaxCode.Contains(input.Filter) || e.CollectiveNumber.Contains(input.Filter) || e.ItemCategory.Contains(input.Filter) || e.AccountAssignment.Contains(input.Filter) || e.OutlineAgreement.Contains(input.Filter) || e.RFQNo.Contains(input.Filter) || e.MaterialService.Contains(input.Filter) || e.ApprovalStatus.Contains(input.Filter) || e.POStatus.Contains(input.Filter) || e.Period.Contains(input.Filter) || e.CommentVendor.Contains(input.Filter) || e.ItemText.Contains(input.Filter) || e.LongText.Contains(input.Filter) || e.OurReference.Contains(input.Filter) || e.POApprovalName.Contains(input.Filter) || e.BuyerCode.Contains(input.Filter) || e.BuyerName.Contains(input.Filter) || e.PICDept.Contains(input.Filter) || e.PICSect.Contains(input.Filter) || e.FuelAllocation.Contains(input.Filter) || e.CostCenter.Contains(input.Filter) || e.CostCenterDescription.Contains(input.Filter) || e.WBSElement.Contains(input.Filter) || e.AssetNo.Contains(input.Filter) || e.FundCenter.Contains(input.Filter) || e.DocumentId.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchasingDocumentFilter), e => e.PurchasingDocument.Contains(input.PurchasingDocumentFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchasingDocTypeFilter), e => e.PurchasingDocType.Contains(input.PurchasingDocTypeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchasingDocTypeDescriptionFilter), e => e.PurchasingDocTypeDescription.Contains(input.PurchasingDocTypeDescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ItemFilter), e => e.Item.Contains(input.ItemFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LineNumberFilter), e => e.LineNumber.Contains(input.LineNumberFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DeletionIndicatorFilter), e => e.DeletionIndicator.Contains(input.DeletionIndicatorFilter))
                        .WhereIf(input.MinDocumentDateFilter != null, e => e.DocumentDate >= input.MinDocumentDateFilter)
                        .WhereIf(input.MaxDocumentDateFilter != null, e => e.DocumentDate <= input.MaxDocumentDateFilter)
                        .WhereIf(input.MinCreatedOnFilter != null, e => e.CreatedOn >= input.MinCreatedOnFilter)
                        .WhereIf(input.MaxCreatedOnFilter != null, e => e.CreatedOn <= input.MaxCreatedOnFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchaseRequisitionFilter), e => e.PurchaseRequisition.Contains(input.PurchaseRequisitionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ItemPRFilter), e => e.ItemPR.Contains(input.ItemPRFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SupplierCodeFilter), e => e.SupplierCode.Contains(input.SupplierCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SupplierNameFilter), e => e.SupplierName.Contains(input.SupplierNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AddressFilter), e => e.Address.Contains(input.AddressFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ItemNoFilter), e => e.ItemNo.Contains(input.ItemNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialGroupFilter), e => e.MaterialGroup.Contains(input.MaterialGroupFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ShortTextFilter), e => e.ShortText.Contains(input.ShortTextFilter))
                        .WhereIf(input.MinOrderQuantityFilter != null, e => e.OrderQuantity >= input.MinOrderQuantityFilter)
                        .WhereIf(input.MaxOrderQuantityFilter != null, e => e.OrderQuantity <= input.MaxOrderQuantityFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OrderUnitFilter), e => e.OrderUnit.Contains(input.OrderUnitFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CurrencyFilter), e => e.Currency.Contains(input.CurrencyFilter))
                        .WhereIf(input.MinDeliveryDateFilter != null, e => e.DeliveryDate >= input.MinDeliveryDateFilter)
                        .WhereIf(input.MaxDeliveryDateFilter != null, e => e.DeliveryDate <= input.MaxDeliveryDateFilter)
                        .WhereIf(input.MinNetPriceFilter != null, e => e.NetPrice >= input.MinNetPriceFilter)
                        .WhereIf(input.MaxNetPriceFilter != null, e => e.NetPrice <= input.MaxNetPriceFilter)
                        .WhereIf(input.MinNetOrderValueFilter != null, e => e.NetOrderValue >= input.MinNetOrderValueFilter)
                        .WhereIf(input.MaxNetOrderValueFilter != null, e => e.NetOrderValue <= input.MaxNetOrderValueFilter)
                        .WhereIf(input.MinDemurrageFilter != null, e => e.Demurrage >= input.MinDemurrageFilter)
                        .WhereIf(input.MaxDemurrageFilter != null, e => e.Demurrage <= input.MaxDemurrageFilter)
                        .WhereIf(input.MinGrossPriceFilter != null, e => e.GrossPrice >= input.MinGrossPriceFilter)
                        .WhereIf(input.MaxGrossPriceFilter != null, e => e.GrossPrice <= input.MaxGrossPriceFilter)
                        .WhereIf(input.MinTotalDiscountFilter != null, e => e.TotalDiscount >= input.MinTotalDiscountFilter)
                        .WhereIf(input.MaxTotalDiscountFilter != null, e => e.TotalDiscount <= input.MaxTotalDiscountFilter)
                        .WhereIf(input.MinFreightCostFilter != null, e => e.FreightCost >= input.MinFreightCostFilter)
                        .WhereIf(input.MaxFreightCostFilter != null, e => e.FreightCost <= input.MaxFreightCostFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ReleaseIndicatorFilter), e => e.ReleaseIndicator.Contains(input.ReleaseIndicatorFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PlantFilter), e => e.Plant.Contains(input.PlantFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchasingGroupFilter), e => e.PurchasingGroup.Contains(input.PurchasingGroupFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TaxCodeFilter), e => e.TaxCode.Contains(input.TaxCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CollectiveNumberFilter), e => e.CollectiveNumber.Contains(input.CollectiveNumberFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ItemCategoryFilter), e => e.ItemCategory.Contains(input.ItemCategoryFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AccountAssignmentFilter), e => e.AccountAssignment.Contains(input.AccountAssignmentFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OutlineAgreementFilter), e => e.OutlineAgreement.Contains(input.OutlineAgreementFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RFQNoFilter), e => e.RFQNo.Contains(input.RFQNoFilter))
                        .WhereIf(input.MinQtyPendingFilter != null, e => e.QtyPending >= input.MinQtyPendingFilter)
                        .WhereIf(input.MaxQtyPendingFilter != null, e => e.QtyPending <= input.MaxQtyPendingFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaterialServiceFilter), e => e.MaterialService.Contains(input.MaterialServiceFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ApprovalStatusFilter), e => e.ApprovalStatus.Contains(input.ApprovalStatusFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.POStatusFilter), e => e.POStatus.Contains(input.POStatusFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PeriodFilter), e => e.Period.Contains(input.PeriodFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CommentVendorFilter), e => e.CommentVendor.Contains(input.CommentVendorFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ItemTextFilter), e => e.ItemText.Contains(input.ItemTextFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LongTextFilter), e => e.LongText.Contains(input.LongTextFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OurReferenceFilter), e => e.OurReference.Contains(input.OurReferenceFilter))
                        .WhereIf(input.MinPRFinalFirstApprovalDateFilter != null, e => e.PRFinalFirstApprovalDate >= input.MinPRFinalFirstApprovalDateFilter)
                        .WhereIf(input.MaxPRFinalFirstApprovalDateFilter != null, e => e.PRFinalFirstApprovalDate <= input.MaxPRFinalFirstApprovalDateFilter)
                        .WhereIf(input.MinPRFinalLastApprovalDateFilter != null, e => e.PRFinalLastApprovalDate >= input.MinPRFinalLastApprovalDateFilter)
                        .WhereIf(input.MaxPRFinalLastApprovalDateFilter != null, e => e.PRFinalLastApprovalDate <= input.MaxPRFinalLastApprovalDateFilter)
                        .WhereIf(input.MinPOFirstApprovalDateFilter != null, e => e.POFirstApprovalDate >= input.MinPOFirstApprovalDateFilter)
                        .WhereIf(input.MaxPOFirstApprovalDateFilter != null, e => e.POFirstApprovalDate <= input.MaxPOFirstApprovalDateFilter)
                        .WhereIf(input.MinPOLastApprovalDateFilter != null, e => e.POLastApprovalDate >= input.MinPOLastApprovalDateFilter)
                        .WhereIf(input.MaxPOLastApprovalDateFilter != null, e => e.POLastApprovalDate <= input.MaxPOLastApprovalDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.POApprovalNameFilter), e => e.POApprovalName.Contains(input.POApprovalNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BuyerCodeFilter), e => e.BuyerCode.Contains(input.BuyerCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BuyerNameFilter), e => e.BuyerName.Contains(input.BuyerNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PICDeptFilter), e => e.PICDept.Contains(input.PICDeptFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PICSectFilter), e => e.PICSect.Contains(input.PICSectFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FuelAllocationFilter), e => e.FuelAllocation.Contains(input.FuelAllocationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterFilter), e => e.CostCenter.Contains(input.CostCenterFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CostCenterDescriptionFilter), e => e.CostCenterDescription.Contains(input.CostCenterDescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WBSElementFilter), e => e.WBSElement.Contains(input.WBSElementFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AssetNoFilter), e => e.AssetNo.Contains(input.AssetNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FundCenterFilter), e => e.FundCenter.Contains(input.FundCenterFilter))
                        .WhereIf(input.MinCreatedDateFilter != null, e => e.CreatedDate >= input.MinCreatedDateFilter)
                        .WhereIf(input.MaxCreatedDateFilter != null, e => e.CreatedDate <= input.MaxCreatedDateFilter)
                        .WhereIf(input.MinUpdatedDateFilter != null, e => e.UpdatedDate >= input.MinUpdatedDateFilter)
                        .WhereIf(input.MaxUpdatedDateFilter != null, e => e.UpdatedDate <= input.MaxUpdatedDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DocumentIdFilter), e => e.DocumentId.Contains(input.DocumentIdFilter));

            var query = (from o in filteredZMM021R
                         select new GetZMM021RForViewDto()
                         {
                             ZMM021R = new ZMM021RDto
                             {
                                 PurchasingDocument = o.PurchasingDocument,
                                 PurchasingDocType = o.PurchasingDocType,
                                 PurchasingDocTypeDescription = o.PurchasingDocTypeDescription,
                                 Item = o.Item,
                                 LineNumber = o.LineNumber,
                                 DeletionIndicator = o.DeletionIndicator,
                                 DocumentDate = o.DocumentDate,
                                 CreatedOn = o.CreatedOn,
                                 PurchaseRequisition = o.PurchaseRequisition,
                                 ItemPR = o.ItemPR,
                                 SupplierCode = o.SupplierCode,
                                 SupplierName = o.SupplierName,
                                 Address = o.Address,
                                 ItemNo = o.ItemNo,
                                 MaterialGroup = o.MaterialGroup,
                                 ShortText = o.ShortText,
                                 OrderQuantity = o.OrderQuantity,
                                 OrderUnit = o.OrderUnit,
                                 Currency = o.Currency,
                                 DeliveryDate = o.DeliveryDate,
                                 NetPrice = o.NetPrice,
                                 NetOrderValue = o.NetOrderValue,
                                 Demurrage = o.Demurrage,
                                 GrossPrice = o.GrossPrice,
                                 TotalDiscount = o.TotalDiscount,
                                 FreightCost = o.FreightCost,
                                 ReleaseIndicator = o.ReleaseIndicator,
                                 Plant = o.Plant,
                                 PurchasingGroup = o.PurchasingGroup,
                                 TaxCode = o.TaxCode,
                                 CollectiveNumber = o.CollectiveNumber,
                                 ItemCategory = o.ItemCategory,
                                 AccountAssignment = o.AccountAssignment,
                                 OutlineAgreement = o.OutlineAgreement,
                                 RFQNo = o.RFQNo,
                                 QtyPending = o.QtyPending,
                                 MaterialService = o.MaterialService,
                                 ApprovalStatus = o.ApprovalStatus,
                                 POStatus = o.POStatus,
                                 Period = o.Period,
                                 CommentVendor = o.CommentVendor,
                                 ItemText = o.ItemText,
                                 LongText = o.LongText,
                                 OurReference = o.OurReference,
                                 PRFinalFirstApprovalDate = o.PRFinalFirstApprovalDate,
                                 PRFinalLastApprovalDate = o.PRFinalLastApprovalDate,
                                 POFirstApprovalDate = o.POFirstApprovalDate,
                                 POLastApprovalDate = o.POLastApprovalDate,
                                 POApprovalName = o.POApprovalName,
                                 BuyerCode = o.BuyerCode,
                                 BuyerName = o.BuyerName,
                                 PICDept = o.PICDept,
                                 PICSect = o.PICSect,
                                 FuelAllocation = o.FuelAllocation,
                                 CostCenter = o.CostCenter,
                                 CostCenterDescription = o.CostCenterDescription,
                                 WBSElement = o.WBSElement,
                                 AssetNo = o.AssetNo,
                                 FundCenter = o.FundCenter,
                                 CreatedDate = o.CreatedDate,
                                 UpdatedDate = o.UpdatedDate,
                                 Id = o.Id
                             }
                         });

            var zmM021RListDtos = await query.ToListAsync();

            return _zmM021RExcelExporter.ExportToFile(zmM021RListDtos);
        }

    }
}