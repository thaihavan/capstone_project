import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
// tslint:disable-next-line:max-line-length
import { MatButtonModule, MatTabsModule, MatButtonToggleModule, MatIconModule, MatDialogModule, MatToolbarModule, MatSidenavModule, MatListModule } from '@angular/material';
import { AgmCoreModule } from '@agm/core';
const Material = [
  MatButtonModule,
  MatButtonToggleModule,
  MatDialogModule,
  MatToolbarModule,
  MatSidenavModule,
  MatListModule,
  MatButtonModule,
  MatIconModule,
  MatTabsModule,
 ];
@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    Material,
    AgmCoreModule
  ],
  exports: [Material]
})
export class SharedModule { }
