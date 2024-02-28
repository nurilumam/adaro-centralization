import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    TransferBudgetItemsServiceProxy,
    CreateOrEditTransferBudgetItemDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { TransferBudgetItemTransferBudgetLookupTableModalComponent } from './transferBudgetItem-transferBudget-lookup-table-modal.component';
import { TransferBudgetItemCostCenterLookupTableModalComponent } from './transferBudgetItem-costCenter-lookup-table-modal.component';

@Component({
    selector: 'createOrEditTransferBudgetItemModal',
    templateUrl: './create-or-edit-transferBudgetItem-modal.component.html',
})
export class CreateOrEditTransferBudgetItemModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('transferBudgetItemTransferBudgetLookupTableModal', { static: true })
    transferBudgetItemTransferBudgetLookupTableModal: TransferBudgetItemTransferBudgetLookupTableModalComponent;
    @ViewChild('transferBudgetItemCostCenterLookupTableModal', { static: true })
    transferBudgetItemCostCenterLookupTableModal: TransferBudgetItemCostCenterLookupTableModalComponent;
    @ViewChild('transferBudgetItemCostCenterLookupTableModal2', { static: true })
    transferBudgetItemCostCenterLookupTableModal2: TransferBudgetItemCostCenterLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    transferBudgetItem: CreateOrEditTransferBudgetItemDto = new CreateOrEditTransferBudgetItemDto();

    transferBudgetDisplayProperty = '';
    costCenterDisplayProperty = '';
    costCenterDisplayProperty2 = '';

    constructor(
        injector: Injector,
        private _transferBudgetItemsServiceProxy: TransferBudgetItemsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(transferBudgetItemId?: string): void {
        if (!transferBudgetItemId) {
            this.transferBudgetItem = new CreateOrEditTransferBudgetItemDto();
            this.transferBudgetItem.id = transferBudgetItemId;
            this.transferBudgetDisplayProperty = '';
            this.costCenterDisplayProperty = '';
            this.costCenterDisplayProperty2 = '';

            this.active = true;
            this.modal.show();
        } else {
            this._transferBudgetItemsServiceProxy
                .getTransferBudgetItemForEdit(transferBudgetItemId)
                .subscribe((result) => {
                    this.transferBudgetItem = result.transferBudgetItem;

                    this.transferBudgetDisplayProperty = result.transferBudgetDisplayProperty;
                    this.costCenterDisplayProperty = result.costCenterDisplayProperty;
                    this.costCenterDisplayProperty2 = result.costCenterDisplayProperty2;

                    this.active = true;
                    this.modal.show();
                });
        }
    }

    save(): void {
        this.saving = true;

        this._transferBudgetItemsServiceProxy
            .createOrEdit(this.transferBudgetItem)
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

    openSelectTransferBudgetModal() {
        this.transferBudgetItemTransferBudgetLookupTableModal.id = this.transferBudgetItem.transferBudgetId;
        this.transferBudgetItemTransferBudgetLookupTableModal.displayName = this.transferBudgetDisplayProperty;
        this.transferBudgetItemTransferBudgetLookupTableModal.show();
    }
    openSelectCostCenterModal() {
        this.transferBudgetItemCostCenterLookupTableModal.id = this.transferBudgetItem.costCenterIdFrom;
        this.transferBudgetItemCostCenterLookupTableModal.displayName = this.costCenterDisplayProperty;
        this.transferBudgetItemCostCenterLookupTableModal.show();
    }
    openSelectCostCenterModal2() {
        this.transferBudgetItemCostCenterLookupTableModal2.id = this.transferBudgetItem.costCenterIdTo;
        this.transferBudgetItemCostCenterLookupTableModal2.displayName = this.costCenterDisplayProperty2;
        this.transferBudgetItemCostCenterLookupTableModal2.show();
    }

    setTransferBudgetIdNull() {
        this.transferBudgetItem.transferBudgetId = null;
        this.transferBudgetDisplayProperty = '';
    }
    setCostCenterIdFromNull() {
        this.transferBudgetItem.costCenterIdFrom = null;
        this.costCenterDisplayProperty = '';
    }
    setCostCenterIdToNull() {
        this.transferBudgetItem.costCenterIdTo = null;
        this.costCenterDisplayProperty2 = '';
    }

    getNewTransferBudgetId() {
        this.transferBudgetItem.transferBudgetId = this.transferBudgetItemTransferBudgetLookupTableModal.id;
        this.transferBudgetDisplayProperty = this.transferBudgetItemTransferBudgetLookupTableModal.displayName;
    }
    getNewCostCenterIdFrom() {
        this.transferBudgetItem.costCenterIdFrom = this.transferBudgetItemCostCenterLookupTableModal.id;
        this.costCenterDisplayProperty = this.transferBudgetItemCostCenterLookupTableModal.displayName;
    }
    getNewCostCenterIdTo() {
        this.transferBudgetItem.costCenterIdTo = this.transferBudgetItemCostCenterLookupTableModal2.id;
        this.costCenterDisplayProperty2 = this.transferBudgetItemCostCenterLookupTableModal2.displayName;
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
