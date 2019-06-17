import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GoogleMapComponent } from './shared/components/google-map/google-map.component';
import { PersonalPageComponent } from './pages/personal-page/personal-page.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { ForgotpasswordPageComponent } from './pages/forgotpassword-page/forgotpassword-page.component';
import { CreatedPostComponent } from './pages/personal-page/components/created-post/created-post.component';
import { CreatePostPageComponent } from './pages/create-post-page/create-post-page.component';
import { UploadImageComponent } from './shared/components/upload-image/upload-image.component';
import { ChangepasswordPageComponent } from './pages/changepassword-page/changepassword-page.component';
import { DetailpostPageComponent } from './pages/detailpost-page/detailpost-page.component';
import { HeaderComponent } from './core/components/header/header.component';
import { EmailConfirmPageComponent } from './pages/email-confirm-page/email-confirm-page.component';
import { ResetPasswordPageComponent } from './pages/reset-password-page/reset-password-page.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { ListPostComponent } from './shared/components/list-post/list-post.component';

const routes: Routes = [
  { path: 'email-confirm/:token', component: EmailConfirmPageComponent },
  {
    path: '', component: HeaderComponent,
    children: [
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      {
        path: 'home', component: HomePageComponent,
        children: [
          { path: '', redirectTo: 'for-you', pathMatch: 'full' },
          { path: ':home-nav', component: ListPostComponent }
        ]
      },
      {
        path: 'personal', component: PersonalPageComponent,
        children: [
          { path: '', redirectTo: 'article', pathMatch: 'full' },
          { path: ':personal-nav', component: ListPostComponent }
        ]
      },
      { path: 'register', component: RegisterPageComponent },
      { path: 'virtual-trips', component: GoogleMapComponent },
      { path: 'forgot-password', component: ForgotpasswordPageComponent },
      { path: 'create-article', component: CreatePostPageComponent },
      { path: 'change-password', component: ChangepasswordPageComponent },
      { path: 'reset-password/:token', component: ResetPasswordPageComponent },
      { path: 'change-password', component: ChangepasswordPageComponent },
      { path: 'post-detail', component: DetailpostPageComponent }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
