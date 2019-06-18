import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { Topic } from 'src/app/pages/interestedtopic-page/interestedtopic-page.component';
import { User } from 'src/app/model/User';
import { UserService } from 'src/app/core/services/user-service/user.service';

@Component({
  selector: 'app-initial-user-information-page',
  templateUrl: './initial-user-information-page.component.html',
  styleUrls: ['./initial-user-information-page.component.css']
})
export class InitialUserInformationPageComponent implements OnInit {
  isLinear = false;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  firstname = '';
  phonenumber = '';
  lastname = '';
  dob = '';
  address = '';
  expression: boolean;
  male = '';
  female = '';
  user: User;
  selectedToppic: string[] = [];
  constructor(private formBuilder: FormBuilder, private userService: UserService) {

    this.user = new User();
  }

  ngOnInit() {
    this.firstFormGroup = this.formBuilder.group({
      firstCtrl: ['', Validators.required]
    });
    this.secondFormGroup = this.formBuilder.group({
      secondCtrl: ['', Validators.required]
    });
  }

  callInterestedtopicPage(stepper: MatStepper) {
      console.log(this.firstname);
      console.log(this.lastname);
      console.log(this.dob);
      console.log(this.phonenumber);
      console.log(this.address);
      console.log(this.female);
      console.log(this.male);

  }

  selectedToppics(topics) {
    this.selectedToppic = topics;
    console.log(this.selectedToppic);
  }

  registerUser() {
    this.userService.registerUser(this.user).subscribe((result: any) => {
      console.log(result);
    });
  }
}
