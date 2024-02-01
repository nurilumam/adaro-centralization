import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { JobSynchronizesComponent } from './jobSynchronizes.component';

const routes: Routes = [
    {
        path: '',
        component: JobSynchronizesComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class JobSynchronizeRoutingModule {}
