import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule, MatButtonToggleModule } from '@angular/material';
import { AgmCoreModule } from '@agm/core';

const Material = [
  MatButtonModule,
  MatButtonToggleModule ];
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
