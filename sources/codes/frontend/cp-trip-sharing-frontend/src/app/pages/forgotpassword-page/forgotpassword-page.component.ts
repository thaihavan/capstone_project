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
  isLoading = false;
  hasErrorMessage = false;
  email: string;
  message: string;
  form = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email])
  });
  constructor(
    private dialog: MatDialog,
    private userService: UserService,
    private titleService: Title
  ) {
    this.titleService.setTitle('Quên mật khẩu');
  }

  ngOnInit() {}

  // form check has validation error
  public hasError = (controlName: string, errorName: string) => {
    return this.form.controls[controlName].hasError(errorName);
  }

  forgotPassword() {
    this.isLoading = true;
    this.userService.forgotPassword(this.email).subscribe(
      (data: any) => {
        this.openDialogMessageConfirm('success');
      },
      (err: HttpErrorResponse) => {
        this.isLoading = false;
        this.hasErrorMessage = true;
        this.message = 'Lấy lại mật khẩu thất bại!';
      },
      () => {
        this.isLoading = false;
        this.openDialogMessageConfirm('success');
      }
    );
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
    instance.message.messageText =
      'Kiểm tra Email! Mật khẩu mới đã được gửi về email của bạn.';
    instance.message.url = '/trang-chu';
  }
  onSubmit() {
    this.forgotPassword();
  }
}
