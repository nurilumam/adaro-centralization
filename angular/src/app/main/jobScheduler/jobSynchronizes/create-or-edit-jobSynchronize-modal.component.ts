import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { JobSynchronizesServiceProxy, CreateOrEditJobSynchronizeDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditJobSynchronizeModal',
    templateUrl: './create-or-edit-jobSynchronize-modal.component.html',
})
export class CreateOrEditJobSynchronizeModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    jobSynchronize: CreateOrEditJobSynchronizeDto = new CreateOrEditJobSynchronizeDto();

    constructor(
        injector: Injector,
        private _jobSynchronizesServiceProxy: JobSynchronizesServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(jobSynchronizeId?: string): void {
        if (!jobSynchronizeId) {
            this.jobSynchronize = new CreateOrEditJobSynchronizeDto();
            this.jobSynchronize.id = jobSynchronizeId;
            this.jobSynchronize.lastUpdate = this._dateTimeService.getStartOfDay();

            this.active = true;
            this.modal.show();
        } else {
            this._jobSynchronizesServiceProxy.getJobSynchronizeForEdit(jobSynchronizeId).subscribe((result) => {
                this.jobSynchronize = result.jobSynchronize;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this._jobSynchronizesServiceProxy
            .createOrEdit(this.jobSynchronize)
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
