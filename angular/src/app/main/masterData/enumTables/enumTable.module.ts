import {NgModule} from '@angular/core';
import {AppSharedModule} from '@app/shared/app-shared.module';
import {AdminSharedModule} from '@app/admin/shared/admin-shared.module';
import {EnumTableRoutingModule} from './enumTable-routing.module';
import {EnumTablesComponent} from './enumTables.component';
import {CreateOrEditEnumTableModalComponent} from './create-or-edit-enumTable-modal.component';
import {ViewEnumTableModalComponent} from './view-enumTable-modal.component';



@NgModule({
    declarations: [
        EnumTablesComponent,
        CreateOrEditEnumTableModalComponent,
        ViewEnumTableModalComponent,
        
    ],
    imports: [AppSharedModule, EnumTableRoutingModule , AdminSharedModule ],
    
})
export class EnumTableModule {
}
