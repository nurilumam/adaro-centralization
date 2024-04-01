import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ZMM020RServiceProxy, ZMM020RDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditZMM020RModalComponent } from './create-or-edit-zmM020R-modal.component';

import { ViewZMM020RModalComponent } from './view-zmM020R-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './zmM020R.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class ZMM020RComponent extends AppComponentBase {
    @ViewChild('createOrEditZMM020RModal', { static: true })
    createOrEditZMM020RModal: CreateOrEditZMM020RModalComponent;
    @ViewChild('viewZMM020RModal', { static: true }) viewZMM020RModal: ViewZMM020RModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    purchaseRequisitionFilter = '';
    documentTypeFilter = '';
    documentTypeTextFilter = '';
    itemRequisitionFilter = '';
    processingStatusCodeFilter = '';
    processingStatusFilter = '';
    deletionIndicatorFilter = '';
    itemCategoryFilter = '';
    accountAssignmentFilter = '';
    materialFilter = '';
    shortTextFilter = '';
    maxQuantityRequestedFilter: number;
    maxQuantityRequestedFilterEmpty: number;
    minQuantityRequestedFilter: number;
    minQuantityRequestedFilterEmpty: number;
    unitOfMeasureFilter = '';
    serviceItemFilter = '';
    serviceFilter = '';
    serviceShortTextFilter = '';
    maxQuantityServiceFilter: number;
    maxQuantityServiceFilterEmpty: number;
    minQuantityServiceFilter: number;
    minQuantityServiceFilterEmpty: number;
    unitOfMeasureServiceFilter = '';
    maxDeliveryDateFilter: DateTime;
    minDeliveryDateFilter: DateTime;
    materialGroupFilter = '';
    plantFilter = '';
    storageLocationFilter = '';
    purchaseGroupFilter = '';
    requisitionerFilter = '';
    requisitionerNameFilter = '';
    purchasingDocumentFilter = '';
    maxPurchaseOrderDateFilter: DateTime;
    minPurchaseOrderDateFilter: DateTime;
    outlineAgreementFilter = '';
    princAgreementItemFilter = '';
    purchasingInfoRecFilter = '';
    statusFilter = '';
    createdByFilter = '';
    currencyFilter = '';
    entrySheetFilter = '';
    goodsReceiptFilter = '';
    supplierCodeFilter = '';
    supplierNameFilter = '';
    releaseIndicatorFilter = '';
    maxUnitPriceFilter: number;
    maxUnitPriceFilterEmpty: number;
    minUnitPriceFilter: number;
    minUnitPriceFilterEmpty: number;
    maxValuationPriceFilter: number;
    maxValuationPriceFilterEmpty: number;
    minValuationPriceFilter: number;
    minValuationPriceFilterEmpty: number;
    itemTextFilter = '';
    longTextFilter = '';
    maxFirstApprovalDateFilter: DateTime;
    minFirstApprovalDateFilter: DateTime;
    firstApprovalNameFilter = '';
    maxLastApprovalDateFilter: DateTime;
    minLastApprovalDateFilter: DateTime;
    lastApprovalNameFilter = '';
    costCenterFilter = '';
    costCenterDescriptionFilter = '';
    wbsElementFilter = '';
    assetFilter = '';
    fundsCenterFilter = '';
    maxRemainQuantityFilter: number;
    maxRemainQuantityFilterEmpty: number;
    minRemainQuantityFilter: number;
    minRemainQuantityFilterEmpty: number;
    maxCreatedDateFilter: DateTime;
    minCreatedDateFilter: DateTime;
    maxUpdatedDateFilter: DateTime;
    minUpdatedDateFilter: DateTime;

    constructor(
        injector: Injector,
        private _zmM020RServiceProxy: ZMM020RServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getZMM020R(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records && this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._zmM020RServiceProxy
            .getAll(
                this.filterText,
                this.purchaseRequisitionFilter,
                this.documentTypeFilter,
                this.documentTypeTextFilter,
                this.itemRequisitionFilter,
                this.processingStatusCodeFilter,
                this.processingStatusFilter,
                this.deletionIndicatorFilter,
                this.itemCategoryFilter,
                this.accountAssignmentFilter,
                this.materialFilter,
                this.shortTextFilter,
                this.maxQuantityRequestedFilter == null
                    ? this.maxQuantityRequestedFilterEmpty
                    : this.maxQuantityRequestedFilter,
                this.minQuantityRequestedFilter == null
                    ? this.minQuantityRequestedFilterEmpty
                    : this.minQuantityRequestedFilter,
                this.unitOfMeasureFilter,
                this.serviceItemFilter,
                this.serviceFilter,
                this.serviceShortTextFilter,
                this.maxQuantityServiceFilter == null
                    ? this.maxQuantityServiceFilterEmpty
                    : this.maxQuantityServiceFilter,
                this.minQuantityServiceFilter == null
                    ? this.minQuantityServiceFilterEmpty
                    : this.minQuantityServiceFilter,
                this.unitOfMeasureServiceFilter,
                this.maxDeliveryDateFilter === undefined
                    ? this.maxDeliveryDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxDeliveryDateFilter),
                this.minDeliveryDateFilter === undefined
                    ? this.minDeliveryDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minDeliveryDateFilter),
                this.materialGroupFilter,
                this.plantFilter,
                this.storageLocationFilter,
                this.purchaseGroupFilter,
                this.requisitionerFilter,
                this.requisitionerNameFilter,
                this.purchasingDocumentFilter,
                this.maxPurchaseOrderDateFilter === undefined
                    ? this.maxPurchaseOrderDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxPurchaseOrderDateFilter),
                this.minPurchaseOrderDateFilter === undefined
                    ? this.minPurchaseOrderDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minPurchaseOrderDateFilter),
                this.outlineAgreementFilter,
                this.princAgreementItemFilter,
                this.purchasingInfoRecFilter,
                this.statusFilter,
                this.createdByFilter,
                this.currencyFilter,
                this.entrySheetFilter,
                this.goodsReceiptFilter,
                this.supplierCodeFilter,
                this.supplierNameFilter,
                this.releaseIndicatorFilter,
                this.maxUnitPriceFilter == null ? this.maxUnitPriceFilterEmpty : this.maxUnitPriceFilter,
                this.minUnitPriceFilter == null ? this.minUnitPriceFilterEmpty : this.minUnitPriceFilter,
                this.maxValuationPriceFilter == null ? this.maxValuationPriceFilterEmpty : this.maxValuationPriceFilter,
                this.minValuationPriceFilter == null ? this.minValuationPriceFilterEmpty : this.minValuationPriceFilter,
                this.itemTextFilter,
                this.longTextFilter,
                this.maxFirstApprovalDateFilter === undefined
                    ? this.maxFirstApprovalDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxFirstApprovalDateFilter),
                this.minFirstApprovalDateFilter === undefined
                    ? this.minFirstApprovalDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minFirstApprovalDateFilter),
                this.firstApprovalNameFilter,
                this.maxLastApprovalDateFilter === undefined
                    ? this.maxLastApprovalDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxLastApprovalDateFilter),
                this.minLastApprovalDateFilter === undefined
                    ? this.minLastApprovalDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minLastApprovalDateFilter),
                this.lastApprovalNameFilter,
                this.costCenterFilter,
                this.costCenterDescriptionFilter,
                this.wbsElementFilter,
                this.assetFilter,
                this.fundsCenterFilter,
                this.maxRemainQuantityFilter == null ? this.maxRemainQuantityFilterEmpty : this.maxRemainQuantityFilter,
                this.minRemainQuantityFilter == null ? this.minRemainQuantityFilterEmpty : this.minRemainQuantityFilter,
                this.maxCreatedDateFilter === undefined
                    ? this.maxCreatedDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxCreatedDateFilter),
                this.minCreatedDateFilter === undefined
                    ? this.minCreatedDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minCreatedDateFilter),
                this.maxUpdatedDateFilter === undefined
                    ? this.maxUpdatedDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxUpdatedDateFilter),
                this.minUpdatedDateFilter === undefined
                    ? this.minUpdatedDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minUpdatedDateFilter),
                this.primengTableHelper.getSorting(this.dataTable),
                this.primengTableHelper.getSkipCount(this.paginator, event),
                this.primengTableHelper.getMaxResultCount(this.paginator, event)
            )
            .subscribe((result) => {
                this.primengTableHelper.totalRecordsCount = result.totalCount;
                this.primengTableHelper.records = result.items;
                this.primengTableHelper.hideLoadingIndicator();
            });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createZMM020R(): void {
        this.createOrEditZMM020RModal.show();
    }

    deleteZMM020R(zmM020R: ZMM020RDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._zmM020RServiceProxy.delete(zmM020R.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._zmM020RServiceProxy
            .getZMM020RToExcel(
                this.filterText,
                this.purchaseRequisitionFilter,
                this.documentTypeFilter,
                this.documentTypeTextFilter,
                this.itemRequisitionFilter,
                this.processingStatusCodeFilter,
                this.processingStatusFilter,
                this.deletionIndicatorFilter,
                this.itemCategoryFilter,
                this.accountAssignmentFilter,
                this.materialFilter,
                this.shortTextFilter,
                this.maxQuantityRequestedFilter == null
                    ? this.maxQuantityRequestedFilterEmpty
                    : this.maxQuantityRequestedFilter,
                this.minQuantityRequestedFilter == null
                    ? this.minQuantityRequestedFilterEmpty
                    : this.minQuantityRequestedFilter,
                this.unitOfMeasureFilter,
                this.serviceItemFilter,
                this.serviceFilter,
                this.serviceShortTextFilter,
                this.maxQuantityServiceFilter == null
                    ? this.maxQuantityServiceFilterEmpty
                    : this.maxQuantityServiceFilter,
                this.minQuantityServiceFilter == null
                    ? this.minQuantityServiceFilterEmpty
                    : this.minQuantityServiceFilter,
                this.unitOfMeasureServiceFilter,
                this.maxDeliveryDateFilter === undefined
                    ? this.maxDeliveryDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxDeliveryDateFilter),
                this.minDeliveryDateFilter === undefined
                    ? this.minDeliveryDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minDeliveryDateFilter),
                this.materialGroupFilter,
                this.plantFilter,
                this.storageLocationFilter,
                this.purchaseGroupFilter,
                this.requisitionerFilter,
                this.requisitionerNameFilter,
                this.purchasingDocumentFilter,
                this.maxPurchaseOrderDateFilter === undefined
                    ? this.maxPurchaseOrderDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxPurchaseOrderDateFilter),
                this.minPurchaseOrderDateFilter === undefined
                    ? this.minPurchaseOrderDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minPurchaseOrderDateFilter),
                this.outlineAgreementFilter,
                this.princAgreementItemFilter,
                this.purchasingInfoRecFilter,
                this.statusFilter,
                this.createdByFilter,
                this.currencyFilter,
                this.entrySheetFilter,
                this.goodsReceiptFilter,
                this.supplierCodeFilter,
                this.supplierNameFilter,
                this.releaseIndicatorFilter,
                this.maxUnitPriceFilter == null ? this.maxUnitPriceFilterEmpty : this.maxUnitPriceFilter,
                this.minUnitPriceFilter == null ? this.minUnitPriceFilterEmpty : this.minUnitPriceFilter,
                this.maxValuationPriceFilter == null ? this.maxValuationPriceFilterEmpty : this.maxValuationPriceFilter,
                this.minValuationPriceFilter == null ? this.minValuationPriceFilterEmpty : this.minValuationPriceFilter,
                this.itemTextFilter,
                this.longTextFilter,
                this.maxFirstApprovalDateFilter === undefined
                    ? this.maxFirstApprovalDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxFirstApprovalDateFilter),
                this.minFirstApprovalDateFilter === undefined
                    ? this.minFirstApprovalDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minFirstApprovalDateFilter),
                this.firstApprovalNameFilter,
                this.maxLastApprovalDateFilter === undefined
                    ? this.maxLastApprovalDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxLastApprovalDateFilter),
                this.minLastApprovalDateFilter === undefined
                    ? this.minLastApprovalDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minLastApprovalDateFilter),
                this.lastApprovalNameFilter,
                this.costCenterFilter,
                this.costCenterDescriptionFilter,
                this.wbsElementFilter,
                this.assetFilter,
                this.fundsCenterFilter,
                this.maxRemainQuantityFilter == null ? this.maxRemainQuantityFilterEmpty : this.maxRemainQuantityFilter,
                this.minRemainQuantityFilter == null ? this.minRemainQuantityFilterEmpty : this.minRemainQuantityFilter,
                this.maxCreatedDateFilter === undefined
                    ? this.maxCreatedDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxCreatedDateFilter),
                this.minCreatedDateFilter === undefined
                    ? this.minCreatedDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minCreatedDateFilter),
                this.maxUpdatedDateFilter === undefined
                    ? this.maxUpdatedDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxUpdatedDateFilter),
                this.minUpdatedDateFilter === undefined
                    ? this.minUpdatedDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minUpdatedDateFilter)
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }

    resetFilters(): void {
        this.filterText = '';
        this.purchaseRequisitionFilter = '';
        this.documentTypeFilter = '';
        this.documentTypeTextFilter = '';
        this.itemRequisitionFilter = '';
        this.processingStatusCodeFilter = '';
        this.processingStatusFilter = '';
        this.deletionIndicatorFilter = '';
        this.itemCategoryFilter = '';
        this.accountAssignmentFilter = '';
        this.materialFilter = '';
        this.shortTextFilter = '';
        this.maxQuantityRequestedFilter = this.maxQuantityRequestedFilterEmpty;
        this.minQuantityRequestedFilter = this.maxQuantityRequestedFilterEmpty;
        this.unitOfMeasureFilter = '';
        this.serviceItemFilter = '';
        this.serviceFilter = '';
        this.serviceShortTextFilter = '';
        this.maxQuantityServiceFilter = this.maxQuantityServiceFilterEmpty;
        this.minQuantityServiceFilter = this.maxQuantityServiceFilterEmpty;
        this.unitOfMeasureServiceFilter = '';
        this.maxDeliveryDateFilter = undefined;
        this.minDeliveryDateFilter = undefined;
        this.materialGroupFilter = '';
        this.plantFilter = '';
        this.storageLocationFilter = '';
        this.purchaseGroupFilter = '';
        this.requisitionerFilter = '';
        this.requisitionerNameFilter = '';
        this.purchasingDocumentFilter = '';
        this.maxPurchaseOrderDateFilter = undefined;
        this.minPurchaseOrderDateFilter = undefined;
        this.outlineAgreementFilter = '';
        this.princAgreementItemFilter = '';
        this.purchasingInfoRecFilter = '';
        this.statusFilter = '';
        this.createdByFilter = '';
        this.currencyFilter = '';
        this.entrySheetFilter = '';
        this.goodsReceiptFilter = '';
        this.supplierCodeFilter = '';
        this.supplierNameFilter = '';
        this.releaseIndicatorFilter = '';
        this.maxUnitPriceFilter = this.maxUnitPriceFilterEmpty;
        this.minUnitPriceFilter = this.maxUnitPriceFilterEmpty;
        this.maxValuationPriceFilter = this.maxValuationPriceFilterEmpty;
        this.minValuationPriceFilter = this.maxValuationPriceFilterEmpty;
        this.itemTextFilter = '';
        this.longTextFilter = '';
        this.maxFirstApprovalDateFilter = undefined;
        this.minFirstApprovalDateFilter = undefined;
        this.firstApprovalNameFilter = '';
        this.maxLastApprovalDateFilter = undefined;
        this.minLastApprovalDateFilter = undefined;
        this.lastApprovalNameFilter = '';
        this.costCenterFilter = '';
        this.costCenterDescriptionFilter = '';
        this.wbsElementFilter = '';
        this.assetFilter = '';
        this.fundsCenterFilter = '';
        this.maxRemainQuantityFilter = this.maxRemainQuantityFilterEmpty;
        this.minRemainQuantityFilter = this.maxRemainQuantityFilterEmpty;
        this.maxCreatedDateFilter = undefined;
        this.minCreatedDateFilter = undefined;
        this.maxUpdatedDateFilter = undefined;
        this.minUpdatedDateFilter = undefined;

        this.getZMM020R();
    }
}
