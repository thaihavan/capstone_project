import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
// tslint:disable-next-line:max-line-length
import { MatCardModule, MatButtonModule, MatTabsModule, MatButtonToggleModule, MatIconModule, MatDialogModule, MatToolbarModule, MatSidenavModule, MatListModule, MatProgressSpinnerModule } from '@angular/material';
import { AgmCoreModule } from '@agm/core';
import {MatMenuModule} from '@angular/material/menu';
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
  MatCardModule,
  MatMenuModule,
  MatProgressSpinnerModule
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
