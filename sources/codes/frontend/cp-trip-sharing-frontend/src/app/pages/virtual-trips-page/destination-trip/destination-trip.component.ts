import { Component, OnInit, Input, ViewChild, Output, EventEmitter } from '@angular/core';
import { LocationMarker } from 'src/app/model/LocationMarker';
import { UploadImageComponent } from 'src/app/shared/components/upload-image/upload-image.component';

@Component({
  selector: 'app-destination-trip',
  templateUrl: './destination-trip.component.html',
  styleUrls: ['./destination-trip.component.css']
})
export class DestinationTripComponent implements OnInit {
  @Input() item: LocationMarker;
  @ViewChild('uploadImage') uploadImage: UploadImageComponent;
  @Output() saveDestination = new EventEmitter();
  constructor() {}

  ngOnInit() {
  }

  // event when change image
  fileClick() {
    this.uploadImage.file.nativeElement.click();
  }

  // get image when crop image done
  ImageCropted(image) {
    this.item.image = image;
    this.saveDestination.emit(this.item);
  }

  // update note
  updateDestination(text) {
    this.item.note = text;
    this.saveDestination.emit(this.item);
  }
}
