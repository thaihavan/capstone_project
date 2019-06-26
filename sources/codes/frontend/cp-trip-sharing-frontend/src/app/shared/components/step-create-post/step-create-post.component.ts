import { Component, OnInit, NgZone, ViewChild, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { LocationMarker } from 'src/app/model/LocationMarker';
import { MatStepper } from '@angular/material';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CreatePostPageComponent } from 'src/app/pages/create-post-page/create-post-page.component';
import { Topic } from 'src/app/model/Topic';
@Component({
  selector: 'app-step-create-post',
  templateUrl: './step-create-post.component.html',
  styleUrls: ['./step-create-post.component.css'],
  providers: [
    {
      provide: STEPPER_GLOBAL_OPTIONS,
      useValue: { displayDefaultIndicatorType: false }
    }
  ]
})
export class StepCreatePostComponent implements OnInit {
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  isOptional = false;
  selectedLocation: LocationMarker[] = [];
  selectedTopic: string[] = [];
  selectable = true;
  removable = true;
  @ViewChild('stepper') stepper: MatStepper;
  fakeinput1 = '';
  fakeinput2 = '';

  constructor(
    private formBuilder: FormBuilder,
    private zone: NgZone,
    private dialogRef: MatDialogRef<StepCreatePostComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit() {
    this.firstFormGroup = this.formBuilder.group({
      firstCtrl: ['', Validators.required]
    });
    this.secondFormGroup = this.formBuilder.group({
      secondCtrl: ['', Validators.required]
    });
  }

  // google places search choose destination
  setAddress(addrObj) {
    this.zone.run(() => {
      let addrKeys: string[];
      let addr: object;
      addr = addrObj;
      addrKeys = Object.keys(addrObj);
      const location: LocationMarker = {
        longitude: addrObj.lng,
        latitude: addrObj.lat,
        formattedAddress: addrObj.formattedAddress,
        locationId: addrObj.locationId,
        icon: '',
        image: '',
        name: addrObj.name,
        note: ''
      };
      this.selectedLocation.push(location);
      this.fakeinput1 = 'abc';
    });
  }
  selectedTopics(topics) {
    this.zone.run(() => {
      console.log(topics);
      this.selectedTopic = topics;
      if (topics.length >= 1) {
        this.fakeinput2 = 'abc';
      } else {
        this.fakeinput2 = '';
      }
    });
  }
  remove(location) {
    this.selectedLocation = this.selectedLocation.filter(item => item !== location);
    if (this.selectedLocation.length === 0) {
      this.fakeinput1 = '';
    }
  }
  createPost() {
    if (this.fakeinput1 !== '') {
      this.data.topics = this.selectedTopic;
      this.data.destinations = this.selectedLocation.map(dest => dest.name);
      this.dialogRef.close(this.data);
    }
  }
}
