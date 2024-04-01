import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ZMM020RComponent } from './zmM020R.component';

const routes: Routes = [
    {
        path: '',
        component: ZMM020RComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ZMM020RRoutingModule {}
