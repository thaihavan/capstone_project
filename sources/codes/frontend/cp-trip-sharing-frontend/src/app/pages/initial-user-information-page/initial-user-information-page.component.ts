import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/model/User';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Title } from '@angular/platform-browser';
import { UploadImageComponent } from 'src/app/shared/components/upload-image/upload-image.component';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { Router } from '@angular/router';

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
              private errorHandler: GlobalErrorHandler,
              private router: Router) {
    this.titleService.setTitle('Khởi tạo');
  }
  ngOnInit() {
    this.checkHasAccount();
    this.firstFormGroup = this.formBuilder.group({
      userName: new FormControl ('', [Validators.required, this.noWhitespaceValidator]),
      displayName: new FormControl ('', [Validators.required, this.noWhitespaceValidator]),
      firstName: new FormControl ('', [Validators.required, this.noWhitespaceValidator]),
      lastName: new FormControl ('', [Validators.required, this.noWhitespaceValidator]),
      birthday: new FormControl ('', [Validators.required, this.noWhitespaceValidator]),
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

  // valid space
  public noWhitespaceValidator(control: FormControl) {
    const isWhitespace = (control.value || '').trim().length === 0;
    let isValid = !isWhitespace;
    if (control.value === '') {
      isValid = true;
    }
    return isValid ? null : { whitespace: true };
}

  // valid user name from server
  validUserName() {
    if (this.username.trim() === '') {
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
        this.router.navigate(['/trang-chu']);
        // window.location.href = '/trang-chu';
      }, (err: HttpErrorResponse) => {
        this.router.navigate(['/khoi-tao']);
        // window.location.href = '/khoi-tao';
      });
    }
  }

  updateUser() {
    this.getValueFromFormGroup();
    this.userService.updateUser(this.user).subscribe((result: any) => {
      this.router.navigate(['/user', this.user.id]);
      // window.location.href = '/user/' + this.user.id;
    }, this.errorHandler.handleError);
  }

  getValueFromFormGroup() {
    this.user.userName = this.username.trim();
    this.user.displayName = this.displayname.trim();
    this.user.firstName = this.firstname.trim();
    this.user.lastName = this.lastname.trim();
    this.user.address = this.address.trim();
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
      this.router.navigate(['/trang-chu']);
      // window.location.href = '/trang-chu';
    }
  }
}
