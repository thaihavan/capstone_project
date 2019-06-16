import { Component, OnInit } from '@angular/core';
import { Account } from 'src/Model/Account';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-changepassword-page',
  templateUrl: './changepassword-page.component.html',
  styleUrls: ['./changepassword-page.component.css']
})
export class ChangepasswordPageComponent implements OnInit {
  oldpassword: string;
  newpassword: string;
  renewpassword: string;
  message: string;
  account: Account;

  constructor(private userService: UserService) {
    this.account = new Account();
  }

  ngOnInit() {
  }
  changePassword() {
     if (this.renewpassword !== this.newpassword) {
       this.message = 'Change Password Fail!';
     } else {
      this.account.OldPassword = this.oldpassword;
      this.account.NewPassword = this.newpassword;
      this.userService.changePassword(this.account).subscribe((message: any) => {
        this.message = 'Đổi mật khẩu thành công';
      }, (err: HttpErrorResponse) => {
        this.message = 'Đăng nhập thất bại kiểm tra email hoặc password!';
        console.log(err);
      });
     }
  }
}
