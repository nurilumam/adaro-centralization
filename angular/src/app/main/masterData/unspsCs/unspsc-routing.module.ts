import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {UNSPSCsComponent} from './unspsCs.component';



const routes: Routes = [
    {
        path: '',
        component: UNSPSCsComponent,
        pathMatch: 'full'
    },
    
    
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class UNSPSCRoutingModule {
}
