import {AppConsts} from "@shared/AppConsts";
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetEnumTableForViewDto, EnumTableDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewEnumTableModal',
    templateUrl: './view-enumTable-modal.component.html'
})
export class ViewEnumTableModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetEnumTableForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetEnumTableForViewDto();
        this.item.enumTable = new EnumTableDto();
    }

    show(item: GetEnumTableForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }
    
    

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
