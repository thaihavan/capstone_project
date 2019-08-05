import { Component, OnInit, NgZone, ViewChild, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { MatStepper } from '@angular/material';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ArticleDestinationItem } from 'src/app/model/ArticleDestinationItem';
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
  selectedLocation: ArticleDestinationItem[] = [];
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
    this.selectedLocation = this.data.destinations;
    this.selectedTopic = this.data.topics;
    if (this.selectedLocation.length > 0) {
      this.fakeinput1 = 'abc';
    }
  }

  // google places search choose destination
  setAddress(addrObj) {
    this.zone.run(() => {
      let addrKeys: string[];
      let addr: object;
      addr = addrObj;
      addrKeys = Object.keys(addrObj);

      const articleDestinationItem = new ArticleDestinationItem();
      articleDestinationItem.id = addrObj.locationId;
      articleDestinationItem.name = addrObj.name;
      this.selectedLocation.push(articleDestinationItem);
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
  remove(locationId: string) {
    this.selectedLocation = this.selectedLocation.filter(item => item.id !== locationId);
    if (this.selectedLocation.length === 0) {
      this.fakeinput1 = '';
    }
  }
  createPost() {
    if (this.fakeinput1 !== '') {
      this.data.topics = this.selectedTopic;
      this.data.destinations = this.selectedLocation;
      this.dialogRef.close(this.data);
    }
  }
}
