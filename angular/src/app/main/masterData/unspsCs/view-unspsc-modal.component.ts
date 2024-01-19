import {AppConsts} from "@shared/AppConsts";
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetUNSPSCForViewDto, UNSPSCDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewUNSPSCModal',
    templateUrl: './view-unspsc-modal.component.html'
})
export class ViewUNSPSCModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetUNSPSCForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetUNSPSCForViewDto();
        this.item.unspsc = new UNSPSCDto();
    }

    show(item: GetUNSPSCForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }
    
    

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
