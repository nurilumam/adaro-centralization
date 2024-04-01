import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { ZMM020RServiceProxy, CreateOrEditZMM020RDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditZMM020RModal',
    templateUrl: './create-or-edit-zmM020R-modal.component.html',
})
export class CreateOrEditZMM020RModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    zmM020R: CreateOrEditZMM020RDto = new CreateOrEditZMM020RDto();

    constructor(
        injector: Injector,
        private _zmM020RServiceProxy: ZMM020RServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(zmM020RId?: string): void {
        if (!zmM020RId) {
            this.zmM020R = new CreateOrEditZMM020RDto();
            this.zmM020R.id = zmM020RId;
            this.zmM020R.deliveryDate = this._dateTimeService.getStartOfDay();
            this.zmM020R.purchaseOrderDate = this._dateTimeService.getStartOfDay();
            this.zmM020R.firstApprovalDate = this._dateTimeService.getStartOfDay();
            this.zmM020R.lastApprovalDate = this._dateTimeService.getStartOfDay();
            this.zmM020R.createdDate = this._dateTimeService.getStartOfDay();
            this.zmM020R.updatedDate = this._dateTimeService.getStartOfDay();

            this.active = true;
            this.modal.show();
        } else {
            this._zmM020RServiceProxy.getZMM020RForEdit(zmM020RId).subscribe((result) => {
                this.zmM020R = result.zmM020R;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this._zmM020RServiceProxy
            .createOrEdit(this.zmM020R)
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
