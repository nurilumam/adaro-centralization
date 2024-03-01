import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { LookupPagesServiceProxy, CreateOrEditLookupPageDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';
import { LookupPageCostCenterLookupTableModalComponent } from './lookupPage-costCenter-lookup-table-modal.component';
import { ActivatedRoute, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Observable } from '@node_modules/rxjs';
import { BreadcrumbItem } from '@app/shared/common/sub-header/sub-header.component';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './create-or-edit-lookupPage.component.html',
    animations: [appModuleAnimation()],
})
export class CreateOrEditLookupPageComponent extends AppComponentBase implements OnInit {
    active = false;
    saving = false;
    @ViewChild('lookupPageCostCenterLookupTableModal', { static: true })
    lookupPageCostCenterLookupTableModal: LookupPageCostCenterLookupTableModalComponent;

    lookupPage: CreateOrEditLookupPageDto = new CreateOrEditLookupPageDto();

    costCenterDisplayProperty = '';

    breadcrumbs: BreadcrumbItem[] = [
        new BreadcrumbItem(this.l('LookupPage'), '/app/main/lookupArea/lookupPages'),
        new BreadcrumbItem(this.l('Entity_Name_Plural_Here') + '' + this.l('Details')),
    ];

    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
        private _lookupPagesServiceProxy: LookupPagesServiceProxy,
        private _router: Router,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.show(this._activatedRoute.snapshot.queryParams['id']);
    }

    show(lookupPageId?: string): void {
        if (!lookupPageId) {
            this.lookupPage = new CreateOrEditLookupPageDto();
            this.lookupPage.id = lookupPageId;
            this.costCenterDisplayProperty = '';

            this.active = true;
        } else {
            this._lookupPagesServiceProxy.getLookupPageForEdit(lookupPageId).subscribe((result) => {
                this.lookupPage = result.lookupPage;

                this.costCenterDisplayProperty = result.costCenterDisplayProperty;

                this.active = true;
            });
        }
    }

    save(): void {
        this.saving = true;

        this._lookupPagesServiceProxy
            .createOrEdit(this.lookupPage)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe((x) => {
                this.saving = false;
                this.notify.info(this.l('SavedSuccessfully'));
                this._router.navigate(['/app/main/lookupArea/lookupPages']);
            });
    }

    saveAndNew(): void {
        this.saving = true;

        this._lookupPagesServiceProxy
            .createOrEdit(this.lookupPage)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe((x) => {
                this.saving = false;
                this.notify.info(this.l('SavedSuccessfully'));
                this.lookupPage = new CreateOrEditLookupPageDto();
            });
    }

    openSelectCostCenterModal() {
        console.log(this.lookupPageCostCenterLookupTableModal);

        this.lookupPageCostCenterLookupTableModal.id = this.lookupPage.costCenterId;
        this.lookupPageCostCenterLookupTableModal.displayName = this.costCenterDisplayProperty;
        this.lookupPageCostCenterLookupTableModal.show();
    }

    setCostCenterIdNull() {
        this.lookupPage.costCenterId = null;
        this.costCenterDisplayProperty = '';
    }

    getNewCostCenterId() {
        console.log(this.lookupPageCostCenterLookupTableModal);
        this.lookupPage.costCenterId = this.lookupPageCostCenterLookupTableModal.id;
        this.costCenterDisplayProperty = this.lookupPageCostCenterLookupTableModal.displayName;
    }
}
