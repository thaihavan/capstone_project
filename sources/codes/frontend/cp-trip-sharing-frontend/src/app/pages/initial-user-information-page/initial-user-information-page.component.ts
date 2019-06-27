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
  checkedRegister: boolean;
  checkedUpdate: boolean;
  selectedTopic: string[] = [];
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

    });
    this.secondFormGroup = this.formBuilder.group({
      secondCtrl: ['', Validators.required]
    });

    if (this.user == null) {
      this.checkedUpdate = false;
      this.checkedRegister = true;
    } else {
      this.checkedUpdate = true;
      this.checkedRegister = false;
    }
  }

  callInterestedtopicPage(stepper: MatStepper) {
    if (this.user.UserName != null && this.user.FirstName != null && this.user.LastName != null && this.user.DisplayName != null) {
      console.log(this.user);
      stepper.next();
    }

  }

  selectedTopics(topics: any) {
    this.user.Interested = topics;
    console.log(this.user.Interested);
  }

  onGenderChange(value: any) {
    this.user.Gender = value;
  }

  registerUser() {
    this.userService.registerUser(this.user).subscribe((result: any) => {
      localStorage.setItem('User', JSON.stringify(result));
      window.location.href = '/home';
    }, (err: HttpErrorResponse) => {
      window.location.href = '/initial';
    });
  }

  updateUser() {
    this.userService.updateUser(this.user).subscribe((result: any) => {
      window.location.href = '/personal/article?userId=' + this.user.UserId;
    }, (err: HttpErrorResponse) => {
      window.location.href = '/personal/article?userId=' + this.user.UserId;
    });
  }
}
