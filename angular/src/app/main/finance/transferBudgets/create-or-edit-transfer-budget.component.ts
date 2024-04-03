import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { TransferBudgetsServiceProxy, CreateOrEditTransferBudgetDto, CreateOrEditTransferBudgetDetailDto, OrganizationUnitDto, OrganizationUnitServiceProxy, CostCentersServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';
import { ActivatedRoute, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Observable } from '@node_modules/rxjs';
import { BreadcrumbItem } from '@app/shared/common/sub-header/sub-header.component';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

import { LookupPageCostCenterLookupTableModalComponent } from './lookupPage-costCenter-lookup-table-modal.component';
import { LazyLoadEvent } from 'primeng/api';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { PrimengTableHelper } from 'shared/helpers/PrimengTableHelper';
import { forEach } from 'lodash-es';

@Component({
    styleUrls: ['./create-or-edit-transfer-budget.component.less'],
    templateUrl: './create-or-edit-transfer-budget.component.html',
    animations: [appModuleAnimation()],
})
export class CreateOrEditTransferBudgetComponent extends AppComponentBase implements OnInit {
    active = false;
    saving = false;
    costCenterId = '';
    costCenterDisplayProperty = '';
    costCenterCode = '';
    costCenterName = '';
    costCenterDepartmentName = '';

    costCenterCodeSender = '';
    costCenterCodeReceiver = '';


    allOrganizationUnits: OrganizationUnitDto[];
    allDivision: OrganizationUnitDto[];
    allDepartment: OrganizationUnitDto[];
    approvalList: string[];

    lookupFrom = true;

    primengTableHelperCostCenterFrom: PrimengTableHelper = new PrimengTableHelper();
    primengTableHelperCostCenterTo: PrimengTableHelper = new PrimengTableHelper();


    transferBudget: CreateOrEditTransferBudgetDto = new CreateOrEditTransferBudgetDto();

    costCenterFrom: CreateOrEditTransferBudgetDetailDto = new CreateOrEditTransferBudgetDetailDto();
    costCenterTo: CreateOrEditTransferBudgetDetailDto = new CreateOrEditTransferBudgetDetailDto();

    clonedProducts: { [s: string]: CreateOrEditTransferBudgetDetailDto; } = {};


    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    @ViewChild('lookupPageCostCenterLookupTableModal', { static: true })
    lookupPageCostCenterLookupTableModal: LookupPageCostCenterLookupTableModalComponent;

    breadcrumbs: BreadcrumbItem[] = [
        new BreadcrumbItem(this.l('TransferBudget'), '/app/main/finance/transferBudgets'),
        new BreadcrumbItem(this.l('Entity_Name_Plural_Here') + '' + this.l('Details')),
    ];



    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
        private _transferBudgetsServiceProxy: TransferBudgetsServiceProxy,
        private _router: Router,
        private _dateTimeService: DateTimeService,
        private _organizationUnitServiceProxy: OrganizationUnitServiceProxy,
        private _costCentersServiceProxy: CostCentersServiceProxy,
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.show(this._activatedRoute.snapshot.queryParams['id']);
        this.getOrganizationUnits();

        this.approvalList = [
            'Department Head (Receiver)',
            'Division Head (Receiver)',
            'Department Head (Sender)',
            'Division Head (Sender)',
            'Respectiver Director',
            'Finance Division/Director'
        ];
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

    getOrganizationUnits(): void {
        this._organizationUnitServiceProxy.getAll().subscribe((result) => {
            this.allOrganizationUnits = result;
            this.allDivision = [];
            for (let index = 0; index < result.length; index++) {
                if (result[index].parentId == null) this.allDivision.push(result[index]);
            }
        });
    }

    changeDivision(): void {
        this.allDepartment = [];
        for (let index = 0; index < this.allOrganizationUnits.length; index++) {
            if (this.allOrganizationUnits[index].parentId == parseInt(this.transferBudget.division)) this.allDepartment.push(this.allOrganizationUnits[index]);
        }
    }

    changeDepartment(): void {
        this._costCentersServiceProxy.getCostCenterFromDepartmentId(parseInt(this.transferBudget.department)).subscribe((result) => {
            const costCenterReceiver = result.costCenter;
            this.costCenterCodeReceiver = costCenterReceiver.costCenterCode.substring(0, (costCenterReceiver.costCenterCode.length - 3));
        });

        this.transferBudget.details = [];
        this.primengTableHelperCostCenterTo.showLoadingIndicator();
        this.primengTableHelperCostCenterTo.totalRecordsCount = this.transferBudget.details.length;
        this.primengTableHelperCostCenterTo.records = this.transferBudget.details;
        this.primengTableHelperCostCenterTo.hideLoadingIndicator();
    }

    save(): void {

        console.log(this.transferBudget);

        // this.saving = true;


        // this._transferBudgetsServiceProxy
        //     .createOrEdit(this.transferBudget)
        //     .pipe(
        //         finalize(() => {
        //             this.saving = false;
        //         })
        //     )
        //     .subscribe((x) => {
        //         this.saving = false;
        //         this.notify.info(this.l('SavedSuccessfully'));
        //         this._router.navigate(['/app/main/finance/transferBudgets']);
        //     });
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



    openSelectCostCenterModal() {
        this.lookupFrom = true;
        this.costCenterId = "";        

        this.lookupPageCostCenterLookupTableModal.isSender = true;
        this.lookupPageCostCenterLookupTableModal.filterText = "";

        if(this.costCenterCodeSender!=undefined&&this.costCenterCodeSender!=''){
            this.lookupPageCostCenterLookupTableModal.isSender = false;
            this.lookupPageCostCenterLookupTableModal.filterText = this.costCenterCodeSender;
        }

        this.lookupPageCostCenterLookupTableModal.id = this.costCenterId;
        this.lookupPageCostCenterLookupTableModal.displayName = this.costCenterDisplayProperty;
        this.lookupPageCostCenterLookupTableModal.show();
    }

    openSelectCostCenterToModal() {
        this.lookupFrom = false;
        this.costCenterId = "";
        this.lookupPageCostCenterLookupTableModal.filterText = this.costCenterCodeReceiver;
        this.lookupPageCostCenterLookupTableModal.isSender = false;
        this.lookupPageCostCenterLookupTableModal.id = this.costCenterId;
        this.lookupPageCostCenterLookupTableModal.displayName = this.costCenterDisplayProperty;
        this.lookupPageCostCenterLookupTableModal.show();
    }

    setCostCenterIdNull() {
        this.costCenterId = null;
        this.costCenterDisplayProperty = '';
    }

    getNewCostCenterId() {
        this.costCenterId = this.lookupPageCostCenterLookupTableModal.id;
        this.costCenterDisplayProperty = this.lookupPageCostCenterLookupTableModal.displayName;
        this.costCenterCode = this.lookupPageCostCenterLookupTableModal.costCenterCode;
        this.costCenterName = this.lookupPageCostCenterLookupTableModal.costCenterName;
        this.costCenterDepartmentName = this.lookupPageCostCenterLookupTableModal.departmentName;

        if (this.costCenterId != "") {
            if (this.lookupFrom) {
                this.getTransferBudgetSender();
            } else {
                this.getTransferBudgetReceiver();
            }
        }
    }


    getTransferBudgetSender(event?: LazyLoadEvent) {
        if (this.transferBudget.details == undefined) {
            this.transferBudget.details = [];
        }


        this.costCenterFrom.costCenterId = this.costCenterId;
        this.costCenterFrom = new CreateOrEditTransferBudgetDetailDto();
        this.costCenterFrom.id = this.costCenterId;
        this.costCenterFrom.costCenterCode = this.lookupPageCostCenterLookupTableModal.costCenterCode;
        this.costCenterFrom.costCenterName = this.lookupPageCostCenterLookupTableModal.costCenterName;
        this.costCenterFrom.departmentName = this.lookupPageCostCenterLookupTableModal.departmentName;
        this.transferBudget.details.push(this.costCenterFrom);

        this.primengTableHelperCostCenterFrom.showLoadingIndicator();
        this.primengTableHelperCostCenterFrom.totalRecordsCount = this.transferBudget.details.length;
        this.primengTableHelperCostCenterFrom.records = this.transferBudget.details;
        this.primengTableHelperCostCenterFrom.hideLoadingIndicator();

        if(this.primengTableHelperCostCenterFrom.records.length>0){
            this.costCenterCodeSender = this.primengTableHelperCostCenterFrom.records[0].costCenterCode;
            this.costCenterCodeSender = this.costCenterCodeSender.substring(0, (this.costCenterCodeSender.length - 3));
        }
    }

    getTransferBudgetReceiver(event?: LazyLoadEvent) {
        if (this.transferBudget.details == undefined) {
            this.transferBudget.details = [];
        }

        this.costCenterTo = new CreateOrEditTransferBudgetDetailDto();
        this.costCenterTo.id = this.costCenterId;

        console.log(this.lookupFrom);
        this.costCenterTo.costCenterId = this.costCenterId;

        // period!: string;
        // amount!: number;
        // transferType!: string;
        // costCenterId!: string;
        // generalLedgerMappingId!: string | undefined;
        // id!: string | undefined;

        this.costCenterTo.costCenterCode = this.lookupPageCostCenterLookupTableModal.costCenterCode;
        this.costCenterTo.costCenterName = this.lookupPageCostCenterLookupTableModal.costCenterName;
        this.costCenterTo.departmentName = this.lookupPageCostCenterLookupTableModal.departmentName;
        this.transferBudget.details.push(this.costCenterTo);

        this.primengTableHelperCostCenterTo.showLoadingIndicator();
        this.primengTableHelperCostCenterTo.totalRecordsCount = this.transferBudget.details.length;
        this.primengTableHelperCostCenterTo.records = this.transferBudget.details;
        this.primengTableHelperCostCenterTo.hideLoadingIndicator();
        
    }

    deleteBudgetItemSender(transferBudgetItem: CreateOrEditTransferBudgetDetailDto) {

        this.transferBudget.details.splice(
            this.transferBudget.details.findIndex(item => item.id === transferBudgetItem.id), 1);

        // this.transferBudget.details.push(this.costCenterFrom);

        this.primengTableHelperCostCenterFrom.showLoadingIndicator();
        this.primengTableHelperCostCenterFrom.totalRecordsCount = this.transferBudget.details.length;
        this.primengTableHelperCostCenterFrom.records = this.transferBudget.details;
        this.primengTableHelperCostCenterFrom.hideLoadingIndicator();

        if(this.primengTableHelperCostCenterFrom.records.length==0){
            this.costCenterCodeSender = '';
        }
    }

    deleteBudgetItemReceiver(transferBudgetItem: CreateOrEditTransferBudgetDetailDto) {

        this.transferBudget.details.splice(
            this.transferBudget.details.findIndex(item => item.id === transferBudgetItem.id), 1);


        this.primengTableHelperCostCenterTo.showLoadingIndicator();
        this.primengTableHelperCostCenterTo.totalRecordsCount = this.transferBudget.details.length;
        this.primengTableHelperCostCenterTo.records = this.transferBudget.details;
        this.primengTableHelperCostCenterTo.hideLoadingIndicator();
    }



    onRowEditInit(product: CreateOrEditTransferBudgetDetailDto) {
        // this.clonedProducts[product.id] = {...product};
    }

    // onRowEditSave(product: CreateOrEditTransferBudgetDetailDto) {
    //     if (product.price > 0) {
    //         delete this.clonedProducts[product.id];
    //         this.messageService.add({severity:'success', summary: 'Success', detail:'Product is updated'});
    //     }
    //     else {
    //         this.messageService.add({severity:'error', summary: 'Error', detail:'Invalid Price'});
    //     }
    // }

    // onRowEditCancel(product: CreateOrEditTransferBudgetDetailDto, index: number) {
    //     this.products2[index] = this.clonedProducts[product.id];
    //     delete this.clonedProducts[product.id];
    // }

}
