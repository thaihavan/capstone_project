import { NgModule } from '@angular/core';
import { Routes, RouterModule, Router } from '@angular/router';
import { PersonalPageComponent } from './pages/personal-page/personal-page.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { ForgotpasswordPageComponent } from './pages/forgotpassword-page/forgotpassword-page.component';
import { CreatePostPageComponent } from './pages/create-post-page/create-post-page.component';
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
import { CreateFindingCompanionsPostComponent } from './pages/create-finding-companions-post/create-finding-companions-post.component';
import { ListPostPageComponent } from './pages/list-post-page/list-post-page.component';
import { DashboardPageComponent } from './admin/pages/dashboard-page/dashboard-page.component';
import { SearchResultPageComponent } from './pages/search-result-page/search-result-page.component';
// tslint:disable-next-line:max-line-length
import { SearchResultContainerComponent } from './pages/search-result-page/components/search-result-container/search-result-container.component';
import { TopicPageAdminComponent } from './admin/pages/dashboard-page/components/topic-page-admin/topic-page-admin.component';
import { OverviewPageAdminComponent } from './admin/pages/dashboard-page/components/overview-page-admin/overview-page-admin.component';
import { PostPageAdminComponent } from './admin/pages/dashboard-page/components/post-page-admin/post-page-admin.component';
import { UserPageAdminComponent } from './admin/pages/dashboard-page/components/user-page-admin/user-page-admin.component';
import { AdminLoginPageComponent } from './admin/pages/admin-login-page/admin-login-page.component';
import { PrivacyPolicyComponent } from './pages/privacy-policy/privacy-policy.component';
import { ReportedUserPageComponent } from './admin/pages/dashboard-page/components/reported-user-page/reported-user-page.component';
import { ReportedPostPageComponent } from './admin/pages/dashboard-page/components/reported-post-page/reported-post-page.component';
// tslint:disable-next-line: max-line-length
import { ReportedCommentPageComponent } from './admin/pages/dashboard-page/components/reported-comment-page/reported-comment-page.component';
import { ErrorPageComponent } from './pages/error-page/error-page.component';


const routes: Routes = [
  {path: 'admin/login', component: AdminLoginPageComponent},
  {
    path: 'admin/dashboard', component: DashboardPageComponent,
    children: [
      { path: '', redirectTo: 'tong-quan', pathMatch: 'full' },
      { path: 'tong-quan', component: OverviewPageAdminComponent},
      { path: 'bai-viet', component: PostPageAdminComponent},
      { path: 'nguoi-dung', component: UserPageAdminComponent},
      { path: 'chu-de', component: TopicPageAdminComponent },
      { path: 'nguoi-dung-vi-pham', component: ReportedUserPageComponent },
      { path: 'bai-viet-vi-pham', component: ReportedPostPageComponent },
      { path: 'binh-luan-vi-pham', component: ReportedCommentPageComponent }
    ]
  },
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
          { path: ':personal-nav', component: ListPostComponent }
        ]
      },
      {
        path: 'search/:searchType', component: SearchResultPageComponent,
        children: [
          { path: '', redirectTo: 'bai-viet', pathMatch: 'full' },
          { path: ':tab', component: SearchResultContainerComponent},
          { path: ':tab/:search', component: SearchResultContainerComponent}
        ]
      },
      { path: 'dang-ky', component: RegisterPageComponent },
      { path: 'tao-chuyen-di', component: VirtualTripsPageComponent },
      { path: 'chuyen-di/:tripId', component: VirtualTripsPageComponent },
      { path: 'quen-mat-khau', component: ForgotpasswordPageComponent },
      { path: 'tao-bai-viet', component: CreatePostPageComponent },
      { path: 'chinh-sua-bai-viet/:articleId', component: CreatePostPageComponent },
      { path: 'dat-lai-mat-khau/:token', component: ResetPasswordPageComponent },
      { path: 'bai-viet/:articleId', component: DetailpostPageComponent },
      { path: 'khoi-tao', component: InitialUserInformationPageComponent },
      { path: 'tin-nhan', component: ChatPageComponent },
      { path: 'tao-bai-viet/tim-ban-dong-hanh', component: CreateFindingCompanionsPostComponent },
      { path: 'tim-ban-dong-hanh/:companionId', component: DetailpostPageComponent},
      { path: 'chinh-sua-tim-ban-dong-hanh/:companionId', component: CreateFindingCompanionsPostComponent},
      { path: 'dieu-khoan', component: PrivacyPolicyComponent }
    ]
  },
  {path: 'error', component: ErrorPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule {
  constructor(private router: Router) {
    this.router.errorHandler = (error: any) => {
        this.router.navigate(['error']); // or redirect to default route
    };
  }
}
