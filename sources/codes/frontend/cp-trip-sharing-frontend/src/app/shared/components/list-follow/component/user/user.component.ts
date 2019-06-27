import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/model/User';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  @Input() user: User;
  user2: any;
  gender: string;
  constructor() { }

  ngOnInit() {
    this.user2 = this.user;
    if (this.user2.gender === true) {
      this.gender = 'Nam';
    } else {
      this.gender = 'Nữ';
    }
  }

}
