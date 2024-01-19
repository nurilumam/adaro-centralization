import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { MaterialRequestsServiceProxy, CreateOrEditMaterialRequestDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';
import { MaterialRequestUNSPSCLookupTableModalComponent } from './materialRequest-unspsc-lookup-table-modal.component';
import { MaterialRequestGeneralLedgerMappingLookupTableModalComponent } from './materialRequest-generalLedgerMapping-lookup-table-modal.component';
import { ActivatedRoute, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Observable } from "@node_modules/rxjs";
import { BreadcrumbItem } from '@app/shared/common/sub-header/sub-header.component';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { FileUploader, FileUploaderOptions } from "@node_modules/ng2-file-upload";
import { IAjaxResponse, TokenService } from "@node_modules/abp-ng2-module";
import { AppConsts } from "@shared/AppConsts";

import { HttpClient } from '@angular/common/http';

@Component({
    templateUrl: './create-or-edit-materialRequest.component.html',
    animations: [appModuleAnimation()]
})
export class CreateOrEditMaterialRequestComponent extends AppComponentBase implements OnInit {
    active = false;
    saving = false;
    @ViewChild('materialRequestUNSPSCLookupTableModal', { static: true }) materialRequestUNSPSCLookupTableModal: MaterialRequestUNSPSCLookupTableModalComponent;
    @ViewChild('materialRequestGeneralLedgerMappingLookupTableModal', { static: true }) materialRequestGeneralLedgerMappingLookupTableModal: MaterialRequestGeneralLedgerMappingLookupTableModalComponent;

    materialRequest: CreateOrEditMaterialRequestDto = new CreateOrEditMaterialRequestDto();

    unspscDisplayProperty = '';
    generalLedgerMappingDisplayProperty = '';


    breadcrumbs: BreadcrumbItem[] = [
        new BreadcrumbItem(this.l("MaterialRequest"), "/app/main/masterDataRequest/materialRequests"),
        new BreadcrumbItem(this.l('Entity_Name_Plural_Here') + '' + this.l('Details')),
    ];

    pictureFileUploader: FileUploader;
    pictureFileToken: string;
    pictureFileName: string;
    pictureFileAcceptedTypes: string = '';
    @ViewChild('MaterialRequest_pictureLabel') materialRequest_pictureLabel: ElementRef;



    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
        private _materialRequestsServiceProxy: MaterialRequestsServiceProxy,
        private _router: Router,
        private _dateTimeService: DateTimeService, private _tokenService: TokenService
        , private _http: HttpClient
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.show(this._activatedRoute.snapshot.queryParams['id']);


        this._http.get(AppConsts.remoteServiceBaseUrl + '/materialRequests/GetPictureFileAllowedTypes')
            .subscribe((data: any) => {
                if (!data || !data.result) {
                    return;
                }

                let list = data.result as string[];
                if (list.length == 0) {
                    return;
                }

                for (let i = 0; i < list.length; i++) {
                    this.pictureFileAcceptedTypes += '.' + list[i] + ',';
                }
            });

    }

    show(materialRequestId?: string): void {

        if (!materialRequestId) {
            this.materialRequest = new CreateOrEditMaterialRequestDto();
            this.materialRequest.id = materialRequestId;
            this.unspscDisplayProperty = '';
            this.generalLedgerMappingDisplayProperty = '';


            this.pictureFileName = null;


            this.active = true;
        } else {
            this._materialRequestsServiceProxy.getMaterialRequestForEdit(materialRequestId).subscribe(result => {
                this.materialRequest = result.materialRequest;

                this.unspscDisplayProperty = result.unspscDisplayProperty;
                this.generalLedgerMappingDisplayProperty = result.generalLedgerMappingDisplayProperty;


                this.pictureFileName = result.pictureFileName;


                this.active = true;
            });
        }


        this.pictureFileUploader = this.initializeUploader(AppConsts.remoteServiceBaseUrl + '/MaterialRequests/UploadpictureFile',
            fileToken => this.pictureFileToken = fileToken);

    }

    save(): void {
        this.saving = true;

        this._materialRequestsServiceProxy.createOrEdit(this.materialRequest)
            .pipe(finalize(() => {
                this.saving = false;
            }))
            .subscribe(x => {
                this.saving = false;
                this.notify.info(this.l('SavedSuccessfully'));
                this._router.navigate(['/app/main/masterDataRequest/materialRequests']);
            })
    }

    saveAndNew(): void {
        this.saving = true;

        this._materialRequestsServiceProxy.createOrEdit(this.materialRequest)
            .pipe(finalize(() => {
                this.saving = false;
            }))
            .subscribe(x => {
                this.saving = false;
                this.notify.info(this.l('SavedSuccessfully'));
                this.materialRequest = new CreateOrEditMaterialRequestDto();
            });
    }

    openSelectUNSPSCModal() {
        this.materialRequestUNSPSCLookupTableModal.id = this.materialRequest.unspscId;
        this.materialRequestUNSPSCLookupTableModal.displayName = this.unspscDisplayProperty;
        this.materialRequestUNSPSCLookupTableModal.show();
    }
    openSelectGeneralLedgerMappingModal() {
        this.materialRequestGeneralLedgerMappingLookupTableModal.id = this.materialRequest.valuationClassId;
        this.materialRequestGeneralLedgerMappingLookupTableModal.displayName = this.generalLedgerMappingDisplayProperty;
        this.materialRequestGeneralLedgerMappingLookupTableModal.show();
    }


    setUNSPSCIdNull() {
        this.materialRequest.unspscId = null;
        this.unspscDisplayProperty = '';
    }
    setValuationClassIdNull() {
        this.materialRequest.valuationClassId = null;
        this.generalLedgerMappingDisplayProperty = '';
    }


    getNewUNSPSCId() {
        this.materialRequest.unspscId = this.materialRequestUNSPSCLookupTableModal.id;
        this.unspscDisplayProperty = this.materialRequestUNSPSCLookupTableModal.displayName;
    }
    getNewValuationClassId() {
        this.materialRequest.valuationClassId = this.materialRequestGeneralLedgerMappingLookupTableModal.id;
        this.generalLedgerMappingDisplayProperty = this.materialRequestGeneralLedgerMappingLookupTableModal.displayName;
    }




    onSelectPictureFile(fileInput: any): void {
        let selectedFile = <File>fileInput.target.files[0];

        this.pictureFileUploader.clearQueue();
        this.pictureFileUploader.addToQueue([selectedFile]);
        this.pictureFileUploader.uploadAll();
    }

    removePictureFile(): void {
        this.message.confirm(
            this.l('DoYouWantToRemoveTheFile'),
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._materialRequestsServiceProxy.removePictureFile(this.materialRequest.id).subscribe(() => {
                        abp.notify.success(this.l('SuccessfullyDeleted'));
                        this.pictureFileName = null;
                    });
                }
            }
        );
    }



    initializeUploader(url: string, onSuccess: (fileToken: string) => void): FileUploader {
        let uploader = new FileUploader({ url: url });

        let _uploaderOptions: FileUploaderOptions = {};
        _uploaderOptions.autoUpload = false;
        _uploaderOptions.authToken = 'Bearer ' + this._tokenService.getToken();
        _uploaderOptions.removeAfterUpload = true;

        uploader.onAfterAddingFile = (file) => {
            file.withCredentials = false;
        };

        uploader.onSuccessItem = (item, response, status) => {
            const resp = <IAjaxResponse>JSON.parse(response);
            if (resp.success && resp.result.fileToken) {
                onSuccess(resp.result.fileToken);
            } else {
                this.message.error(resp.result.message);
            }
        };

        uploader.setOptions(_uploaderOptions);
        return uploader;
    }



    getDownloadUrl(id: string): string {
        return AppConsts.remoteServiceBaseUrl + '/File/DownloadBinaryFile?id=' + id;
    }


}
