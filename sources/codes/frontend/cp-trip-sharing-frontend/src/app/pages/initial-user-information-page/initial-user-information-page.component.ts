import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { User } from 'src/app/model/User';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Title } from '@angular/platform-browser';
import { UploadImageComponent } from 'src/app/shared/components/upload-image/upload-image.component';

@Component({
  selector: 'app-initial-user-information-page',
  templateUrl: './initial-user-information-page.component.html',
  styleUrls: ['./initial-user-information-page.component.css']
})
export class InitialUserInformationPageComponent implements OnInit {
  @ViewChild('uploadImage') uploadImage: UploadImageComponent;
  isLinear = true;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  user: User = new User();
  isRegister: boolean;
  selectedTopic: string[] = [];
  username = new FormControl('', [Validators.required]);
  displayname = new FormControl('', [Validators.required]);
  firstname = new FormControl('', [Validators.required]);
  lastname = new FormControl('', [Validators.required]);
  address = new FormControl();
  birthday = new FormControl();
  constructor(private formBuilder: FormBuilder, private userService: UserService, private titleService: Title) {
    this.titleService.setTitle('Khởi tạo');
  }
  ngOnInit() {
    console.log(this.user);
    this.firstFormGroup = this.formBuilder.group({});
    this.secondFormGroup = this.formBuilder.group({
      secondCtrl: ['', Validators.required]
    });

    if (!this.user || !this.user.id || this.user.id == null) {
      this.user.avatar = 'https://storage.googleapis.com/trip-sharing-final-image-bucket/no_profile_image.png';
      this.isRegister = true;
    } else {
      this.isRegister = false;
      this.setValueForTextField();
    }
  }

  setValueForTextField() {
    this.username.setValue(this.user.userName);
    this.displayname.setValue(this.user.displayName);
    this.firstname.setValue(this.user.firstName);
    this.lastname.setValue(this.user.lastName);
    this.address.setValue(this.user.address);
    this.birthday.setValue(this.user.dob);
  }
  getErrorMessage() {
    if (this.username.hasError('required') || this.firstname.hasError('required') ||
      this.lastname.hasError('required') || this.address.hasError('required')) {
      return 'Bạn phải nhập thông tin này';
    } else {
      return true;
    }
  }
  callInterestedtopicPage(stepper: MatStepper) {
    this.isLinear = false;
    stepper.next();
  }

  selectedTopics(topics: any) {
    this.user.interested = topics;
  }

  onGenderChange(value: any) {
    this.user.gender = value;
  }

  registerUser() {
    this.getValueFromFormGroup();
    this.userService.registerUser(this.user).subscribe((result: any) => {
      localStorage.setItem('User', JSON.stringify(result));
      window.location.href = '/trang-chu';
    }, (err: HttpErrorResponse) => {
      window.location.href = '/khoi-tao';
    });
  }

  updateUser() {
    this.getValueFromFormGroup();
    this.userService.updateUser(this.user).subscribe((result: any) => {
      window.location.href = '/user/' + this.user.id;
    }, (err: HttpErrorResponse) => {
      console.log(err);
    });
  }

  getValueFromFormGroup() {
    this.user.userName = this.username.value;
    this.user.displayName = this.displayname.value;
    this.user.firstName = this.firstname.value;
    this.user.lastName = this.lastname.value;
    this.user.address = this.address.value;
    this.user.dob = this.birthday.value;
  }

    // change avatar image
    changeAvatar() {
      this.uploadImage.file.nativeElement.click();
    }
    // Image crop
    ImageCropted(image) {
      this.user.avatar = image;
    }
}
