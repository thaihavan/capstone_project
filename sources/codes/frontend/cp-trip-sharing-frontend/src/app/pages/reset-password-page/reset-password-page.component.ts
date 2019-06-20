import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';
import { Globals } from 'src/globals/globalvalues';
import { MatDialog } from '@angular/material';
import { ResetPasswordModel } from 'src/app/model/ResetPasswordModel';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-reset-password-page',
  templateUrl: './reset-password-page.component.html',
  styleUrls: ['./reset-password-page.component.css']
})
export class ResetPasswordPageComponent implements OnInit {
  token: string = null;
  newpassword: string = null;
  renewpassword: string = null;
  message: string = null;
  resetPasswordModel: ResetPasswordModel;
  constructor(private route: ActivatedRoute, private dialog: MatDialog, private userService: UserService, public globals: Globals) {
    this.resetPasswordModel = new ResetPasswordModel();
  }

  ngOnInit() {
    this.token = this.route.snapshot.paramMap.get('token');
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
    instance.message.messageText = 'Đổi mật khẩu thành công bạn có thể đăng nhập lại!';
    instance.message.url = '/home';
  }

  resetPassword() {
    if (this.newpassword !== this.renewpassword) {
      this.message = 'Mật khẩu không trùng khớp';
    } else {
      this.resetPasswordModel.NewPassword = this.renewpassword;
      this.userService.resetPassword(this.token, this.resetPasswordModel).subscribe((data: any) => {
        this.openDialogMessageConfirm();
      }, (err: HttpErrorResponse) => {
        this.message = 'Đăng kí thất bại!';
      });
    }
  }

}
