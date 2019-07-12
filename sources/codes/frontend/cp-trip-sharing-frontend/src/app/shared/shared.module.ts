import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
// tslint:disable-next-line:max-line-length
import { MatChipsModule, MatInputModule, MatFormFieldModule, MatCardModule, MatButtonModule, MatTabsModule, MatButtonToggleModule, MatIconModule, MatDialogModule, MatToolbarModule, MatSidenavModule, MatListModule, MatProgressSpinnerModule, MatCheckboxModule } from '@angular/material';
import { MatRadioModule } from '@angular/material/radio';
import { AgmCoreModule } from '@agm/core';
import { MatMenuModule } from '@angular/material/menu';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
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
import { SendMessagePopupComponent } from './components/send-message-popup/send-message-popup.component';
import { ListNotificationComponent } from './components/list-notification/list-notification.component';
import { CustomDatePipe } from './pipes/custom-date-pipe/custom-date.pipe';
import { DialogStepFindingCompanionsComponent } from './components/dialog-step-finding-companions/dialog-step-finding-companions.component';
import { StepFindingCompanionsComponent } from './components/step-finding-companions/step-finding-companions.component';
import { StepLableCompanionPostComponent } from './components/step-lable-companion-post/step-lable-companion-post.component';
import { ListPostHorizontalComponent } from './components/list-post-horizontal/list-post-horizontal.component';
import { PostSmallComponent } from './components/post-small/post-small.component';
import { ArticleSmallComponent } from './components/post-small/components/article-small/article-small.component';
import { VirtualTripSmallComponent } from './components/post-small/components/virtual-trip-small/virtual-trip-small.component';
import { CompanionPostSmallComponent } from './components/post-small/components/companion-post-small/companion-post-small.component';
import { PostFilterComponent } from './components/post-filter/post-filter.component';
import { CurrencyPipe } from './pipes/currency-pipe/currency.pipe';

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
  MatGridListModule,
  MatTooltipModule,
  MatDatepickerModule,
  MatNativeDateModule
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
    MessageComponent,
    SendMessagePopupComponent,
    ListNotificationComponent,
    CustomDatePipe,
    DialogStepFindingCompanionsComponent,
    StepFindingCompanionsComponent,
    StepLableCompanionPostComponent,
    ListPostHorizontalComponent,
    PostSmallComponent,
    ArticleSmallComponent,
    VirtualTripSmallComponent,
    CompanionPostSmallComponent,
    PostFilterComponent,
    CurrencyPipe
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
    ListUserBlockedComponent,
    MessageComponent,
    UserComponent,
    SendMessagePopupComponent,
    ListNotificationComponent,
    CustomDatePipe,
    DialogStepFindingCompanionsComponent,
    StepFindingCompanionsComponent,
    StepLableCompanionPostComponent,
    ListPostHorizontalComponent,
    PostSmallComponent,
    ArticleSmallComponent,
    VirtualTripSmallComponent,
    CompanionPostSmallComponent,
    PostFilterComponent,
    CurrencyPipe
  ],
  entryComponents: [
    StepCreatePostComponent,
    CropImageComponent,
    LoadingScreenComponent,
    SendMessagePopupComponent,
    DialogStepFindingCompanionsComponent
  ]
})
export class SharedModule { }
