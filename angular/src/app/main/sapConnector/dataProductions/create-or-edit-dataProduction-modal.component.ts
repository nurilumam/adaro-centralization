import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { DataProductionsServiceProxy, CreateOrEditDataProductionDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditDataProductionModal',
    templateUrl: './create-or-edit-dataProduction-modal.component.html',
})
export class CreateOrEditDataProductionModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    dataProduction: CreateOrEditDataProductionDto = new CreateOrEditDataProductionDto();

    constructor(
        injector: Injector,
        private _dataProductionsServiceProxy: DataProductionsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(dataProductionId?: string): void {
        if (!dataProductionId) {
            this.dataProduction = new CreateOrEditDataProductionDto();
            this.dataProduction.id = dataProductionId;
            this.dataProduction.postingDate = this._dateTimeService.getStartOfDay();
            this.dataProduction.entryDate = this._dateTimeService.getStartOfDay();
            this.dataProduction.documentDate = this._dateTimeService.getStartOfDay();
            this.dataProduction.interfaceCreatedDate = this._dateTimeService.getStartOfDay();

            this.active = true;
            this.modal.show();
        } else {
            this._dataProductionsServiceProxy.getDataProductionForEdit(dataProductionId).subscribe((result) => {
                this.dataProduction = result.dataProduction;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this._dataProductionsServiceProxy
            .createOrEdit(this.dataProduction)
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
