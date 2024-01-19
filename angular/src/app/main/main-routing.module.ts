import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    
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
