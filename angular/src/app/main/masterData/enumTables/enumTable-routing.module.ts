import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {EnumTablesComponent} from './enumTables.component';



const routes: Routes = [
    {
        path: '',
        component: EnumTablesComponent,
        pathMatch: 'full'
    },
    
    
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class EnumTableRoutingModule {
}
