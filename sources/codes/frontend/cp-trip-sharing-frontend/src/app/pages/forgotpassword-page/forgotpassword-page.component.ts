import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';
import { MatDialog } from '@angular/material';
import { Account } from 'src/app/model/Account';
import { HttpErrorResponse } from '@angular/common/http';
import { Title } from '@angular/platform-browser';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-forgotpassword-page',
  templateUrl: './forgotpassword-page.component.html',
  styleUrls: ['./forgotpassword-page.component.css']
})
export class ForgotpasswordPageComponent implements OnInit {
  acount: Account;
  message: string;
  form = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.email
    ])
  });
  constructor(private dialog: MatDialog, private userService: UserService,
              private titleService: Title) {
    this.acount = new Account();
    this.titleService.setTitle('Quên mật khẩu');
  }

  ngOnInit() {
  }
  forgotPassword() {
    const email = this.form.value.email;
    this.userService.forgotPassword(email).subscribe((data: any) => {
      this.openDialogMessageConfirm('success');
    }, (err: HttpErrorResponse) => {
      this.message = 'Lấy lại mật khẩu thất bại!';
    });
  }
  openDialogMessageConfirm(messageType: string) {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '500px',
      height: 'auto',
      position: {
        top: '20px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageType = messageType;
    instance.message.messageText = 'Kiểm tra Email! Mật khẩu mới đã được gửi về email của bạn.';
    instance.message.url = '/trang-chu';
  }
}
