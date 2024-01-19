import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MaterialsComponent } from './materials.component';
import { CreateOrEditMaterialComponent } from './create-or-edit-material.component';
import { ViewMaterialComponent } from './view-material.component';

const routes: Routes = [
    {
        path: '',
        component: MaterialsComponent,
        pathMatch: 'full',
    },

    {
        path: 'createOrEdit',
        component: CreateOrEditMaterialComponent,
        pathMatch: 'full',
    },

    {
        path: 'view',
        component: ViewMaterialComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MaterialRoutingModule {}
