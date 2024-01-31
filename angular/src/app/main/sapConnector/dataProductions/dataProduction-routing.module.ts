import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DataProductionsComponent } from './dataProductions.component';

const routes: Routes = [
    {
        path: '',
        component: DataProductionsComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class DataProductionRoutingModule {}
