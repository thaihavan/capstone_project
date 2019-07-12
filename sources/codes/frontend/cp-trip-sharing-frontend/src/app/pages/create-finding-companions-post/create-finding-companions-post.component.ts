import { Component, OnInit, ViewChild, NgZone } from '@angular/core';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { UploadImageComponent } from 'src/app/shared/components/upload-image/upload-image.component';
import { UploadAdapter } from 'src/app/model/UploadAdapter';
import { UploadImageService } from 'src/app/core/services/upload-image-service/upload-image.service';
import {
  FormControl,
  FormGroupDirective,
  NgForm,
  Validators,
} from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { MatDialog } from '@angular/material';
// tslint:disable-next-line:max-line-length
import { DialogStepFindingCompanionsComponent } from 'src/app/shared/components/dialog-step-finding-companions/dialog-step-finding-companions.component';
import { Schedule } from 'src/app/model/Schedule';
import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { LocationMarker } from 'src/app/model/LocationMarker';
import { CompanionPost } from 'src/app/model/CompanionPost';
import { ArticleDestinationItem } from 'src/app/model/ArticleDestinationItem';
import { FindingCompanionService } from 'src/app/core/services/post-service/finding-companion.service';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';
import { error } from 'util';
@Component({
  selector: 'app-create-finding-companions-post',
  templateUrl: './create-finding-companions-post.component.html',
  styleUrls: ['./create-finding-companions-post.component.css'],
  providers: [{
    provide: STEPPER_GLOBAL_OPTIONS, useValue: {displayDefaultIndicatorType: false}
  }]
})
export class CreateFindingCompanionsPostComponent implements OnInit {
  constructor(
    private imageService: UploadImageService,
    public dialog: MatDialog,
    private zone: NgZone,
    private companionService: FindingCompanionService
  ) {}
  @ViewChild('uploadImage') uploadImage: UploadImageComponent;
  @ViewChild('myEditor') myEditor;
  @ViewChild('startPicker') startPicker;
  @ViewChild('endPicker') endPicker;
  @ViewChild('stepper') stepper;
  isUpdate: boolean;
  imgUrl;
  isHasImg = false;
  isPublic = true;
  title: string;
  fromDate: Date;
  toDate: Date;
  estimatedDate: Date;
  estAdultAmount: number;
  estChildAmount: number;
  maxMembers: number;
  minMembers: number;
  content = '<p>Hello world!</p>';
  public Editor = DecoupledEditor;
  minDate = new Date(2000, 0, 1);
  maxDate = new Date(2020, 0, 1);
  adultAmountFormControl = new FormControl('adultAmountFormControl', [Validators.required]);

  matcher = new MyErrorStateMatcher();
  listSchedules: Schedule[] = [];
  estimatedCostItems: string[] = [];
  estCostItem = '';
  destinations: ArticleDestinationItem[] = [];
  companionPost: CompanionPost;
  ngOnInit() {
  }
  public onReady(editor) {
    editor.ui
      .getEditableElement()
      .parentElement.insertBefore(
        editor.ui.view.toolbar.element,
        editor.ui.getEditableElement()
      );
    editor.plugins.get('FileRepository').createUploadAdapter = loader => {
      // tslint:disable-next-line:no-string-literal
      console.log(loader['file']);
      return new UploadAdapter(loader, this.imageService);
    };
  }

  fileClick() {
    this.uploadImage.file.nativeElement.click();
  }
  ImageCropted(image) {
    this.imgUrl = image;
    this.isHasImg = true;
  }
  removeImage() {
    this.imgUrl = '';
    this.isHasImg = false;
  }
  // create step
  createStep(schedule: Schedule) {
    if (schedule === undefined || schedule === null) {
      schedule = new Schedule();
    }
    const dialogRef = this.dialog.open(DialogStepFindingCompanionsComponent, {
      width: '60%',
      data: {
        scheduleTitle: schedule.title,
        scheduleDate: schedule.day,
        scheduleNote: schedule.content,
        isUpdate: this.isUpdate
      }
    });
    dialogRef.afterClosed().subscribe(res => {
      if (res !== undefined) {
        schedule = new Schedule();
        // tslint:disable-next-line:no-debugger
        debugger;
        schedule.title = res.scheduleTitle;
        schedule.day = res.scheduleDate;
        schedule.content = res.scheduleNote;
        this.listSchedules.push(schedule);
      }
    });
  }
  openStDateToggle() {
    this.startPicker.open();
  }
  openEnDateToggle() {
    this.endPicker.open();
  }

  // update schedule item
  updateStepper(event) {
    this.createStep(event);
  }

  // delete schedule item
  deleteStepper(event) {
    this.listSchedules.splice(event, 1);
  }

  // add estimated cost item
  addEstimatedCostItem() {
    if (this.estCostItem !== '') {
      this.estimatedCostItems.push(this.estCostItem);
      this.estCostItem = '';
    }
  }

  // remove estimated cost item
  removeEstimatedCostItem(index) {
    this.estimatedCostItems.splice(index, 1);
  }

  // remove destination
  removeDestination(index) {
    this.destinations.splice(index, 1);
  }

  // on google-map-search submit add address location.
  addDestination(addrObj) {
    this.zone.run(() => {
      let addrKeys;
      let addr;
      addr = addrObj;
      addrKeys = Object.keys(addrObj);
      const location: ArticleDestinationItem = {
        id: addrObj.locationId,
        name: addrObj.name
      };
      this.destinations.push(location);
    });

  }

  // create finding companions post
  createPost() {
    this.companionPost = new CompanionPost();
    this.companionPost.from = this.fromDate;
    this.companionPost.to = this.toDate;
    this.companionPost.expiredDate = this.estimatedDate;
    this.companionPost.minMembers = this.minMembers;
    this.companionPost.maxMembers = this.maxMembers;
    this.companionPost.scheduleItems = this.listSchedules;
    this.companionPost.estimatedCostItems = this.estimatedCostItems;
    this.companionPost.post.title = this.title;
    this.companionPost.post.content = this.content;
    this.companionPost.post.isPublic = this.isPublic;
    this.companionPost.post.coverImage = this.imgUrl;
    this.companionPost.destinations = this.destinations;
    this.companionPost.estimatedCost = this.estAdultAmount;
    this.companionService.createPost(this.companionPost).subscribe(res => {
      this.openDialogMessageConfirm('Bàn đăng đã được tạo!', res.id);
    }, (err) => {
        console.log(err);
      }
      , () => {

      }
      );
  }

   // open dialog confirm
   openDialogMessageConfirm(message: string, data) {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '400px',
      height: '200px',
      position: {
        top: '10px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = message;
    instance.message.url = '/bai-viet-tim-ban-dong-hanh/' + data;
  }
}
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(
    control: FormControl | null,
    form: FormGroupDirective | NgForm | null
  ): boolean {
    const isSubmitted = form && form.submitted;
    return !!(
      control &&
      control.invalid &&
      (control.dirty || control.touched || isSubmitted)
    );
  }
}
