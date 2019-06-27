import { Component, OnInit, Input, ViewChild } from '@angular/core';
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
export class ArticleComponent implements OnInit {
  like: Like;
  @Input() article: Article;
  token: string;
  follow = false;
  bookmark = false;
  checkUser = true;
  user: any;
  userId: string;
  listUserIdFollowing: string[] = [];
  listPostIdBookMark: string[] = [];
  postType: any;
  name = 'PhongNV';
  @ViewChild('slideShow') slideShow: any;
  imageSources: (string | IImage)[] = [
    // tslint:disable-next-line:max-line-length
    { url: 'http://static.asiawebdirect.com/m/bangkok/portals/vietnam/homepage/ha-long-bay/pagePropertiesImage/ha-long-bay.jpg', caption: 'The first slide', href: '#config' },
    { url: 'http://toproomserbia.com/wp-content/uploads/st_uploadfont/The-Ultimate-Guide-to-Traveling-When-You%E2%80%99re-Broke.jpg' },
    { url: 'http://static.asiawebdirect.com/m/bangkok/portals/vietnam/homepage/pagePropertiesImage/vietnam.jpg' }
  ];
  arrContent = [
    // tslint:disable-next-line:max-line-length
    'The Shiba Inu is the smallest of the six original and distinct spitz breeds of dog from Japan. A small,',
    'agile dog that copes very well with mountainous terrain,',
    'the Shiba Inu was originally bred for hunting.',
  ];
  currContent = '';
  constructor(private postService: PostService, private userService: UserService) {
    this.like = new Like();
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
    if (this.article.post.postType === 'virtualtrip') {
      this.postType = this.article.post.postType;
      this.slideShow.onIndexChanged.subscribe(slide => {
        this.currContent = this.arrContent[this.slideShow.slideIndex];
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

  bookmarkPost(postId: any) {
    if (this.bookmark === false) {
      this.userService.addBookMark(postId, this.token).subscribe((data: any) => {
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
}
export class VirtualDisplay {
  urlImg = [
    'https://www.statravel.co.uk/static/uk_division_web_live/assets/sta-travel-default-min.jpg',
    'http://toproomserbia.com/wp-content/uploads/st_uploadfont/The-Ultimate-Guide-to-Traveling-When-You%E2%80%99re-Broke.jpg',
    'https://cdn-images-1.medium.com/max/2600/0*8HkryPmlsZCucKbv'
  ];
}
