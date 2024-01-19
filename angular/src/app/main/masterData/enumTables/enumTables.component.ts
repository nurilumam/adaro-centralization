import {AppConsts} from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute , Router} from '@angular/router';
import { EnumTablesServiceProxy, EnumTableDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditEnumTableModalComponent } from './create-or-edit-enumTable-modal.component';

import { ViewEnumTableModalComponent } from './view-enumTable-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

             import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './enumTables.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class EnumTablesComponent extends AppComponentBase {
    
    
    @ViewChild('createOrEditEnumTableModal', { static: true }) createOrEditEnumTableModal: CreateOrEditEnumTableModalComponent;
    @ViewChild('viewEnumTableModal', { static: true }) viewEnumTableModal: ViewEnumTableModalComponent;   
    
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    enumCodeFilter = '';
    enumValueFilter = '';
    enumLabelFilter = '';






    constructor(
        injector: Injector,
        private _enumTablesServiceProxy: EnumTablesServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
             private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getEnumTables(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records &&
                this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._enumTablesServiceProxy.getAll(
            this.filterText,
            this.enumCodeFilter,
            this.enumValueFilter,
            this.enumLabelFilter,
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

    createEnumTable(): void {
        this.createOrEditEnumTableModal.show();        
    }


    deleteEnumTable(enumTable: EnumTableDto): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._enumTablesServiceProxy.delete(enumTable.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._enumTablesServiceProxy.getEnumTablesToExcel(
        this.filterText,
            this.enumCodeFilter,
            this.enumValueFilter,
            this.enumLabelFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
    
    
    
    
    

    resetFilters(): void {
        this.filterText = '';
            this.enumCodeFilter = '';
    this.enumValueFilter = '';
    this.enumLabelFilter = '';

        this.getEnumTables();
    }
}
