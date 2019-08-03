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
import { DeleteConfirmPopupComponent } from '../shared/components/delete-confirm-popup/delete-confirm-popup.component';
import { OverviewPageAdminComponent } from './pages/dashboard-page/components/overview-page-admin/overview-page-admin.component';
import { PostPageAdminComponent } from './pages/dashboard-page/components/post-page-admin/post-page-admin.component';
import { UserPageAdminComponent } from './pages/dashboard-page/components/user-page-admin/user-page-admin.component';
import { TopicPageAdminComponent } from './pages/dashboard-page/components/topic-page-admin/topic-page-admin.component';
import { ReportPageAdminComponent } from './pages/dashboard-page/components/report-page-admin/report-page-admin.component';
import { FormAddTopicsComponent } from './pages/dashboard-page/components/topic-page-admin/form-add-topics/form-add-topics.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { AdminLoginPageComponent } from './pages/admin-login-page/admin-login-page.component';

@NgModule({
  declarations: [
    DashboardPageComponent,
    HeaderAdminComponent,
    FormAddTopicsComponent,
    OverviewPageAdminComponent,
    PostPageAdminComponent,
    UserPageAdminComponent,
    TopicPageAdminComponent,
    ReportPageAdminComponent,
    AdminLoginPageComponent
  ],
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
    NgxChartsModule
  ],
  exports: [
    DashboardPageComponent,
    AdminLoginPageComponent
  ],
  entryComponents: [
    DeleteConfirmPopupComponent,
    FormAddTopicsComponent
  ]
})
export class AdminModule { }
