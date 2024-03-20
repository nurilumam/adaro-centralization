import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { RptProcurementAdjustRoutingModule } from './rptProcurementAdjust-routing.module';
import { RptProcurementAdjustsComponent } from './rptProcurementAdjusts.component';
import { CreateOrEditRptProcurementAdjustModalComponent } from './create-or-edit-rptProcurementAdjust-modal.component';
import { ViewRptProcurementAdjustModalComponent } from './view-rptProcurementAdjust-modal.component';

@NgModule({
    declarations: [
        RptProcurementAdjustsComponent,
        CreateOrEditRptProcurementAdjustModalComponent,
        ViewRptProcurementAdjustModalComponent,
    ],
    imports: [AppSharedModule, RptProcurementAdjustRoutingModule, AdminSharedModule],
})
export class RptProcurementAdjustModule {}
