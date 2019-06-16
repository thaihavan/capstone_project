import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Account } from 'src/Model/Account';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Globals } from 'src/globals/globalvalues';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  email : string = "";
  password : string = "";
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
        localStorage.setItem("UserId",acc.userId);
        localStorage.setItem('Token', acc.token);
        localStorage.setItem('Email', acc.email);
        localStorage.setItem('Username', acc.username);
        localStorage.setItem('Role', acc.role);
        window.location.href = this.globals.urllocal;
        this.dialogRef.close();
        // Nếu là đăng nhập lần đầu thì redirect sang trang initial-user-information
        // trong initial-user-information gồm có cập nhật tt cá nhân, bước thứ 2 là chọn chủ để quan tâm
        // xem angular material step.
        // cuối cùng là ra trang homepage
    },(err: HttpErrorResponse) => { 
      this.message = 'Đăng nhập thất bại kiểm tra email hoặc password!';
      console.log(err); });
  }
}
