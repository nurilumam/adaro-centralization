import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LookupPagesComponent } from './lookupPages.component';
import { CreateOrEditLookupPageComponent } from './create-or-edit-lookupPage.component';
import { ViewLookupPageComponent } from './view-lookupPage.component';

const routes: Routes = [
    {
        path: '',
        component: LookupPagesComponent,
        pathMatch: 'full',
    },

    {
        path: 'createOrEdit',
        component: CreateOrEditLookupPageComponent,
        pathMatch: 'full',
    },

    {
        path: 'view',
        component: ViewLookupPageComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class LookupPageRoutingModule {}
