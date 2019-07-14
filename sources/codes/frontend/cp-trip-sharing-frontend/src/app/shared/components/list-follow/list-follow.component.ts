import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/model/User';

@Component({
  selector: 'app-list-follow',
  templateUrl: './list-follow.component.html',
  styleUrls: ['./list-follow.component.css']
})
export class ListFollowComponent implements OnInit {
  title: string;
  listUser: User[];
  listUserSave: User[] = [];
  follow: boolean;
  followed: boolean;
  searchUserName: any;
  constructor() { }

  ngOnInit() {
    this.listUserSave = this.listUser;
  }

  search(searchUser: any) {
    this.listUser = this.listUserSave;
    const listUserSearch: User[] = [];
    if (searchUser !== '') {
      this.listUserSave.forEach(user => {
        if (user.displayName.toLowerCase().indexOf(searchUser) > -1) {
          listUserSearch.push(user);
        }
      });
      this.listUser = listUserSearch;
    }
  }

}
