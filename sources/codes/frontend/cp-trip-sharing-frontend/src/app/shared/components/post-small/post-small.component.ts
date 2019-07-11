import { Component, OnInit, Input, ViewChild, AfterViewInit } from '@angular/core';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { Like } from 'src/app/model/Like';
import { HttpErrorResponse } from '@angular/common/http';
import { IImage } from 'ng-simple-slideshow';
import { trigger, state, transition, style, animate } from '@angular/animations';
import { Bookmark } from 'src/app/model/Bookmark';
import { NotifyService } from 'src/app/core/services/notify-service/notify.service';
import { Notification } from 'src/app/model/Notification';
import { NotificationTemplates } from 'src/app/core/globals/NotificationTemplates';
import { HostGlobal } from 'src/app/core/global-variables';

@Component({
  selector: 'app-post-small',
  templateUrl: './post-small.component.html',
  styleUrls: ['./post-small.component.css']
})
export class PostSmallComponent implements OnInit {

  @Input() post: any;
  @Input() postType: string;

  token: string;

  follow = false;
  bookmark = false;

  user: any;
  userId: string;

  listUserIdFollowing: string[] = [];
  listPostIdBookMark: string[] = [];

  constructor(private postService: PostService,
              private userService: UserService) {
    this.token = localStorage.getItem('Token');
  }

  ngOnInit() {
    this.user = JSON.parse(localStorage.getItem('Account'));
    if (this.user == null) {
      this.userId = 'chua dang nhap';
    } else {
      this.userId = this.user.userId;
    }
    this.getStates();
  }

  getStates(): void {
    this.listUserIdFollowing = JSON.parse(localStorage.getItem('listUserIdFollowing'));
    if (this.listUserIdFollowing != null) {
      this.follow = this.listUserIdFollowing.indexOf(this.post.post.author.id) !== -1;
    }
    this.listPostIdBookMark = JSON.parse(localStorage.getItem('listPostIdBookmark'));
    if (this.listPostIdBookMark != null) {
      this.bookmark = this.listPostIdBookMark.indexOf(this.post.post.id) !== -1;
    }
  }

  followPerson(userId: any) {
    if (this.follow === false) {
      this.userService.addFollow(userId, this.token).subscribe((data: any) => {
        this.follow = true;

        this.listUserIdFollowing.push(userId);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.userService.unFollow(userId, this.token).subscribe((data: any) => {
        this.follow = false;
        const unfollow = this.listUserIdFollowing.indexOf(userId);
        this.listUserIdFollowing.splice(unfollow, 1);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }

  bookmarkPost() {
    if (this.bookmark === false) {
      const bookmarkObject = new Bookmark();
      bookmarkObject.CoverImage = this.post.post.coverImage;
      bookmarkObject.PostId = this.post.post.id;
      bookmarkObject.PostType = this.postType;
      bookmarkObject.Title = this.post.post.title;

      this.userService.addBookMark(bookmarkObject, this.token).subscribe((data: any) => {
        this.bookmark = true;
        this.listPostIdBookMark.push(this.post.post.id);
        localStorage.setItem('listPostIdBookmark', JSON.stringify(this.listPostIdBookMark));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.userService.deleteBookMark(this.post.post.id, this.token).subscribe((data: any) => {
        this.bookmark = false;
        const unbookmark = this.listPostIdBookMark.indexOf(this.post.post.id);
        this.listPostIdBookMark.splice(unbookmark, 1);
        localStorage.setItem('listPostIdBookmark', JSON.stringify(this.listPostIdBookMark));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }

  gotoPersionalPage(userId: string) {
    window.location.href = `/user/${userId}`;
  }

}
