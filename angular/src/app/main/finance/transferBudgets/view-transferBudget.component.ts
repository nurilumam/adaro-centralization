import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import {
    TransferBudgetsServiceProxy,
    GetTransferBudgetForViewDto,
    TransferBudgetDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { BreadcrumbItem } from '@app/shared/common/sub-header/sub-header.component';
@Component({
    templateUrl: './view-transferBudget.component.html',
    animations: [appModuleAnimation()],
})
export class ViewTransferBudgetComponent extends AppComponentBase implements OnInit {
    active = false;
    saving = false;

    item: GetTransferBudgetForViewDto;

    breadcrumbs: BreadcrumbItem[] = [
        new BreadcrumbItem(this.l('TransferBudget'), '/app/main/finance/transferBudgets'),
        new BreadcrumbItem(this.l('TransferBudgets') + '' + this.l('Details')),
    ];
    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
        private _transferBudgetsServiceProxy: TransferBudgetsServiceProxy
    ) {
        super(injector);
        this.item = new GetTransferBudgetForViewDto();
        this.item.transferBudget = new TransferBudgetDto();
    }

    ngOnInit(): void {
        this.show(this._activatedRoute.snapshot.queryParams['id']);
    }

    show(transferBudgetId: string): void {
        this._transferBudgetsServiceProxy.getTransferBudgetForView(transferBudgetId).subscribe((result) => {
            this.item = result;
            this.active = true;
        });
    }
}
