import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { MaterialsServiceProxy, GetMaterialForViewDto, MaterialDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { BreadcrumbItem } from '@app/shared/common/sub-header/sub-header.component';
@Component({
    templateUrl: './view-material.component.html',
    animations: [appModuleAnimation()],
})
export class ViewMaterialComponent extends AppComponentBase implements OnInit {
    active = false;
    saving = false;

    item: GetMaterialForViewDto;

    breadcrumbs: BreadcrumbItem[] = [
        new BreadcrumbItem(this.l('Material'), '/app/main/masterData/materials'),
        new BreadcrumbItem(this.l('Materials') + '' + this.l('Details')),
    ];
    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
        private _materialsServiceProxy: MaterialsServiceProxy
    ) {
        super(injector);
        this.item = new GetMaterialForViewDto();
        this.item.material = new MaterialDto();
    }

    ngOnInit(): void {
        this.show(this._activatedRoute.snapshot.queryParams['id']);
    }

    show(materialId: string): void {
        this._materialsServiceProxy.getMaterialForView(materialId).subscribe((result) => {
            this.item = result;
            this.active = true;
        });
    }

    getDownloadUrl(id: string): string {
        return AppConsts.remoteServiceBaseUrl + '/File/DownloadBinaryFile?id=' + id;
    }
}
