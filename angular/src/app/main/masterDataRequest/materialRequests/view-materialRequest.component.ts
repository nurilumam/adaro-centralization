﻿import {AppConsts} from "@shared/AppConsts";
import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { MaterialRequestsServiceProxy, GetMaterialRequestForViewDto, MaterialRequestDto , MaterialRequestStatus} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { BreadcrumbItem } from '@app/shared/common/sub-header/sub-header.component';
@Component({
    templateUrl: './view-materialRequest.component.html',
    animations: [appModuleAnimation()]
})
export class ViewMaterialRequestComponent extends AppComponentBase implements OnInit {

    active = false;
    saving = false;

    item: GetMaterialRequestForViewDto;
    materialRequestStatus = MaterialRequestStatus;

breadcrumbs: BreadcrumbItem[]= [
                        new BreadcrumbItem(this.l("MaterialRequest"),"/app/main/masterDataRequest/materialRequests"),
                        new BreadcrumbItem(this.l('MaterialRequests') + '' + this.l('Details')),
                    ];
    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
         private _materialRequestsServiceProxy: MaterialRequestsServiceProxy
    ) {
        super(injector);
        this.item = new GetMaterialRequestForViewDto();
        this.item.materialRequest = new MaterialRequestDto();        
    }

    ngOnInit(): void {
        this.show(this._activatedRoute.snapshot.queryParams['id']);
    }

    show(materialRequestId: string): void {
      this._materialRequestsServiceProxy.getMaterialRequestForView(materialRequestId).subscribe(result => {      
                 this.item = result;
                this.active = true;
            });       
    }
    
    
                 getDownloadUrl(id: string): string {
                     return AppConsts.remoteServiceBaseUrl + '/File/DownloadBinaryFile?id=' + id;
                 }
             
}
