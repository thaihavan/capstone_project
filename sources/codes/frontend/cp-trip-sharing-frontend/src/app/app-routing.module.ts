import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GoogleMapComponent } from './shared/components/google-map/google-map.component';
import { PersonalPageComponent } from './pages/personal-page/personal-page.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { ForgotpasswordPageComponent } from './pages/forgotpassword-page/forgotpassword-page.component';
import { CreatePostPageComponent } from './pages/create-post-page/create-post-page.component';
import { UploadImageComponent } from './shared/components/upload-image/upload-image.component';
import { ChangepasswordPageComponent } from './pages/changepassword-page/changepassword-page.component';
import { DetailpostPageComponent } from './pages/detailpost-page/detailpost-page.component';
import { HeaderComponent } from './core/components/header/header.component';
import { EmailConfirmPageComponent } from './pages/email-confirm-page/email-confirm-page.component';
import { ResetPasswordPageComponent } from './pages/reset-password-page/reset-password-page.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { ListPostComponent } from './shared/components/list-post/list-post.component';
import { InitialUserInformationPageComponent } from './pages/initial-user-information-page/initial-user-information-page.component';
import { VirtualTripsPageComponent } from './pages/virtual-trips-page/virtual-trips-page.component';
import { ListBookmarksComponent } from './shared/components/list-bookmarks/list-bookmarks.component';
import { ChatPageComponent } from './pages/chat-page/chat-page.component';
import { ListUserBlockedComponent } from './shared/components/list-user-blocked/list-user-blocked.component';
import { CreateFindingCompanionsPostComponent } from './pages/create-finding-companions-post/create-finding-companions-post.component';
import { ListPostPageComponent } from './pages/list-post-page/list-post-page.component';
import { DashboardPageComponent } from './admin/pages/dashboard-page/dashboard-page.component';


const routes: Routes = [
  { path: 'dashboard', component: DashboardPageComponent },
  { path: 'xac-nhan-email/:token', component: EmailConfirmPageComponent },
  {
    path: '', component: HeaderComponent,
    children: [
      { path: '', redirectTo: 'trang-chu', pathMatch: 'full' },
      { path: 'trang-chu', component: HomePageComponent },
      { path: 'trang-chu/:home-nav', component: ListPostPageComponent },
      {
        path: 'user/:userId', component: PersonalPageComponent,
        children: [
          { path: '', redirectTo: 'bai-viet', pathMatch: 'full' },
          { path: 'da-danh-dau', component: ListBookmarksComponent },
          { path: 'danh-sach-chan', component: ListUserBlockedComponent },
          { path: ':personal-nav', component: ListPostComponent }
        ]
      },
      { path: 'dang-ky', component: RegisterPageComponent },
      { path: 'chuyen-di', component: VirtualTripsPageComponent },
      { path: 'chuyen-di/:tripId', component: VirtualTripsPageComponent },
      { path: 'quen-mat-khau', component: ForgotpasswordPageComponent },
      { path: 'tao-bai-viet', component: CreatePostPageComponent },
      { path: 'chinh-sua-bai-viet/:articleId', component: CreatePostPageComponent },
      { path: 'doi-mat-khau', component: ChangepasswordPageComponent },
      { path: 'dat-lai-mat-khau/:token', component: ResetPasswordPageComponent },
      { path: 'bai-viet/:articleId', component: DetailpostPageComponent },
      { path: 'khoi-tao', component: InitialUserInformationPageComponent },
      { path: 'tin-nhan', component: ChatPageComponent },
      { path: 'tao-bai-tim-ban-dong-hanh', component: CreateFindingCompanionsPostComponent },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
