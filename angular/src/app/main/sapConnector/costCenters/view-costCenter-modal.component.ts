import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetCostCenterForViewDto, CostCenterDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewCostCenterModal',
    templateUrl: './view-costCenter-modal.component.html',
})
export class ViewCostCenterModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetCostCenterForViewDto;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetCostCenterForViewDto();
        this.item.costCenter = new CostCenterDto();
    }

    show(item: GetCostCenterForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
