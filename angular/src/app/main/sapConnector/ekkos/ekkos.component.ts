import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EKKOsServiceProxy, EkkoDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditEkkoModalComponent } from './create-or-edit-ekko-modal.component';

import { ViewEkkoModalComponent } from './view-ekko-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './ekkos.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class EkkosComponent extends AppComponentBase {
    @ViewChild('createOrEditEkkoModal', { static: true }) createOrEditEkkoModal: CreateOrEditEkkoModalComponent;
    @ViewChild('viewEkkoModal', { static: true }) viewEkkoModal: ViewEkkoModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    mandtFilter = '';
    ebelnFilter = '';
    bukrsFilter = '';
    bstypFilter = '';
    bsartFilter = '';
    bsakzFilter = '';
    loekzFilter = '';
    statuFilter = '';
    maxAEDATFilter: DateTime;
    minAEDATFilter: DateTime;
    ernamFilter = '';
    maxPINCRFilter: number;
    maxPINCRFilterEmpty: number;
    minPINCRFilter: number;
    minPINCRFilterEmpty: number;
    maxLPONRFilter: number;
    maxLPONRFilterEmpty: number;
    minLPONRFilter: number;
    minLPONRFilterEmpty: number;
    lifnrFilter = '';
    ztermFilter = '';
    maxZBD1TFilter: number;
    maxZBD1TFilterEmpty: number;
    minZBD1TFilter: number;
    minZBD1TFilterEmpty: number;
    maxZBD2TFilter: number;
    maxZBD2TFilterEmpty: number;
    minZBD2TFilter: number;
    minZBD2TFilterEmpty: number;
    maxZBD3TFilter: number;
    maxZBD3TFilterEmpty: number;
    minZBD3TFilter: number;
    minZBD3TFilterEmpty: number;
    maxZBD1PFilter: number;
    maxZBD1PFilterEmpty: number;
    minZBD1PFilter: number;
    minZBD1PFilterEmpty: number;
    maxZBD2PFilter: number;
    maxZBD2PFilterEmpty: number;
    minZBD2PFilter: number;
    minZBD2PFilterEmpty: number;
    ekorgFilter = '';
    ekgrpFilter = '';
    waersFilter = '';
    maxWKURSFilter: number;
    maxWKURSFilterEmpty: number;
    minWKURSFilter: number;
    minWKURSFilterEmpty: number;
    kufixFilter = '';
    maxBEDATFilter: DateTime;
    minBEDATFilter: DateTime;
    maxKDATBFilter: DateTime;
    minKDATBFilter: DateTime;
    maxKDATEFilter: DateTime;
    minKDATEFilter: DateTime;
    maxBWBDTFilter: DateTime;
    minBWBDTFilter: DateTime;
    maxGWLDTFilter: DateTime;
    minGWLDTFilter: DateTime;
    maxIHRANFilter: DateTime;
    minIHRANFilter: DateTime;
    kunnrFilter = '';
    konnrFilter = '';
    abgruFilter = '';
    autlfFilter = '';
    weaktFilter = '';
    reswkFilter = '';
    lblifFilter = '';
    incO1Filter = '';
    incO2Filter = '';
    submiFilter = '';
    knumvFilter = '';
    kalsmFilter = '';
    procstatFilter = '';
    unsezFilter = '';
    frggrFilter = '';
    frgsxFilter = '';
    frgkeFilter = '';
    frgzuFilter = '';
    adrnrFilter = '';

    constructor(
        injector: Injector,
        private _EKKOsServiceProxy: EKKOsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getEkkos(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records && this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._EKKOsServiceProxy
            .getAll(
                this.filterText,
                this.mandtFilter,
                this.ebelnFilter,
                this.bukrsFilter,
                this.bstypFilter,
                this.bsartFilter,
                this.bsakzFilter,
                this.loekzFilter,
                this.statuFilter,
                this.maxAEDATFilter === undefined
                    ? this.maxAEDATFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxAEDATFilter),
                this.minAEDATFilter === undefined
                    ? this.minAEDATFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minAEDATFilter),
                this.ernamFilter,
                this.maxPINCRFilter == null ? this.maxPINCRFilterEmpty : this.maxPINCRFilter,
                this.minPINCRFilter == null ? this.minPINCRFilterEmpty : this.minPINCRFilter,
                this.maxLPONRFilter == null ? this.maxLPONRFilterEmpty : this.maxLPONRFilter,
                this.minLPONRFilter == null ? this.minLPONRFilterEmpty : this.minLPONRFilter,
                this.lifnrFilter,
                this.ztermFilter,
                this.maxZBD1TFilter == null ? this.maxZBD1TFilterEmpty : this.maxZBD1TFilter,
                this.minZBD1TFilter == null ? this.minZBD1TFilterEmpty : this.minZBD1TFilter,
                this.maxZBD2TFilter == null ? this.maxZBD2TFilterEmpty : this.maxZBD2TFilter,
                this.minZBD2TFilter == null ? this.minZBD2TFilterEmpty : this.minZBD2TFilter,
                this.maxZBD3TFilter == null ? this.maxZBD3TFilterEmpty : this.maxZBD3TFilter,
                this.minZBD3TFilter == null ? this.minZBD3TFilterEmpty : this.minZBD3TFilter,
                this.maxZBD1PFilter == null ? this.maxZBD1PFilterEmpty : this.maxZBD1PFilter,
                this.minZBD1PFilter == null ? this.minZBD1PFilterEmpty : this.minZBD1PFilter,
                this.maxZBD2PFilter == null ? this.maxZBD2PFilterEmpty : this.maxZBD2PFilter,
                this.minZBD2PFilter == null ? this.minZBD2PFilterEmpty : this.minZBD2PFilter,
                this.ekorgFilter,
                this.ekgrpFilter,
                this.waersFilter,
                this.maxWKURSFilter == null ? this.maxWKURSFilterEmpty : this.maxWKURSFilter,
                this.minWKURSFilter == null ? this.minWKURSFilterEmpty : this.minWKURSFilter,
                this.kufixFilter,
                this.maxBEDATFilter === undefined
                    ? this.maxBEDATFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxBEDATFilter),
                this.minBEDATFilter === undefined
                    ? this.minBEDATFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minBEDATFilter),
                this.maxKDATBFilter === undefined
                    ? this.maxKDATBFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxKDATBFilter),
                this.minKDATBFilter === undefined
                    ? this.minKDATBFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minKDATBFilter),
                this.maxKDATEFilter === undefined
                    ? this.maxKDATEFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxKDATEFilter),
                this.minKDATEFilter === undefined
                    ? this.minKDATEFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minKDATEFilter),
                this.maxBWBDTFilter === undefined
                    ? this.maxBWBDTFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxBWBDTFilter),
                this.minBWBDTFilter === undefined
                    ? this.minBWBDTFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minBWBDTFilter),
                this.maxGWLDTFilter === undefined
                    ? this.maxGWLDTFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxGWLDTFilter),
                this.minGWLDTFilter === undefined
                    ? this.minGWLDTFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minGWLDTFilter),
                this.maxIHRANFilter === undefined
                    ? this.maxIHRANFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxIHRANFilter),
                this.minIHRANFilter === undefined
                    ? this.minIHRANFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minIHRANFilter),
                this.kunnrFilter,
                this.konnrFilter,
                this.abgruFilter,
                this.autlfFilter,
                this.weaktFilter,
                this.reswkFilter,
                this.lblifFilter,
                this.incO1Filter,
                this.incO2Filter,
                this.submiFilter,
                this.knumvFilter,
                this.kalsmFilter,
                this.procstatFilter,
                this.unsezFilter,
                this.frggrFilter,
                this.frgsxFilter,
                this.frgkeFilter,
                this.frgzuFilter,
                this.adrnrFilter,
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

    createEkko(): void {
        this.createOrEditEkkoModal.show();
    }

    deleteEkko(ekko: EkkoDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._EKKOsServiceProxy.delete(ekko.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._EKKOsServiceProxy
            .getEkkosToExcel(
                this.filterText,
                this.mandtFilter,
                this.ebelnFilter,
                this.bukrsFilter,
                this.bstypFilter,
                this.bsartFilter,
                this.bsakzFilter,
                this.loekzFilter,
                this.statuFilter,
                this.maxAEDATFilter === undefined
                    ? this.maxAEDATFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxAEDATFilter),
                this.minAEDATFilter === undefined
                    ? this.minAEDATFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minAEDATFilter),
                this.ernamFilter,
                this.maxPINCRFilter == null ? this.maxPINCRFilterEmpty : this.maxPINCRFilter,
                this.minPINCRFilter == null ? this.minPINCRFilterEmpty : this.minPINCRFilter,
                this.maxLPONRFilter == null ? this.maxLPONRFilterEmpty : this.maxLPONRFilter,
                this.minLPONRFilter == null ? this.minLPONRFilterEmpty : this.minLPONRFilter,
                this.lifnrFilter,
                this.ztermFilter,
                this.maxZBD1TFilter == null ? this.maxZBD1TFilterEmpty : this.maxZBD1TFilter,
                this.minZBD1TFilter == null ? this.minZBD1TFilterEmpty : this.minZBD1TFilter,
                this.maxZBD2TFilter == null ? this.maxZBD2TFilterEmpty : this.maxZBD2TFilter,
                this.minZBD2TFilter == null ? this.minZBD2TFilterEmpty : this.minZBD2TFilter,
                this.maxZBD3TFilter == null ? this.maxZBD3TFilterEmpty : this.maxZBD3TFilter,
                this.minZBD3TFilter == null ? this.minZBD3TFilterEmpty : this.minZBD3TFilter,
                this.maxZBD1PFilter == null ? this.maxZBD1PFilterEmpty : this.maxZBD1PFilter,
                this.minZBD1PFilter == null ? this.minZBD1PFilterEmpty : this.minZBD1PFilter,
                this.maxZBD2PFilter == null ? this.maxZBD2PFilterEmpty : this.maxZBD2PFilter,
                this.minZBD2PFilter == null ? this.minZBD2PFilterEmpty : this.minZBD2PFilter,
                this.ekorgFilter,
                this.ekgrpFilter,
                this.waersFilter,
                this.maxWKURSFilter == null ? this.maxWKURSFilterEmpty : this.maxWKURSFilter,
                this.minWKURSFilter == null ? this.minWKURSFilterEmpty : this.minWKURSFilter,
                this.kufixFilter,
                this.maxBEDATFilter === undefined
                    ? this.maxBEDATFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxBEDATFilter),
                this.minBEDATFilter === undefined
                    ? this.minBEDATFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minBEDATFilter),
                this.maxKDATBFilter === undefined
                    ? this.maxKDATBFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxKDATBFilter),
                this.minKDATBFilter === undefined
                    ? this.minKDATBFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minKDATBFilter),
                this.maxKDATEFilter === undefined
                    ? this.maxKDATEFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxKDATEFilter),
                this.minKDATEFilter === undefined
                    ? this.minKDATEFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minKDATEFilter),
                this.maxBWBDTFilter === undefined
                    ? this.maxBWBDTFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxBWBDTFilter),
                this.minBWBDTFilter === undefined
                    ? this.minBWBDTFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minBWBDTFilter),
                this.maxGWLDTFilter === undefined
                    ? this.maxGWLDTFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxGWLDTFilter),
                this.minGWLDTFilter === undefined
                    ? this.minGWLDTFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minGWLDTFilter),
                this.maxIHRANFilter === undefined
                    ? this.maxIHRANFilter
                    : this._dateTimeService.getEndOfDayForDate(this.maxIHRANFilter),
                this.minIHRANFilter === undefined
                    ? this.minIHRANFilter
                    : this._dateTimeService.getStartOfDayForDate(this.minIHRANFilter),
                this.kunnrFilter,
                this.konnrFilter,
                this.abgruFilter,
                this.autlfFilter,
                this.weaktFilter,
                this.reswkFilter,
                this.lblifFilter,
                this.incO1Filter,
                this.incO2Filter,
                this.submiFilter,
                this.knumvFilter,
                this.kalsmFilter,
                this.procstatFilter,
                this.unsezFilter,
                this.frggrFilter,
                this.frgsxFilter,
                this.frgkeFilter,
                this.frgzuFilter,
                this.adrnrFilter
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }

    resetFilters(): void {
        this.filterText = '';
        this.mandtFilter = '';
        this.ebelnFilter = '';
        this.bukrsFilter = '';
        this.bstypFilter = '';
        this.bsartFilter = '';
        this.bsakzFilter = '';
        this.loekzFilter = '';
        this.statuFilter = '';
        this.maxAEDATFilter = undefined;
        this.minAEDATFilter = undefined;
        this.ernamFilter = '';
        this.maxPINCRFilter = this.maxPINCRFilterEmpty;
        this.minPINCRFilter = this.maxPINCRFilterEmpty;
        this.maxLPONRFilter = this.maxLPONRFilterEmpty;
        this.minLPONRFilter = this.maxLPONRFilterEmpty;
        this.lifnrFilter = '';
        this.ztermFilter = '';
        this.maxZBD1TFilter = this.maxZBD1TFilterEmpty;
        this.minZBD1TFilter = this.maxZBD1TFilterEmpty;
        this.maxZBD2TFilter = this.maxZBD2TFilterEmpty;
        this.minZBD2TFilter = this.maxZBD2TFilterEmpty;
        this.maxZBD3TFilter = this.maxZBD3TFilterEmpty;
        this.minZBD3TFilter = this.maxZBD3TFilterEmpty;
        this.maxZBD1PFilter = this.maxZBD1PFilterEmpty;
        this.minZBD1PFilter = this.maxZBD1PFilterEmpty;
        this.maxZBD2PFilter = this.maxZBD2PFilterEmpty;
        this.minZBD2PFilter = this.maxZBD2PFilterEmpty;
        this.ekorgFilter = '';
        this.ekgrpFilter = '';
        this.waersFilter = '';
        this.maxWKURSFilter = this.maxWKURSFilterEmpty;
        this.minWKURSFilter = this.maxWKURSFilterEmpty;
        this.kufixFilter = '';
        this.maxBEDATFilter = undefined;
        this.minBEDATFilter = undefined;
        this.maxKDATBFilter = undefined;
        this.minKDATBFilter = undefined;
        this.maxKDATEFilter = undefined;
        this.minKDATEFilter = undefined;
        this.maxBWBDTFilter = undefined;
        this.minBWBDTFilter = undefined;
        this.maxGWLDTFilter = undefined;
        this.minGWLDTFilter = undefined;
        this.maxIHRANFilter = undefined;
        this.minIHRANFilter = undefined;
        this.kunnrFilter = '';
        this.konnrFilter = '';
        this.abgruFilter = '';
        this.autlfFilter = '';
        this.weaktFilter = '';
        this.reswkFilter = '';
        this.lblifFilter = '';
        this.incO1Filter = '';
        this.incO2Filter = '';
        this.submiFilter = '';
        this.knumvFilter = '';
        this.kalsmFilter = '';
        this.procstatFilter = '';
        this.unsezFilter = '';
        this.frggrFilter = '';
        this.frgsxFilter = '';
        this.frgkeFilter = '';
        this.frgzuFilter = '';
        this.adrnrFilter = '';

        this.getEkkos();
    }
}
