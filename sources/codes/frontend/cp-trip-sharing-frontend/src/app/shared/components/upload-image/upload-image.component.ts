import { Component, OnInit, Input, Output, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CropImageComponent } from './crop-image/crop-image.component';
import { EventEmitter } from '@angular/core';

@Component({
  selector: 'app-upload-image',
  templateUrl: './upload-image.component.html',
  styleUrls: ['./upload-image.component.css']
})
export class UploadImageComponent implements OnInit {
  imageChangedEvent: any = '';
  @Input() width = '60%';
  @Input() aspectRatio;
  @Input() resizeToWidth;
  @Output() croppedImage = new EventEmitter();
  @ViewChild('file') file;
  constructor(public dialog: MatDialog) {}

  ngOnInit() {
  }

  fileChangeEvent(event: any): void {
    this.imageChangedEvent = event;
    this.openDialog();
  }
  openDialog(): void {
    const dialogRef = this.dialog.open(CropImageComponent, {
      width: this.width,
      data: { imageChangedEvent: this.imageChangedEvent,
        aspectRatio: this.aspectRatio,
        resizeToWidth: this.resizeToWidth,
        croppedImage: ''
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      if (result !== undefined) {
        if (result.croppedImage !== '') {
          this.croppedImage.emit(result.croppedImage);
          this.file.nativeElement.value = '';
          console.log('cropimage', result.croppedImage);
        }
      }
    });
  }
}
