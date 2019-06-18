import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { Globals } from 'src/globals/globalvalues';
import { Account } from 'src/app/model/Account';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  email = '';
  password = '';
  account: Account;
  message: string;

  constructor(private globals: Globals, private dialogRef: MatDialogRef<LoginPageComponent>, private userService: UserService) {
    this.account = new Account();
  }

  ngOnInit() {
  }
  callRegisterPage(): void {
    this.dialogRef.close();
  }

  forgotPassword(): void {
    this.dialogRef.close();
  }

  loginFunction() {
    this.account.Email = this.email;
    this.account.Password = this.password;
    this.userService.getAccount(this.account).subscribe((acc: any) => {
        localStorage.setItem('Account', acc);
        localStorage.setItem('Token', acc.token);
        console.log(acc.token);
        // Call http request to userservice để lấy thông tin user
       // this.userService.getUserById(acc.userId).subscribe((result: any) => {
         //  console.log(result);
        // this.dialogRef.close();
        //});
        // Nếu là đăng nhập lần đầu thì redirect sang trang initial-user-information
        // trong initial-user-information gồm có cập nhật tt cá nhân, bước thứ 2 là chọn chủ để quan tâm
        // xem angular material step.
        // cuối cùng là ra trang homepage

    },
    (err: HttpErrorResponse) => {
      this.message = 'Đăng nhập thất bại kiểm tra email hoặc password!';
      console.log(err);
    });
  }
}
