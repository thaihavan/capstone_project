import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';
import { Account } from 'src/app/model/Account';
import { Globals } from 'src/globals/globalvalues';
import { Title } from '@angular/platform-browser';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit {
  email = '';
  password = '';
  repass = '';
  message: string;
  account: Account;
  error: boolean;
  form = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.email
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(6)
    ]),
    repassword: new FormControl('', [
      Validators.required,
      Validators.minLength(6)
    ]),
    checkbox: new FormControl('', [
      Validators.required
    ])
  });
  constructor(private dialog: MatDialog, private userService: UserService,
              public globals: Globals, private titleService: Title) {
    this.titleService.setTitle('Đăng ký');
    this.account = new Account();
  }

  ngOnInit() {
  }

  onSubmit() {
    if (this.form.value.password === this.form.value.repassword) {
      this.account.Email = this.form.value.email;
      this.account.Password = this.form.value.password;
      this.userService.registerAccount(this.account).subscribe((message: any) => {
        this.openDialogMessageConfirm();
        setTimeout(() => {
          window.location.href = this.globals.urllocal;
        }, 5000);
      }, (err: HttpErrorResponse) => {
        this.error = true;
        this.message = 'Đăng kí thất bại!';
      });
      this.error = false;
    } else {
      this.message = 'Mật khẩu không trùng khớp!';
      this.error = true;
    }

  }

  openDialogMessageConfirm() {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '380px',
      height: '200px',
      position: {
        top: '10px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = 'Đăng kí thành công, vui lòng kiểm tra lại email!';
    instance.message.url = '/trang-chu';
  }
}
