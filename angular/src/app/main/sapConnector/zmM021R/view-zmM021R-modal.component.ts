import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetZMM021RForViewDto, ZMM021RDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewZMM021RModal',
    templateUrl: './view-zmM021R-modal.component.html',
})
export class ViewZMM021RModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetZMM021RForViewDto;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetZMM021RForViewDto();
        this.item.zmM021R = new ZMM021RDto();
    }

    show(item: GetZMM021RForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
