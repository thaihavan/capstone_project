import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';
import { Globals } from 'src/globals/globalvalues';
import { MatDialog } from '@angular/material';
import { Account } from 'src/app/model/Account';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-forgotpassword-page',
  templateUrl: './forgotpassword-page.component.html',
  styleUrls: ['./forgotpassword-page.component.css']
})
export class ForgotpasswordPageComponent implements OnInit {
  email: string;
  acount: Account;
  message: string;
  constructor(private dialog: MatDialog, private userService: UserService, public globals: Globals) {
    this.acount = new Account();
  }

  ngOnInit() {
  }
  forgotPassword() {
    this.acount.Email = this.email;
    this.userService.forgotPassword(this.acount).subscribe((data: any) => {
      this.openDialogMessageConfirm();
    }, (err: HttpErrorResponse) => {
      this.message = 'Đăng kí thất bại!';
    });
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
    instance.message.messageText = 'Kiểm tra Email! Mật khẩu mới đã được gửi về email của bạn.';
    instance.message.url = '/home';
  }
}
