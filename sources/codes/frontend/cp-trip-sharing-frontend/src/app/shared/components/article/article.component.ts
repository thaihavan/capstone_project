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
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css'],
  animations: [
    trigger('myTrigger', [
      state('void', style({
        opacity: 0,
        left: '-100%',

      })),
      state('*', style({
        opacity: 1,
        left: '0%',
      })),
      transition('void => *', [animate('0.5s ease-in')]),
    ])
  ],
})
export class ArticleComponent implements OnInit, AfterViewInit {
  checkRemoved = false;
  like: Like;
  @Input() post: any;
  @Input() listType: string;
  token: string;
  follow = false;
  bookmark = false;
  checkUser = true;
  bookmarkObject: Bookmark;
  user: any;
  userId: string;
  listUserIdFollowing: string[] = [];
  listPostIdBookMark: string[] = [];

  // if article is virtual trip variable for virtual trip
  @ViewChild('slideShow') slideShow: any;
  imageSources: (string | IImage)[] = [];
  desTitle = '';
  desNote = '';
  desFormat = '';
  constructor(private postService: PostService,
              private userService: UserService,
              private notifyService: NotifyService) {
    this.like = new Like();
    this.bookmarkObject = new Bookmark();
    this.token = localStorage.getItem('Token');
  }

  ngOnInit() {
    this.user = JSON.parse(localStorage.getItem('Account'));
    if (this.user == null) {
      this.userId = 'chua dang nhap';
    } else {
      this.userId = this.user.userId;
    }
    this.listUserIdFollowing = JSON.parse(localStorage.getItem('listUserIdFollowing'));
    if (this.listUserIdFollowing != null) {
      // tslint:disable-next-line:prefer-for-of
      for (let i = 0; i < this.listUserIdFollowing.length; i++) {
        if (this.post.post.author.id === this.listUserIdFollowing[i]) {
          this.follow = true;
          break;
        }
      }
    }
    this.listPostIdBookMark = JSON.parse(localStorage.getItem('listPostIdBookmark'));
    if (this.listPostIdBookMark != null) {
      // tslint:disable-next-line:prefer-for-of
      for (let i = 0; i < this.listPostIdBookMark.length; i++) {
        if (this.post.post.id === this.listPostIdBookMark[i]) {
          this.bookmark = true;
          break;
        }
      }
    }
    // if article is virtual post get imageurls and post's contetn
    if (this.listType === 'virtual-trip') {
      this.setVirtualTripDisplay();
    }
  }

  ngAfterViewInit(): void {
    // set slideshow indexchage
    if (this.listType === 'virtual-trip') {
      this.slideShow.onIndexChanged.subscribe(slide => {
        if (this.slideShow.slideIndex === 0) {
          this.desTitle = this.post.post.title;
          this.desFormat = this.post.post.pubDate;
          this.desNote = this.post.post.content;
        } else {
          this.desTitle = this.post.items[this.slideShow.slideIndex - 1].name;
          this.desFormat = this.post.items[this.slideShow.slideIndex - 1].formattedAddress;
          this.desNote = this.post.items[this.slideShow.slideIndex - 1].note;
        }
      });
    }
  }

  likePost(like: any) {
    this.like.ObjectId = this.post.post.id;
    this.like.ObjectType = 'post';
    if (like === false) {
      this.postService.likeAPost(this.like).subscribe((data: any) => {
        this.post.post.liked = true;
        this.post.post.likeCount += 1;

        // Notify
        this.sendNotification();
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.postService.unlikeAPost(this.like).subscribe((data: any) => {
        this.post.post.liked = false;
        this.post.post.likeCount -= 1;
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
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

  bookmarkPost(postId: any, title: any, imgCover: any, postType: any) {
    if (this.bookmark === false) {
      this.bookmarkObject.CoverImage = imgCover;
      this.bookmarkObject.PostId = postId;
      this.bookmarkObject.PostType = postType;
      this.bookmarkObject.Title = title;
      this.userService.addBookMark(this.bookmarkObject, this.token).subscribe((data: any) => {
        this.bookmark = true;
        this.listPostIdBookMark.push(postId);
        localStorage.setItem('listPostIdBookmark', JSON.stringify(this.listPostIdBookMark));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.userService.deleteBookMark(postId, this.token).subscribe((data: any) => {
        this.bookmark = false;
        const unbookmark = this.listPostIdBookMark.indexOf(postId);
        this.listPostIdBookMark.splice(unbookmark, 1);
        localStorage.setItem('listPostIdBookmark', JSON.stringify(this.listPostIdBookMark));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }

  // if article is virtualtrip get imageurl for slideshow.
  setVirtualTripDisplay() {
    if (this.post.post.coverImage !== null && this.post.post.coverImage !== '') {
      const url = { url: this.post.post.coverImage, caption: this.post.post.title, href: '#config' };
      this.imageSources.push(url);
    } else {
      // tslint:disable-next-line:max-line-length
      const url = { url: 'https://1.bp.blogspot.com/-ZvShXfYhUGo/TsU-7srTr6I/AAAAAAAACkk/YAnQeyudiNo/s1600/Sapa_Vietnam.jpg', caption: this.post.post.title, href: '#config' };
      this.imageSources.push(url);
    }
    this.post.items.forEach(art => {
      const url = { url: art.image, caption: art.name, href: '#config' };
      this.imageSources.push(url);
    });

    this.desTitle = this.post.post.title;
    this.desFormat = this.post.post.pubDate;
    this.desNote = this.post.post.content;
  }

  editPost() {
    if (this.listType === 'virtual-trip') {
      window.location.href = '/chuyen-di?tripId=' + this.post.id;
    } else {
      window.location.href = '/chinh-sua-bai-viet/' + this.post.id;
    }
  }

  getShortDescription(htmlContent) {
    // Convert html string to DOM object
    const div = document.createElement('div');
    div.innerHTML = htmlContent.trim();

    const pTags = div.getElementsByTagName('p');
    let pContent = '';
    for (let i = 0; i < pTags.length; i++) {
      pContent += pTags.item(i).innerText.trim() + ' ';

      if (pContent.length > 250) {
        break;
      }
    }

    if (pContent.length > 250) {
      pContent = pContent.substr(0, 250) + '...';
    }

    return pContent;
  }

  gotoPersionalPage(authorId: any) {
    window.location.href = '/user/' + authorId;
  }

  romovePost(articleId: any) {
    this.postService.removeArticle(articleId).subscribe((data: any) => {
      this.checkRemoved = true;
    }, (err: HttpErrorResponse) => {
      console.log(err);
    });
  }

  sendNotification() {
    const user = JSON.parse(localStorage.getItem('User'));

    const notification = new Notification();
    notification.content = new NotificationTemplates()
      .getLikePostNotiTemplate(user.displayName, this.post.post.title);
    notification.displayImage = user.profileImage;
    notification.receivers = [this.post.post.authorId];
    notification.url = HostGlobal.HOST_FRONTEND + '/bai-viet/' + this.post.id;

    this.notifyService.sendNotification(notification);
  }
}
