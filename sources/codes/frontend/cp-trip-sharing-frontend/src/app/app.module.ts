import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
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
import { FlexLayoutModule } from '@angular/flex-layout';
import { GoogleMapSearchComponent } from './shared/components/google-map-search/google-map-search.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { PersonalPageComponent } from './pages/personal-page/personal-page.component';
import {ForgotpasswordPageComponent} from './pages/forgotpassword-page/forgotpassword-page.component';
import { CreatePostPageComponent } from './pages/create-post-page/create-post-page.component';
import { CreatedPostComponent } from './pages/personal-page/components/created-post/created-post.component';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { MatMenuModule} from '@angular/material/menu';
import { HttpClientInMemoryWebApiModule } from 'angular-in-memory-web-api';
import { Globals } from 'src/globals/globalvalues';
import { InMemoryService } from './core/services/service-1/inMemory.service';
import { HttpClientModule } from '@angular/common/http';
import { InterestedtopicPageComponent } from './pages/interestedtopic-page/interestedtopic-page.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { UploadImageComponent } from './shared/components/upload-image/upload-image.component';
import { CropImageComponent } from './shared/components/upload-image/crop-image/crop-image.component';
import { ImageCropperModule } from 'ngx-image-cropper';
@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    OnlyForHomePageComponent,
    LoginPageComponent,
    GoogleMapComponent,
    HeaderComponent,
    GoogleMapSearchComponent,
    RegisterPageComponent,
    PersonalPageComponent,
    ForgotpasswordPageComponent,
    CreatePostPageComponent,
    CreatedPostComponent,
    InterestedtopicPageComponent,
    UploadImageComponent,
    CropImageComponent
  ],
  imports: [
    MatMenuModule,
    BrowserModule,
    AppRoutingModule,
    FlexLayoutModule,
    BrowserAnimationsModule,
    SharedModule,
    HttpClientModule,
    FormsModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyDIPTZ7dpn5_hralWGDP4glqkqAaac6qeA',
      libraries: ['places']
    }),
    HttpClientModule,
    InfiniteScrollModule,
    // HttpClientInMemoryWebApiModule.forRoot(
    //   InMemoryService, { dataEncapsulation: false }
    // ),
    CKEditorModule,
    ImageCropperModule
  ],
  providers: [Globals],
  bootstrap: [AppComponent],
  entryComponents: [LoginPageComponent, InterestedtopicPageComponent, CropImageComponent]
})
export class AppModule { }
