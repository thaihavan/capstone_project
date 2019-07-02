import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
// tslint:disable-next-line:max-line-length
import { MatChipsModule, MatInputModule, MatFormFieldModule, MatCardModule, MatButtonModule, MatTabsModule, MatButtonToggleModule, MatIconModule, MatDialogModule, MatToolbarModule, MatSidenavModule, MatListModule, MatProgressSpinnerModule, MatCheckboxModule } from '@angular/material';
import { MatRadioModule } from '@angular/material/radio';
import { AgmCoreModule } from '@agm/core';
import { MatMenuModule } from '@angular/material/menu';
import { SingleCommentComponent } from './components/single-comment/single-comment.component';
import { MessagePopupComponent } from './components/message-popup/message-popup.component';
import { MatStepperModule } from '@angular/material/stepper';
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
import { LoadingScreenComponent } from './components/loading-screen/loading-screen.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { ListFollowComponent } from './components/list-follow/list-follow.component';
import { UserComponent } from './components/list-follow/component/user/user.component';
import { SlideshowModule } from 'ng-simple-slideshow';
import { IfChangesDirective } from './directives/if-changes.directive';
import { ReadMoreComponent } from './components/read-more/read-more.component';
import { EditableTextComponent } from './components/editable-text/editable-text.component';
import { BookmarkPostComponent } from './components/bookmark-post/bookmark-post.component';
import { ListBookmarksComponent } from './components/list-bookmarks/list-bookmarks.component';
import { ListUserBlockedComponent } from './components/list-user-blocked/list-user-blocked.component';
import { MessageComponent } from './components/message/message.component';

const Material = [
  MatButtonModule,
  MatButtonToggleModule,
  MatDialogModule,
  MatToolbarModule,
  MatSidenavModule,
  MatListModule,
  MatButtonModule,
  MatCheckboxModule,
  MatIconModule,
  MatTabsModule,
  MatCardModule,
  MatMenuModule,
  MatProgressSpinnerModule,
  MatStepperModule,
  MatFormFieldModule,
  MatInputModule,
  MatChipsModule,
  MatRadioModule,
  MatGridListModule
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
    CropImageComponent,
    LoadingScreenComponent,
    ListFollowComponent,
    UserComponent,
    IfChangesDirective,
    ReadMoreComponent,
    EditableTextComponent,
    ListBookmarksComponent,
    BookmarkPostComponent,
    ListUserBlockedComponent,
    MessageComponent
  ],
  imports: [
    CommonModule,
    Material,
    AgmCoreModule,
    FormsModule,
    ReactiveFormsModule,
    ImageCropperModule,
    InfiniteScrollModule,
    SlideshowModule,
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
    CropImageComponent,
    LoadingScreenComponent,
    IfChangesDirective,
    ReadMoreComponent,
    EditableTextComponent,
    MessageComponent
  ], entryComponents: [StepCreatePostComponent, CropImageComponent, LoadingScreenComponent]
})
export class SharedModule { }
