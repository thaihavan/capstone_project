import { Component, OnInit, Input, ViewChild, AfterViewInit } from '@angular/core';
import { Post } from 'src/app/model/Post';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { Like } from 'src/app/model/Like';
import { HttpErrorResponse } from '@angular/common/http';
import { User } from 'src/app/model/User';
import { IImage } from 'ng-simple-slideshow';
import { trigger, state, transition, style, animate } from '@angular/animations';
import { isAbsolute } from 'path';
import { Article } from 'src/app/model/Article';
import { Bookmark } from 'src/app/model/Bookmark';
import { Globals } from 'src/globals/globalvalues';
import { LocationMarker } from 'src/app/model/LocationMarker';


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

  like: Like;
  @Input() article;
  @Input() typeArticle;
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
  constructor(private postService: PostService, private userService: UserService, public globals: Globals) {
    this.like = new Like();
    this.bookmarkObject = new Bookmark();
    this.token = localStorage.getItem('Token');
  }

  ngOnInit() {
    this.user = JSON.parse(localStorage.getItem('Account'));
    if (this.user != null && this.user.userId === this.article.post.author.id) {
      this.checkUser = false;
    }
    this.listUserIdFollowing = JSON.parse(localStorage.getItem('listUserIdFollowing'));
    if (this.listUserIdFollowing != null) {
      // tslint:disable-next-line:prefer-for-of
      for (let i = 0; i < this.listUserIdFollowing.length; i++) {
        if (this.article.post.author.id === this.listUserIdFollowing[i]) {
          this.follow = true;
          break;
        }
      }
    }
    this.listPostIdBookMark = JSON.parse(localStorage.getItem('listPostIdBookmark'));
    if (this.listPostIdBookMark != null) {
      // tslint:disable-next-line:prefer-for-of
      for (let i = 0; i < this.listPostIdBookMark.length; i++) {
        if (this.article.post.id === this.listPostIdBookMark[i]) {
          this.bookmark = true;
          break;
        }
      }
    }
    // if article is virtual post get imageurls and post's contetn
    if (this.typeArticle === 'virtual-trips') {
      this.setVirtualTripDisplay();
    }
  }

  ngAfterViewInit(): void {
    // set slideshow indexchage
    if (this.typeArticle === 'virtual-trips') {
      this.slideShow.onIndexChanged.subscribe(slide => {
        if (this.slideShow.slideIndex === 0) {
          this.desTitle = this.article.post.title;
          this.desFormat = this.article.post.pubDate;
          this.desNote = this.article.post.content;
        } else {
          this.desTitle = this.article.items[this.slideShow.slideIndex - 1].name;
          this.desFormat = this.article.items[this.slideShow.slideIndex - 1].formattedAddress;
          this.desNote = this.article.items[this.slideShow.slideIndex - 1].note;
        }
      });
    }
  }

  likePost(like: any) {
    this.like.ObjectId = this.article.post.id;
    this.like.ObjectType = 'post';
    if (like === false) {
      this.postService.likeAPost(this.like).subscribe((data: any) => {
        this.article.post.liked = true;
        this.article.post.likeCount += 1;
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.postService.unlikeAPost(this.like).subscribe((data: any) => {
        this.article.post.liked = false;
        this.article.post.likeCount -= 1;
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
    if (this.article.post.coverImage !== null && this.article.post.coverImage !== '') {
      const url = { url: this.article.post.coverImage, caption: this.article.post.title, href: '#config' };
      this.imageSources.push(url);
    } else {
      // tslint:disable-next-line:max-line-length
      const url = { url: 'https://1.bp.blogspot.com/-ZvShXfYhUGo/TsU-7srTr6I/AAAAAAAACkk/YAnQeyudiNo/s1600/Sapa_Vietnam.jpg', caption: this.article.post.title, href: '#config' };
      this.imageSources.push(url);
    }
    this.article.items.forEach(art => {
      const url = { url: art.image, caption: art.name, href: '#config' };
      this.imageSources.push(url);
    });

    this.desTitle = this.article.post.title;
    this.desFormat = this.article.post.pubDate;
    this.desNote = this.article.post.content;
  }

  editPost() {
    if (this.typeArticle === 'virtual-trips') {
      window.location.href = this.globals.urllocal + '/chuyen-di?tripId=' + this.article.id;
    } else {
      window.location.href = this.globals.urllocal + '/chinh-sua-bai-viet/' + this.article.id;
    }
  }

  gotoPersionalPage(authorId: any) {
    window.location.href = '/user/' + authorId;
  }
}
