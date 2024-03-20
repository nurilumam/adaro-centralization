import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RptProcurementAdjustsComponent } from './rptProcurementAdjusts.component';

const routes: Routes = [
    {
        path: '',
        component: RptProcurementAdjustsComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class RptProcurementAdjustRoutingModule {}
