import {NgModule} from '@angular/core';
import {AppSharedModule} from '@app/shared/app-shared.module';
import {AdminSharedModule} from '@app/admin/shared/admin-shared.module';
import {GeneralLedgerMappingRoutingModule} from './generalLedgerMapping-routing.module';
import {GeneralLedgerMappingsComponent} from './generalLedgerMappings.component';
import {CreateOrEditGeneralLedgerMappingModalComponent} from './create-or-edit-generalLedgerMapping-modal.component';
import {ViewGeneralLedgerMappingModalComponent} from './view-generalLedgerMapping-modal.component';



@NgModule({
    declarations: [
        GeneralLedgerMappingsComponent,
        CreateOrEditGeneralLedgerMappingModalComponent,
        ViewGeneralLedgerMappingModalComponent,
        
    ],
    imports: [AppSharedModule, GeneralLedgerMappingRoutingModule , AdminSharedModule ],
    
})
export class GeneralLedgerMappingModule {
}
