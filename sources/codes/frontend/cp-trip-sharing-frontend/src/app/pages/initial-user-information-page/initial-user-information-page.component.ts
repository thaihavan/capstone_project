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
  selectedToppic: Topic[] = [];
  constructor(private formBuilder: FormBuilder, private userService: UserService) {
    this.expression = false;
    this.user = new User();
  }

  ngOnInit() {
    this.firstFormGroup = this.formBuilder.group({
      firstCtrl: ['', Validators.required]
    });
    this.secondFormGroup = this.formBuilder.group({
      secondCtrl: ['', Validators.required]
    });
    this.secondFormGroup = this.formBuilder.group({
      check: [false, Validators.requiredTrue]
    });
  }

  callInterestedtopicPage(stepper: MatStepper) {
    if (this.firstname !== '' && this.lastname !== '' && this.dob !== '' &&
      this.phonenumber !== '' && this.address !== '') {
      console.log(this.firstname);
      console.log(this.lastname);
      console.log(this.dob);
      console.log(this.phonenumber);
      console.log(this.address);
      console.log(this.female);
      console.log(this.male);
      // this.user.FirstName = this.firstname;
      this.user.LastName = this.lastname;
      this.user.Dob = new Date(this.dob);
      this.user.Interested = this.selectedToppic;
      this.expression = true;
      setTimeout(() => {
        stepper.next();
      }, 80);
    }
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
