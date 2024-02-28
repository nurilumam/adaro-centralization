import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { TransferBudgetsServiceProxy, CreateOrEditTransferBudgetDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';
import { ActivatedRoute, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Observable } from '@node_modules/rxjs';
import { BreadcrumbItem } from '@app/shared/common/sub-header/sub-header.component';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './create-or-edit-transfer-budget.component.html',
    animations: [appModuleAnimation()],
})
export class CreateOrEditTransferBudgetComponent extends AppComponentBase implements OnInit {
    active = false;
    saving = false;

    transferBudget: CreateOrEditTransferBudgetDto = new CreateOrEditTransferBudgetDto();

    breadcrumbs: BreadcrumbItem[] = [
        new BreadcrumbItem(this.l('TransferBudget'), '/app/main/finance/transferBudgets'),
        new BreadcrumbItem(this.l('Entity_Name_Plural_Here') + '' + this.l('Details')),
    ];

    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
        private _transferBudgetsServiceProxy: TransferBudgetsServiceProxy,
        private _router: Router,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.show(this._activatedRoute.snapshot.queryParams['id']);
    }

    show(transferBudgetId?: string): void {
        if (!transferBudgetId) {
            this.transferBudget = new CreateOrEditTransferBudgetDto();
            this.transferBudget.id = transferBudgetId;

            this.active = true;
        } else {
            this._transferBudgetsServiceProxy.getTransferBudgetForEdit(transferBudgetId).subscribe((result) => {
                this.transferBudget = result.transferBudget;

                this.active = true;
            });
        }
    }

    save(): void {
        this.saving = true;

        this._transferBudgetsServiceProxy
            .createOrEdit(this.transferBudget)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe((x) => {
                this.saving = false;
                this.notify.info(this.l('SavedSuccessfully'));
                this._router.navigate(['/app/main/finance/transferBudgets']);
            });
    }

    saveAndNew(): void {
        this.saving = true;

        this._transferBudgetsServiceProxy
            .createOrEdit(this.transferBudget)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe((x) => {
                this.saving = false;
                this.notify.info(this.l('SavedSuccessfully'));
                this.transferBudget = new CreateOrEditTransferBudgetDto();
            });
    }
}
