import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MaterialsServiceProxy, MaterialDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';

import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './materials.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class MaterialsComponent extends AppComponentBase {
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    materialNoFilter = '';
    materialNameFilter = '';
    materialNameSAPFilter = '';
    descriptionFilter = '';
    sizeFilter = '';
    uoMFilter = '';
    brandFilter = '';
    imageMainFilter = '';
    materialGroupDisplayPropertyFilter = '';
    unspscDisplayPropertyFilter = '';
    generalLedgerMappingDisplayPropertyFilter = '';

    constructor(
        injector: Injector,
        private _materialsServiceProxy: MaterialsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _router: Router,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getMaterials(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records && this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._materialsServiceProxy
            .getAll(
                this.filterText,
                this.materialNoFilter,
                this.materialNameFilter,
                this.materialNameSAPFilter,
                this.descriptionFilter,
                this.sizeFilter,
                this.uoMFilter,
                this.brandFilter,
                this.materialGroupDisplayPropertyFilter,
                this.unspscDisplayPropertyFilter,
                this.generalLedgerMappingDisplayPropertyFilter,
                this.primengTableHelper.getSorting(this.dataTable),
                this.primengTableHelper.getSkipCount(this.paginator, event),
                this.primengTableHelper.getMaxResultCount(this.paginator, event)
            )
            .subscribe((result) => {
                this.primengTableHelper.totalRecordsCount = result.totalCount;
                this.primengTableHelper.records = result.items;
                this.primengTableHelper.hideLoadingIndicator();
            });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createMaterial(): void {
        this._router.navigate(['/app/main/masterData/materials/createOrEdit']);
    }

    deleteMaterial(material: MaterialDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._materialsServiceProxy.delete(material.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._materialsServiceProxy
            .getMaterialsToExcel(
                this.filterText,
                this.materialNoFilter,
                this.materialNameFilter,
                this.materialNameSAPFilter,
                this.descriptionFilter,
                this.sizeFilter,
                this.uoMFilter,
                this.brandFilter,
                this.materialGroupDisplayPropertyFilter,
                this.unspscDisplayPropertyFilter,
                this.generalLedgerMappingDisplayPropertyFilter
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }

    getDownloadUrl(id: string): string {
        return AppConsts.remoteServiceBaseUrl + '/File/DownloadBinaryFile?id=' + id;
    }

    resetFilters(): void {
        this.filterText = '';
        this.materialNoFilter = '';
        this.materialNameFilter = '';
        this.materialNameSAPFilter = '';
        this.descriptionFilter = '';
        this.sizeFilter = '';
        this.uoMFilter = '';
        this.brandFilter = '';
        this.imageMainFilter = '';
        this.materialGroupDisplayPropertyFilter = '';
        this.unspscDisplayPropertyFilter = '';
        this.generalLedgerMappingDisplayPropertyFilter = '';

        this.getMaterials();
    }
}
