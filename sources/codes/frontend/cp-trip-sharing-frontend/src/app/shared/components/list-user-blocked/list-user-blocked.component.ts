import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { User } from 'src/app/model/User';

@Component({
  selector: 'app-list-user-blocked',
  templateUrl: './list-user-blocked.component.html',
  styleUrls: ['./list-user-blocked.component.css']
})
export class ListUserBlockedComponent implements OnInit {
  listUser: any[];
  listUser2: any[] = [];
  listUser3: any[] = [];
  title = 'Danh sách người dùng bạn đã chặn';
  searchUserName: any;
  constructor(private userService: UserService) {
    this.getListUserBlocked();
  }

  ngOnInit() { }

  getListUserBlocked() {
    const token = localStorage.getItem('Token');
    this.userService.getAllUserBlockedByUserId(token).subscribe((result: any) => {
      this.listUser = result;
      this.listUser.forEach(user => console.log(user));
      this.listUser2 = this.listUser;
    }, (error: HttpErrorResponse) => {
      console.log(error);
    });
  }

  search(searchUser: any) {
    this.listUser = this.listUser2;
    this.listUser3 = [];
    if (searchUser !== '') {
      this.listUser.forEach(user => {
        if (user.displayName.toLowerCase().indexOf(searchUser) > -1) {
          this.listUser3.push(user);
        }
      });
      this.listUser = this.listUser3;
    }
  }
}
