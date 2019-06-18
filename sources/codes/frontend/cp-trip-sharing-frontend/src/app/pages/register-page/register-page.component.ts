import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';
import { Account } from 'src/app/model/Account';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit {
  email = '';
  password = '';
  repass = '';
  account: Account;
  message: string;
  constructor(private dialog: MatDialog, private userService: UserService) {
    this.account = new Account();
  }

  ngOnInit() {
  }

  callInterestedtopicPage(): void {
    this.account.Email = this.email;
    this.account.Password = this.password;
    this.userService.registerAccount(this.account).subscribe((message: any) => {
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
      }
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = 'Đăng kí thành công, vui lòng kiểm tra lại email!';
  }
}
