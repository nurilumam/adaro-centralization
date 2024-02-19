import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { EKKOsServiceProxy, CreateOrEditEkkoDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditEkkoModal',
    templateUrl: './create-or-edit-ekko-modal.component.html',
})
export class CreateOrEditEkkoModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    ekko: CreateOrEditEkkoDto = new CreateOrEditEkkoDto();

    constructor(
        injector: Injector,
        private _EKKOsServiceProxy: EKKOsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(ekkoId?: string): void {
        if (!ekkoId) {
            this.ekko = new CreateOrEditEkkoDto();
            this.ekko.id = ekkoId;
            this.ekko.aedat = this._dateTimeService.getStartOfDay();
            this.ekko.bedat = this._dateTimeService.getStartOfDay();
            this.ekko.kdatb = this._dateTimeService.getStartOfDay();
            this.ekko.kdate = this._dateTimeService.getStartOfDay();
            this.ekko.bwbdt = this._dateTimeService.getStartOfDay();
            this.ekko.gwldt = this._dateTimeService.getStartOfDay();
            this.ekko.ihran = this._dateTimeService.getStartOfDay();

            this.active = true;
            this.modal.show();
        } else {
            this._EKKOsServiceProxy.getEkkoForEdit(ekkoId).subscribe((result) => {
                this.ekko = result.ekko;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this._EKKOsServiceProxy
            .createOrEdit(this.ekko)
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
