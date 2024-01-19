import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { MaterialsServiceProxy, CreateOrEditMaterialDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';
import { MaterialMaterialGroupLookupTableModalComponent } from './material-materialGroup-lookup-table-modal.component';
import { MaterialUNSPSCLookupTableModalComponent } from './material-unspsc-lookup-table-modal.component';
import { MaterialGeneralLedgerMappingLookupTableModalComponent } from './material-generalLedgerMapping-lookup-table-modal.component';
import { ActivatedRoute, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Observable } from '@node_modules/rxjs';
import { BreadcrumbItem } from '@app/shared/common/sub-header/sub-header.component';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { FileUploader, FileUploaderOptions } from '@node_modules/ng2-file-upload';
import { IAjaxResponse, TokenService } from '@node_modules/abp-ng2-module';
import { AppConsts } from '@shared/AppConsts';

import { HttpClient } from '@angular/common/http';

@Component({
    templateUrl: './create-or-edit-material.component.html',
    animations: [appModuleAnimation()],
})
export class CreateOrEditMaterialComponent extends AppComponentBase implements OnInit {
    active = false;
    saving = false;
    @ViewChild('materialMaterialGroupLookupTableModal', { static: true })
    materialMaterialGroupLookupTableModal: MaterialMaterialGroupLookupTableModalComponent;
    @ViewChild('materialUNSPSCLookupTableModal', { static: true })
    materialUNSPSCLookupTableModal: MaterialUNSPSCLookupTableModalComponent;
    @ViewChild('materialGeneralLedgerMappingLookupTableModal', { static: true })
    materialGeneralLedgerMappingLookupTableModal: MaterialGeneralLedgerMappingLookupTableModalComponent;

    material: CreateOrEditMaterialDto = new CreateOrEditMaterialDto();

    materialGroupDisplayProperty = '';
    unspscDisplayProperty = '';
    generalLedgerMappingDisplayProperty = '';

    breadcrumbs: BreadcrumbItem[] = [
        new BreadcrumbItem(this.l('Material'), '/app/main/masterData/materials'),
        new BreadcrumbItem(this.l('Entity_Name_Plural_Here') + '' + this.l('Details')),
    ];

    imageMainFileUploader: FileUploader;
    imageMainFileToken: string;
    imageMainFileName: string;
    imageMainFileAcceptedTypes: string = '';
    @ViewChild('Material_imageMainLabel') material_imageMainLabel: ElementRef;

    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
        private _materialsServiceProxy: MaterialsServiceProxy,
        private _router: Router,
        private _dateTimeService: DateTimeService,
        private _tokenService: TokenService,
        private _http: HttpClient
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.show(this._activatedRoute.snapshot.queryParams['id']);

        this._http
            .get(AppConsts.remoteServiceBaseUrl + '/materials/GetImageMainFileAllowedTypes')
            .subscribe((data: any) => {
                if (!data || !data.result) {
                    return;
                }

                let list = data.result as string[];
                if (list.length == 0) {
                    return;
                }

                for (let i = 0; i < list.length; i++) {
                    this.imageMainFileAcceptedTypes += '.' + list[i] + ',';
                }
            });
    }

    show(materialId?: string): void {
        if (!materialId) {
            this.material = new CreateOrEditMaterialDto();
            this.material.id = materialId;
            this.materialGroupDisplayProperty = '';
            this.unspscDisplayProperty = '';
            this.generalLedgerMappingDisplayProperty = '';

            this.imageMainFileName = null;

            this.active = true;
        } else {
            this._materialsServiceProxy.getMaterialForEdit(materialId).subscribe((result) => {
                this.material = result.material;

                this.materialGroupDisplayProperty = result.materialGroupDisplayProperty;
                this.unspscDisplayProperty = result.unspscDisplayProperty;
                this.generalLedgerMappingDisplayProperty = result.generalLedgerMappingDisplayProperty;

                this.imageMainFileName = result.imageMainFileName;

                this.active = true;
            });
        }

        this.imageMainFileUploader = this.initializeUploader(
            AppConsts.remoteServiceBaseUrl + '/Materials/UploadimageMainFile',
            (fileToken) => (this.imageMainFileToken = fileToken)
        );
    }

    save(): void {
        this.saving = true;

        this._materialsServiceProxy
            .createOrEdit(this.material)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe((x) => {
                this.saving = false;
                this.notify.info(this.l('SavedSuccessfully'));
                this._router.navigate(['/app/main/masterData/materials']);
            });
    }

    saveAndNew(): void {
        this.saving = true;

        this._materialsServiceProxy
            .createOrEdit(this.material)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe((x) => {
                this.saving = false;
                this.notify.info(this.l('SavedSuccessfully'));
                this.material = new CreateOrEditMaterialDto();
            });
    }

    openSelectMaterialGroupModal() {
        this.materialMaterialGroupLookupTableModal.id = this.material.materialGroupId;
        this.materialMaterialGroupLookupTableModal.displayName = this.materialGroupDisplayProperty;
        this.materialMaterialGroupLookupTableModal.show();
    }
    openSelectUNSPSCModal() {
        this.materialUNSPSCLookupTableModal.id = this.material.unspscId;
        this.materialUNSPSCLookupTableModal.displayName = this.unspscDisplayProperty;
        this.materialUNSPSCLookupTableModal.show();
    }
    openSelectGeneralLedgerMappingModal() {
        this.materialGeneralLedgerMappingLookupTableModal.id = this.material.generalLedgerMappingId;
        this.materialGeneralLedgerMappingLookupTableModal.displayName = this.generalLedgerMappingDisplayProperty;
        this.materialGeneralLedgerMappingLookupTableModal.show();
    }

    setMaterialGroupIdNull() {
        this.material.materialGroupId = null;
        this.materialGroupDisplayProperty = '';
    }
    setUNSPSCIdNull() {
        this.material.unspscId = null;
        this.unspscDisplayProperty = '';
    }
    setGeneralLedgerMappingIdNull() {
        this.material.generalLedgerMappingId = null;
        this.generalLedgerMappingDisplayProperty = '';
    }

    getNewMaterialGroupId() {
        this.material.materialGroupId = this.materialMaterialGroupLookupTableModal.id;
        this.materialGroupDisplayProperty = this.materialMaterialGroupLookupTableModal.displayName;
    }
    getNewUNSPSCId() {
        this.material.unspscId = this.materialUNSPSCLookupTableModal.id;
        this.unspscDisplayProperty = this.materialUNSPSCLookupTableModal.displayName;
    }
    getNewGeneralLedgerMappingId() {
        this.material.generalLedgerMappingId = this.materialGeneralLedgerMappingLookupTableModal.id;
        this.generalLedgerMappingDisplayProperty = this.materialGeneralLedgerMappingLookupTableModal.displayName;
    }

    onSelectImageMainFile(fileInput: any): void {
        let selectedFile = <File>fileInput.target.files[0];

        this.imageMainFileUploader.clearQueue();
        this.imageMainFileUploader.addToQueue([selectedFile]);
        this.imageMainFileUploader.uploadAll();
    }

    removeImageMainFile(): void {
        this.message.confirm(this.l('DoYouWantToRemoveTheFile'), this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._materialsServiceProxy.removeImageMainFile(this.material.id).subscribe(() => {
                    abp.notify.success(this.l('SuccessfullyDeleted'));
                    this.imageMainFileName = null;
                });
            }
        });
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
