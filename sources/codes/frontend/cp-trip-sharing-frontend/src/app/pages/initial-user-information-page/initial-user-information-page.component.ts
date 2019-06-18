import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { User } from 'src/app/model/User';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-initial-user-information-page',
  templateUrl: './initial-user-information-page.component.html',
  styleUrls: ['./initial-user-information-page.component.css']
})
export class InitialUserInformationPageComponent implements OnInit {
  isLinear = false;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;

  user: User;
  selectedToppic: string[] = [];
  constructor(private formBuilder: FormBuilder, private userService: UserService) {
    this.user = new User();
    this.user.UserName = '';
    this.user.DisplayName = '';
    this.user.FirstName = '';
    this.user.LastName = '';
    this.user.Dob = null;
    this.user.Gender = true;
    this.user.Address = '';
  }

  ngOnInit() {
    this.firstFormGroup = this.formBuilder.group({
      usernameFormCtrl: ['', Validators.required],
      firstnameFormCtrl: ['', Validators.required],
      lastnameFormCtrl: ['', Validators.required],
      displayNameFormCtrl: ['', Validators.required],
    });
    this.secondFormGroup = this.formBuilder.group({
      secondCtrl: ['', Validators.required]
    });
  }

  callInterestedtopicPage(stepper: MatStepper) {
    console.log(this.user);
    stepper.next();
  }

  selectedToppics(topics) {
    this.user.Interested = topics;
    console.log(this.user.Interested);
  }

  onGenderChange(value) {
    this.user.Gender = value;
  }

  registerUser() {
    debugger;
    this.userService.registerUser(this.user).subscribe((result: any) => {
      window.location.href = '/home';
    }, (err: HttpErrorResponse) => {
      window.location.href = '/initial';
    });
  }
}
