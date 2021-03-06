import { Component, OnInit, Input, ViewChild, AfterViewInit, Output, EventEmitter } from '@angular/core';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { Bookmark } from 'src/app/model/Bookmark';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { Router } from '@angular/router';
@Component({
  selector: 'app-post-small',
  templateUrl: './post-small.component.html',
  styleUrls: ['./post-small.component.css']
})
export class PostSmallComponent implements OnInit {

  @Input() post: any;
  @Input() postType: string;
  @Output() checkStageFollow = new EventEmitter();
  isBookmarkWaitingRespone = false;
  token: string;

  follow = false;
  bookmark = false;
  isPublic = true;

  user: any;
  userId: string;

  listUserIdFollowing: string[] = [];
  listPostIdBookMark: string[] = [];

  constructor(private userService: UserService,
              private errorHandler: GlobalErrorHandler,
              private router: Router) {
    this.token = localStorage.getItem('Token');
  }

  ngOnInit() {
    this.user = JSON.parse(localStorage.getItem('User'));
    if (this.user == null) {
      this.userId = 'chua dang nhap';
    } else {
      this.userId = this.user.id;
    }
    this.getStates();
    this.isPublic = this.checUserPostIsPublic();
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
    this.listUserIdFollowing = JSON.parse(localStorage.getItem('listUserIdFollowing'));
    if (this.follow === false) {
      this.userService.addFollow(userId, this.token).subscribe((data: any) => {
        this.follow = true;

        this.listUserIdFollowing.push(userId);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
        this.checkStageFollow.emit();
      }, this.errorHandler.handleError);
    } else {
      this.userService.unFollow(userId, this.token).subscribe((data: any) => {
        this.follow = false;
        const unfollow = this.listUserIdFollowing.indexOf(userId);
        this.listUserIdFollowing.splice(unfollow, 1);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, this.errorHandler.handleError);
    }
  }

  bookmarkPost() {
    if (this.isBookmarkWaitingRespone) {
      return;
    }
    this.isBookmarkWaitingRespone = true;
    this.listPostIdBookMark = JSON.parse(localStorage.getItem('listPostIdBookmark'));
    if (this.bookmark === false) {
      const bookmarkObject = new Bookmark();
      bookmarkObject.postId = this.post.id;

      this.userService.addBookMark(bookmarkObject, this.token).subscribe((data: any) => {
        this.bookmark = true;
        this.listPostIdBookMark.push(this.post.post.id);
        localStorage.setItem('listPostIdBookmark', JSON.stringify(this.listPostIdBookMark));
      }, (error) => {
        this.isBookmarkWaitingRespone = false;
      },
      () => {
        this.isBookmarkWaitingRespone = false;
      });
    } else {
      this.userService.deleteBookMark(this.post.post.id, this.token).subscribe((data: any) => {
        this.bookmark = false;
        const unbookmark = this.listPostIdBookMark.indexOf(this.post.post.id);
        this.listPostIdBookMark.splice(unbookmark, 1);
        localStorage.setItem('listPostIdBookmark', JSON.stringify(this.listPostIdBookMark));
      }, (error) => {
        this.isBookmarkWaitingRespone = false;
      },
      () => {
        this.isBookmarkWaitingRespone = false;
      });
    }
  }

  gotoPersionalPage(userId: string) {
    this.router.navigate(['/user', userId]);
  }
  checUserPostIsPublic() {
    if (this.postType === 'virtual-trip') {
      const user = JSON.parse(localStorage.getItem('User'));
      if (user) {
        if (this.post.post.author.id !== user.id && !this.post.post.isPublic) {
          return false;
        }
      } else {
        if (!this.post.post.isPublic) {
          return false;
        }
      }
    }
    return true;
  }
}
