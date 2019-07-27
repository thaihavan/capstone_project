import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { User } from 'src/app/model/User';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';

@Component({
  selector: 'app-list-user-blocked',
  templateUrl: './list-user-blocked.component.html',
  styleUrls: ['./list-user-blocked.component.css']
})
export class ListUserBlockedComponent implements OnInit {
  listUser: any[];
  listUserSave: any[];
  title = 'Danh sách người dùng bạn đã chặn';
  searchUserName: any;
  constructor(private userService: UserService,
              private errorHandler: GlobalErrorHandler) {
    this.getListUserBlocked();
  }

  ngOnInit() { }

  getListUserBlocked() {
    const token = localStorage.getItem('Token');
    this.userService.getAllUserBlockedByUserId(token).subscribe((result: any) => {
      this.listUser = result;
      this.listUserSave = this.listUser;
    }, this.errorHandler.handleError);
  }

  search(searchUser: any) {
    this.listUser = this.listUserSave;
    const listUserSearch: any[] = [];
    if (searchUser !== '') {
      this.listUser.forEach(user => {
        if (user.displayName.toLowerCase().indexOf(searchUser) > -1) {
          listUserSearch.push(user);
        }
      });
      this.listUser = listUserSearch;
    }
  }
}
