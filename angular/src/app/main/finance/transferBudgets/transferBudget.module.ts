import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { TransferBudgetRoutingModule } from './transferBudget-routing.module';
import { TransferBudgetsComponent } from './transferBudgets.component';
import { CreateOrEditTransferBudgetComponent } from './create-or-edit-transfer-budget.component';
import { ViewTransferBudgetComponent } from './view-transferBudget.component';
import { LookupPageCostCenterLookupTableModalComponent } from './lookupPage-costCenter-lookup-table-modal.component';

@NgModule({
    declarations: [
        TransferBudgetsComponent, 
        CreateOrEditTransferBudgetComponent, 
        ViewTransferBudgetComponent, 
        LookupPageCostCenterLookupTableModalComponent
    ],
    imports: [AppSharedModule, TransferBudgetRoutingModule, AdminSharedModule],
})
export class TransferBudgetModule {}
