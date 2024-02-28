import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    
                    {
                        path: 'finance/transferBudgetItems',
                        loadChildren: () => import('./finance/transferBudgetItems/transferBudgetItem.module').then(m => m.TransferBudgetItemModule),
                        data: { permission: 'Pages.TransferBudgetItems' }
                    },
                
                    
                    {
                        path: 'finance/transferBudgets',
                        loadChildren: () => import('./finance/transferBudgets/transferBudget.module').then(m => m.TransferBudgetModule),
                        data: { permission: 'Pages.TransferBudgets' }
                    },
                
                    
                    {
                        path: 'sapConnector/ekpOs',
                        loadChildren: () => import('./sapConnector/ekpOs/ekpo.module').then(m => m.EKPOModule),
                        data: { permission: 'Pages.EKPOs' }
                    },
                
                    
                    {
                        path: 'sapConnector/ekkos',
                        loadChildren: () => import('./sapConnector/ekkos/ekko.module').then(m => m.EkkoModule),
                        data: { permission: 'Pages.Ekkos' }
                    },
                
                    
                    {
                        path: 'jobScheduler/jobSynchronizes',
                        loadChildren: () => import('./jobScheduler/jobSynchronizes/jobSynchronize.module').then(m => m.JobSynchronizeModule),
                        data: { permission: 'Pages.JobSynchronizes' }
                    },
                
                    
                    {
                        path: 'sapConnector/costCenters',
                        loadChildren: () => import('./sapConnector/costCenters/costCenter.module').then(m => m.CostCenterModule),
                        data: { permission: 'Pages.CostCenters' }
                    },
                
                    
                    {
                        path: 'sapConnector/dataProductions',
                        loadChildren: () => import('./sapConnector/dataProductions/dataProduction.module').then(m => m.DataProductionModule),
                        data: { permission: 'Pages.DataProductions' }
                    },
                
                    
                    {
                        path: 'masterDataRequest/materialRequests',
                        loadChildren: () => import('./masterDataRequest/materialRequests/materialRequest.module').then(m => m.MaterialRequestModule),
                        data: { permission: 'Pages.MaterialRequests' }
                    },
                
                    
                    {
                        path: 'masterData/materials',
                        loadChildren: () => import('./masterData/materials/material.module').then(m => m.MaterialModule),
                        data: { permission: 'Pages.Materials' }
                    },
                
                    
                    {
                        path: 'masterData/unspsCs',
                        loadChildren: () => import('./masterData/unspsCs/unspsc.module').then(m => m.UNSPSCModule),
                        data: { permission: 'Pages.UNSPSCs' }
                    },
                
                    
                    {
                        path: 'masterData/materialGroups',
                        loadChildren: () => import('./masterData/materialGroups/materialGroup.module').then(m => m.MaterialGroupModule),
                        data: { permission: 'Pages.MaterialGroups' }
                    },
                
                    
                    {
                        path: 'masterData/generalLedgerMappings',
                        loadChildren: () => import('./masterData/generalLedgerMappings/generalLedgerMapping.module').then(m => m.GeneralLedgerMappingModule),
                        data: { permission: 'Pages.GeneralLedgerMappings' }
                    },
                
                    
                    {
                        path: 'masterData/enumTables',
                        loadChildren: () => import('./masterData/enumTables/enumTable.module').then(m => m.EnumTableModule),
                        data: { permission: 'Pages.EnumTables' }
                    },
                
                    {
                        path: 'dashboard',
                        loadChildren: () => import('./dashboard/dashboard.module').then((m) => m.DashboardModule),
                        data: { permission: 'Pages.Tenant.Dashboard' },
                    },
                    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
                    { path: '**', redirectTo: 'dashboard' },
                ],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class MainRoutingModule {}
