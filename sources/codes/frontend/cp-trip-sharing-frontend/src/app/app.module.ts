import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { OnlyForHomePageComponent } from './pages/home-page/components/only-for-home-page/only-for-home-page.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './shared/shared.module';
import { GoogleMapComponent } from './shared/components/google-map/google-map.component';
import { AgmCoreModule } from '@agm/core';
import {HeaderComponent} from 'src/app/core/components/header/header.component';
import { MatDialogModule, MatToolbarModule, MatSidenavModule, MatListModule, MatButtonModule, MatIconModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { GoogleMapSearchComponent } from './shared/components/google-map-search/google-map-search.component';
@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    OnlyForHomePageComponent,
    LoginPageComponent,
    GoogleMapComponent,
    HeaderComponent,
    GoogleMapSearchComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    MatButtonModule,
    FlexLayoutModule,
    BrowserAnimationsModule,
    SharedModule,
    MatDialogModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyDIPTZ7dpn5_hralWGDP4glqkqAaac6qeA',
      libraries: ['places']
    })
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [LoginPageComponent]
})
export class AppModule { }
