import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { Title } from '@angular/platform-browser';
import { Account } from 'src/app/model/Account';
import { AuthService, GoogleLoginProvider } from 'angularx-social-login';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NotifyService } from 'src/app/core/services/notify-service/notify.service';

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
  listUserIdFollowing: string[] = [];
  listPostIdBookMark: string[] = [];
  form = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.email
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(5)
    ])
  });
  constructor(private titleService: Title,
              private dialogRef: MatDialogRef<LoginPageComponent>,
              private userService: UserService,
              private authService: AuthService,
              private notifyService: NotifyService) {
    this.titleService.setTitle('Đăng nhập');
    this.account = new Account();
  }

  ngOnInit() {
  }

  signInWithGoogle(): void {
    this.authService.signIn(GoogleLoginProvider.PROVIDER_ID).then((gUser) => {
      console.log(gUser);
      this.userService.loginWithgoogle(gUser.authToken).subscribe((account: any) => {
        localStorage.setItem('Account', JSON.stringify(account));
        localStorage.setItem('Token', account.token);
        // Call http request to userservice để lấy thông tin user
        this.userService.getUserById(account.userId).subscribe((user: any) => {
          if (user == null) {
            window.location.href = '/khoi-tao';
          } else {
            localStorage.setItem('User', JSON.stringify(user));
            this.getFollowings();
            this.getListPostIdBookmark();
            window.location.href = '/trang-chu';
          }
        });
      }, (error: HttpErrorResponse) => {
        this.message = 'Đăng nhập bằng Google thất bại!';
        console.log(error);
      });
    }).catch((error) => {
      console.log(error);
    });
  }

  callRegisterPage(): void {
    this.dialogRef.close();
  }

  forgotPassword(): void {
    this.dialogRef.close();
  }

  loginFunction() {
    this.account.Email = this.form.value.email;
    this.account.Password = this.form.value.password;
    this.userService.getAccount(this.account).subscribe((acc: any) => {
      localStorage.setItem('Account', JSON.stringify(acc));
      localStorage.setItem('Token', acc.token);
      // Call http request to userservice để lấy thông tin user
      this.userService.getUserById(acc.userId).subscribe((user: any) => {
        if (user == null) {
          window.location.href = '/khoi-tao';
        } else {
          localStorage.setItem('User', JSON.stringify(user));
          this.getFollowings();
          this.getListPostIdBookmark();
          window.location.href = '/trang-chu';
        }
      });
    }, (err: HttpErrorResponse) => {
      this.message = 'Đăng nhập thất bại kiểm tra email hoặc password!';
      console.log(err);
    });
  }

  getFollowings() {
    const token = localStorage.getItem('Token');
    if (token != null) {
      this.userService.getAllFollowingId(token).subscribe((result: any) => {
        this.listUserIdFollowing = result;
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }

  getListPostIdBookmark() {
    const token = localStorage.getItem('Token');
    if (token != null) {
      this.userService.getListPostIdBookmarks(token).subscribe((result: any) => {
        this.listPostIdBookMark = result;
        localStorage.setItem('listPostIdBookmark', JSON.stringify(this.listPostIdBookMark));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }
}
