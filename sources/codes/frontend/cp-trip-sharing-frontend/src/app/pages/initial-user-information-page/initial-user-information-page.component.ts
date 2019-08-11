import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/model/User';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Title } from '@angular/platform-browser';
import { UploadImageComponent } from 'src/app/shared/components/upload-image/upload-image.component';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';

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
  username = '';
  displayname = '';
  firstname = '';
  lastname = '';
  address = '';
  gender = 'true';
  birthday: Date;
  fakeinput = '';
  isValidUserName = true;
  constructor(private formBuilder: FormBuilder,
              private userService: UserService,
              private titleService: Title,
              private errorHandler: GlobalErrorHandler) {
    this.titleService.setTitle('Khởi tạo');
  }
  ngOnInit() {
    this.checkHasAccount();
    this.firstFormGroup = this.formBuilder.group({
      userName: ['', Validators.required],
      displayName: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      birthday: ['', Validators.required],
      address: [''],
      gender: ['true']
    });
    this.secondFormGroup = this.formBuilder.group({
      fakeinput: ['', Validators.required]
    });

    if (!this.user || !this.user.id || this.user.id == null) {
      this.user.avatar = 'https://storage.googleapis.com/trip-sharing-cp-image-bucket/image-default-user-avatar.png';
      this.isRegister = true;
    } else {
      this.isRegister = false;
      this.setValueForTextField();
    }
  }

  setValueForTextField() {
    this.username = this.user.userName;
    this.displayname = this.user.displayName;
    this.firstname = this.user.firstName;
    this.lastname = this.user.lastName;
    this.address = this.user.address;
    this.birthday = this.user.dob;
    this.gender = this.user.gender ? 'true' : 'false';
  }
  // form check has validation error
  public hasError = (controlName: string, errorName: string) => {
    return this.firstFormGroup.controls[controlName].hasError(errorName);
  }

  // valid user name from server
  validUserName() {
    if (this.username === '') {
      return;
    }
    this.userService.checkValidateUserName(this.username).subscribe(res => {
    },
      (error) => {
        this.isValidUserName = false;
        this.firstFormGroup.controls.userName.setErrors({
          notMatched: true
        });
      },
      () => {
        this.isValidUserName = true;
        this.firstFormGroup.controls.userName.setErrors(null);
      }
    );
  }

  selectedTopics(topics: any) {
    this.user.interested = topics;
    if (this.user.interested.length !== 0) {
      this.fakeinput = 'has topic';
    } else {
      this.fakeinput = '';
    }
  }

  registerUser() {
    if (this.secondFormGroup.valid) {
      this.getValueFromFormGroup();
      this.userService.registerUser(this.user).subscribe((result: any) => {
        localStorage.setItem('User', JSON.stringify(result));
        window.location.href = '/trang-chu';
      }, (err: HttpErrorResponse) => {
        window.location.href = '/khoi-tao';
      });
    }
  }

  updateUser() {
    this.getValueFromFormGroup();
    this.userService.updateUser(this.user).subscribe((result: any) => {
      window.location.href = '/user/' + this.user.id;
    }, this.errorHandler.handleError);
  }

  getValueFromFormGroup() {
    this.user.userName = this.username;
    this.user.displayName = this.displayname;
    this.user.firstName = this.firstname;
    this.user.lastName = this.lastname;
    this.user.address = this.address;
    this.user.dob = this.birthday;
    this.user.gender = this.gender === 'true' ? true : false;
  }

  // change avatar image
  changeAvatar() {
    this.uploadImage.file.nativeElement.click();
  }
  // Image crop
  ImageCropted(image) {
    this.user.avatar = image;
  }
  // check has account?
  checkHasAccount() {
    const account = JSON.parse(localStorage.getItem('Account'));
    if (account === null) {
      window.location.href = '/trang-chu';
    }
  }
}
