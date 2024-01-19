import {AppConsts} from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute , Router} from '@angular/router';
import { GeneralLedgerMappingsServiceProxy, GeneralLedgerMappingDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditGeneralLedgerMappingModalComponent } from './create-or-edit-generalLedgerMapping-modal.component';

import { ViewGeneralLedgerMappingModalComponent } from './view-generalLedgerMapping-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

             import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './generalLedgerMappings.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class GeneralLedgerMappingsComponent extends AppComponentBase {
    
    
    @ViewChild('createOrEditGeneralLedgerMappingModal', { static: true }) createOrEditGeneralLedgerMappingModal: CreateOrEditGeneralLedgerMappingModalComponent;
    @ViewChild('viewGeneralLedgerMappingModal', { static: true }) viewGeneralLedgerMappingModal: ViewGeneralLedgerMappingModalComponent;   
    
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    glAccountFilter = '';
    glAccountDescriptionFilter = '';
    mappingTypeFilter = '';
    valuationClassFilter = '';
    valuationClassDescriptionFilter = '';






    constructor(
        injector: Injector,
        private _generalLedgerMappingsServiceProxy: GeneralLedgerMappingsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
             private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getGeneralLedgerMappings(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records &&
                this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._generalLedgerMappingsServiceProxy.getAll(
            this.filterText,
            this.glAccountFilter,
            this.glAccountDescriptionFilter,
            this.mappingTypeFilter,
            this.valuationClassFilter,
            this.valuationClassDescriptionFilter,
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

    createGeneralLedgerMapping(): void {
        this.createOrEditGeneralLedgerMappingModal.show();        
    }


    deleteGeneralLedgerMapping(generalLedgerMapping: GeneralLedgerMappingDto): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._generalLedgerMappingsServiceProxy.delete(generalLedgerMapping.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._generalLedgerMappingsServiceProxy.getGeneralLedgerMappingsToExcel(
        this.filterText,
            this.glAccountFilter,
            this.glAccountDescriptionFilter,
            this.mappingTypeFilter,
            this.valuationClassFilter,
            this.valuationClassDescriptionFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
    
    
    
    
    

    resetFilters(): void {
        this.filterText = '';
            this.glAccountFilter = '';
    this.glAccountDescriptionFilter = '';
    this.mappingTypeFilter = '';
    this.valuationClassFilter = '';
    this.valuationClassDescriptionFilter = '';

        this.getGeneralLedgerMappings();
    }
}
