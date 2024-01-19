import {NgModule} from '@angular/core';
import {AppSharedModule} from '@app/shared/app-shared.module';
import {AdminSharedModule} from '@app/admin/shared/admin-shared.module';
import {MaterialRequestRoutingModule} from './materialRequest-routing.module';
import {MaterialRequestsComponent} from './materialRequests.component';
import {CreateOrEditMaterialRequestComponent} from './create-or-edit-materialRequest.component';
import {ViewMaterialRequestComponent} from './view-materialRequest.component';
import {MaterialRequestUNSPSCLookupTableModalComponent} from './materialRequest-unspsc-lookup-table-modal.component';
    					import {MaterialRequestGeneralLedgerMappingLookupTableModalComponent} from './materialRequest-generalLedgerMapping-lookup-table-modal.component';
    					


@NgModule({
    declarations: [
        MaterialRequestsComponent,
        CreateOrEditMaterialRequestComponent,
        ViewMaterialRequestComponent,
        
    					MaterialRequestUNSPSCLookupTableModalComponent,
    					MaterialRequestGeneralLedgerMappingLookupTableModalComponent,
    ],
    imports: [AppSharedModule, MaterialRequestRoutingModule , AdminSharedModule ],
    
})
export class MaterialRequestModule {
}
