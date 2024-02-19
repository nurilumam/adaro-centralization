import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EKPOsServiceProxy, EKPODto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditEKPOModalComponent } from './create-or-edit-ekpo-modal.component';

import { ViewEKPOModalComponent } from './view-ekpo-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './ekpOs.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class EKPOsComponent extends AppComponentBase {
    @ViewChild('createOrEditEKPOModal', { static: true }) createOrEditEKPOModal: CreateOrEditEKPOModalComponent;
    @ViewChild('viewEKPOModal', { static: true }) viewEKPOModal: ViewEKPOModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    mandtFilter = '';
    ebelnFilter = '';
    maxEBELPFilter: number;
    maxEBELPFilterEmpty: number;
    minEBELPFilter: number;
    minEBELPFilterEmpty: number;
    uniqueidFilter = '';
    loekzFilter = '';
    statuFilter = '';
    maxAEDATFilter: DateTime;
    minAEDATFilter: DateTime;
    txZ01Filter = '';
    matnrFilter = '';
    ematnFilter = '';
    bukrsFilter = '';
    werksFilter = '';
    lgortFilter = '';
    bednrFilter = '';
    matklFilter = '';
    infnrFilter = '';
    idnlfFilter = '';
    maxKTMNGFilter: number;
    maxKTMNGFilterEmpty: number;
    minKTMNGFilter: number;
    minKTMNGFilterEmpty: number;
    maxMENGEFilter: number;
    maxMENGEFilterEmpty: number;
    minMENGEFilter: number;
    minMENGEFilterEmpty: number;
    meinsFilter = '';
    bprmeFilter = '';
    maxBPUMZFilter: number;
    maxBPUMZFilterEmpty: number;
    minBPUMZFilter: number;
    minBPUMZFilterEmpty: number;
    maxBPUMNFilter: number;
    maxBPUMNFilterEmpty: number;
    minBPUMNFilter: number;
    minBPUMNFilterEmpty: number;
    maxUMREZFilter: number;
    maxUMREZFilterEmpty: number;
    minUMREZFilter: number;
    minUMREZFilterEmpty: number;
    maxUMRENFilter: number;
    maxUMRENFilterEmpty: number;
    minUMRENFilter: number;
    minUMRENFilterEmpty: number;
    maxNETPRFilter: number;
    maxNETPRFilterEmpty: number;
    minNETPRFilter: number;
    minNETPRFilterEmpty: number;
    maxPEINHFilter: number;
    maxPEINHFilterEmpty: number;
    minPEINHFilter: number;
    minPEINHFilterEmpty: number;
    maxNETWRFilter: number;
    maxNETWRFilterEmpty: number;
    minNETWRFilter: number;
    minNETWRFilterEmpty: number;
    maxBRTWRFilter: number;
    maxBRTWRFilterEmpty: number;
    minBRTWRFilter: number;
    minBRTWRFilterEmpty: number;
    maxAGDATFilter: DateTime;
    minAGDATFilter: DateTime;
    maxWEBAZFilter: number;
    maxWEBAZFilterEmpty: number;
    minWEBAZFilter: number;
    minWEBAZFilterEmpty: number;
    mwskzFilter = '';
    bonusFilter = '';
    insmkFilter = '';
    spinfFilter = '';
    prsdrFilter = '';
    bwtarFilter = '';
    bwttyFilter = '';
    abskzFilter = '';
    pstypFilter = '';
    knttpFilter = '';
    konnrFilter = '';
    maxKTPNRFilter: number;
    maxKTPNRFilterEmpty: number;
    minKTPNRFilter: number;
    minKTPNRFilterEmpty: number;
    maxPACKNOFilter: number;
    maxPACKNOFilterEmpty: number;
    minPACKNOFilter: number;
    minPACKNOFilterEmpty: number;
    anfnrFilter = '';
    banfnFilter = '';
    maxBNFPOFilter: number;
    maxBNFPOFilterEmpty: number;
    minBNFPOFilter: number;
    minBNFPOFilterEmpty: number;

    constructor(
        injector: Injector,
        private _ekpOsServiceProxy: EKPOsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getEKPOs(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records && this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._ekpOsServiceProxy
            .getAll(
                this.filterText,
                this.mandtFilter,
                this.ebelnFilter,
                this.maxEBELPFilter == null ? this.maxEBELPFilterEmpty : this.maxEBELPFilter,
                this.minEBELPFilter == null ? this.minEBELPFilterEmpty : this.minEBELPFilter,
                this.uniqueidFilter,
                this.loekzFilter,
                this.statuFilter,
                this.maxAEDATFilter === undefined
                    ? this.maxAEDATFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxAEDATFilter),
                this.minAEDATFilter === undefined
                    ? this.minAEDATFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minAEDATFilter),
                this.txZ01Filter,
                this.matnrFilter,
                this.ematnFilter,
                this.bukrsFilter,
                this.werksFilter,
                this.lgortFilter,
                this.bednrFilter,
                this.matklFilter,
                this.infnrFilter,
                this.idnlfFilter,
                this.maxKTMNGFilter == null ? this.maxKTMNGFilterEmpty : this.maxKTMNGFilter,
                this.minKTMNGFilter == null ? this.minKTMNGFilterEmpty : this.minKTMNGFilter,
                this.maxMENGEFilter == null ? this.maxMENGEFilterEmpty : this.maxMENGEFilter,
                this.minMENGEFilter == null ? this.minMENGEFilterEmpty : this.minMENGEFilter,
                this.meinsFilter,
                this.bprmeFilter,
                this.maxBPUMZFilter == null ? this.maxBPUMZFilterEmpty : this.maxBPUMZFilter,
                this.minBPUMZFilter == null ? this.minBPUMZFilterEmpty : this.minBPUMZFilter,
                this.maxBPUMNFilter == null ? this.maxBPUMNFilterEmpty : this.maxBPUMNFilter,
                this.minBPUMNFilter == null ? this.minBPUMNFilterEmpty : this.minBPUMNFilter,
                this.maxUMREZFilter == null ? this.maxUMREZFilterEmpty : this.maxUMREZFilter,
                this.minUMREZFilter == null ? this.minUMREZFilterEmpty : this.minUMREZFilter,
                this.maxUMRENFilter == null ? this.maxUMRENFilterEmpty : this.maxUMRENFilter,
                this.minUMRENFilter == null ? this.minUMRENFilterEmpty : this.minUMRENFilter,
                this.maxNETPRFilter == null ? this.maxNETPRFilterEmpty : this.maxNETPRFilter,
                this.minNETPRFilter == null ? this.minNETPRFilterEmpty : this.minNETPRFilter,
                this.maxPEINHFilter == null ? this.maxPEINHFilterEmpty : this.maxPEINHFilter,
                this.minPEINHFilter == null ? this.minPEINHFilterEmpty : this.minPEINHFilter,
                this.maxNETWRFilter == null ? this.maxNETWRFilterEmpty : this.maxNETWRFilter,
                this.minNETWRFilter == null ? this.minNETWRFilterEmpty : this.minNETWRFilter,
                this.maxBRTWRFilter == null ? this.maxBRTWRFilterEmpty : this.maxBRTWRFilter,
                this.minBRTWRFilter == null ? this.minBRTWRFilterEmpty : this.minBRTWRFilter,
                this.maxAGDATFilter === undefined
                    ? this.maxAGDATFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxAGDATFilter),
                this.minAGDATFilter === undefined
                    ? this.minAGDATFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minAGDATFilter),
                this.maxWEBAZFilter == null ? this.maxWEBAZFilterEmpty : this.maxWEBAZFilter,
                this.minWEBAZFilter == null ? this.minWEBAZFilterEmpty : this.minWEBAZFilter,
                this.mwskzFilter,
                this.bonusFilter,
                this.insmkFilter,
                this.spinfFilter,
                this.prsdrFilter,
                this.bwtarFilter,
                this.bwttyFilter,
                this.abskzFilter,
                this.pstypFilter,
                this.knttpFilter,
                this.konnrFilter,
                this.maxKTPNRFilter == null ? this.maxKTPNRFilterEmpty : this.maxKTPNRFilter,
                this.minKTPNRFilter == null ? this.minKTPNRFilterEmpty : this.minKTPNRFilter,
                this.maxPACKNOFilter == null ? this.maxPACKNOFilterEmpty : this.maxPACKNOFilter,
                this.minPACKNOFilter == null ? this.minPACKNOFilterEmpty : this.minPACKNOFilter,
                this.anfnrFilter,
                this.banfnFilter,
                this.maxBNFPOFilter == null ? this.maxBNFPOFilterEmpty : this.maxBNFPOFilter,
                this.minBNFPOFilter == null ? this.minBNFPOFilterEmpty : this.minBNFPOFilter,
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

    createEKPO(): void {
        this.createOrEditEKPOModal.show();
    }

    deleteEKPO(ekpo: EKPODto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._ekpOsServiceProxy.delete(ekpo.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._ekpOsServiceProxy
            .getEKPOsToExcel(
                this.filterText,
                this.mandtFilter,
                this.ebelnFilter,
                this.maxEBELPFilter == null ? this.maxEBELPFilterEmpty : this.maxEBELPFilter,
                this.minEBELPFilter == null ? this.minEBELPFilterEmpty : this.minEBELPFilter,
                this.uniqueidFilter,
                this.loekzFilter,
                this.statuFilter,
                this.maxAEDATFilter === undefined
                    ? this.maxAEDATFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxAEDATFilter),
                this.minAEDATFilter === undefined
                    ? this.minAEDATFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minAEDATFilter),
                this.txZ01Filter,
                this.matnrFilter,
                this.ematnFilter,
                this.bukrsFilter,
                this.werksFilter,
                this.lgortFilter,
                this.bednrFilter,
                this.matklFilter,
                this.infnrFilter,
                this.idnlfFilter,
                this.maxKTMNGFilter == null ? this.maxKTMNGFilterEmpty : this.maxKTMNGFilter,
                this.minKTMNGFilter == null ? this.minKTMNGFilterEmpty : this.minKTMNGFilter,
                this.maxMENGEFilter == null ? this.maxMENGEFilterEmpty : this.maxMENGEFilter,
                this.minMENGEFilter == null ? this.minMENGEFilterEmpty : this.minMENGEFilter,
                this.meinsFilter,
                this.bprmeFilter,
                this.maxBPUMZFilter == null ? this.maxBPUMZFilterEmpty : this.maxBPUMZFilter,
                this.minBPUMZFilter == null ? this.minBPUMZFilterEmpty : this.minBPUMZFilter,
                this.maxBPUMNFilter == null ? this.maxBPUMNFilterEmpty : this.maxBPUMNFilter,
                this.minBPUMNFilter == null ? this.minBPUMNFilterEmpty : this.minBPUMNFilter,
                this.maxUMREZFilter == null ? this.maxUMREZFilterEmpty : this.maxUMREZFilter,
                this.minUMREZFilter == null ? this.minUMREZFilterEmpty : this.minUMREZFilter,
                this.maxUMRENFilter == null ? this.maxUMRENFilterEmpty : this.maxUMRENFilter,
                this.minUMRENFilter == null ? this.minUMRENFilterEmpty : this.minUMRENFilter,
                this.maxNETPRFilter == null ? this.maxNETPRFilterEmpty : this.maxNETPRFilter,
                this.minNETPRFilter == null ? this.minNETPRFilterEmpty : this.minNETPRFilter,
                this.maxPEINHFilter == null ? this.maxPEINHFilterEmpty : this.maxPEINHFilter,
                this.minPEINHFilter == null ? this.minPEINHFilterEmpty : this.minPEINHFilter,
                this.maxNETWRFilter == null ? this.maxNETWRFilterEmpty : this.maxNETWRFilter,
                this.minNETWRFilter == null ? this.minNETWRFilterEmpty : this.minNETWRFilter,
                this.maxBRTWRFilter == null ? this.maxBRTWRFilterEmpty : this.maxBRTWRFilter,
                this.minBRTWRFilter == null ? this.minBRTWRFilterEmpty : this.minBRTWRFilter,
                this.maxAGDATFilter === undefined
                    ? this.maxAGDATFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxAGDATFilter),
                this.minAGDATFilter === undefined
                    ? this.minAGDATFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minAGDATFilter),
                this.maxWEBAZFilter == null ? this.maxWEBAZFilterEmpty : this.maxWEBAZFilter,
                this.minWEBAZFilter == null ? this.minWEBAZFilterEmpty : this.minWEBAZFilter,
                this.mwskzFilter,
                this.bonusFilter,
                this.insmkFilter,
                this.spinfFilter,
                this.prsdrFilter,
                this.bwtarFilter,
                this.bwttyFilter,
                this.abskzFilter,
                this.pstypFilter,
                this.knttpFilter,
                this.konnrFilter,
                this.maxKTPNRFilter == null ? this.maxKTPNRFilterEmpty : this.maxKTPNRFilter,
                this.minKTPNRFilter == null ? this.minKTPNRFilterEmpty : this.minKTPNRFilter,
                this.maxPACKNOFilter == null ? this.maxPACKNOFilterEmpty : this.maxPACKNOFilter,
                this.minPACKNOFilter == null ? this.minPACKNOFilterEmpty : this.minPACKNOFilter,
                this.anfnrFilter,
                this.banfnFilter,
                this.maxBNFPOFilter == null ? this.maxBNFPOFilterEmpty : this.maxBNFPOFilter,
                this.minBNFPOFilter == null ? this.minBNFPOFilterEmpty : this.minBNFPOFilter
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }

    resetFilters(): void {
        this.filterText = '';
        this.mandtFilter = '';
        this.ebelnFilter = '';
        this.maxEBELPFilter = this.maxEBELPFilterEmpty;
        this.minEBELPFilter = this.maxEBELPFilterEmpty;
        this.uniqueidFilter = '';
        this.loekzFilter = '';
        this.statuFilter = '';
        this.maxAEDATFilter = undefined;
        this.minAEDATFilter = undefined;
        this.txZ01Filter = '';
        this.matnrFilter = '';
        this.ematnFilter = '';
        this.bukrsFilter = '';
        this.werksFilter = '';
        this.lgortFilter = '';
        this.bednrFilter = '';
        this.matklFilter = '';
        this.infnrFilter = '';
        this.idnlfFilter = '';
        this.maxKTMNGFilter = this.maxKTMNGFilterEmpty;
        this.minKTMNGFilter = this.maxKTMNGFilterEmpty;
        this.maxMENGEFilter = this.maxMENGEFilterEmpty;
        this.minMENGEFilter = this.maxMENGEFilterEmpty;
        this.meinsFilter = '';
        this.bprmeFilter = '';
        this.maxBPUMZFilter = this.maxBPUMZFilterEmpty;
        this.minBPUMZFilter = this.maxBPUMZFilterEmpty;
        this.maxBPUMNFilter = this.maxBPUMNFilterEmpty;
        this.minBPUMNFilter = this.maxBPUMNFilterEmpty;
        this.maxUMREZFilter = this.maxUMREZFilterEmpty;
        this.minUMREZFilter = this.maxUMREZFilterEmpty;
        this.maxUMRENFilter = this.maxUMRENFilterEmpty;
        this.minUMRENFilter = this.maxUMRENFilterEmpty;
        this.maxNETPRFilter = this.maxNETPRFilterEmpty;
        this.minNETPRFilter = this.maxNETPRFilterEmpty;
        this.maxPEINHFilter = this.maxPEINHFilterEmpty;
        this.minPEINHFilter = this.maxPEINHFilterEmpty;
        this.maxNETWRFilter = this.maxNETWRFilterEmpty;
        this.minNETWRFilter = this.maxNETWRFilterEmpty;
        this.maxBRTWRFilter = this.maxBRTWRFilterEmpty;
        this.minBRTWRFilter = this.maxBRTWRFilterEmpty;
        this.maxAGDATFilter = undefined;
        this.minAGDATFilter = undefined;
        this.maxWEBAZFilter = this.maxWEBAZFilterEmpty;
        this.minWEBAZFilter = this.maxWEBAZFilterEmpty;
        this.mwskzFilter = '';
        this.bonusFilter = '';
        this.insmkFilter = '';
        this.spinfFilter = '';
        this.prsdrFilter = '';
        this.bwtarFilter = '';
        this.bwttyFilter = '';
        this.abskzFilter = '';
        this.pstypFilter = '';
        this.knttpFilter = '';
        this.konnrFilter = '';
        this.maxKTPNRFilter = this.maxKTPNRFilterEmpty;
        this.minKTPNRFilter = this.maxKTPNRFilterEmpty;
        this.maxPACKNOFilter = this.maxPACKNOFilterEmpty;
        this.minPACKNOFilter = this.maxPACKNOFilterEmpty;
        this.anfnrFilter = '';
        this.banfnFilter = '';
        this.maxBNFPOFilter = this.maxBNFPOFilterEmpty;
        this.minBNFPOFilter = this.maxBNFPOFilterEmpty;

        this.getEKPOs();
    }
}
