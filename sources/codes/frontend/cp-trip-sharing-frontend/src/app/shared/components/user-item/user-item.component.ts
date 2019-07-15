import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/model/User';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MatDialog } from '@angular/material';
import { MessagePopupComponent } from '../message-popup/message-popup.component';

@Component({
  selector: 'app-user-item',
  templateUrl: './user-item.component.html',
  styleUrls: ['./user-item.component.css']
})
export class UserItemComponent implements OnInit {

  @Input() user: User;
  @Input() showBlocked: boolean;
  @Input() showFollowButton = false;

  isYou = false;
  gender: string;
  showAddress = true;
  imgAvatar = '';
  isFollowed = false;
  listUserIdFollowing: string[] = [];
  constructor(private userService: UserService, public dialog: MatDialog) { }

  ngOnInit() {
    const user = JSON.parse(localStorage.getItem('User'));
    if (user != null) {
      this.isYou = user.id === this.user.id;

      if (this.showFollowButton === true && user.id !== this.user.id) {
        this.listUserIdFollowing = JSON.parse(localStorage.getItem('listUserIdFollowing'));
        if (this.listUserIdFollowing != null) {
          this.isFollowed = this.listUserIdFollowing.indexOf(this.user.id) !== -1;
        }
      }
      this.imgAvatar = this.user.avatar;
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
    if (this.isFollowed === false) {
      this.userService.addFollow(userId, token).subscribe((data: any) => {
        this.isFollowed = false;
        this.listUserIdFollowing.push(userId);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.userService.unFollow(userId, token).subscribe((data: any) => {
        this.isFollowed = false;
        const unfollow = this.listUserIdFollowing.indexOf(userId);
        this.listUserIdFollowing.splice(unfollow, 1);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }

}
