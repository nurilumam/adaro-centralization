import {AppConsts} from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute , Router} from '@angular/router';
import { UNSPSCsServiceProxy, UNSPSCDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditUNSPSCModalComponent } from './create-or-edit-unspsc-modal.component';

import { ViewUNSPSCModalComponent } from './view-unspsc-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

             import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './unspsCs.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class UNSPSCsComponent extends AppComponentBase {
    
    
    @ViewChild('createOrEditUNSPSCModal', { static: true }) createOrEditUNSPSCModal: CreateOrEditUNSPSCModalComponent;
    @ViewChild('viewUNSPSCModal', { static: true }) viewUNSPSCModal: ViewUNSPSCModalComponent;   
    
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    unspsC_CodeFilter = '';
    descriptionFilter = '';
    accountCodeFilter = '';
    descriptionIdFilter = '';






    constructor(
        injector: Injector,
        private _unspsCsServiceProxy: UNSPSCsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
             private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getUNSPSCs(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records &&
                this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._unspsCsServiceProxy.getAll(
            this.filterText,
            this.unspsC_CodeFilter,
            this.descriptionFilter,
            this.accountCodeFilter,
            this.descriptionIdFilter,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createUNSPSC(): void {
        this.createOrEditUNSPSCModal.show();        
    }


    deleteUNSPSC(unspsc: UNSPSCDto): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._unspsCsServiceProxy.delete(unspsc.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._unspsCsServiceProxy.getUNSPSCsToExcel(
        this.filterText,
            this.unspsC_CodeFilter,
            this.descriptionFilter,
            this.accountCodeFilter,
            this.descriptionIdFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
    
    
    
    
    

    resetFilters(): void {
        this.filterText = '';
            this.unspsC_CodeFilter = '';
    this.descriptionFilter = '';
    this.accountCodeFilter = '';
    this.descriptionIdFilter = '';

        this.getUNSPSCs();
    }
}
