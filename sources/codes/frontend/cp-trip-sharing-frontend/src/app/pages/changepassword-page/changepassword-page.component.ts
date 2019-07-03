import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ChangePassword } from 'src/app/model/ChangePassword';
import { Title } from '@angular/platform-browser';

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
  changePasswordObject: ChangePassword;

  constructor(private userService: UserService, private titleService: Title) {
    this.changePasswordObject = new ChangePassword();
    this.titleService.setTitle('Đổi mật khẩu');
  }

  ngOnInit() {
  }
  changePassword() {
     if (this.renewpassword !== this.newpassword) {
       this.message = 'Mật khẩu không trùng khớp';
     } else {
      this.changePasswordObject.CurrentPassword = this.oldpassword;
      this.changePasswordObject.NewPassword = this.newpassword;
      this.userService.changePassword(this.changePasswordObject).subscribe((message: any) => {
        this.message = 'Đổi mật khẩu thành công';
      }, (err: HttpErrorResponse) => {
        this.message = 'Đổi mật khẩu thất bại!';
      });
     }
  }
}
