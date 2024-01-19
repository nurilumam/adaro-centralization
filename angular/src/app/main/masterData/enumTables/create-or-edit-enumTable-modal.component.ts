import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { EnumTablesServiceProxy, CreateOrEditEnumTableDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

             import { DateTimeService } from '@app/shared/common/timing/date-time.service';



@Component({
    selector: 'createOrEditEnumTableModal',
    templateUrl: './create-or-edit-enumTable-modal.component.html'
})
export class CreateOrEditEnumTableModalComponent extends AppComponentBase implements OnInit{
   
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    enumTable: CreateOrEditEnumTableDto = new CreateOrEditEnumTableDto();




    constructor(
        injector: Injector,
        private _enumTablesServiceProxy: EnumTablesServiceProxy,
             private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }
    
    show(enumTableId?: string): void {
    

        if (!enumTableId) {
            this.enumTable = new CreateOrEditEnumTableDto();
            this.enumTable.id = enumTableId;


            this.active = true;
            this.modal.show();
        } else {
            this._enumTablesServiceProxy.getEnumTableForEdit(enumTableId).subscribe(result => {
                this.enumTable = result.enumTable;



                this.active = true;
                this.modal.show();
            });
        }
        
        
    }

    save(): void {
            this.saving = true;
            
			
			
            this._enumTablesServiceProxy.createOrEdit(this.enumTable)
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
