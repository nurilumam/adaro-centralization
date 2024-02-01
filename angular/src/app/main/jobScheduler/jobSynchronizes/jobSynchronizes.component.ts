import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
    JobSynchronizesServiceProxy,
    JobSynchronizeDto,
    JobSchedulerType,
    JobSchedulerStatus,
} from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditJobSynchronizeModalComponent } from './create-or-edit-jobSynchronize-modal.component';

import { ViewJobSynchronizeModalComponent } from './view-jobSynchronize-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './jobSynchronizes.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class JobSynchronizesComponent extends AppComponentBase {
    @ViewChild('createOrEditJobSynchronizeModal', { static: true })
    createOrEditJobSynchronizeModal: CreateOrEditJobSynchronizeModalComponent;
    @ViewChild('viewJobSynchronizeModal', { static: true }) viewJobSynchronizeModal: ViewJobSynchronizeModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    jobNameFilter = '';
    jobTypeFilter = -1;
    dataSourceFilter = '';
    lastStatusFilter = -1;
    uriFilter = '';
    maxLastUpdateFilter: DateTime;
    minLastUpdateFilter: DateTime;
    maxTotalInsertFilter: number;
    maxTotalInsertFilterEmpty: number;
    minTotalInsertFilter: number;
    minTotalInsertFilterEmpty: number;
    maxTotalUpdateFilter: number;
    maxTotalUpdateFilterEmpty: number;
    minTotalUpdateFilter: number;
    minTotalUpdateFilterEmpty: number;
    maxTotalDeleteFilter: number;
    maxTotalDeleteFilterEmpty: number;
    minTotalDeleteFilter: number;
    minTotalDeleteFilterEmpty: number;

    jobSchedulerType = JobSchedulerType;
    jobSchedulerStatus = JobSchedulerStatus;

    constructor(
        injector: Injector,
        private _jobSynchronizesServiceProxy: JobSynchronizesServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getJobSynchronizes(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records && this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._jobSynchronizesServiceProxy
            .getAll(
                this.filterText,
                this.jobNameFilter,
                this.jobTypeFilter,
                this.dataSourceFilter,
                this.lastStatusFilter,
                this.uriFilter,
                this.maxLastUpdateFilter === undefined
                    ? this.maxLastUpdateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxLastUpdateFilter),
                this.minLastUpdateFilter === undefined
                    ? this.minLastUpdateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minLastUpdateFilter),
                this.maxTotalInsertFilter == null ? this.maxTotalInsertFilterEmpty : this.maxTotalInsertFilter,
                this.minTotalInsertFilter == null ? this.minTotalInsertFilterEmpty : this.minTotalInsertFilter,
                this.maxTotalUpdateFilter == null ? this.maxTotalUpdateFilterEmpty : this.maxTotalUpdateFilter,
                this.minTotalUpdateFilter == null ? this.minTotalUpdateFilterEmpty : this.minTotalUpdateFilter,
                this.maxTotalDeleteFilter == null ? this.maxTotalDeleteFilterEmpty : this.maxTotalDeleteFilter,
                this.minTotalDeleteFilter == null ? this.minTotalDeleteFilterEmpty : this.minTotalDeleteFilter,
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

    createJobSynchronize(): void {
        this.createOrEditJobSynchronizeModal.show();
    }

    deleteJobSynchronize(jobSynchronize: JobSynchronizeDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._jobSynchronizesServiceProxy.delete(jobSynchronize.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._jobSynchronizesServiceProxy
            .getJobSynchronizesToExcel(
                this.filterText,
                this.jobNameFilter,
                this.jobTypeFilter,
                this.dataSourceFilter,
                this.lastStatusFilter,
                this.uriFilter,
                this.maxLastUpdateFilter === undefined
                    ? this.maxLastUpdateFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxLastUpdateFilter),
                this.minLastUpdateFilter === undefined
                    ? this.minLastUpdateFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minLastUpdateFilter),
                this.maxTotalInsertFilter == null ? this.maxTotalInsertFilterEmpty : this.maxTotalInsertFilter,
                this.minTotalInsertFilter == null ? this.minTotalInsertFilterEmpty : this.minTotalInsertFilter,
                this.maxTotalUpdateFilter == null ? this.maxTotalUpdateFilterEmpty : this.maxTotalUpdateFilter,
                this.minTotalUpdateFilter == null ? this.minTotalUpdateFilterEmpty : this.minTotalUpdateFilter,
                this.maxTotalDeleteFilter == null ? this.maxTotalDeleteFilterEmpty : this.maxTotalDeleteFilter,
                this.minTotalDeleteFilter == null ? this.minTotalDeleteFilterEmpty : this.minTotalDeleteFilter
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }

    resetFilters(): void {
        this.filterText = '';
        this.jobNameFilter = '';
        this.jobTypeFilter = -1;
        this.dataSourceFilter = '';
        this.lastStatusFilter = -1;
        this.uriFilter = '';
        this.maxLastUpdateFilter = undefined;
        this.minLastUpdateFilter = undefined;
        this.maxTotalInsertFilter = this.maxTotalInsertFilterEmpty;
        this.minTotalInsertFilter = this.maxTotalInsertFilterEmpty;
        this.maxTotalUpdateFilter = this.maxTotalUpdateFilterEmpty;
        this.minTotalUpdateFilter = this.maxTotalUpdateFilterEmpty;
        this.maxTotalDeleteFilter = this.maxTotalDeleteFilterEmpty;
        this.minTotalDeleteFilter = this.maxTotalDeleteFilterEmpty;

        this.getJobSynchronizes();
    }
}
