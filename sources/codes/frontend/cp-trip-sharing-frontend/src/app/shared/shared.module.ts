import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
// tslint:disable-next-line:max-line-length
import { MatCardModule, MatButtonModule, MatTabsModule, MatButtonToggleModule, MatIconModule, MatDialogModule, MatToolbarModule, MatSidenavModule, MatListModule, MatProgressSpinnerModule } from '@angular/material';
import { AgmCoreModule } from '@agm/core';
import {MatMenuModule} from '@angular/material/menu';
import { SingleCommentComponent } from './components/single-comment/single-comment.component';
import { MessagePopupComponent } from './components/message-popup/message-popup.component';
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
  declarations: [MessagePopupComponent],
  imports: [
    CommonModule,
    Material,
    AgmCoreModule
  ],
  exports: [Material]
})
export class SharedModule { }
