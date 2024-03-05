import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ZMM021RComponent } from './zmM021R.component';

const routes: Routes = [
    {
        path: '',
        component: ZMM021RComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ZMM021RRoutingModule {}
