import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { EkkoRoutingModule } from './ekko-routing.module';
import { EkkosComponent } from './ekkos.component';
import { CreateOrEditEkkoModalComponent } from './create-or-edit-ekko-modal.component';
import { ViewEkkoModalComponent } from './view-ekko-modal.component';

@NgModule({
    declarations: [EkkosComponent, CreateOrEditEkkoModalComponent, ViewEkkoModalComponent],
    imports: [AppSharedModule, EkkoRoutingModule, AdminSharedModule],
})
export class EkkoModule {}
