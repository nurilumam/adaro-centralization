import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RptProcurementAdjustsServiceProxy, RptProcurementAdjustDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditRptProcurementAdjustModalComponent } from './create-or-edit-rptProcurementAdjust-modal.component';

import { ViewRptProcurementAdjustModalComponent } from './view-rptProcurementAdjust-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './rptProcurementAdjusts.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class RptProcurementAdjustsComponent extends AppComponentBase {
    @ViewChild('createOrEditRptProcurementAdjustModal', { static: true })
    createOrEditRptProcurementAdjustModal: CreateOrEditRptProcurementAdjustModalComponent;
    @ViewChild('viewRptProcurementAdjustModal', { static: true })
    viewRptProcurementAdjustModal: ViewRptProcurementAdjustModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    purchasingDocumentFilter = '';
    isContractFilter = -1;
    isAdjustFilter = -1;
    maxDayAdjustFilter: number;
    maxDayAdjustFilterEmpty: number;
    minDayAdjustFilter: number;
    minDayAdjustFilterEmpty: number;
    remarkFilter = '';

    constructor(
        injector: Injector,
        private _rptProcurementAdjustsServiceProxy: RptProcurementAdjustsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getRptProcurementAdjusts(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records && this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._rptProcurementAdjustsServiceProxy
            .getAll(
                this.filterText,
                this.purchasingDocumentFilter,
                this.isContractFilter,
                this.isAdjustFilter,
                this.maxDayAdjustFilter == null ? this.maxDayAdjustFilterEmpty : this.maxDayAdjustFilter,
                this.minDayAdjustFilter == null ? this.minDayAdjustFilterEmpty : this.minDayAdjustFilter,
                this.remarkFilter,
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

    createRptProcurementAdjust(): void {
        this.createOrEditRptProcurementAdjustModal.show();
    }

    deleteRptProcurementAdjust(rptProcurementAdjust: RptProcurementAdjustDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._rptProcurementAdjustsServiceProxy.delete(rptProcurementAdjust.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._rptProcurementAdjustsServiceProxy
            .getRptProcurementAdjustsToExcel(
                this.filterText,
                this.purchasingDocumentFilter,
                this.isContractFilter,
                this.isAdjustFilter,
                this.maxDayAdjustFilter == null ? this.maxDayAdjustFilterEmpty : this.maxDayAdjustFilter,
                this.minDayAdjustFilter == null ? this.minDayAdjustFilterEmpty : this.minDayAdjustFilter,
                this.remarkFilter
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }

    resetFilters(): void {
        this.filterText = '';
        this.purchasingDocumentFilter = '';
        this.isContractFilter = -1;
        this.isAdjustFilter = -1;
        this.maxDayAdjustFilter = this.maxDayAdjustFilterEmpty;
        this.minDayAdjustFilter = this.maxDayAdjustFilterEmpty;
        this.remarkFilter = '';

        this.getRptProcurementAdjusts();
    }
}
