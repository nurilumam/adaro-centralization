import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GeneralLedgerAccountsServiceProxy, GeneralLedgerAccountDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditGeneralLedgerAccountModalComponent } from './create-or-edit-generalLedgerAccount-modal.component';

import { ViewGeneralLedgerAccountModalComponent } from './view-generalLedgerAccount-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './generalLedgerAccounts.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class GeneralLedgerAccountsComponent extends AppComponentBase {
    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('createOrEditGeneralLedgerAccountModal', { static: true })
    createOrEditGeneralLedgerAccountModal: CreateOrEditGeneralLedgerAccountModalComponent;
    @ViewChild('viewGeneralLedgerAccountModal', { static: true })
    viewGeneralLedgerAccountModal: ViewGeneralLedgerAccountModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    fundsCenterFilter = '';
    maxConsumableBudgetFilter: number;
    maxConsumableBudgetFilterEmpty: number;
    minConsumableBudgetFilter: number;
    minConsumableBudgetFilterEmpty: number;
    maxConsumedBudgetFilter: number;
    maxConsumedBudgetFilterEmpty: number;
    minConsumedBudgetFilter: number;
    minConsumedBudgetFilterEmpty: number;
    maxAvailableAmountFilter: number;
    maxAvailableAmountFilterEmpty: number;
    minAvailableAmountFilter: number;
    minAvailableAmountFilterEmpty: number;
    maxCurrentBudgetFilter: number;
    maxCurrentBudgetFilterEmpty: number;
    minCurrentBudgetFilter: number;
    minCurrentBudgetFilterEmpty: number;
    maxCommitmentActualsFilter: number;
    maxCommitmentActualsFilterEmpty: number;
    minCommitmentActualsFilter: number;
    minCommitmentActualsFilterEmpty: number;
    fundsCenterDescriptionFilter = '';
    costCenterCostCenterNameFilter = '';

    _entityTypeFullName = 'Adaro.Centralize.SAPConnector.GeneralLedgerAccount';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _generalLedgerAccountsServiceProxy: GeneralLedgerAccountsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.entityHistoryEnabled = this.setIsEntityHistoryEnabled();
    }

    private setIsEntityHistoryEnabled(): boolean {
        let customSettings = (abp as any).custom;
        return (
            this.isGrantedAny('Pages.Administration.AuditLogs') &&
            customSettings.EntityHistory &&
            customSettings.EntityHistory.isEnabled &&
            _filter(
                customSettings.EntityHistory.enabledEntities,
                (entityType) => entityType === this._entityTypeFullName
            ).length === 1
        );
    }

    getGeneralLedgerAccounts(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records && this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._generalLedgerAccountsServiceProxy
            .getAll(
                this.filterText,
                this.fundsCenterFilter,
                this.maxConsumableBudgetFilter == null
                    ? this.maxConsumableBudgetFilterEmpty
                    : this.maxConsumableBudgetFilter,
                this.minConsumableBudgetFilter == null
                    ? this.minConsumableBudgetFilterEmpty
                    : this.minConsumableBudgetFilter,
                this.maxConsumedBudgetFilter == null ? this.maxConsumedBudgetFilterEmpty : this.maxConsumedBudgetFilter,
                this.minConsumedBudgetFilter == null ? this.minConsumedBudgetFilterEmpty : this.minConsumedBudgetFilter,
                this.maxAvailableAmountFilter == null
                    ? this.maxAvailableAmountFilterEmpty
                    : this.maxAvailableAmountFilter,
                this.minAvailableAmountFilter == null
                    ? this.minAvailableAmountFilterEmpty
                    : this.minAvailableAmountFilter,
                this.maxCurrentBudgetFilter == null ? this.maxCurrentBudgetFilterEmpty : this.maxCurrentBudgetFilter,
                this.minCurrentBudgetFilter == null ? this.minCurrentBudgetFilterEmpty : this.minCurrentBudgetFilter,
                this.maxCommitmentActualsFilter == null
                    ? this.maxCommitmentActualsFilterEmpty
                    : this.maxCommitmentActualsFilter,
                this.minCommitmentActualsFilter == null
                    ? this.minCommitmentActualsFilterEmpty
                    : this.minCommitmentActualsFilter,
                this.fundsCenterDescriptionFilter,
                this.costCenterCostCenterNameFilter,
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

    createGeneralLedgerAccount(): void {
        this.createOrEditGeneralLedgerAccountModal.show();
    }

    showHistory(generalLedgerAccount: GeneralLedgerAccountDto): void {
        this.entityTypeHistoryModal.show({
            entityId: generalLedgerAccount.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: '',
        });
    }

    deleteGeneralLedgerAccount(generalLedgerAccount: GeneralLedgerAccountDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._generalLedgerAccountsServiceProxy.delete(generalLedgerAccount.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._generalLedgerAccountsServiceProxy
            .getGeneralLedgerAccountsToExcel(
                this.filterText,
                this.fundsCenterFilter,
                this.maxConsumableBudgetFilter == null
                    ? this.maxConsumableBudgetFilterEmpty
                    : this.maxConsumableBudgetFilter,
                this.minConsumableBudgetFilter == null
                    ? this.minConsumableBudgetFilterEmpty
                    : this.minConsumableBudgetFilter,
                this.maxConsumedBudgetFilter == null ? this.maxConsumedBudgetFilterEmpty : this.maxConsumedBudgetFilter,
                this.minConsumedBudgetFilter == null ? this.minConsumedBudgetFilterEmpty : this.minConsumedBudgetFilter,
                this.maxAvailableAmountFilter == null
                    ? this.maxAvailableAmountFilterEmpty
                    : this.maxAvailableAmountFilter,
                this.minAvailableAmountFilter == null
                    ? this.minAvailableAmountFilterEmpty
                    : this.minAvailableAmountFilter,
                this.maxCurrentBudgetFilter == null ? this.maxCurrentBudgetFilterEmpty : this.maxCurrentBudgetFilter,
                this.minCurrentBudgetFilter == null ? this.minCurrentBudgetFilterEmpty : this.minCurrentBudgetFilter,
                this.maxCommitmentActualsFilter == null
                    ? this.maxCommitmentActualsFilterEmpty
                    : this.maxCommitmentActualsFilter,
                this.minCommitmentActualsFilter == null
                    ? this.minCommitmentActualsFilterEmpty
                    : this.minCommitmentActualsFilter,
                this.fundsCenterDescriptionFilter,
                this.costCenterCostCenterNameFilter
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }

    resetFilters(): void {
        this.filterText = '';
        this.fundsCenterFilter = '';
        this.maxConsumableBudgetFilter = this.maxConsumableBudgetFilterEmpty;
        this.minConsumableBudgetFilter = this.maxConsumableBudgetFilterEmpty;
        this.maxConsumedBudgetFilter = this.maxConsumedBudgetFilterEmpty;
        this.minConsumedBudgetFilter = this.maxConsumedBudgetFilterEmpty;
        this.maxAvailableAmountFilter = this.maxAvailableAmountFilterEmpty;
        this.minAvailableAmountFilter = this.maxAvailableAmountFilterEmpty;
        this.maxCurrentBudgetFilter = this.maxCurrentBudgetFilterEmpty;
        this.minCurrentBudgetFilter = this.maxCurrentBudgetFilterEmpty;
        this.maxCommitmentActualsFilter = this.maxCommitmentActualsFilterEmpty;
        this.minCommitmentActualsFilter = this.maxCommitmentActualsFilterEmpty;
        this.fundsCenterDescriptionFilter = '';
        this.costCenterCostCenterNameFilter = '';

        this.getGeneralLedgerAccounts();
    }
}
