import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CostCentersServiceProxy, CostCenterDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditCostCenterModalComponent } from './create-or-edit-costCenter-modal.component';

import { ViewCostCenterModalComponent } from './view-costCenter-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './costCenters.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class CostCentersComponent extends AppComponentBase {
    @ViewChild('createOrEditCostCenterModal', { static: true })
    createOrEditCostCenterModal: CreateOrEditCostCenterModalComponent;
    @ViewChild('viewCostCenterModal', { static: true }) viewCostCenterModal: ViewCostCenterModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    controllingAreaFilter = '';
    costCenterNameFilter = '';
    descriptionFilter = '';
    actStateFilter = '';
    isActiveFilter = -1;

    constructor(
        injector: Injector,
        private _costCentersServiceProxy: CostCentersServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getCostCenters(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records && this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._costCentersServiceProxy
            .getAll(
                this.filterText,
                this.controllingAreaFilter,
                this.costCenterNameFilter,
                this.descriptionFilter,
                this.actStateFilter,
                this.isActiveFilter,
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

    createCostCenter(): void {
        this.createOrEditCostCenterModal.show();
    }

    deleteCostCenter(costCenter: CostCenterDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._costCentersServiceProxy.delete(costCenter.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._costCentersServiceProxy
            .getCostCentersToExcel(
                this.filterText,
                this.controllingAreaFilter,
                this.costCenterNameFilter,
                this.descriptionFilter,
                this.actStateFilter,
                this.isActiveFilter
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }

    resetFilters(): void {
        this.filterText = '';
        this.controllingAreaFilter = '';
        this.costCenterNameFilter = '';
        this.descriptionFilter = '';
        this.actStateFilter = '';
        this.isActiveFilter = -1;

        this.getCostCenters();
    }
}
