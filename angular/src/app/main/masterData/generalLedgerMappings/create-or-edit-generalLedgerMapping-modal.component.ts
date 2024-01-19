import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { GeneralLedgerMappingsServiceProxy, CreateOrEditGeneralLedgerMappingDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

             import { DateTimeService } from '@app/shared/common/timing/date-time.service';



@Component({
    selector: 'createOrEditGeneralLedgerMappingModal',
    templateUrl: './create-or-edit-generalLedgerMapping-modal.component.html'
})
export class CreateOrEditGeneralLedgerMappingModalComponent extends AppComponentBase implements OnInit{
   
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    generalLedgerMapping: CreateOrEditGeneralLedgerMappingDto = new CreateOrEditGeneralLedgerMappingDto();




    constructor(
        injector: Injector,
        private _generalLedgerMappingsServiceProxy: GeneralLedgerMappingsServiceProxy,
             private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }
    
    show(generalLedgerMappingId?: string): void {
    

        if (!generalLedgerMappingId) {
            this.generalLedgerMapping = new CreateOrEditGeneralLedgerMappingDto();
            this.generalLedgerMapping.id = generalLedgerMappingId;


            this.active = true;
            this.modal.show();
        } else {
            this._generalLedgerMappingsServiceProxy.getGeneralLedgerMappingForEdit(generalLedgerMappingId).subscribe(result => {
                this.generalLedgerMapping = result.generalLedgerMapping;



                this.active = true;
                this.modal.show();
            });
        }
        
        
    }

    save(): void {
            this.saving = true;
            
			
			
            this._generalLedgerMappingsServiceProxy.createOrEdit(this.generalLedgerMapping)
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
