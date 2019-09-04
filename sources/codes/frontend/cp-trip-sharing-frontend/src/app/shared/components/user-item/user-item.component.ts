import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { User } from 'src/app/model/User';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MatDialog } from '@angular/material';
import { MessagePopupComponent } from '../message-popup/message-popup.component';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';

@Component({
  selector: 'app-user-item',
  templateUrl: './user-item.component.html',
  styleUrls: ['./user-item.component.css']
})
export class UserItemComponent implements OnInit {

  @Input() user: User;
  @Input() showBlockedButton = false;
  @Input() showFollowButton = false;
  @Output() removeBLockUser = new EventEmitter();

  isYou = false;
  gender: string;
  showAddress = true;
  isFollowed = false;
  listUserIdFollowing: string[] = [];
  constructor(private userService: UserService,
              public dialog: MatDialog,
              private errorHandler: GlobalErrorHandler) { }

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
    } else {
      this.isYou = user === null;
    }
  }

  removeBlocked(userId: any) {
    const token = localStorage.getItem('Token');
    if (token != null) {
      this.userService.unBlock(userId, token).subscribe((result: any) => {
        this.removeBLockUser.emit(userId);
      }, this.errorHandler.handleError);
    }
  }

  gotoPersionalPage(authorId: any) {
    if (!this.showBlockedButton) {
      window.location.href = '/user/' + authorId;
    }
  }

  followPerson(userId: any) {
    const token = localStorage.getItem('Token');
    this.listUserIdFollowing = JSON.parse(localStorage.getItem('listUserIdFollowing'));
    if (this.isFollowed === false) {
      this.userService.addFollow(userId, token).subscribe((data: any) => {
        this.isFollowed = true;
        this.listUserIdFollowing.push(userId);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, this.errorHandler.handleError);
    } else {
      this.userService.unFollow(userId, token).subscribe((data: any) => {
        this.isFollowed = false;
        const unfollow = this.listUserIdFollowing.indexOf(userId);
        this.listUserIdFollowing.splice(unfollow, 1);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, this.errorHandler.handleError);
    }
  }

}
