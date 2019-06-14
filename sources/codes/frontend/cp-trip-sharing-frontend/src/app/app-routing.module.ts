import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GoogleMapComponent } from './shared/components/google-map/google-map.component';
import { PersonalPageComponent } from './pages/personal-page/personal-page.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { ForgotpasswordPageComponent } from './pages/forgotpassword-page/forgotpassword-page.component';
import { CreatedPostComponent } from './pages/personal-page/components/created-post/created-post.component';
import { CreatePostPageComponent } from './pages/create-post-page/create-post-page.component';
import { UploadImageComponent } from './shared/components/upload-image/upload-image.component';
import { ChangepasswordPageComponent } from './pages/changepassword-page/changepassword-page.component'

const routes: Routes = [
  { path: 'heroes', component: GoogleMapComponent },
  { path: 'personal', component: PersonalPageComponent,
    children: [
      {path: 'register', component: RegisterPageComponent },
      {path: 'virtual', component: GoogleMapComponent},
      {path: '', redirectTo: 'personalfeed', pathMatch: 'full'},
      {path: 'personalfeed', component: CreatedPostComponent },
    ]
  },
  { path: 'register', component: RegisterPageComponent},
  { path: 'virtual', component: GoogleMapComponent},
  { path: '', redirectTo: 'personal', pathMatch: 'full'},
  { path: 'forgot', component: ForgotpasswordPageComponent},
  {path: 'createpost', component: CreatePostPageComponent },
  {path: 'changePassword',component: ChangepasswordPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
