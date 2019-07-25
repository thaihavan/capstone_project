import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { Title } from '@angular/platform-browser';
import { Account } from 'src/app/model/Account';
import { AuthService, GoogleLoginProvider, FacebookLoginProvider } from 'angularx-social-login';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NotifyService } from 'src/app/core/services/notify-service/notify.service';
import { AlertifyService } from 'src/app/core/services/alertify-service/alertify.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  email = '';
  password = '';
  pasHide = true;
  isLoading = false;
  account: Account;
  message: string;
  isInvalEmailPass = false;
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
              private notifyService: NotifyService,
              private alertifyService: AlertifyService) {
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
            window.location.href = '/';
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

  signInWithFb(): void {
    this.authService.signIn(FacebookLoginProvider.PROVIDER_ID).then((fUser) => {
      console.log(fUser);
      // this.userService.loginWithgoogle(fUser.authToken).subscribe((account: any) => {
      //   localStorage.setItem('Account', JSON.stringify(account));
      //   localStorage.setItem('Token', account.token);
      //   // Call http request to userservice để lấy thông tin user
      //   this.userService.getUserById(account.userId).subscribe((user: any) => {
      //     if (user == null) {
      //       window.location.href = '/khoi-tao';
      //     } else {
      //       localStorage.setItem('User', JSON.stringify(user));
      //       window.location.href = '/';
      //     }
      //   });
      // }, (error: HttpErrorResponse) => {
      //   this.message = 'Đăng nhập bằng Google thất bại!';
      //   console.log(error);
      // });
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
    if (!this.form.invalid) {
      this.isLoading = true;
      this.account.email = this.form.value.email;
      this.account.password = this.form.value.password;
      this.userService.getAccount(this.account).subscribe((acc: any) => {
        localStorage.setItem('Account', JSON.stringify(acc));
        localStorage.setItem('Token', acc.token);
        // Call http request to userservice để lấy thông tin user
        this.userService.getUserById(acc.userId).subscribe((user: any) => {
          if (user == null) {
            window.location.href = '/khoi-tao';
          } else {
            localStorage.setItem('User', JSON.stringify(user));
            window.location.href = '/';
          }
        });
      }, (err: HttpErrorResponse) => {
        this.isLoading = false;
        if (err.error.message === 'Email or password is incorrect') {
          this.alertifyService.error('Tài khoản hoặc mật khẩu chưa đúng');
          this.isInvalEmailPass = true;
        }
      },
      () => {
        this.isInvalEmailPass = false;
      });
    }
  }

}
