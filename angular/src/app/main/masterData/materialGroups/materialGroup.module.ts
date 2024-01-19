import {NgModule} from '@angular/core';
import {AppSharedModule} from '@app/shared/app-shared.module';
import {AdminSharedModule} from '@app/admin/shared/admin-shared.module';
import {MaterialGroupRoutingModule} from './materialGroup-routing.module';
import {MaterialGroupsComponent} from './materialGroups.component';
import {CreateOrEditMaterialGroupModalComponent} from './create-or-edit-materialGroup-modal.component';
import {ViewMaterialGroupModalComponent} from './view-materialGroup-modal.component';



@NgModule({
    declarations: [
        MaterialGroupsComponent,
        CreateOrEditMaterialGroupModalComponent,
        ViewMaterialGroupModalComponent,
        
    ],
    imports: [AppSharedModule, MaterialGroupRoutingModule , AdminSharedModule ],
    
})
export class MaterialGroupModule {
}
