import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { DataProductionRoutingModule } from './dataProduction-routing.module';
import { DataProductionsComponent } from './dataProductions.component';
import { CreateOrEditDataProductionModalComponent } from './create-or-edit-dataProduction-modal.component';
import { ViewDataProductionModalComponent } from './view-dataProduction-modal.component';

@NgModule({
    declarations: [
        DataProductionsComponent,
        CreateOrEditDataProductionModalComponent,
        ViewDataProductionModalComponent,
    ],
    imports: [AppSharedModule, DataProductionRoutingModule, AdminSharedModule],
})
export class DataProductionModule {}
