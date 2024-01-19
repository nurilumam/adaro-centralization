import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {MaterialRequestsComponent} from './materialRequests.component';
import {CreateOrEditMaterialRequestComponent} from './create-or-edit-materialRequest.component';
import {ViewMaterialRequestComponent} from './view-materialRequest.component';

const routes: Routes = [
    {
        path: '',
        component: MaterialRequestsComponent,
        pathMatch: 'full'
    },
    
			    {
                    path: 'createOrEdit',
                    component: CreateOrEditMaterialRequestComponent,
                    pathMatch: 'full'
                },
			
    
			    {
                    path: 'view',
                    component: ViewMaterialRequestComponent,
                    pathMatch: 'full'
                }
			
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MaterialRequestRoutingModule {
}
