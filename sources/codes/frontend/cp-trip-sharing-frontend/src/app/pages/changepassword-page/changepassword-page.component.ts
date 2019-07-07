import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ChangePassword } from 'src/app/model/ChangePassword';
import { Title } from '@angular/platform-browser';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-changepassword-page',
  templateUrl: './changepassword-page.component.html',
  styleUrls: ['./changepassword-page.component.css']
})
export class ChangepasswordPageComponent implements OnInit {
  error = false;
  message: string;
  changePasswordObject: ChangePassword;
  form = new FormGroup({
    oldpassword: new FormControl('', [
      Validators.required,
      Validators.minLength(5)
    ]),
    newpassword: new FormControl('', [
      Validators.required,
      Validators.minLength(5)
    ]),
    repassword: new FormControl('', [
      Validators.required,
      Validators.minLength(5)
    ]),
  });
  constructor(private userService: UserService, private titleService: Title) {
    this.changePasswordObject = new ChangePassword();
    this.titleService.setTitle('Đổi mật khẩu');
  }

  ngOnInit() {
  }
  changePassword() {
     if (this.form.value.newpassword !== this.form.value.repassword) {
       this.message = 'Mật khẩu không trùng khớp';
     } else {
      this.changePasswordObject.CurrentPassword = this.form.value.oldpassword;
      this.changePasswordObject.NewPassword = this.form.value.newpassword;
      this.userService.changePassword(this.changePasswordObject).subscribe((message: any) => {
        this.message = 'Đổi mật khẩu thành công';
      }, (err: HttpErrorResponse) => {
        this.message = 'Đổi mật khẩu thất bại!';
      });
     }
     this.error = true;
  }
}
