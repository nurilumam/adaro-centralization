import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { EKPORoutingModule } from './ekpo-routing.module';
import { EKPOsComponent } from './ekpOs.component';
import { CreateOrEditEKPOModalComponent } from './create-or-edit-ekpo-modal.component';
import { ViewEKPOModalComponent } from './view-ekpo-modal.component';

@NgModule({
    declarations: [EKPOsComponent, CreateOrEditEKPOModalComponent, ViewEKPOModalComponent],
    imports: [AppSharedModule, EKPORoutingModule, AdminSharedModule],
})
export class EKPOModule {}
