import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { MaterialRoutingModule } from './material-routing.module';
import { MaterialsComponent } from './materials.component';
import { CreateOrEditMaterialComponent } from './create-or-edit-material.component';
import { ViewMaterialComponent } from './view-material.component';
import { MaterialMaterialGroupLookupTableModalComponent } from './material-materialGroup-lookup-table-modal.component';
import { MaterialUNSPSCLookupTableModalComponent } from './material-unspsc-lookup-table-modal.component';
import { MaterialGeneralLedgerMappingLookupTableModalComponent } from './material-generalLedgerMapping-lookup-table-modal.component';

@NgModule({
    declarations: [
        MaterialsComponent,
        CreateOrEditMaterialComponent,
        ViewMaterialComponent,

        MaterialMaterialGroupLookupTableModalComponent,
        MaterialUNSPSCLookupTableModalComponent,
        MaterialGeneralLedgerMappingLookupTableModalComponent,
    ],
    imports: [AppSharedModule, MaterialRoutingModule, AdminSharedModule],
})
export class MaterialModule {}
