import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import {
    GetJobSynchronizeForViewDto,
    JobSynchronizeDto,
    JobSchedulerType,
    JobSchedulerStatus,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewJobSynchronizeModal',
    templateUrl: './view-jobSynchronize-modal.component.html',
})
export class ViewJobSynchronizeModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetJobSynchronizeForViewDto;
    jobSchedulerType = JobSchedulerType;
    jobSchedulerStatus = JobSchedulerStatus;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetJobSynchronizeForViewDto();
        this.item.jobSynchronize = new JobSynchronizeDto();
    }

    show(item: GetJobSynchronizeForViewDto): void {
        console.log(item);
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
