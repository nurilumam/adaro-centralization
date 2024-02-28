import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TransferBudgetsComponent } from './transferBudgets.component';
import { CreateOrEditTransferBudgetComponent } from './create-or-edit-transfer-budget.component';
import { ViewTransferBudgetComponent } from './view-transferBudget.component';

const routes: Routes = [
    {
        path: '',
        component: TransferBudgetsComponent,
        pathMatch: 'full',
    },

    {
        path: 'createOrEdit',
        component: CreateOrEditTransferBudgetComponent,
        pathMatch: 'full',
    },

    {
        path: 'view',
        component: ViewTransferBudgetComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class TransferBudgetRoutingModule {}
