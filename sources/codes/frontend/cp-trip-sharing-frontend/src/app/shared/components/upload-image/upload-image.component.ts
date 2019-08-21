import { Component, OnInit, Input, Output, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CropImageComponent } from './crop-image/crop-image.component';
import { EventEmitter } from '@angular/core';
import { UploadImageService } from 'src/app/core/services/upload-image-service/upload-image.service';
import { ImageUpload } from 'src/app/model/ImageUpload';
import { LoadingScreenComponent } from '../loading-screen/loading-screen.component';

@Component({
  selector: 'app-upload-image',
  templateUrl: './upload-image.component.html',
  styleUrls: ['./upload-image.component.css']
})
export class UploadImageComponent implements OnInit {
  imageChangedEvent: any = '';
  imagePost: ImageUpload;
  @Input() width = '60%';
  @Input() aspectRatio;
  @Input() resizeToWidth;
  @Output() croppedImage = new EventEmitter();
  @ViewChild('file') file;
  constructor(
    public dialog: MatDialog,
    private imageService: UploadImageService
  ) {}

  ngOnInit() {
    this.imagePost = new ImageUpload();
  }

  fileChangeEvent(event: any): void {
    this.imageChangedEvent = event;
    this.openDialog();
  }
  openDialog(): void {
    const dialogRef = this.dialog.open(CropImageComponent, {
      width: this.width,
      data: {
        imageChangedEvent: this.imageChangedEvent,
        aspectRatio: this.aspectRatio,
        resizeToWidth: this.resizeToWidth,
        croppedImage: '',
        type: ''
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        if (result.croppedImage !== '') {
          const dialogLoad  = this.dialog.open(LoadingScreenComponent, {
            height: '100vh',
            data: {
              title: '',
              isPublic: ''
            },
            panelClass: 'full-screen-modal',
            disableClose: true
          });
          const image = result.croppedImage.split(',');
          this.imagePost.image = image[1];
          this.imagePost.type = result.type;
          this.imageService.uploadImage(this.imagePost).subscribe(
            res => {
              result.croppedImage = res.image;
              dialogLoad.close();
              this.croppedImage.emit(result.croppedImage);
              this.file.nativeElement.value = '';
            },
            complete => {
            }
          );
        }
      }
    });
  }
}
