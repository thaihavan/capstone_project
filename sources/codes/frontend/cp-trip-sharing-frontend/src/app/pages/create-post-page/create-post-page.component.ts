import { Component, OnInit, ViewChild } from '@angular/core';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { UploadImageComponent } from 'src/app/shared/components/upload-image/upload-image.component';
import { UploadAdapter } from 'src/app/model/UploadAdapter';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { StepCreatePostComponent } from 'src/app/shared/components/step-create-post/step-create-post.component';
import { Article } from 'src/app/model/Article';
import { Post } from 'src/app/model/Post';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { DatePipe } from '@angular/common';
import { Account } from 'src/app/model/Account';
import { Author } from 'src/app/model/Author';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';
import { UploadImageService } from 'src/app/core/services/upload-image-service/upload-image.service';
import { ActivatedRoute } from '@angular/router';

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
  content = '<p>Hello world!</p>';
  articlereturn: Article;
  articleId: string;
  isUpdate: boolean;
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
    private http: HttpClient,
    public dialog: MatDialog,
    private postService: PostService,
    private datePipe: DatePipe,
    private imageService: UploadImageService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.articlereturn = new Article();
    this.articleId = this.route.snapshot.queryParamMap.get('id');
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
          this.content = res.post.content;
          this.articlereturn = res;
        },
        error => {},
        () => {}
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
        article.destinations = res.destinations;
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
          this.articlereturn.destinations = res.destinations;
          this.postService.updateArticle(this.articlereturn).subscribe(
            data => {},
            error => {
              console.log(error);
            },
            () => {
              this.openDialogMessageConfirm('Chỉnh sửa bài thành công');
            }
          );
        } else {
          this.postService.createPost(article).subscribe(
            (data: any) => {
              this.articlereturn.post = data.post;
              this.openDialogMessageConfirm('Bạn đã đăng bài thành công');
            },
            error => {
              console.log(error);
            }
          );
        }
      }
    });
  }

  // dialog crop image
  openDialogMessageConfirm(message: string) {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '400px',
      height: '200px',
      position: {
        top: '10px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = message;
    instance.message.url = '/post-detail?postId=' + this.articlereturn.post.id;
  }
  createPost() {
    if (this.isUpdate) {
      this.openDialog(
        this.articlereturn.topics,
        this.articlereturn.destinations
      );
    } else {
      if (this.myEditor && this.myEditor.editorInstance) {
      this.openDialog([], []);
      }
    }
  }
}
