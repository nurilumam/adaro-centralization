import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DataProductionsServiceProxy, DataProductionDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditDataProductionModalComponent } from './create-or-edit-dataProduction-modal.component';

import { ViewDataProductionModalComponent } from './view-dataProduction-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './dataProductions.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class DataProductionsComponent extends AppComponentBase {
    @ViewChild('createOrEditDataProductionModal', { static: true })
    createOrEditDataProductionModal: CreateOrEditDataProductionModalComponent;
    @ViewChild('viewDataProductionModal', { static: true }) viewDataProductionModal: ViewDataProductionModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    intfIdFilter = '';
    intfSiteFilter = '';
    intfSessionFilter = '';
    objectIdFilter = '';
    messageTypeFilter = '';
    materialDocumentFilter = '';
    maxMaterialDocYearFilter: number;
    maxMaterialDocYearFilterEmpty: number;
    minMaterialDocYearFilter: number;
    minMaterialDocYearFilterEmpty: number;
    maxMaterialDocItemFilter: number;
    maxMaterialDocItemFilterEmpty: number;
    minMaterialDocItemFilter: number;
    minMaterialDocItemFilterEmpty: number;
    orderFilter = '';
    maxReservationFilter: number;
    maxReservationFilterEmpty: number;
    minReservationFilter: number;
    minReservationFilterEmpty: number;
    purchaseOrderFilter = '';
    maxPurchaseOrderItemFilter: number;
    maxPurchaseOrderItemFilterEmpty: number;
    minPurchaseOrderItemFilter: number;
    minPurchaseOrderItemFilterEmpty: number;
    movementTypeFilter = '';
    movementTypeTextFilter = '';
    plantFilter = '';
    storageLocationFilter = '';
    materialFilter = '';
    materialDescriptionFilter = '';
    vendorFilter = '';
    maxQuantityFilter: number;
    maxQuantityFilterEmpty: number;
    minQuantityFilter: number;
    minQuantityFilterEmpty: number;
    maxQtyInOrderUnitFilter: number;
    maxQtyInOrderUnitFilterEmpty: number;
    minQtyInOrderUnitFilter: number;
    minQtyInOrderUnitFilterEmpty: number;
    unitOfEntryFilter = '';
    maxPostingDateFilter: DateTime;
    minPostingDateFilter: DateTime;
    maxEntryDateFilter: DateTime;
    minEntryDateFilter: DateTime;
    timeOfEntryFilter = '';
    userNameFilter = '';
    documentHeaderTextFilter = '';
    maxDocumentDateFilter: DateTime;
    minDocumentDateFilter: DateTime;
    batchFilter = '';
    costCenterFilter = '';
    referenceFilter = '';
    maxInterfaceCreatedDateFilter: DateTime;
    minInterfaceCreatedDateFilter: DateTime;
    interfaceCreatedByFilter = '';

    constructor(
        injector: Injector,
        private _dataProductionsServiceProxy: DataProductionsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getDataProductions(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records && this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._dataProductionsServiceProxy
            .getAll(
                this.filterText,
                this.intfIdFilter,
                this.intfSiteFilter,
                this.intfSessionFilter,
                this.objectIdFilter,
                this.messageTypeFilter,
                this.materialDocumentFilter,
                this.maxMaterialDocYearFilter == null
                    ? this.maxMaterialDocYearFilterEmpty
                    : this.maxMaterialDocYearFilter,
                this.minMaterialDocYearFilter == null
                    ? this.minMaterialDocYearFilterEmpty
                    : this.minMaterialDocYearFilter,
                this.maxMaterialDocItemFilter == null
                    ? this.maxMaterialDocItemFilterEmpty
                    : this.maxMaterialDocItemFilter,
                this.minMaterialDocItemFilter == null
                    ? this.minMaterialDocItemFilterEmpty
                    : this.minMaterialDocItemFilter,
                this.orderFilter,
                this.maxReservationFilter == null ? this.maxReservationFilterEmpty : this.maxReservationFilter,
                this.minReservationFilter == null ? this.minReservationFilterEmpty : this.minReservationFilter,
                this.purchaseOrderFilter,
                this.maxPurchaseOrderItemFilter == null
                    ? this.maxPurchaseOrderItemFilterEmpty
                    : this.maxPurchaseOrderItemFilter,
                this.minPurchaseOrderItemFilter == null
                    ? this.minPurchaseOrderItemFilterEmpty
                    : this.minPurchaseOrderItemFilter,
                this.movementTypeFilter,
                this.movementTypeTextFilter,
                this.plantFilter,
                this.storageLocationFilter,
                this.materialFilter,
                this.materialDescriptionFilter,
                this.vendorFilter,
                this.maxQuantityFilter == null ? this.maxQuantityFilterEmpty : this.maxQuantityFilter,
                this.minQuantityFilter == null ? this.minQuantityFilterEmpty : this.minQuantityFilter,
                this.maxQtyInOrderUnitFilter == null ? this.maxQtyInOrderUnitFilterEmpty : this.maxQtyInOrderUnitFilter,
                this.minQtyInOrderUnitFilter == null ? this.minQtyInOrderUnitFilterEmpty : this.minQtyInOrderUnitFilter,
                this.unitOfEntryFilter,
                this.maxPostingDateFilter === undefined
                    ? this.maxPostingDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxPostingDateFilter),
                this.minPostingDateFilter === undefined
                    ? this.minPostingDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minPostingDateFilter),
                this.maxEntryDateFilter === undefined
                    ? this.maxEntryDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxEntryDateFilter),
                this.minEntryDateFilter === undefined
                    ? this.minEntryDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minEntryDateFilter),
                this.timeOfEntryFilter,
                this.userNameFilter,
                this.documentHeaderTextFilter,
                this.maxDocumentDateFilter === undefined
                    ? this.maxDocumentDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxDocumentDateFilter),
                this.minDocumentDateFilter === undefined
                    ? this.minDocumentDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minDocumentDateFilter),
                this.batchFilter,
                this.costCenterFilter,
                this.referenceFilter,
                this.maxInterfaceCreatedDateFilter === undefined
                    ? this.maxInterfaceCreatedDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxInterfaceCreatedDateFilter),
                this.minInterfaceCreatedDateFilter === undefined
                    ? this.minInterfaceCreatedDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minInterfaceCreatedDateFilter),
                this.interfaceCreatedByFilter,
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

    createDataProduction(): void {
        this.createOrEditDataProductionModal.show();
    }

    deleteDataProduction(dataProduction: DataProductionDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._dataProductionsServiceProxy.delete(dataProduction.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._dataProductionsServiceProxy
            .getDataProductionsToExcel(
                this.filterText,
                this.intfIdFilter,
                this.intfSiteFilter,
                this.intfSessionFilter,
                this.objectIdFilter,
                this.messageTypeFilter,
                this.materialDocumentFilter,
                this.maxMaterialDocYearFilter == null
                    ? this.maxMaterialDocYearFilterEmpty
                    : this.maxMaterialDocYearFilter,
                this.minMaterialDocYearFilter == null
                    ? this.minMaterialDocYearFilterEmpty
                    : this.minMaterialDocYearFilter,
                this.maxMaterialDocItemFilter == null
                    ? this.maxMaterialDocItemFilterEmpty
                    : this.maxMaterialDocItemFilter,
                this.minMaterialDocItemFilter == null
                    ? this.minMaterialDocItemFilterEmpty
                    : this.minMaterialDocItemFilter,
                this.orderFilter,
                this.maxReservationFilter == null ? this.maxReservationFilterEmpty : this.maxReservationFilter,
                this.minReservationFilter == null ? this.minReservationFilterEmpty : this.minReservationFilter,
                this.purchaseOrderFilter,
                this.maxPurchaseOrderItemFilter == null
                    ? this.maxPurchaseOrderItemFilterEmpty
                    : this.maxPurchaseOrderItemFilter,
                this.minPurchaseOrderItemFilter == null
                    ? this.minPurchaseOrderItemFilterEmpty
                    : this.minPurchaseOrderItemFilter,
                this.movementTypeFilter,
                this.movementTypeTextFilter,
                this.plantFilter,
                this.storageLocationFilter,
                this.materialFilter,
                this.materialDescriptionFilter,
                this.vendorFilter,
                this.maxQuantityFilter == null ? this.maxQuantityFilterEmpty : this.maxQuantityFilter,
                this.minQuantityFilter == null ? this.minQuantityFilterEmpty : this.minQuantityFilter,
                this.maxQtyInOrderUnitFilter == null ? this.maxQtyInOrderUnitFilterEmpty : this.maxQtyInOrderUnitFilter,
                this.minQtyInOrderUnitFilter == null ? this.minQtyInOrderUnitFilterEmpty : this.minQtyInOrderUnitFilter,
                this.unitOfEntryFilter,
                this.maxPostingDateFilter === undefined
                    ? this.maxPostingDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxPostingDateFilter),
                this.minPostingDateFilter === undefined
                    ? this.minPostingDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minPostingDateFilter),
                this.maxEntryDateFilter === undefined
                    ? this.maxEntryDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxEntryDateFilter),
                this.minEntryDateFilter === undefined
                    ? this.minEntryDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minEntryDateFilter),
                this.timeOfEntryFilter,
                this.userNameFilter,
                this.documentHeaderTextFilter,
                this.maxDocumentDateFilter === undefined
                    ? this.maxDocumentDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxDocumentDateFilter),
                this.minDocumentDateFilter === undefined
                    ? this.minDocumentDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minDocumentDateFilter),
                this.batchFilter,
                this.costCenterFilter,
                this.referenceFilter,
                this.maxInterfaceCreatedDateFilter === undefined
                    ? this.maxInterfaceCreatedDateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxInterfaceCreatedDateFilter),
                this.minInterfaceCreatedDateFilter === undefined
                    ? this.minInterfaceCreatedDateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minInterfaceCreatedDateFilter),
                this.interfaceCreatedByFilter
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }

    resetFilters(): void {
        this.filterText = '';
        this.intfIdFilter = '';
        this.intfSiteFilter = '';
        this.intfSessionFilter = '';
        this.objectIdFilter = '';
        this.messageTypeFilter = '';
        this.materialDocumentFilter = '';
        this.maxMaterialDocYearFilter = this.maxMaterialDocYearFilterEmpty;
        this.minMaterialDocYearFilter = this.maxMaterialDocYearFilterEmpty;
        this.maxMaterialDocItemFilter = this.maxMaterialDocItemFilterEmpty;
        this.minMaterialDocItemFilter = this.maxMaterialDocItemFilterEmpty;
        this.orderFilter = '';
        this.maxReservationFilter = this.maxReservationFilterEmpty;
        this.minReservationFilter = this.maxReservationFilterEmpty;
        this.purchaseOrderFilter = '';
        this.maxPurchaseOrderItemFilter = this.maxPurchaseOrderItemFilterEmpty;
        this.minPurchaseOrderItemFilter = this.maxPurchaseOrderItemFilterEmpty;
        this.movementTypeFilter = '';
        this.movementTypeTextFilter = '';
        this.plantFilter = '';
        this.storageLocationFilter = '';
        this.materialFilter = '';
        this.materialDescriptionFilter = '';
        this.vendorFilter = '';
        this.maxQuantityFilter = this.maxQuantityFilterEmpty;
        this.minQuantityFilter = this.maxQuantityFilterEmpty;
        this.maxQtyInOrderUnitFilter = this.maxQtyInOrderUnitFilterEmpty;
        this.minQtyInOrderUnitFilter = this.maxQtyInOrderUnitFilterEmpty;
        this.unitOfEntryFilter = '';
        this.maxPostingDateFilter = undefined;
        this.minPostingDateFilter = undefined;
        this.maxEntryDateFilter = undefined;
        this.minEntryDateFilter = undefined;
        this.timeOfEntryFilter = '';
        this.userNameFilter = '';
        this.documentHeaderTextFilter = '';
        this.maxDocumentDateFilter = undefined;
        this.minDocumentDateFilter = undefined;
        this.batchFilter = '';
        this.costCenterFilter = '';
        this.referenceFilter = '';
        this.maxInterfaceCreatedDateFilter = undefined;
        this.minInterfaceCreatedDateFilter = undefined;
        this.interfaceCreatedByFilter = '';

        this.getDataProductions();
    }
}
