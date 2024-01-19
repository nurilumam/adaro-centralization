﻿import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { MaterialGroupsServiceProxy, CreateOrEditMaterialGroupDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

             import { DateTimeService } from '@app/shared/common/timing/date-time.service';



@Component({
    selector: 'createOrEditMaterialGroupModal',
    templateUrl: './create-or-edit-materialGroup-modal.component.html'
})
export class CreateOrEditMaterialGroupModalComponent extends AppComponentBase implements OnInit{
   
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    materialGroup: CreateOrEditMaterialGroupDto = new CreateOrEditMaterialGroupDto();




    constructor(
        injector: Injector,
        private _materialGroupsServiceProxy: MaterialGroupsServiceProxy,
             private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }
    
    show(materialGroupId?: string): void {
    

        if (!materialGroupId) {
            this.materialGroup = new CreateOrEditMaterialGroupDto();
            this.materialGroup.id = materialGroupId;


            this.active = true;
            this.modal.show();
        } else {
            this._materialGroupsServiceProxy.getMaterialGroupForEdit(materialGroupId).subscribe(result => {
                this.materialGroup = result.materialGroup;



                this.active = true;
                this.modal.show();
            });
        }
        
        
    }

    save(): void {
            this.saving = true;
            
			
			
            this._materialGroupsServiceProxy.createOrEdit(this.materialGroup)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }













    close(): void {
        this.active = false;
        this.modal.hide();
    }
    
     ngOnInit(): void {
        
     }    
}
