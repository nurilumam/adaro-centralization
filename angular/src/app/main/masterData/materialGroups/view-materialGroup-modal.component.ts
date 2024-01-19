import {AppConsts} from "@shared/AppConsts";
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetMaterialGroupForViewDto, MaterialGroupDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewMaterialGroupModal',
    templateUrl: './view-materialGroup-modal.component.html'
})
export class ViewMaterialGroupModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetMaterialGroupForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetMaterialGroupForViewDto();
        this.item.materialGroup = new MaterialGroupDto();
    }

    show(item: GetMaterialGroupForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }
    
    

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
