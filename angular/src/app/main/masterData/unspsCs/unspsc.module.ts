import {NgModule} from '@angular/core';
import {AppSharedModule} from '@app/shared/app-shared.module';
import {AdminSharedModule} from '@app/admin/shared/admin-shared.module';
import {UNSPSCRoutingModule} from './unspsc-routing.module';
import {UNSPSCsComponent} from './unspsCs.component';
import {CreateOrEditUNSPSCModalComponent} from './create-or-edit-unspsc-modal.component';
import {ViewUNSPSCModalComponent} from './view-unspsc-modal.component';



@NgModule({
    declarations: [
        UNSPSCsComponent,
        CreateOrEditUNSPSCModalComponent,
        ViewUNSPSCModalComponent,
        
    ],
    imports: [AppSharedModule, UNSPSCRoutingModule , AdminSharedModule ],
    
})
export class UNSPSCModule {
}
