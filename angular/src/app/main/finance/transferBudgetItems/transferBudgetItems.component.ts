import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TransferBudgetItemsServiceProxy, TransferBudgetItemDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditTransferBudgetItemModalComponent } from './create-or-edit-transferBudgetItem-modal.component';

import { ViewTransferBudgetItemModalComponent } from './view-transferBudgetItem-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './transferBudgetItems.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class TransferBudgetItemsComponent extends AppComponentBase {
    @ViewChild('createOrEditTransferBudgetItemModal', { static: true })
    createOrEditTransferBudgetItemModal: CreateOrEditTransferBudgetItemModalComponent;
    @ViewChild('viewTransferBudgetItemModal', { static: true })
    viewTransferBudgetItemModal: ViewTransferBudgetItemModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    periodFromFilter = '';
    maxAmountFromFilter: number;
    maxAmountFromFilterEmpty: number;
    minAmountFromFilter: number;
    minAmountFromFilterEmpty: number;
    periodToFilter = '';
    maxAmountToFilter: number;
    maxAmountToFilterEmpty: number;
    minAmountToFilter: number;
    minAmountToFilterEmpty: number;
    transferBudgetDisplayPropertyFilter = '';
    costCenterDisplayPropertyFilter = '';
    costCenterDisplayProperty2Filter = '';

    constructor(
        injector: Injector,
        private _transferBudgetItemsServiceProxy: TransferBudgetItemsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getTransferBudgetItems(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records && this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._transferBudgetItemsServiceProxy
            .getAll(
                this.filterText,
                this.periodFromFilter,
                this.maxAmountFromFilter == null ? this.maxAmountFromFilterEmpty : this.maxAmountFromFilter,
                this.minAmountFromFilter == null ? this.minAmountFromFilterEmpty : this.minAmountFromFilter,
                this.periodToFilter,
                this.maxAmountToFilter == null ? this.maxAmountToFilterEmpty : this.maxAmountToFilter,
                this.minAmountToFilter == null ? this.minAmountToFilterEmpty : this.minAmountToFilter,
                this.transferBudgetDisplayPropertyFilter,
                this.costCenterDisplayPropertyFilter,
                this.costCenterDisplayProperty2Filter,
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

    createTransferBudgetItem(): void {
        this.createOrEditTransferBudgetItemModal.show();
    }

    deleteTransferBudgetItem(transferBudgetItem: TransferBudgetItemDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._transferBudgetItemsServiceProxy.delete(transferBudgetItem.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._transferBudgetItemsServiceProxy
            .getTransferBudgetItemsToExcel(
                this.filterText,
                this.periodFromFilter,
                this.maxAmountFromFilter == null ? this.maxAmountFromFilterEmpty : this.maxAmountFromFilter,
                this.minAmountFromFilter == null ? this.minAmountFromFilterEmpty : this.minAmountFromFilter,
                this.periodToFilter,
                this.maxAmountToFilter == null ? this.maxAmountToFilterEmpty : this.maxAmountToFilter,
                this.minAmountToFilter == null ? this.minAmountToFilterEmpty : this.minAmountToFilter,
                this.transferBudgetDisplayPropertyFilter,
                this.costCenterDisplayPropertyFilter,
                this.costCenterDisplayProperty2Filter
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }

    resetFilters(): void {
        this.filterText = '';
        this.periodFromFilter = '';
        this.maxAmountFromFilter = this.maxAmountFromFilterEmpty;
        this.minAmountFromFilter = this.maxAmountFromFilterEmpty;
        this.periodToFilter = '';
        this.maxAmountToFilter = this.maxAmountToFilterEmpty;
        this.minAmountToFilter = this.maxAmountToFilterEmpty;
        this.transferBudgetDisplayPropertyFilter = '';
        this.costCenterDisplayPropertyFilter = '';
        this.costCenterDisplayProperty2Filter = '';

        this.getTransferBudgetItems();
    }
}
