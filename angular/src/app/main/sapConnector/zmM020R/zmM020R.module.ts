import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { ZMM020RRoutingModule } from './zmM020R-routing.module';
import { ZMM020RComponent } from './zmM020R.component';
import { CreateOrEditZMM020RModalComponent } from './create-or-edit-zmM020R-modal.component';
import { ViewZMM020RModalComponent } from './view-zmM020R-modal.component';

@NgModule({
    declarations: [ZMM020RComponent, CreateOrEditZMM020RModalComponent, ViewZMM020RModalComponent],
    imports: [AppSharedModule, ZMM020RRoutingModule, AdminSharedModule],
})
export class ZMM020RModule {}
