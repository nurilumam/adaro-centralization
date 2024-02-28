import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { TransferBudgetItemRoutingModule } from './transferBudgetItem-routing.module';
import { TransferBudgetItemsComponent } from './transferBudgetItems.component';
import { CreateOrEditTransferBudgetItemModalComponent } from './create-or-edit-transferBudgetItem-modal.component';
import { ViewTransferBudgetItemModalComponent } from './view-transferBudgetItem-modal.component';
import { TransferBudgetItemTransferBudgetLookupTableModalComponent } from './transferBudgetItem-transferBudget-lookup-table-modal.component';
import { TransferBudgetItemCostCenterLookupTableModalComponent } from './transferBudgetItem-costCenter-lookup-table-modal.component';

@NgModule({
    declarations: [
        TransferBudgetItemsComponent,
        CreateOrEditTransferBudgetItemModalComponent,
        ViewTransferBudgetItemModalComponent,

        TransferBudgetItemTransferBudgetLookupTableModalComponent,
        TransferBudgetItemCostCenterLookupTableModalComponent,
    ],
    imports: [AppSharedModule, TransferBudgetItemRoutingModule, AdminSharedModule],
})
export class TransferBudgetItemModule {}
