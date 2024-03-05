import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { ZMM021RServiceProxy, CreateOrEditZMM021RDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditZMM021RModal',
    templateUrl: './create-or-edit-zmM021R-modal.component.html',
})
export class CreateOrEditZMM021RModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    zmM021R: CreateOrEditZMM021RDto = new CreateOrEditZMM021RDto();

    constructor(
        injector: Injector,
        private _zmM021RServiceProxy: ZMM021RServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(zmM021RId?: string): void {
        if (!zmM021RId) {
            this.zmM021R = new CreateOrEditZMM021RDto();
            this.zmM021R.id = zmM021RId;
            this.zmM021R.documentDate = this._dateTimeService.getStartOfDay();
            this.zmM021R.createdOn = this._dateTimeService.getStartOfDay();
            this.zmM021R.deliveryDate = this._dateTimeService.getStartOfDay();
            this.zmM021R.prFinalFirstApprovalDate = this._dateTimeService.getStartOfDay();
            this.zmM021R.prFinalLastApprovalDate = this._dateTimeService.getStartOfDay();
            this.zmM021R.poFirstApprovalDate = this._dateTimeService.getStartOfDay();
            this.zmM021R.poLastApprovalDate = this._dateTimeService.getStartOfDay();
            this.zmM021R.createdDate = this._dateTimeService.getStartOfDay();
            this.zmM021R.updatedDate = this._dateTimeService.getStartOfDay();

            this.active = true;
            this.modal.show();
        } else {
            this._zmM021RServiceProxy.getZMM021RForEdit(zmM021RId).subscribe((result) => {
                this.zmM021R = result.zmM021R;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this._zmM021RServiceProxy
            .createOrEdit(this.zmM021R)
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
