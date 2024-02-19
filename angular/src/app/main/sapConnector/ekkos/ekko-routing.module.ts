import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EkkosComponent } from './ekkos.component';

const routes: Routes = [
    {
        path: '',
        component: EkkosComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class EkkoRoutingModule {}
