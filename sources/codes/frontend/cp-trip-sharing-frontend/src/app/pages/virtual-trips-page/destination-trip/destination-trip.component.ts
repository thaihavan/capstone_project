import { Component, OnInit, Input, ViewChild, Output, EventEmitter } from '@angular/core';
import { LocationMarker } from 'src/app/model/LocationMarker';
import { UploadImageComponent } from 'src/app/shared/components/upload-image/upload-image.component';
import { ActionDestination } from 'src/app/model/ActionDestination';
import { UploadImageService } from 'src/app/core/services/upload-image-service/upload-image.service';
import { ImageUpload } from 'src/app/model/ImageUpload';

@Component({
  selector: 'app-destination-trip',
  templateUrl: './destination-trip.component.html',
  styleUrls: ['./destination-trip.component.css']
})
export class DestinationTripComponent implements OnInit {
  @Input() item: LocationMarker;
  @Input() userRole;
  @ViewChild('uploadImage') uploadImage: UploadImageComponent;
  @Output() saveDestination = new EventEmitter();
  acDestination = new ActionDestination();
  constructor(private uploadImageService: UploadImageService) {}

  ngOnInit() {
    this.initImage();
  }

  // init image
  initImage() {
    if (this.item.image.includes('maps.googleapis.com')) {
      this.uploadImageService.uploadGoogleMapImage(this.item.image).subscribe(res => {
        // tslint:disable-next-line:prefer-const
        let ImagePost = new ImageUpload();
        const reader = new FileReader();
        // Add a listener to handle successful reading of the blob
        reader.addEventListener('load', () => {
          // Set the src attribute of the image to be the resulting data URL
          // obtained after reading the content of the blob
          const image = reader.result.toString().split(',');
          ImagePost.image = image[1];
          ImagePost.type = res.type;
          this.uploadImageService.uploadImage(ImagePost).subscribe(
            url => {
              this.item.image = url.image;
            },
            (error) => {
              console.log(error);
            },
            () => {
            }
          );
        });
        // Start reading the content of the blob
        // The result should be a base64 data URL
        reader.readAsDataURL(res);
      });
    }
  }

  // event when change image
  fileClick() {
    this.uploadImage.file.nativeElement.click();
  }

  // get image when crop image done
  ImageCropted(image) {
    this.item.image = image;
    this.acDestination.typeAction = 'update';
    this.acDestination.item = this.item;
    this.saveDestination.emit(this.acDestination);
  }

  // update note
  updateDestination(text) {
    this.item.note = text;
    this.acDestination.typeAction = 'update';
    this.acDestination.item = this.item;
    this.saveDestination.emit(this.acDestination);
  }
  actionDestination(actionType) {
    // const acDestination = new ActionDestination();
    this.acDestination.typeAction = actionType;
    this.acDestination.item = this.item;
    this.saveDestination.emit(this.acDestination);
  }

}
