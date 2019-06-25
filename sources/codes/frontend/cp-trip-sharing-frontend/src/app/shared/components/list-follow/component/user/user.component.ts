import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/model/User';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  @Input() user: User;
  gender: string;
  constructor() { }

  ngOnInit() {
    console.log(this.user);
    if (this.user.Gender === true) {
      this.gender = 'Name';
    } else {
      this.gender = 'Nữ';
    }
  }

}
