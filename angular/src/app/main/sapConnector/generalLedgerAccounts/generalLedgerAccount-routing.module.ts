import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GeneralLedgerAccountsComponent } from './generalLedgerAccounts.component';

const routes: Routes = [
    {
        path: '',
        component: GeneralLedgerAccountsComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class GeneralLedgerAccountRoutingModule {}
