import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetRptProcurementAdjustForViewDto, RptProcurementAdjustDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewRptProcurementAdjustModal',
    templateUrl: './view-rptProcurementAdjust-modal.component.html',
})
export class ViewRptProcurementAdjustModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetRptProcurementAdjustForViewDto;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetRptProcurementAdjustForViewDto();
        this.item.rptProcurementAdjust = new RptProcurementAdjustDto();
    }

    show(item: GetRptProcurementAdjustForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
