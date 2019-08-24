import { Component, OnInit, ViewChild, NgZone } from '@angular/core';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { UploadImageComponent } from 'src/app/shared/components/upload-image/upload-image.component';
import { UploadAdapter } from 'src/app/model/UploadAdapter';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { StepCreatePostComponent } from 'src/app/shared/components/step-create-post/step-create-post.component';
import { Article } from 'src/app/model/Article';
import { Post } from 'src/app/model/Post';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { DatePipe } from '@angular/common';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';
import { UploadImageService } from 'src/app/core/services/upload-image-service/upload-image.service';
import { ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { AlertifyService } from 'src/app/core/services/alertify-service/alertify.service';
import { ArticleDestinationItem } from 'src/app/model/ArticleDestinationItem';

@Component({
  selector: 'app-create-post-page',
  templateUrl: './create-post-page.component.html',
  styleUrls: ['./create-post-page.component.css'],
  providers: [DatePipe]
})
export class CreatePostPageComponent implements OnInit {
  @ViewChild('uploadImage') uploadImage: UploadImageComponent;
  @ViewChild('myEditor') myEditor;
  imgUrl;
  isHasImg = false;
  isPublic = true;
  title: string;
  content = '';
  articlereturn: Article;
  articleId: string;
  isUpdate: boolean;
  destinations: ArticleDestinationItem[] = [];
  config = {
    filebrowserUploadUrl:
      'http://192.168.0.107:8000/api/crm/v1.0/crm-distribution-library-files',
    fileTools_requestHeaders: {
      'X-Requested-With': 'xhr',
      Authorization: 'Bearer ' + localStorage.getItem('access_token')
    },
    filebrowserUploadMethod: 'xhr',
    on: {
      instanceReady(evt) {
        const editor = evt.editor;
        console.log('editor ===>', editor);
      },
      fileUploadRequest(evt) {
        console.log('evt ===>', evt);
      }
    }
  };
  public Editor = DecoupledEditor;
  public onReady(editor) {
    editor.ui
      .getEditableElement()
      .parentElement.insertBefore(
        editor.ui.view.toolbar.element,
        editor.ui.getEditableElement()
      );
    editor.plugins.get('FileRepository').createUploadAdapter = loader => {
      // tslint:disable-next-line:no-string-literal
      console.log(loader['file']);
      return new UploadAdapter(loader, this.imageService);
    };
  }
  constructor(
    public dialog: MatDialog,
    private postService: PostService,
    private datePipe: DatePipe,
    private imageService: UploadImageService,
    private route: ActivatedRoute,
    private titleService: Title,
    private errorHandler: GlobalErrorHandler,
    private alertify: AlertifyService,
    private zone: NgZone
  ) {
    this.titleService.setTitle('Tạo bài viết');
  }

  ngOnInit() {
    this.articlereturn = new Article();
    this.articleId = this.route.snapshot.paramMap.get('articleId');
    if (
      this.articleId !== undefined &&
      this.articleId !== null &&
      this.articleId !== ''
    ) {
      this.isUpdate = true;
      this.postService.getArticleById(this.articleId).subscribe(
        res => {
          this.title = res.post.title;
          this.imgUrl = res.post.coverImage;
          if (this.imgUrl !== undefined) {
            this.isHasImg = true;
          }
          this.destinations = res.destinations;
          this.content = res.post.content;
          this.articlereturn = res;
        },
        this.errorHandler.handleError
      );
    }
  }
  uploadImg(files) {
    if (files.lenght === 0) {
      return;
    }
    const mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      console.log('Only imgaes are supported!');
    }
    const reader = new FileReader();
    reader.readAsDataURL(files[0]);
    reader.onload = event => {
      console.log('load image');
      this.imgUrl = reader.result;
      this.isHasImg = true;
    };
  }
  fileClick() {
    this.uploadImage.file.nativeElement.click();
  }
  ImageCropted(image) {
    this.imgUrl = image;
    this.isHasImg = true;
  }
  removeImage() {
    this.imgUrl = '';
    this.isHasImg = false;
  }
  openDialog(arrTopics, arrDestinations): void {
    const dialogRef = this.dialog.open(StepCreatePostComponent, {
      width: '60%',
      data: {
        topics: arrTopics,
        destinations: arrDestinations,
        isUpdate: this.isUpdate
      }
    });
    dialogRef.afterClosed().subscribe(res => {
      if (res !== undefined) {
        const article = new Article();
        article.topics = res.topics;
        // article.topics = []; // Because res.topics is undefined. will be removed
        article.destinations = this.destinations;
        const post = new Post();
        post.title = this.title;
        post.content = this.myEditor.editorInstance.getData();
        post.isPublic = this.isPublic;
        post.pubDate = this.datePipe.transform(
          new Date(),
          'yyyy-MM-dd hh:mm:ss'
        );
        post.coverImage = this.imgUrl;
        article.post = post;

        console.log('article param:', article);
        if (this.isUpdate) {
          this.articlereturn.post.title = post.title;
          this.articlereturn.post.content = post.content;
          this.articlereturn.post.pubDate = post.pubDate;
          this.articlereturn.post.isPublic = post.isPublic;
          this.articlereturn.post.coverImage = post.coverImage;
          this.articlereturn.topics = res.topics;
          this.articlereturn.destinations = this.destinations;
          this.postService.updateArticle(this.articlereturn).subscribe(
            (data: Article) => {
              this.articlereturn = data;
              this.openDialogMessageConfirm('Chỉnh sửa bài thành công', 'success');
            },
            (err: HttpErrorResponse) => {
              this.openDialogMessageConfirm('Chỉnh sửa bài thất bại', 'danger');
            }
          );
        } else {
          this.postService.createPost(article).subscribe(
            (data: Article) => {
              this.articlereturn = data;
              this.openDialogMessageConfirm('Bạn đã đăng bài thành công', 'success');
            },
            this.errorHandler.handleError
          );
        }
      }
    });
  }

  // dialog crop image
  openDialogMessageConfirm(message: string, messageType: string) {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '500px',
      height: 'auto',
      position: {
        top: '20px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = message;
    instance.message.messageType = messageType;
    instance.message.url = '/bai-viet/' + this.articlereturn.id;
  }
  createPost() {
    if (this.title === '' || this.content === '' || this.destinations.length === 0) {
      this.alertify.error('Bạn cần nhập thông tin bài viết');
      window.scroll({
        top: 200,
        left: 0,
        behavior: 'smooth'
      });
      return;
    }
    if (this.isUpdate) {
      this.openDialog(
        this.articlereturn.topics, []
      );
    } else {
      if (this.myEditor && this.myEditor.editorInstance) {
        this.openDialog([], []);
      }
    }
  }

  // on google-map-search submit add address location.
  addDestination(addrObj) {
    this.zone.run(() => {
      let addrKeys;
      let addr;
      addr = addrObj;
      addrKeys = Object.keys(addrObj);
      const location: ArticleDestinationItem = {
        id: addrObj.locationId,
        name: addrObj.name
      };
      this.destinations.push(location);
    });
  }

  // remove destination
  removeDestination(index) {
    this.destinations.splice(index, 1);
  }
}
