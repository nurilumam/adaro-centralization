import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    RptProcurementAdjustsServiceProxy,
    CreateOrEditRptProcurementAdjustDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditRptProcurementAdjustModal',
    templateUrl: './create-or-edit-rptProcurementAdjust-modal.component.html',
})
export class CreateOrEditRptProcurementAdjustModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    rptProcurementAdjust: CreateOrEditRptProcurementAdjustDto = new CreateOrEditRptProcurementAdjustDto();

    constructor(
        injector: Injector,
        private _rptProcurementAdjustsServiceProxy: RptProcurementAdjustsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(rptProcurementAdjustId?: string): void {
        if (!rptProcurementAdjustId) {
            this.rptProcurementAdjust = new CreateOrEditRptProcurementAdjustDto();
            this.rptProcurementAdjust.id = rptProcurementAdjustId;

            this.active = true;
            this.modal.show();
        } else {
            this._rptProcurementAdjustsServiceProxy
                .getRptProcurementAdjustForEdit(rptProcurementAdjustId)
                .subscribe((result) => {
                    this.rptProcurementAdjust = result.rptProcurementAdjust;

                    this.active = true;
                    this.modal.show();
                });
        }
    }

    save(): void {
        this.saving = true;

        this._rptProcurementAdjustsServiceProxy
            .createOrEdit(this.rptProcurementAdjust)
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
