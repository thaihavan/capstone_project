import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';
import { MatDialog } from '@angular/material';
import { ResetPasswordModel } from 'src/app/model/ResetPasswordModel';
import { HttpErrorResponse } from '@angular/common/http';
import { Title } from '@angular/platform-browser';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-reset-password-page',
  templateUrl: './reset-password-page.component.html',
  styleUrls: ['./reset-password-page.component.css']
})
export class ResetPasswordPageComponent implements OnInit {
  token: string = null;
  message: string = null;
  resetPasswordModel: ResetPasswordModel;
  form = new FormGroup({
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(5)
    ]),
    repassword: new FormControl('', [
      Validators.required,
      Validators.minLength(5)
    ])
  });
  constructor(private route: ActivatedRoute, private dialog: MatDialog, private userService: UserService,
              private titleService: Title) {
    this.titleService.setTitle('Đặt lại mật khẩu');
    this.resetPasswordModel = new ResetPasswordModel();
  }

  ngOnInit() {
    this.token = this.route.snapshot.paramMap.get('token');
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
    instance.message.messageText = 'Đổi mật khẩu thành công bạn có thể đăng nhập lại!';
    instance.message.url = '/trang-chu';
  }

  resetPassword() {
    if (this.form.value.password !== this.form.value.repassword) {
      this.message = 'Mật khẩu không trùng khớp';
    } else {
      this.resetPasswordModel.NewPassword = this.form.value.repassword;
      this.userService.resetPassword(this.token, this.resetPasswordModel).subscribe((data: any) => {
        this.openDialogMessageConfirm('success');
      }, (err: HttpErrorResponse) => {
        this.message = 'Đặt lại mật khẩu thất bại!';
      });
    }
  }

}
