import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { ZMM021RRoutingModule } from './zmM021R-routing.module';
import { ZMM021RComponent } from './zmM021R.component';
import { CreateOrEditZMM021RModalComponent } from './create-or-edit-zmM021R-modal.component';
import { ViewZMM021RModalComponent } from './view-zmM021R-modal.component';

@NgModule({
    declarations: [ZMM021RComponent, CreateOrEditZMM021RModalComponent, ViewZMM021RModalComponent],
    imports: [AppSharedModule, ZMM021RRoutingModule, AdminSharedModule],
})
export class ZMM021RModule {}
