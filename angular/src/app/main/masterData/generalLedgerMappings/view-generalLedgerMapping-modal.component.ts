import {AppConsts} from "@shared/AppConsts";
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetGeneralLedgerMappingForViewDto, GeneralLedgerMappingDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewGeneralLedgerMappingModal',
    templateUrl: './view-generalLedgerMapping-modal.component.html'
})
export class ViewGeneralLedgerMappingModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetGeneralLedgerMappingForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetGeneralLedgerMappingForViewDto();
        this.item.generalLedgerMapping = new GeneralLedgerMappingDto();
    }

    show(item: GetGeneralLedgerMappingForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }
    
    

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
