import { Component, OnInit, NgZone, ViewChild, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ArticleDestinationItem } from 'src/app/model/ArticleDestinationItem';
@Component({
  selector: 'app-step-create-post',
  templateUrl: './step-create-post.component.html',
  styleUrls: ['./step-create-post.component.css'],
  providers: []
})
export class StepCreatePostComponent implements OnInit {
  secondFormGroup: FormGroup;
  isOptional = false;
  selectedLocation: ArticleDestinationItem[] = [];
  selectedTopic: string[] = [];
  selectable = true;
  fakeinput2 = '';
  isDisable = true;

  constructor(
    private formBuilder: FormBuilder,
    private zone: NgZone,
    private dialogRef: MatDialogRef<StepCreatePostComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.isDisable = true;
  }

  ngOnInit() {
    this.secondFormGroup = this.formBuilder.group({
      secondCtrl: ['', Validators.required]
    });
    this.selectedLocation = this.data.destinations;
    this.selectedTopic = this.data.topics;
  }

  selectedTopics(topics) {
    this.zone.run(() => {
      this.selectedTopic = topics;
      if (topics.length >= 1) {
        this.fakeinput2 = 'abc';
        this.isDisable = true;
      } else {
        this.fakeinput2 = '';
        this.isDisable = false;
      }
    });
  }

  createPost() {
    if (this.fakeinput2 !== '') {
      this.data.topics = this.selectedTopic;
      this.data.destinations = this.selectedLocation;
      this.dialogRef.close(this.data);
    }
  }
}
