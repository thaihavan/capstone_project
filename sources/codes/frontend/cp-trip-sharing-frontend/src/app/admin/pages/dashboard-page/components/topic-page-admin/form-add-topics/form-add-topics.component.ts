import { Component, OnInit } from '@angular/core';
import { Topic } from 'src/app/model/Topic';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MatDialog, MatDialogRef } from '@angular/material';
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
  constructor(private postService: PostService,
              private imageService: UploadImageService,
              private dialogRef: MatDialogRef<FormAddTopicsComponent>) { }

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

  addOrUpdateTopic() {
    if (this.topic.imgUrl != null && this.topic.name != null) {
      this.postService.addOrUpdateTopic(this.topic).subscribe((result: any) => {
        this.dialogRef.close(result);
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.message = 'Bạn phải điền đầy đủ thông tin';
    }
  }

}
