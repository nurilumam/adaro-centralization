import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EKPOsComponent } from './ekpOs.component';

const routes: Routes = [
    {
        path: '',
        component: EKPOsComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class EKPORoutingModule {}
