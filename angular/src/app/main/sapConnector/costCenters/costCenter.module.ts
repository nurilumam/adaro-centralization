import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { CostCenterRoutingModule } from './costCenter-routing.module';
import { CostCentersComponent } from './costCenters.component';
import { CreateOrEditCostCenterModalComponent } from './create-or-edit-costCenter-modal.component';
import { ViewCostCenterModalComponent } from './view-costCenter-modal.component';

@NgModule({
    declarations: [CostCentersComponent, CreateOrEditCostCenterModalComponent, ViewCostCenterModalComponent],
    imports: [AppSharedModule, CostCenterRoutingModule, AdminSharedModule],
})
export class CostCenterModule {}
