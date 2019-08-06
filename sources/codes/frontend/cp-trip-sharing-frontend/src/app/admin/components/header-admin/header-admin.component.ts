import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Account } from 'src/app/model/Account';
import { User } from 'src/app/model/User';

@Component({
  selector: 'app-header-admin',
  templateUrl: './header-admin.component.html',
  styleUrls: ['./header-admin.component.css']
})
export class HeaderAdminComponent implements OnInit {
  account: Account;
  user: User;

  constructor(private userService: UserService) {
    this.account = JSON.parse(sessionStorage.getItem('Account'));
    this.user = JSON.parse(sessionStorage.getItem('User'));
  }

  ngOnInit() {
  }

  signOut() {
    localStorage.clear();
    this.userService.Logout().subscribe((res: any) => {
      window.location.href = '';
    }, (error: HttpErrorResponse) => {
      console.log(error);
      window.location.href = '';
    });
  }

}
