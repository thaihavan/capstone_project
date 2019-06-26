import { Component, OnInit, Input, HostListener } from '@angular/core';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { Post } from 'src/app/model/Post';
import { ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { Article } from 'src/app/model/Article';
import { UserService } from 'src/app/core/services/user-service/user.service';
@Component({
  selector: 'app-list-post',
  templateUrl: './list-post.component.html',
  styleUrls: ['./list-post.component.css']
})
export class ListPostComponent implements OnInit {

  VALID_PERSONAL_NAVS: string[] = [
    'feed',
    'articles',
    'virtual-trips',
    'companion-posts'
  ];
  VALID_HOME_NAVS: string[] = [
    'for-you',
    'popular',
    'newest',
    'follower',
    'topic'
  ];

  posts: Post[] = [];
  articles: Article[] = [];
  pageIndex: 1;
  isLoading = true;
  listUserIdFollowing: string[] = [];
  homeNav: string;
  personalNav: string;

  isScrollTopShow = false;
  topPosToStartShowing = 100;

  @HostListener('window:scroll') checkScroll() {
    const scrollPosition =
      window.pageYOffset ||
      document.documentElement.scrollTop ||
      document.body.scrollTop ||
      0;
    if (scrollPosition >= this.topPosToStartShowing) {
      this.isScrollTopShow = true;
    } else {
      this.isScrollTopShow = false;
    }
  }

  constructor(private route: ActivatedRoute, private postService: PostService, private userService: UserService) { }

  ngOnInit() {
    this.setNavParams();
    console.log('home-nav:', this.homeNav);
    console.log('personal-nav:', this.personalNav);
    this.getPost();
    const token = localStorage.getItem('Token');
    if (token != null) {
      this.userService.getAllFollowingId(localStorage.getItem('Token')).subscribe((result: any) => {
        this.listUserIdFollowing = result;
        console.log(this.listUserIdFollowing + ' listId');
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });

    }
  }
  onScroll() {
    console.log('list-post-on-scrole');
    this.isLoading = true;

    // Continue loading data
    // this.getPost();
  }

  setNavParams() {
    // Get parameter from url
    this.homeNav = this.route.snapshot.paramMap.get('home-nav');
    this.personalNav = this.route.snapshot.paramMap.get('personal-nav');

    // If home-nav is not valid
    if (this.homeNav != null && this.VALID_HOME_NAVS.indexOf(this.homeNav) === -1) {
      this.homeNav = 'for-you';
    }

    // If personal-nav is not valid
    if (this.personalNav != null && this.VALID_PERSONAL_NAVS.indexOf(this.personalNav) === -1) {
      this.personalNav = 'feed';
    }
  }

  getPost(): void {
    const token = localStorage.getItem('Token');
    if (this.homeNav != null) {
      // Call api
      if (token == null) {
        this.postService.getAllPost().subscribe((data: any) => {
          this.articles = data;
          this.articles.forEach(post => {
            this.posts.push(post.post);
          });
          console.log(this.posts);
          this.isLoading = false;
        }, (err: HttpErrorResponse) => {
          console.log(err);
        });
      } else {
        this.postService.getAllPostwithToken(token).subscribe((data: any) => {
          this.articles = data;
          this.articles.forEach(post => {
            this.posts.push(post.post);
          });
          console.log(this.posts);
          this.isLoading = false;
        }, (err: HttpErrorResponse) => {
          console.log(err);
        });
      }
    } else if (this.personalNav != null) {
      // Call api
      const account = JSON.parse(localStorage.getItem('Account'));
      this.postService.getAllPostByUserId(account.userId).subscribe((data: any) => {
        this.articles = data;
        this.articles.forEach(post => {
          this.posts.push(post.post);
        });
        this.isLoading = false;
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }

  gotoTop() {
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth'
    });
  }
}
