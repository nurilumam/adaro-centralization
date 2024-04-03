import { Component, ViewChild, Injector, Output, EventEmitter, ViewEncapsulation } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { LookupPagesServiceProxy, LookupPageCostCenterLookupTableDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
@Component({
    selector: 'lookupPageCostCenterLookupTableModal',
    styleUrls: ['./lookupPage-costCenter-lookup-table-modal.component.less'],
    encapsulation: ViewEncapsulation.None,
    templateUrl: './lookupPage-costCenter-lookup-table-modal.component.html',
})
export class LookupPageCostCenterLookupTableModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    filterText = '';
    id: string;
    costCenter: LookupPageCostCenterLookupTableDto;
    displayName: string;
    costCenterCode: string;
    costCenterName: string | undefined;
    departmentName: string;
    isSender:boolean;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    active = false;
    saving = false;

    constructor(injector: Injector, private _lookupPagesServiceProxy: LookupPagesServiceProxy) {
        super(injector);
    }

    show(): void {
        this.active = true;
        this.paginator.rows = 50;
        this.getAll();
        this.modal.show();
    }

    getAll(event?: LazyLoadEvent) {
        if (!this.active) {
            return;
        }

        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records && this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();
        console.log(this.isSender);

        this._lookupPagesServiceProxy
            .getAllCostCenterForLookupTable(
                this.filterText,
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

    setAndSave(costCenter: LookupPageCostCenterLookupTableDto) {
        this.id = costCenter.id;
        this.costCenter = costCenter;
        this.costCenterCode = costCenter.costCenterCode;
        this.costCenterName = costCenter.costCenterName;
        this.displayName = costCenter.displayName;
        this.departmentName = costCenter.departmentName;
        this.active = false;
        this.modal.hide();
        this.modalSave.emit(null);
    }

    close(): void {
        this.active = false;
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
