import { Component, OnInit, Inject } from '@angular/core';
import { ImageCroppedEvent } from 'ngx-image-cropper';
import { UploadImageComponent } from '../upload-image.component';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
@Component({
  selector: 'app-crop-image',
  templateUrl: './crop-image.component.html',
  styleUrls: ['./crop-image.component.css']
})
export class CropImageComponent implements OnInit {
  croppedImage: any = '';
  constructor(public dialogRef: MatDialogRef<UploadImageComponent>, @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
  }

  imageCropped(event: ImageCroppedEvent) {
      this.croppedImage = event.base64;
      this.data.croppedImage = event.base64;
  }
  imageLoaded() {
      // show cropper
  }
  cropperReady() {
      // cropper ready
  }
  loadImageFailed() {
      // show message
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
}
