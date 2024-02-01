import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { JobSynchronizeRoutingModule } from './jobSynchronize-routing.module';
import { JobSynchronizesComponent } from './jobSynchronizes.component';
import { CreateOrEditJobSynchronizeModalComponent } from './create-or-edit-jobSynchronize-modal.component';
import { ViewJobSynchronizeModalComponent } from './view-jobSynchronize-modal.component';

@NgModule({
    declarations: [
        JobSynchronizesComponent,
        CreateOrEditJobSynchronizeModalComponent,
        ViewJobSynchronizeModalComponent,
    ],
    imports: [AppSharedModule, JobSynchronizeRoutingModule, AdminSharedModule],
})
export class JobSynchronizeModule {}
