import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/model/User';

@Component({
  selector: 'app-list-follow',
  templateUrl: './list-follow.component.html',
  styleUrls: ['./list-follow.component.css']
})
export class ListFollowComponent implements OnInit {
  title: string;
  listUser: User[];
  listUser2: any[] = [];
  listUser3: any[] = [];
  searchUserName: any;
  constructor() { }

  ngOnInit() {
    this.listUser2 = this.listUser;
  }

  search(searchUser: any) {
    this.listUser2 = this.listUser;
    this.listUser3 = [];
    if (searchUser !== '') {
      this.listUser2.forEach(user => {
        if (user.displayName.toLowerCase().indexOf(searchUser) > -1) {
          this.listUser3.push(user);
        }
      });
      this.listUser2 = this.listUser3;
    }
  }

}
