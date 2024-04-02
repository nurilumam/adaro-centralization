import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { GeneralLedgerAccountRoutingModule } from './generalLedgerAccount-routing.module';
import { GeneralLedgerAccountsComponent } from './generalLedgerAccounts.component';
import { CreateOrEditGeneralLedgerAccountModalComponent } from './create-or-edit-generalLedgerAccount-modal.component';
import { ViewGeneralLedgerAccountModalComponent } from './view-generalLedgerAccount-modal.component';
import { GeneralLedgerAccountCostCenterLookupTableModalComponent } from './generalLedgerAccount-costCenter-lookup-table-modal.component';

@NgModule({
    declarations: [
        GeneralLedgerAccountsComponent,
        CreateOrEditGeneralLedgerAccountModalComponent,
        ViewGeneralLedgerAccountModalComponent,

        GeneralLedgerAccountCostCenterLookupTableModalComponent,
    ],
    imports: [AppSharedModule, GeneralLedgerAccountRoutingModule, AdminSharedModule],
})
export class GeneralLedgerAccountModule {}
