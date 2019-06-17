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
import { Ptor } from 'protractor';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { DatePipe } from '@angular/common';
import { Account } from 'src/app/model/Account';
import { Author } from 'src/app/model/Author';

@Component({
  selector: 'app-create-post-page',
  templateUrl: './create-post-page.component.html',
  styleUrls: ['./create-post-page.component.css'],
  providers: [DatePipe]
})
export class CreatePostPageComponent implements OnInit {
  @ViewChild('uploadImage') uploadImage: UploadImageComponent;
  @ViewChild('myEditor') myEditor;
  imgUrl: any;
  isHasImg = false;
  isPublic = true;
  title: string;
  config = {
    filebrowserUploadUrl: 'http://192.168.0.107:8000/api/crm/v1.0/crm-distribution-library-files',
    fileTools_requestHeaders: {
        'X-Requested-With': 'xhr',
        Authorization: 'Bearer ' + localStorage.getItem('access_token')
    },
    filebrowserUploadMethod: 'xhr',
    on: {
        instanceReady( evt ) {
            const editor = evt.editor;
            console.log('editor ===>', editor);
        },
        fileUploadRequest(evt) {
            console.log( 'evt ===>', evt );
        },
    },
};
  public Editor = DecoupledEditor;
  public onReady(editor) {
    editor.ui
      .getEditableElement()
      .parentElement.insertBefore(
        editor.ui.view.toolbar.element,
        editor.ui.getEditableElement()
      );
    editor.plugins.get('FileRepository').createUploadAdapter = (loader) => {
        // tslint:disable-next-line:no-string-literal
        console.log(loader['file']);
        return new UploadAdapter(loader, this.http);
      };
  }
  constructor( private http: HttpClient, public dialog: MatDialog,
               private postService: PostService,
               private datePipe: DatePipe) {}

  ngOnInit() {
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
    console.log(this.uploadImage.file);
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
  openDialog(): void {
    const dialogRef = this.dialog.open(StepCreatePostComponent, {
      width: '60%',
      data: {
        toppics: [],
        destinations: [],
    }
    });
    dialogRef.afterClosed().subscribe(res => {
      if (res !== undefined) {
        const article = new Article();
        article.toppics = res.toppics;
        article.destinations = res.destinations;
        const post = new Post();
        post.title = this.title;
        const author = new Author();
        // author.authorId = localStorage.getItem('UserId');
        post.author = null;
        post.content = this.myEditor.editorInstance.getData();
        post.isPublic = this.isPublic;
        post.pubDate = this.datePipe.transform( new Date(), 'yyyy-MM-dd hh:mm:ss');
        article.post = post;
        this.postService.createPost(article).subscribe(data => {
        }, error => {
          console.log(error);
        });
      }
    });
  }
  createPost() {
    if (this.myEditor && this.myEditor.editorInstance) {
      console.log(this.myEditor.editorInstance.getData());
    }
    this.openDialog();
  }
}
