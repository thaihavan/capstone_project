import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GoogleMapComponent } from './shared/components/google-map/google-map.component';
import { PersonalPageComponent } from './pages/personal-page/personal-page.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';

const routes: Routes = [
  { path: 'heroes', component: GoogleMapComponent },
  { path: 'personal', component: PersonalPageComponent,
    children: [
      {path: 'register', component: RegisterPageComponent },
      {path: 'virtual', component: GoogleMapComponent},
      {path: '', redirectTo: 'register', pathMatch: 'full'}
    ]
  },
  { path: 'register', component: RegisterPageComponent},
  { path: 'virtual', component: GoogleMapComponent},
  { path: '', redirectTo: 'personal', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
