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
  avatar = 'https://oto.com.vn/diendan/images/noavatar.jpg';
  constructor(private formBuilder: FormBuilder, private userService: UserService, private titleService: Title) {
    this.titleService.setTitle('Khởi tạo');
    this.user = new User();
    this.user.UserName = '';
    this.user.DisplayName = '';
    this.user.FirstName = '';
    this.user.LastName = '';
    this.user.Dob = null;
    this.user.Gender = true;
    this.user.Address = '';
    this.user.Interested = [];
  }
  isLinear = true;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  user: User;
  isRegister: boolean;
  selectedTopic: string[] = [];
  username = new FormControl('', [Validators.required]);
  displayname = new FormControl('', [Validators.required]);
  firstname = new FormControl('', [Validators.required]);
  lastname = new FormControl('', [Validators.required]);
  address = new FormControl();
  birthday = new FormControl();
  ngOnInit() {
    this.firstFormGroup = this.formBuilder.group({});
    this.secondFormGroup = this.formBuilder.group({
      secondCtrl: ['', Validators.required]
    });

    if (!this.user || !this.user.UserId || this.user.UserId == null) {
      this.isRegister = true;
    } else {
      this.isRegister = false;
      this.setValueForTextField();
    }
  }

  setValueForTextField() {
    this.username.setValue(this.user.UserName);
    this.displayname.setValue(this.user.DisplayName);
    this.firstname.setValue(this.user.FirstName);
    this.lastname.setValue(this.user.LastName);
    this.address.setValue(this.user.Address);
    this.birthday.setValue(this.user.Dob);
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
    this.user.Interested = topics;
  }

  onGenderChange(value: any) {
    this.user.Gender = value;
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
      window.location.href = '/user/' + this.user.UserId;
    }, (err: HttpErrorResponse) => {
      console.log(err);
    });
  }

  getValueFromFormGroup() {
    this.user.UserName = this.username.value;
    this.user.DisplayName = this.displayname.value;
    this.user.FirstName = this.firstname.value;
    this.user.LastName = this.lastname.value;
    this.user.Address = this.address.value;
    this.user.Dob = this.birthday.value;
  }

    // change avatar image
    changeAvatar() {
      this.uploadImage.file.nativeElement.click();
    }
    // Image crop
    ImageCropted(image) {
      this.avatar = image;
    }
}
