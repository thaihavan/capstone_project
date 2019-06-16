import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';
import { Account } from 'src/Model/Account';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit {
  username = '';
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
    this.account.Username = this.username;
    this.userService.registerAccount(this.account).subscribe((message: any) => {
      this.openDialogMessageConfirm();
      console.log('Register OK');
    }, (err: HttpErrorResponse) => {
      this.message = 'Đăng kí thất bại!';
    });
  }

  openDialogMessageConfirm() {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '400px',
      height: '200px',
      position: {
        top: '10px'
      }
    });
    const instance = dialogRef.componentInstance;
    instance.message = 'Đăng kí thành công, vui lòng kiểm tra lại email';
  }
}
