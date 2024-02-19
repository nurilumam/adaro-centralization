import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { EKPOsServiceProxy, CreateOrEditEKPODto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditEKPOModal',
    templateUrl: './create-or-edit-ekpo-modal.component.html',
})
export class CreateOrEditEKPOModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    ekpo: CreateOrEditEKPODto = new CreateOrEditEKPODto();

    constructor(
        injector: Injector,
        private _ekpOsServiceProxy: EKPOsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(ekpoId?: string): void {
        if (!ekpoId) {
            this.ekpo = new CreateOrEditEKPODto();
            this.ekpo.id = ekpoId;
            this.ekpo.aedat = this._dateTimeService.getStartOfDay();
            this.ekpo.agdat = this._dateTimeService.getStartOfDay();

            this.active = true;
            this.modal.show();
        } else {
            this._ekpOsServiceProxy.getEKPOForEdit(ekpoId).subscribe((result) => {
                this.ekpo = result.ekpo;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this._ekpOsServiceProxy
            .createOrEdit(this.ekpo)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
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

    ngOnInit(): void {}
}
