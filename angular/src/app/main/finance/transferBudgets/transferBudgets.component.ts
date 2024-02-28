import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TransferBudgetsServiceProxy, TransferBudgetDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';

import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './transferBudgets.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class TransferBudgetsComponent extends AppComponentBase {
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    documentNoFilter = '';
    departmentFilter = '';
    divisionFilter = '';
    purposeFilter = '';
    projectActivitiesFilter = '';
    reasonFilter = '';
    scopeofWorkFilter = '';
    locationFilter = '';

    constructor(
        injector: Injector,
        private _transferBudgetsServiceProxy: TransferBudgetsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _router: Router,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getTransferBudgets(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records && this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._transferBudgetsServiceProxy
            .getAll(
                this.filterText,
                this.documentNoFilter,
                this.departmentFilter,
                this.divisionFilter,
                this.purposeFilter,
                this.projectActivitiesFilter,
                this.reasonFilter,
                this.scopeofWorkFilter,
                this.locationFilter,
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

    createTransferBudget(): void {
        this._router.navigate(['/app/main/finance/transferBudgets/createOrEdit']);
    }

    deleteTransferBudget(transferBudget: TransferBudgetDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._transferBudgetsServiceProxy.delete(transferBudget.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._transferBudgetsServiceProxy
            .getTransferBudgetsToExcel(
                this.filterText,
                this.documentNoFilter,
                this.departmentFilter,
                this.divisionFilter,
                this.purposeFilter,
                this.projectActivitiesFilter,
                this.reasonFilter,
                this.scopeofWorkFilter,
                this.locationFilter
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }

    resetFilters(): void {
        this.filterText = '';
        this.documentNoFilter = '';
        this.departmentFilter = '';
        this.divisionFilter = '';
        this.purposeFilter = '';
        this.projectActivitiesFilter = '';
        this.reasonFilter = '';
        this.scopeofWorkFilter = '';
        this.locationFilter = '';

        this.getTransferBudgets();
    }
}
