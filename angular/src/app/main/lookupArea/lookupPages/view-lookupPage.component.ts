import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import {
    LookupPagesServiceProxy,
    GetLookupPageForViewDto,
    LookupPageDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { BreadcrumbItem } from '@app/shared/common/sub-header/sub-header.component';
@Component({
    templateUrl: './view-lookupPage.component.html',
    animations: [appModuleAnimation()],
})
export class ViewLookupPageComponent extends AppComponentBase implements OnInit {
    active = false;
    saving = false;

    item: GetLookupPageForViewDto;

    breadcrumbs: BreadcrumbItem[] = [
        new BreadcrumbItem(this.l('LookupPage'), '/app/main/lookupArea/lookupPages'),
        new BreadcrumbItem(this.l('LookupPages') + '' + this.l('Details')),
    ];
    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
        private _lookupPagesServiceProxy: LookupPagesServiceProxy
    ) {
        super(injector);
        this.item = new GetLookupPageForViewDto();
        this.item.lookupPage = new LookupPageDto();
    }

    ngOnInit(): void {
        this.show(this._activatedRoute.snapshot.queryParams['id']);
    }

    show(lookupPageId: string): void {
        this._lookupPagesServiceProxy.getLookupPageForView(lookupPageId).subscribe((result) => {
            this.item = result;
            this.active = true;
        });
    }
}
