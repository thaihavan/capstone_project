import { Component, OnInit } from '@angular/core';
import { Topic } from 'src/app/model/Topic';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MatDialog } from '@angular/material';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';
import { UploadImageService } from 'src/app/core/services/upload-image-service/upload-image.service';
import { ImageUpload } from 'src/app/model/ImageUpload';

@Component({
  selector: 'app-form-add-topics',
  templateUrl: './form-add-topics.component.html',
  styleUrls: ['./form-add-topics.component.css']
})
export class FormAddTopicsComponent implements OnInit {
  imagePath: any;
  imgURL: any;
  message: string;
  topic: Topic = new Topic();
  constructor(private postService: PostService, public dialog: MatDialog, private imageService: UploadImageService) { }

  ngOnInit() {
  }

  preview(files: any) {
    if (files.length === 0) {
      return;
    }

    const mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      this.message = 'Only images are supported.';
      return;
    }

    const reader = new FileReader();
    this.imagePath = files;
    reader.readAsDataURL(files[0]);
    // tslint:disable-next-line:variable-name
    reader.onload = (_event) => {
      this.imgURL = reader.result;
      const imageBase64 = this.imgURL.split(',');
      const imageUpload: ImageUpload = new ImageUpload();
      imageUpload.type = this.imagePath[0].type;
      imageUpload.image = imageBase64[1];
      this.imageService.uploadImage(imageUpload).subscribe((res: any) => {
        this.topic.imgUrl = res.image;
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    };
  }

  addTopic() {
    if (this.topic.imgUrl != null && this.topic.name != null) {
      this.postService.addTopic(this.topic).subscribe((result: any) => {
        this.dialog.closeAll();
        this.openDialogMessageConfirm();
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.message = 'Update Image Fail';
    }
  }

  openDialogMessageConfirm() {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '320px',
      height: 'auto',
      position: {
        top: '20px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = 'Bạn đã thêm chủ đề thành công!';
    instance.message.url = '/dashboard/chu-de';
  }
}
