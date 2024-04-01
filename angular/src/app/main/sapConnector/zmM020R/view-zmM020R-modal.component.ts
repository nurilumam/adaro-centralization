import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetZMM020RForViewDto, ZMM020RDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewZMM020RModal',
    templateUrl: './view-zmM020R-modal.component.html',
})
export class ViewZMM020RModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetZMM020RForViewDto;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetZMM020RForViewDto();
        this.item.zmM020R = new ZMM020RDto();
    }

    show(item: GetZMM020RForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
