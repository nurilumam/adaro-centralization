import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {MaterialGroupsComponent} from './materialGroups.component';



const routes: Routes = [
    {
        path: '',
        component: MaterialGroupsComponent,
        pathMatch: 'full'
    },
    
    
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MaterialGroupRoutingModule {
}
