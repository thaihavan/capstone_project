import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { User } from 'src/app/model/User';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-initial-user-information-page',
  templateUrl: './initial-user-information-page.component.html',
  styleUrls: ['./initial-user-information-page.component.css']
})
export class InitialUserInformationPageComponent implements OnInit {
  isLinear = true;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  user: User;
  isRegister: boolean;
  selectedTopic: string[] = [];
  form = new FormGroup({
    username: new FormControl('', [
      Validators.required
    ]),
    displayName: new FormControl('', [
      Validators.required
    ]),
    firstname: new FormControl('', [
      Validators.required
    ]),
    lastname: new FormControl('', [
      Validators.required
    ]),
    address: new FormControl()
  });
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

  ngOnInit() {
    this.firstFormGroup = this.formBuilder.group({

    });
    this.secondFormGroup = this.formBuilder.group({
      secondCtrl: ['', Validators.required]
    });

    if (!this.user || !this.user.UserId || this.user.UserId == null) {
      this.isRegister = true;
    } else {
      this.isRegister = false;
      console.log(this.user);
      this.form.setValue({
        username: this.user.UserName,
        displayName: this.user.DisplayName,
        firstname: this.user.FirstName,
        lastname: this.user.LastName,
        address: this.user.Address
      });

      this.form.patchValue({ dob: this.user.Dob });
    }
  }

  callInterestedtopicPage(stepper: MatStepper) {
    this.isLinear = false;
    stepper.next();
  }

  selectedTopics(topics: any) {
    this.user.Interested = topics;
    console.log(this.user.Interested);
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
      window.location.href = '/user/' + this.user.UserId;
    });
  }

  getValueFromFormGroup() {
    this.user.UserName = this.form.value.username;
    this.user.DisplayName = this.form.value.displayName;
    this.user.FirstName = this.form.value.firstname;
    this.user.LastName = this.form.value.lastname;
    this.user.Address =  this.form.value.address;
  }
}
