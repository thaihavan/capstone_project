import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardPageComponent } from './pages/dashboard-page/dashboard-page.component';
import { MatMenuModule, MatInputModule, MatExpansionModule, MatBadgeModule } from '@angular/material';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from '../app-routing.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { SharedModule } from '../shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HeaderAdminComponent } from './components/header-admin/header-admin.component';
import { InterestedTopicAdminsComponent } from './pages/interested-topic-admins/interested-topic-admins.component';
import { FormAddTopicsComponent } from './pages/interested-topic-admins/form-add-topics/form-add-topics.component';

@NgModule({
  declarations: [DashboardPageComponent, HeaderAdminComponent, InterestedTopicAdminsComponent,FormAddTopicsComponent],
  imports: [
    CommonModule,
    MatMenuModule,
    BrowserModule,
    AppRoutingModule,
    FlexLayoutModule,
    SharedModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatExpansionModule,
    MatBadgeModule,
  ],
  exports: [
    DashboardPageComponent
  ]
})
export class AdminModule { }
