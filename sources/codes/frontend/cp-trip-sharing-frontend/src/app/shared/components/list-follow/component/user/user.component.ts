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
  @Input() listFollowed = false;
  gender: string;
  showAddress = true;
  imgAvatar = '';
  follow = false;
  followed = false;
  listUserIdFollowing: string[] = [];
  constructor(private userService: UserService, public dialog: MatDialog) { }

  ngOnInit() {
    console.log(this.user);
    const user = JSON.parse(localStorage.getItem('User'));
    if (this.listFollowed === true && user.id !== this.user.id ) {
      this.listUserIdFollowing = JSON.parse(localStorage.getItem('listUserIdFollowing'));
      if (this.listUserIdFollowing != null) {
        // tslint:disable-next-line:prefer-for-of
        for (let i = 0; i < this.listUserIdFollowing.length; i++) {
          if (this.user.id === this.listUserIdFollowing[i]) {
            this.followed = true;
            this.follow = false;
            break;
          } else {
            this.followed = false;
            this.follow = true;
          }
        }
      }
    }
    if (this.user.avatar == null) {
      this.imgAvatar = 'https://gody.vn/public/v3/images/bg/br-register.jpg';
    } else {
      this.imgAvatar = this.user.avatar;
    }
    if (this.user.address === '') {
      this.user.address = 'Việt Nam';
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
    const user = JSON.parse(localStorage.getItem('User'));
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '320px',
      height: 'auto',
      position: {
        top: '20px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = 'Đã bỏ chặn người dùng thành công!';
    instance.message.url = '/user/' + user.id + '/danh-sach-chan';
  }

  gotoPersionalPage(authorId: any) {
    window.location.href = '/user/' + authorId;
  }

  followPerson(userId: any) {
    const token = localStorage.getItem('Token');
    if (this.followed === false && this.follow === true) {
      this.userService.addFollow(userId, token).subscribe((data: any) => {
        this.followed = true;
        this.follow = false;
        this.listUserIdFollowing.push(userId);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.userService.unFollow(userId, token).subscribe((data: any) => {
        this.followed = false;
        this.follow = true;
        const unfollow = this.listUserIdFollowing.indexOf(userId);
        this.listUserIdFollowing.splice(unfollow, 1);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }
}
