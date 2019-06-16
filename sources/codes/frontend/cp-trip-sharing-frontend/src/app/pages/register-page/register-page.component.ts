import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { InterestedtopicPageComponent } from '../interestedtopic-page/interestedtopic-page.component';
import { Account } from 'src/Model/Account';
import { HttpErrorResponse } from '@angular/common/http';
import { LoginPageComponent } from 'src/app/pages/login-page/login-page.component'

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit {
  username: string = "";
  email: string = "";
  password: string = "";
  repass: string = "";
  account: Account;
  message: string;
  constructor(private dialog: MatDialog, private userService: UserService) {
    this.account = new Account();
  }

  ngOnInit() {
  }

  callInterestedtopicPage(): void {
    this.account.Email = this.email;
    this.account.Password = this.password;
    this.account.Username = this.username;
    this.userService.registerAccount(this.account).subscribe((message: any) => {
      // TODO: hiển thị popup đăng ký thành công, vui long kiểm tra email.
      // user click OK thì redirect sang trang login.
    }, (err: HttpErrorResponse) => {
      this.message = 'Đăng kí thất bại!';
    });
  }

  openDialogLoginForm() {
    const dialogRef = this.dialog.open(LoginPageComponent, {
      height: 'auto',
      width: '400px'
    });
  }
}
