import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { OnlyForHomePageComponent } from './pages/home-page/components/only-for-home-page/only-for-home-page.component';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    OnlyForHomePageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
