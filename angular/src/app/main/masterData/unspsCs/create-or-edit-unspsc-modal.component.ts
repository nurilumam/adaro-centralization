import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { UNSPSCsServiceProxy, CreateOrEditUNSPSCDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

             import { DateTimeService } from '@app/shared/common/timing/date-time.service';



@Component({
    selector: 'createOrEditUNSPSCModal',
    templateUrl: './create-or-edit-unspsc-modal.component.html'
})
export class CreateOrEditUNSPSCModalComponent extends AppComponentBase implements OnInit{
   
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    unspsc: CreateOrEditUNSPSCDto = new CreateOrEditUNSPSCDto();




    constructor(
        injector: Injector,
        private _unspsCsServiceProxy: UNSPSCsServiceProxy,
             private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }
    
    show(unspscId?: string): void {
    

        if (!unspscId) {
            this.unspsc = new CreateOrEditUNSPSCDto();
            this.unspsc.id = unspscId;


            this.active = true;
            this.modal.show();
        } else {
            this._unspsCsServiceProxy.getUNSPSCForEdit(unspscId).subscribe(result => {
                this.unspsc = result.unspsc;



                this.active = true;
                this.modal.show();
            });
        }
        
        
    }

    save(): void {
            this.saving = true;
            
			
			
            this._unspsCsServiceProxy.createOrEdit(this.unspsc)
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
