﻿import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    GeneralLedgerAccountsServiceProxy,
    CreateOrEditGeneralLedgerAccountDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { GeneralLedgerAccountCostCenterLookupTableModalComponent } from './generalLedgerAccount-costCenter-lookup-table-modal.component';

@Component({
    selector: 'createOrEditGeneralLedgerAccountModal',
    templateUrl: './create-or-edit-generalLedgerAccount-modal.component.html',
})
export class CreateOrEditGeneralLedgerAccountModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('generalLedgerAccountCostCenterLookupTableModal', { static: true })
    generalLedgerAccountCostCenterLookupTableModal: GeneralLedgerAccountCostCenterLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    generalLedgerAccount: CreateOrEditGeneralLedgerAccountDto = new CreateOrEditGeneralLedgerAccountDto();

    costCenterCostCenterName = '';

    constructor(
        injector: Injector,
        private _generalLedgerAccountsServiceProxy: GeneralLedgerAccountsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(generalLedgerAccountId?: string): void {
        if (!generalLedgerAccountId) {
            this.generalLedgerAccount = new CreateOrEditGeneralLedgerAccountDto();
            this.generalLedgerAccount.id = generalLedgerAccountId;
            this.costCenterCostCenterName = '';

            this.active = true;
            this.modal.show();
        } else {
            this._generalLedgerAccountsServiceProxy
                .getGeneralLedgerAccountForEdit(generalLedgerAccountId)
                .subscribe((result) => {
                    this.generalLedgerAccount = result.generalLedgerAccount;

                    this.costCenterCostCenterName = result.costCenterCostCenterName;

                    this.active = true;
                    this.modal.show();
                });
        }
    }

    save(): void {
        this.saving = true;

        this._generalLedgerAccountsServiceProxy
            .createOrEdit(this.generalLedgerAccount)
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

    openSelectCostCenterModal() {
        this.generalLedgerAccountCostCenterLookupTableModal.id = this.generalLedgerAccount.costCenterId;
        this.generalLedgerAccountCostCenterLookupTableModal.displayName = this.costCenterCostCenterName;
        this.generalLedgerAccountCostCenterLookupTableModal.show();
    }

    setCostCenterIdNull() {
        this.generalLedgerAccount.costCenterId = null;
        this.costCenterCostCenterName = '';
    }

    getNewCostCenterId() {
        this.generalLedgerAccount.costCenterId = this.generalLedgerAccountCostCenterLookupTableModal.id;
        this.costCenterCostCenterName = this.generalLedgerAccountCostCenterLookupTableModal.displayName;
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
