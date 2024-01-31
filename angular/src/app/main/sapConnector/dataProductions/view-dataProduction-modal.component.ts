﻿import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetDataProductionForViewDto, DataProductionDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewDataProductionModal',
    templateUrl: './view-dataProduction-modal.component.html',
})
export class ViewDataProductionModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetDataProductionForViewDto;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetDataProductionForViewDto();
        this.item.dataProduction = new DataProductionDto();
    }

    show(item: GetDataProductionForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
