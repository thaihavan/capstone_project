import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/model/User';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MessagePopupComponent } from '../../../message-popup/message-popup.component';
import { MatDialog } from '@angular/material';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  @Input() user: any;
  @Input() checkBlocked: boolean;
  gender: string;
  constructor(private userService: UserService, public dialog: MatDialog) { }

  ngOnInit() {
    if (this.user.gender === true) {
      this.gender = 'Nam';
    } else {
      this.gender = 'Nữ';
    }
  }

  removeBlocked(userId: any) {
    const token = localStorage.getItem('Token');
    if (token != null) {
      this.userService.unBlock(userId, token).subscribe((result: any) => {
        this.openDialogMessageConfirm();
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
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
    instance.message.messageText = 'Đã bỏ chặn người dùng thành công!';
    instance.message.url = '/personal/blocked';
  }

}
