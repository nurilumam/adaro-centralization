﻿import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { CostCentersServiceProxy, CreateOrEditCostCenterDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditCostCenterModal',
    templateUrl: './create-or-edit-costCenter-modal.component.html',
})
export class CreateOrEditCostCenterModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    costCenter: CreateOrEditCostCenterDto = new CreateOrEditCostCenterDto();

    constructor(
        injector: Injector,
        private _costCentersServiceProxy: CostCentersServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(costCenterId?: string): void {
        if (!costCenterId) {
            this.costCenter = new CreateOrEditCostCenterDto();
            this.costCenter.id = costCenterId;

            this.active = true;
            this.modal.show();
        } else {
            this._costCentersServiceProxy.getCostCenterForEdit(costCenterId).subscribe((result) => {
                this.costCenter = result.costCenter;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this._costCentersServiceProxy
            .createOrEdit(this.costCenter)
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
