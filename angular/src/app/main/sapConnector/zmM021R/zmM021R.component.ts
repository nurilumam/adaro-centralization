import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ZMM021RServiceProxy, ZMM021RDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditZMM021RModalComponent } from './create-or-edit-zmM021R-modal.component';

import { ViewZMM021RModalComponent } from './view-zmM021R-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './zmM021R.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class ZMM021RComponent extends AppComponentBase {
    @ViewChild('createOrEditZMM021RModal', { static: true })
    createOrEditZMM021RModal: CreateOrEditZMM021RModalComponent;
    @ViewChild('viewZMM021RModal', { static: true }) viewZMM021RModal: ViewZMM021RModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    purchasingDocumentFilter = '';
    purchasingDocTypeFilter = '';
    purchasingDocTypeDescriptionFilter = '';
    itemFilter = '';
    lineNumberFilter = '';
    deletionIndicatorFilter = '';
    maxDocumentDateFilter: DateTime;
    minDocumentDateFilter: DateTime;
    maxCreatedOnFilter: DateTime;
    minCreatedOnFilter: DateTime;
    purchaseRequisitionFilter = '';
    itemPRFilter = '';
    supplierCodeFilter = '';
    supplierNameFilter = '';
    addressFilter = '';
    itemNoFilter = '';
    materialGroupFilter = '';
    shortTextFilter = '';
    maxOrderQuantityFilter: number;
    maxOrderQuantityFilterEmpty: number;
    minOrderQuantityFilter: number;
    minOrderQuantityFilterEmpty: number;
    orderUnitFilter = '';
    currencyFilter = '';
    maxDeliveryDateFilter: DateTime;
    minDeliveryDateFilter: DateTime;
    maxNetPriceFilter: number;
    maxNetPriceFilterEmpty: number;
    minNetPriceFilter: number;
    minNetPriceFilterEmpty: number;
    maxNetOrderValueFilter: number;
    maxNetOrderValueFilterEmpty: number;
    minNetOrderValueFilter: number;
    minNetOrderValueFilterEmpty: number;
    maxDemurrageFilter: number;
    maxDemurrageFilterEmpty: number;
    minDemurrageFilter: number;
    minDemurrageFilterEmpty: number;
    maxGrossPriceFilter: number;
    maxGrossPriceFilterEmpty: number;
    minGrossPriceFilter: number;
    minGrossPriceFilterEmpty: number;
    maxTotalDiscountFilter: number;
    maxTotalDiscountFilterEmpty: number;
    minTotalDiscountFilter: number;
    minTotalDiscountFilterEmpty: number;
    maxFreightCostFilter: number;
    maxFreightCostFilterEmpty: number;
    minFreightCostFilter: number;
    minFreightCostFilterEmpty: number;
    releaseIndicatorFilter = '';
    plantFilter = '';
    purchasingGroupFilter = '';
    taxCodeFilter = '';
    collectiveNumberFilter = '';
    itemCategoryFilter = '';
    accountAssignmentFilter = '';
    outlineAgreementFilter = '';
    rfqNoFilter = '';
    maxQtyPendingFilter: number;
    maxQtyPendingFilterEmpty: number;
    minQtyPendingFilter: number;
    minQtyPendingFilterEmpty: number;
    materialServiceFilter = '';
    approvalStatusFilter = '';
    poStatusFilter = '';
    periodFilter = '';
    commentVendorFilter = '';
    itemTextFilter = '';
    longTextFilter = '';
    ourReferenceFilter = '';
    maxPRFinalFirstApprovalDateFilter: DateTime;
    minPRFinalFirstApprovalDateFilter: DateTime;
    maxPRFinalLastApprovalDateFilter: DateTime;
    minPRFinalLastApprovalDateFilter: DateTime;
    maxPOFirstApprovalDateFilter: DateTime;
    minPOFirstApprovalDateFilter: DateTime;
    maxPOLastApprovalDateFilter: DateTime;
    minPOLastApprovalDateFilter: DateTime;
    poApprovalNameFilter = '';
    buyerCodeFilter = '';
    buyerNameFilter = '';
    picDeptFilter = '';
    picSectFilter = '';
    fuelAllocationFilter = '';
    costCenterFilter = '';
    costCenterDescriptionFilter = '';
    wbsElementFilter = '';
    assetNoFilter = '';
    fundCenterFilter = '';
    maxCreatedDateFilter: DateTime;
    minCreatedDateFilter: DateTime;
    maxUpdatedDateFilter: DateTime;
    minUpdatedDateFilter: DateTime;
    documentIdFilter = '';

    constructor(
        injector: Injector,
        private _zmM021RServiceProxy: ZMM021RServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getZMM021R(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records && this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._zmM021RServiceProxy
            .getAll(
                this.filterText,
                this.purchasingDocumentFilter,
                this.purchasingDocTypeFilter,
                this.purchasingDocTypeDescriptionFilter,
                this.itemFilter,
                this.lineNumberFilter,
                this.deletionIndicatorFilter,
                this.maxDocumentDateFilter === undefined
                    ? this.maxDocumentDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxDocumentDateFilter),
                this.minDocumentDateFilter === undefined
                    ? this.minDocumentDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minDocumentDateFilter),
                this.maxCreatedOnFilter === undefined
                    ? this.maxCreatedOnFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxCreatedOnFilter),
                this.minCreatedOnFilter === undefined
                    ? this.minCreatedOnFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minCreatedOnFilter),
                this.purchaseRequisitionFilter,
                this.itemPRFilter,
                this.supplierCodeFilter,
                this.supplierNameFilter,
                this.addressFilter,
                this.itemNoFilter,
                this.materialGroupFilter,
                this.shortTextFilter,
                this.maxOrderQuantityFilter == null ? this.maxOrderQuantityFilterEmpty : this.maxOrderQuantityFilter,
                this.minOrderQuantityFilter == null ? this.minOrderQuantityFilterEmpty : this.minOrderQuantityFilter,
                this.orderUnitFilter,
                this.currencyFilter,
                this.maxDeliveryDateFilter === undefined
                    ? this.maxDeliveryDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxDeliveryDateFilter),
                this.minDeliveryDateFilter === undefined
                    ? this.minDeliveryDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minDeliveryDateFilter),
                this.maxNetPriceFilter == null ? this.maxNetPriceFilterEmpty : this.maxNetPriceFilter,
                this.minNetPriceFilter == null ? this.minNetPriceFilterEmpty : this.minNetPriceFilter,
                this.maxNetOrderValueFilter == null ? this.maxNetOrderValueFilterEmpty : this.maxNetOrderValueFilter,
                this.minNetOrderValueFilter == null ? this.minNetOrderValueFilterEmpty : this.minNetOrderValueFilter,
                this.maxDemurrageFilter == null ? this.maxDemurrageFilterEmpty : this.maxDemurrageFilter,
                this.minDemurrageFilter == null ? this.minDemurrageFilterEmpty : this.minDemurrageFilter,
                this.maxGrossPriceFilter == null ? this.maxGrossPriceFilterEmpty : this.maxGrossPriceFilter,
                this.minGrossPriceFilter == null ? this.minGrossPriceFilterEmpty : this.minGrossPriceFilter,
                this.maxTotalDiscountFilter == null ? this.maxTotalDiscountFilterEmpty : this.maxTotalDiscountFilter,
                this.minTotalDiscountFilter == null ? this.minTotalDiscountFilterEmpty : this.minTotalDiscountFilter,
                this.maxFreightCostFilter == null ? this.maxFreightCostFilterEmpty : this.maxFreightCostFilter,
                this.minFreightCostFilter == null ? this.minFreightCostFilterEmpty : this.minFreightCostFilter,
                this.releaseIndicatorFilter,
                this.plantFilter,
                this.purchasingGroupFilter,
                this.taxCodeFilter,
                this.collectiveNumberFilter,
                this.itemCategoryFilter,
                this.accountAssignmentFilter,
                this.outlineAgreementFilter,
                this.rfqNoFilter,
                this.maxQtyPendingFilter == null ? this.maxQtyPendingFilterEmpty : this.maxQtyPendingFilter,
                this.minQtyPendingFilter == null ? this.minQtyPendingFilterEmpty : this.minQtyPendingFilter,
                this.materialServiceFilter,
                this.approvalStatusFilter,
                this.poStatusFilter,
                this.periodFilter,
                this.commentVendorFilter,
                this.itemTextFilter,
                this.longTextFilter,
                this.ourReferenceFilter,
                this.maxPRFinalFirstApprovalDateFilter === undefined
                    ? this.maxPRFinalFirstApprovalDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxPRFinalFirstApprovalDateFilter),
                this.minPRFinalFirstApprovalDateFilter === undefined
                    ? this.minPRFinalFirstApprovalDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minPRFinalFirstApprovalDateFilter),
                this.maxPRFinalLastApprovalDateFilter === undefined
                    ? this.maxPRFinalLastApprovalDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxPRFinalLastApprovalDateFilter),
                this.minPRFinalLastApprovalDateFilter === undefined
                    ? this.minPRFinalLastApprovalDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minPRFinalLastApprovalDateFilter),
                this.maxPOFirstApprovalDateFilter === undefined
                    ? this.maxPOFirstApprovalDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxPOFirstApprovalDateFilter),
                this.minPOFirstApprovalDateFilter === undefined
                    ? this.minPOFirstApprovalDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minPOFirstApprovalDateFilter),
                this.maxPOLastApprovalDateFilter === undefined
                    ? this.maxPOLastApprovalDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxPOLastApprovalDateFilter),
                this.minPOLastApprovalDateFilter === undefined
                    ? this.minPOLastApprovalDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minPOLastApprovalDateFilter),
                this.poApprovalNameFilter,
                this.buyerCodeFilter,
                this.buyerNameFilter,
                this.picDeptFilter,
                this.picSectFilter,
                this.fuelAllocationFilter,
                this.costCenterFilter,
                this.costCenterDescriptionFilter,
                this.wbsElementFilter,
                this.assetNoFilter,
                this.fundCenterFilter,
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
                this.documentIdFilter,
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

    createZMM021R(): void {
        this.createOrEditZMM021RModal.show();
    }

    deleteZMM021R(zmM021R: ZMM021RDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._zmM021RServiceProxy.delete(zmM021R.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._zmM021RServiceProxy
            .getZMM021RToExcel(
                this.filterText,
                this.purchasingDocumentFilter,
                this.purchasingDocTypeFilter,
                this.purchasingDocTypeDescriptionFilter,
                this.itemFilter,
                this.lineNumberFilter,
                this.deletionIndicatorFilter,
                this.maxDocumentDateFilter === undefined
                    ? this.maxDocumentDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxDocumentDateFilter),
                this.minDocumentDateFilter === undefined
                    ? this.minDocumentDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minDocumentDateFilter),
                this.maxCreatedOnFilter === undefined
                    ? this.maxCreatedOnFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxCreatedOnFilter),
                this.minCreatedOnFilter === undefined
                    ? this.minCreatedOnFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minCreatedOnFilter),
                this.purchaseRequisitionFilter,
                this.itemPRFilter,
                this.supplierCodeFilter,
                this.supplierNameFilter,
                this.addressFilter,
                this.itemNoFilter,
                this.materialGroupFilter,
                this.shortTextFilter,
                this.maxOrderQuantityFilter == null ? this.maxOrderQuantityFilterEmpty : this.maxOrderQuantityFilter,
                this.minOrderQuantityFilter == null ? this.minOrderQuantityFilterEmpty : this.minOrderQuantityFilter,
                this.orderUnitFilter,
                this.currencyFilter,
                this.maxDeliveryDateFilter === undefined
                    ? this.maxDeliveryDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxDeliveryDateFilter),
                this.minDeliveryDateFilter === undefined
                    ? this.minDeliveryDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minDeliveryDateFilter),
                this.maxNetPriceFilter == null ? this.maxNetPriceFilterEmpty : this.maxNetPriceFilter,
                this.minNetPriceFilter == null ? this.minNetPriceFilterEmpty : this.minNetPriceFilter,
                this.maxNetOrderValueFilter == null ? this.maxNetOrderValueFilterEmpty : this.maxNetOrderValueFilter,
                this.minNetOrderValueFilter == null ? this.minNetOrderValueFilterEmpty : this.minNetOrderValueFilter,
                this.maxDemurrageFilter == null ? this.maxDemurrageFilterEmpty : this.maxDemurrageFilter,
                this.minDemurrageFilter == null ? this.minDemurrageFilterEmpty : this.minDemurrageFilter,
                this.maxGrossPriceFilter == null ? this.maxGrossPriceFilterEmpty : this.maxGrossPriceFilter,
                this.minGrossPriceFilter == null ? this.minGrossPriceFilterEmpty : this.minGrossPriceFilter,
                this.maxTotalDiscountFilter == null ? this.maxTotalDiscountFilterEmpty : this.maxTotalDiscountFilter,
                this.minTotalDiscountFilter == null ? this.minTotalDiscountFilterEmpty : this.minTotalDiscountFilter,
                this.maxFreightCostFilter == null ? this.maxFreightCostFilterEmpty : this.maxFreightCostFilter,
                this.minFreightCostFilter == null ? this.minFreightCostFilterEmpty : this.minFreightCostFilter,
                this.releaseIndicatorFilter,
                this.plantFilter,
                this.purchasingGroupFilter,
                this.taxCodeFilter,
                this.collectiveNumberFilter,
                this.itemCategoryFilter,
                this.accountAssignmentFilter,
                this.outlineAgreementFilter,
                this.rfqNoFilter,
                this.maxQtyPendingFilter == null ? this.maxQtyPendingFilterEmpty : this.maxQtyPendingFilter,
                this.minQtyPendingFilter == null ? this.minQtyPendingFilterEmpty : this.minQtyPendingFilter,
                this.materialServiceFilter,
                this.approvalStatusFilter,
                this.poStatusFilter,
                this.periodFilter,
                this.commentVendorFilter,
                this.itemTextFilter,
                this.longTextFilter,
                this.ourReferenceFilter,
                this.maxPRFinalFirstApprovalDateFilter === undefined
                    ? this.maxPRFinalFirstApprovalDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxPRFinalFirstApprovalDateFilter),
                this.minPRFinalFirstApprovalDateFilter === undefined
                    ? this.minPRFinalFirstApprovalDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minPRFinalFirstApprovalDateFilter),
                this.maxPRFinalLastApprovalDateFilter === undefined
                    ? this.maxPRFinalLastApprovalDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxPRFinalLastApprovalDateFilter),
                this.minPRFinalLastApprovalDateFilter === undefined
                    ? this.minPRFinalLastApprovalDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minPRFinalLastApprovalDateFilter),
                this.maxPOFirstApprovalDateFilter === undefined
                    ? this.maxPOFirstApprovalDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxPOFirstApprovalDateFilter),
                this.minPOFirstApprovalDateFilter === undefined
                    ? this.minPOFirstApprovalDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minPOFirstApprovalDateFilter),
                this.maxPOLastApprovalDateFilter === undefined
                    ? this.maxPOLastApprovalDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxPOLastApprovalDateFilter),
                this.minPOLastApprovalDateFilter === undefined
                    ? this.minPOLastApprovalDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minPOLastApprovalDateFilter),
                this.poApprovalNameFilter,
                this.buyerCodeFilter,
                this.buyerNameFilter,
                this.picDeptFilter,
                this.picSectFilter,
                this.fuelAllocationFilter,
                this.costCenterFilter,
                this.costCenterDescriptionFilter,
                this.wbsElementFilter,
                this.assetNoFilter,
                this.fundCenterFilter,
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
                this.documentIdFilter
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }

    resetFilters(): void {
        this.filterText = '';
        this.purchasingDocumentFilter = '';
        this.purchasingDocTypeFilter = '';
        this.purchasingDocTypeDescriptionFilter = '';
        this.itemFilter = '';
        this.lineNumberFilter = '';
        this.deletionIndicatorFilter = '';
        this.maxDocumentDateFilter = undefined;
        this.minDocumentDateFilter = undefined;
        this.maxCreatedOnFilter = undefined;
        this.minCreatedOnFilter = undefined;
        this.purchaseRequisitionFilter = '';
        this.itemPRFilter = '';
        this.supplierCodeFilter = '';
        this.supplierNameFilter = '';
        this.addressFilter = '';
        this.itemNoFilter = '';
        this.materialGroupFilter = '';
        this.shortTextFilter = '';
        this.maxOrderQuantityFilter = this.maxOrderQuantityFilterEmpty;
        this.minOrderQuantityFilter = this.maxOrderQuantityFilterEmpty;
        this.orderUnitFilter = '';
        this.currencyFilter = '';
        this.maxDeliveryDateFilter = undefined;
        this.minDeliveryDateFilter = undefined;
        this.maxNetPriceFilter = this.maxNetPriceFilterEmpty;
        this.minNetPriceFilter = this.maxNetPriceFilterEmpty;
        this.maxNetOrderValueFilter = this.maxNetOrderValueFilterEmpty;
        this.minNetOrderValueFilter = this.maxNetOrderValueFilterEmpty;
        this.maxDemurrageFilter = this.maxDemurrageFilterEmpty;
        this.minDemurrageFilter = this.maxDemurrageFilterEmpty;
        this.maxGrossPriceFilter = this.maxGrossPriceFilterEmpty;
        this.minGrossPriceFilter = this.maxGrossPriceFilterEmpty;
        this.maxTotalDiscountFilter = this.maxTotalDiscountFilterEmpty;
        this.minTotalDiscountFilter = this.maxTotalDiscountFilterEmpty;
        this.maxFreightCostFilter = this.maxFreightCostFilterEmpty;
        this.minFreightCostFilter = this.maxFreightCostFilterEmpty;
        this.releaseIndicatorFilter = '';
        this.plantFilter = '';
        this.purchasingGroupFilter = '';
        this.taxCodeFilter = '';
        this.collectiveNumberFilter = '';
        this.itemCategoryFilter = '';
        this.accountAssignmentFilter = '';
        this.outlineAgreementFilter = '';
        this.rfqNoFilter = '';
        this.maxQtyPendingFilter = this.maxQtyPendingFilterEmpty;
        this.minQtyPendingFilter = this.maxQtyPendingFilterEmpty;
        this.materialServiceFilter = '';
        this.approvalStatusFilter = '';
        this.poStatusFilter = '';
        this.periodFilter = '';
        this.commentVendorFilter = '';
        this.itemTextFilter = '';
        this.longTextFilter = '';
        this.ourReferenceFilter = '';
        this.maxPRFinalFirstApprovalDateFilter = undefined;
        this.minPRFinalFirstApprovalDateFilter = undefined;
        this.maxPRFinalLastApprovalDateFilter = undefined;
        this.minPRFinalLastApprovalDateFilter = undefined;
        this.maxPOFirstApprovalDateFilter = undefined;
        this.minPOFirstApprovalDateFilter = undefined;
        this.maxPOLastApprovalDateFilter = undefined;
        this.minPOLastApprovalDateFilter = undefined;
        this.poApprovalNameFilter = '';
        this.buyerCodeFilter = '';
        this.buyerNameFilter = '';
        this.picDeptFilter = '';
        this.picSectFilter = '';
        this.fuelAllocationFilter = '';
        this.costCenterFilter = '';
        this.costCenterDescriptionFilter = '';
        this.wbsElementFilter = '';
        this.assetNoFilter = '';
        this.fundCenterFilter = '';
        this.maxCreatedDateFilter = undefined;
        this.minCreatedDateFilter = undefined;
        this.maxUpdatedDateFilter = undefined;
        this.minUpdatedDateFilter = undefined;
        this.documentIdFilter = '';

        this.getZMM021R();
    }
}
