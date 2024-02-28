import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TransferBudgetItemsComponent } from './transferBudgetItems.component';

const routes: Routes = [
    {
        path: '',
        component: TransferBudgetItemsComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class TransferBudgetItemRoutingModule {}
