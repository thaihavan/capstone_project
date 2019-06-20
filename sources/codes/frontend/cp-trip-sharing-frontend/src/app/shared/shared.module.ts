import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
// tslint:disable-next-line:max-line-length
import { MatChipsModule, MatInputModule, MatFormFieldModule, MatCardModule, MatButtonModule, MatTabsModule, MatButtonToggleModule, MatIconModule, MatDialogModule, MatToolbarModule, MatSidenavModule, MatListModule, MatProgressSpinnerModule } from '@angular/material';
import {MatRadioModule} from '@angular/material/radio';
import { AgmCoreModule } from '@agm/core';
import {MatMenuModule} from '@angular/material/menu';
import { SingleCommentComponent } from './components/single-comment/single-comment.component';
import { MessagePopupComponent } from './components/message-popup/message-popup.component';
import {MatStepperModule} from '@angular/material/stepper';
import { CommentContainerComponent } from './components/comment-container/comment-container.component';
import { ListPostComponent } from './components/list-post/list-post.component';
import { ArticleComponent } from './components/article/article.component';
import { StepCreatePostComponent } from './components/step-create-post/step-create-post.component';
import { GoogleMapSearchComponent } from './components/google-map-search/google-map-search.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InterestedTopicComponent } from './components/interested-topic/interested-topic.component';
import { UploadImageComponent } from './components/upload-image/upload-image.component';
import { CropImageComponent } from './components/upload-image/crop-image/crop-image.component';
import { ImageCropperModule } from 'ngx-image-cropper';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
const Material = [
  MatButtonModule,
  MatButtonToggleModule,
  MatDialogModule,
  MatToolbarModule,
  MatSidenavModule,
  MatListModule,
  MatButtonModule,
  MatIconModule,
  MatTabsModule,
  MatCardModule,
  MatMenuModule,
  MatProgressSpinnerModule,
  MatStepperModule,
  MatFormFieldModule,
  MatInputModule,
  MatChipsModule,
  MatRadioModule
 ];
@NgModule({
  declarations: [
    MessagePopupComponent,
    CommentContainerComponent,
    SingleCommentComponent,
    ListPostComponent,
    ArticleComponent,
    StepCreatePostComponent,
    GoogleMapSearchComponent,
    InterestedTopicComponent,
    UploadImageComponent,
    CropImageComponent
  ],
  imports: [
    CommonModule,
    Material,
    AgmCoreModule,
    FormsModule,
    ReactiveFormsModule,
    ImageCropperModule,
    InfiniteScrollModule
  ],
  exports: [
    Material,
    InfiniteScrollModule,
    SingleCommentComponent,
    CommentContainerComponent,
    ArticleComponent,
    ListPostComponent,
    StepCreatePostComponent,
    GoogleMapSearchComponent,
    InterestedTopicComponent,
    UploadImageComponent,
    CropImageComponent
  ], entryComponents: [StepCreatePostComponent, CropImageComponent]
})
export class SharedModule { }
