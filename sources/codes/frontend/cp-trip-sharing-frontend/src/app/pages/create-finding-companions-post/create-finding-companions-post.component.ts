import {
  Component,
  OnInit,
  ViewChild,
  NgZone,
  AfterViewInit
} from '@angular/core';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { UploadImageComponent } from 'src/app/shared/components/upload-image/upload-image.component';
import { UploadAdapter } from 'src/app/model/UploadAdapter';
import { UploadImageService } from 'src/app/core/services/upload-image-service/upload-image.service';
import {
  FormControl,
  FormGroupDirective,
  NgForm,
  Validators,
  FormGroup,
  FormBuilder
} from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { MatDialog } from '@angular/material';
// tslint:disable-next-line:max-line-length
import { DialogStepFindingCompanionsComponent } from 'src/app/shared/components/dialog-step-finding-companions/dialog-step-finding-companions.component';
import { Schedule } from 'src/app/model/Schedule';
import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { CompanionPost } from 'src/app/model/CompanionPost';
import { ArticleDestinationItem } from 'src/app/model/ArticleDestinationItem';
import { FindingCompanionService } from 'src/app/core/services/post-service/finding-companion.service';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';
import { AlertifyService } from 'src/app/core/services/alertify-service/alertify.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-create-finding-companions-post',
  templateUrl: './create-finding-companions-post.component.html',
  styleUrls: ['./create-finding-companions-post.component.css'],
  providers: [
    {
      provide: STEPPER_GLOBAL_OPTIONS,
      useValue: { displayDefaultIndicatorType: false }
    }
  ]
})
export class CreateFindingCompanionsPostComponent
  implements OnInit, AfterViewInit {
  constructor(
    private imageService: UploadImageService,
    public dialog: MatDialog,
    private zone: NgZone,
    private companionService: FindingCompanionService,
    private fb: FormBuilder,
    private alertifyService: AlertifyService,
    private route: ActivatedRoute,
    private postCopmanionService: FindingCompanionService
  ) {
    this.initForm();
  }
  @ViewChild('uploadImage') uploadImage: UploadImageComponent;
  @ViewChild('myEditor') myEditor;
  @ViewChild('startPicker') startPicker;
  @ViewChild('endPicker') endPicker;
  @ViewChild('stepper') stepper;
  isUpdate: boolean;
  imgUrl;
  user: any;
  isHasImg = false;
  isPublic = true;
  title: string;
  fromDate: Date;
  toDate: Date;
  estimatedDate: Date;
  estAdultAmount = '';
  maxMembers: number;
  minMembers: number;
  isMemberValide = true;
  content = '';
  public Editor = DecoupledEditor;
  currentDate = new Date();
  minDate = new Date();
  maxDate = new Date(2020, 0, 1);
  companionForm: FormGroup;
  // tslint:disable-next-line: no-use-before-declare
  memfileMatcher = new CrossFieldErrorMatcher();

  listSchedules: Schedule[] = [];
  estimatedCostItems: string[] = [];
  estCostItem = '';
  destinations: ArticleDestinationItem[] = [];
  companionPost: CompanionPost;
  companionPostId: string;

  ngOnInit() {
    this.user = localStorage.getItem('User');
    if (this.user == null) {
      window.location.href = '/trang-chu';
    } else {
    this.companionPost = new CompanionPost();
    this.minDate.setDate(this.minDate.getDate() + 1);
    this.companionPostId = this.route.snapshot.paramMap.get('companionId');
    if (
      this.companionPostId !== undefined &&
      this.companionPostId !== null &&
      this.companionPostId !== ''
    ) {
      this.isUpdate = true;
      this.postCopmanionService.getPost(this.companionPostId).subscribe(
        res => {
          this.companionPost = res;
          this.fromDate = this.companionPost.from ;
          this.toDate = this.companionPost.to ;
          this.estimatedDate = this.companionPost.expiredDate ;
          this.minMembers = this.companionPost.minMembers ;
          this.maxMembers = this.companionPost.maxMembers ;
          this.estimatedCostItems = this.companionPost.estimatedCostItems ;
          this.title = this.companionPost.post.title;
          this.content = this.companionPost.post.content ;
          this.isPublic = this.companionPost.post.isPublic ;
          this.imgUrl = this.companionPost.post.coverImage ;
          this.isHasImg = true;
          this.destinations = this.companionPost.destinations ;
          this.estAdultAmount = this.onAmountChange(this.companionPost.estimatedCost.toString());
          setTimeout( () => {this.listSchedules = this.companionPost.scheduleItems; }, 2000);
        }
      );
    }
  }
  }
  ngAfterViewInit(): void {
  }
  public onReady(editor) {
    editor.ui
      .getEditableElement()
      .parentElement.insertBefore(
        editor.ui.view.toolbar.element,
        editor.ui.getEditableElement()
      );
    editor.plugins.get('FileRepository').createUploadAdapter = loader => {
      return new UploadAdapter(loader, this.imageService);
    };
  }

  // init validation form
  initForm() {
    this.companionForm = this.fb.group(
      {
        fromDate: new FormControl('', [Validators.required]),
        toDate: new FormControl('', [Validators.required]),
        estimatedDate: new FormControl('', [Validators.required]),
        minMembers: new FormControl('', [Validators.required, Validators.max(100), Validators.min(1)]),
        maxMembers: new FormControl('', [Validators.required, Validators.max(100), Validators.min(1)]),
        estAdultAmount: new FormControl()
      },
      {
        validator: this.validateMember
      }
    );
  }

  // form check has validation error
  public hasError = (controlName: string, errorName: string) => {
    return this.companionForm.controls[controlName].hasError(errorName);
  }

  // form check has validation error min max member
  hasMemberError = (controlName: string) => {
    const formHasError = this.companionForm.hasError('isNotValidateMember');
    const dirty = this.companionForm.controls[controlName].dirty;
    return formHasError && dirty;
  }

  // form validation amount number only
  validationAmount(form: FormControl) {
    let amount: string = form.value;
    const arrAmout = amount.split(',');
    amount = '';
    arrAmout.forEach(element => {
      amount += element;
    });
    const checkNum = Number(amount);
    return checkNum ? null : { isNotANumber: true };
  }
  // form validation min max members
  validateMember(form: FormGroup) {
    const condition =
      form.get('minMembers').value >= form.get('maxMembers').value;
    return condition ? { isNotValidateMember: true } : null;
  }

  // on amount change
  onAmountChange(str) {
    let text: string = str.toString();
    text = text.replace(/[^\d,]/g, '0');
    const arrText = text.split(',');
    text = '';
    arrText.forEach(element => {
      text += element;
    });
    let convertStr = '';
    let strLength = 0;
    for (let index = text.length - 1; index >= 0; index--) {
      strLength += 1;
      convertStr = text.charAt(index) + convertStr;
      if (strLength % 3 === 0 && index !== text.length && index !== 0) {
        convertStr = ',' + convertStr;
      }
    }
    return convertStr;
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
  createStep(schedule: Schedule, update: boolean) {
    if (this.fromDate === undefined || this.toDate === undefined) {
      this.alertifyService.error('Yêu cầu nhập ngày đi!');
      return;
    }
    if (schedule === undefined || schedule === null) {
      schedule = new Schedule();
    }
    const dialogRef = this.dialog.open(DialogStepFindingCompanionsComponent, {
      width: '60%',
      data: {
        scheduleTitle: schedule.title,
        scheduleDate: schedule.day,
        scheduleNote: schedule.content,
        isUpdate: update,
        minDate: this.fromDate,
        maxDate: this.toDate
      }
    });
    dialogRef.afterClosed().subscribe(res => {
      if (res !== undefined) {
       const updateSchedule = new Schedule();
       updateSchedule.title = res.scheduleTitle;
       updateSchedule.day = res.scheduleDate;
       updateSchedule.content = res.scheduleNote;
       if (update) {
         const index =  this.listSchedules.findIndex(s => schedule.title === s.title);
         this.listSchedules[index] = updateSchedule;
        } else {
          this.listSchedules.push(updateSchedule);
        }
      }
    });
  }
  openStDateToggle() {
    this.startPicker.open();
  }
  openEnDateToggle() {
    this.endPicker.open();
  }

  // Expires date
  maxExpiresDate(fromDate) {
    if (fromDate) {
      // tslint:disable-next-line:prefer-const
      let fDate = new Date(fromDate);
      fDate.setDate(fDate.getDate() - 1);
      return fDate;
    }
    return fromDate;
  }
  // update schedule item
  updateStepper(event) {
    this.createStep(event, true);
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
    if (!addrObj) {
      this.alertifyService.error('Địa điểm không tồn tại!');
      return;
    }
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
    if (!this.imgUrl) {
      this.alertifyService.error('Yêu cầu ảnh bìa cho bài viết');
      this.goToTop(0);
      return;
    }
    if (this.destinations.length < 1) {
      this.alertifyService.error('Yêu cầu nhập địa điểm cho chuyến đi');
      this.goToTop(200);
      return;
    }
    if (this.companionForm.invalid) {
      return;
    }
    if (this.content.trim() === '') {
      this.alertifyService.error('Yêu cầu nhập đầy đủ thông tin');
      this.goToTop(600);
      return;
    }
    if (!this.isUpdate) {
      this.companionPost = new CompanionPost();
    }
    this.companionPost.from = this.fromDate;
    this.companionPost.to = this.toDate;
    this.companionPost.expiredDate = this.estimatedDate;
    this.companionPost.minMembers = this.minMembers;
    this.companionPost.maxMembers = this.maxMembers;
    this.companionPost.scheduleItems = this.listSchedules;
    this.companionPost.estimatedCostItems = this.estimatedCostItems;
    this.companionPost.estimatedCost = Number(
      this.estAdultAmount.replace(/\,/g, '')
    );
    this.companionPost.post.title = this.title;
    this.companionPost.post.content = this.content;
    this.companionPost.post.isPublic = this.isPublic;
    this.companionPost.post.coverImage = this.imgUrl;
    this.companionPost.destinations = this.destinations;
    if (this.isUpdate) {
      this.companionService.updatePost(this.companionPost).subscribe(
        res => {
          this.openDialogMessageConfirm('Bạn đã cập nhật thành công!', res.id , 'success' );
        },
        (error) => {
          this.openDialogMessageConfirm(error.message, null, 'danger');
        }
      );
      return;
    }
    this.companionService.createPost(this.companionPost).subscribe(
        res => {
          this.openDialogMessageConfirm('Bàn đăng đã được tạo!', res.id, 'success' );
        },
        (err: HttpErrorResponse) => {
          this.openDialogMessageConfirm(err.message, null, 'danger');
        }
      );
  }

  // on submit form
  onSubmit() {
    // stop here if form is invalid
    if (!this.imgUrl) {
      return;
    }
    if (this.destinations.length < 1) {
      return;
    }
    if (this.companionForm.invalid) {
      this.alertifyService.error('Lỗi thông tin bài viết!');
      this.goToTop(200);
      return;
  }
  }

  // scroll to top
  goToTop(height) {
    window.scroll({
      top: height,
      left: 0,
      behavior: 'smooth'
    });
  }
  // open dialog confirm
  openDialogMessageConfirm(message: string, data, messageType: string) {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '500px',
      height: 'auto',
      position: {
        top: '20px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = message;
    instance.message.messageType = messageType;
    instance.message.url = '/tim-ban-dong-hanh/' + data;
  }

}

class CrossFieldErrorMatcher implements ErrorStateMatcher {
  isErrorState(
    control: FormControl | null,
    form: FormGroupDirective | NgForm | null
  ): boolean {
    const isSubmitted = form && form.submitted;
    return (
      (control.dirty || control.touched || isSubmitted) &&
      (form.hasError('isNotValidateMember') || control.invalid)
    );
  }
}
