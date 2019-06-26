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
  constructor(public dialogReferent: MatDialogRef<UploadImageComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
  }

  imageCropped(event: ImageCroppedEvent) {
    // tslint:disable-next-line:no-debugger
      debugger;
      this.croppedImage = event.base64;
      this.data.croppedImage = event.base64;
      this.data.type = event.file.type;
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
    this.dialogReferent.close();
  }
}
