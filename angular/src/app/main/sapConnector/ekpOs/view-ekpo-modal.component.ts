import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetEKPOForViewDto, EKPODto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewEKPOModal',
    templateUrl: './view-ekpo-modal.component.html',
})
export class ViewEKPOModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetEKPOForViewDto;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetEKPOForViewDto();
        this.item.ekpo = new EKPODto();
    }

    show(item: GetEKPOForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
