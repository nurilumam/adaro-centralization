import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { LookupPageRoutingModule } from './lookupPage-routing.module';
import { LookupPagesComponent } from './lookupPages.component';
import { CreateOrEditLookupPageComponent } from './create-or-edit-lookupPage.component';
import { ViewLookupPageComponent } from './view-lookupPage.component';
import { LookupPageCostCenterLookupTableModalComponent } from './lookupPage-costCenter-lookup-table-modal.component';

@NgModule({
    declarations: [
        LookupPagesComponent,
        CreateOrEditLookupPageComponent,
        ViewLookupPageComponent,

        LookupPageCostCenterLookupTableModalComponent,
    ],
    imports: [AppSharedModule, LookupPageRoutingModule, AdminSharedModule],
})
export class LookupPageModule {}
