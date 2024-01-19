import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {GeneralLedgerMappingsComponent} from './generalLedgerMappings.component';



const routes: Routes = [
    {
        path: '',
        component: GeneralLedgerMappingsComponent,
        pathMatch: 'full'
    },
    
    
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class GeneralLedgerMappingRoutingModule {
}
