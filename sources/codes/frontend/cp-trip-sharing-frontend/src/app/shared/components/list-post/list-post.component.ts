import { Component, OnInit, Input, HostListener } from '@angular/core';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { Post } from 'src/app/model/Post';
import { ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { Article } from 'src/app/model/Article';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { VirtualTrip } from 'src/app/model/VirtualTrip';
import { PostFilter } from 'src/app/model/PostFilter';
import { Topic } from 'src/app/model/Topic';
import { VirtualTripService } from 'src/app/core/services/post-service/virtual-trip.service';
import { ArticleDisplay } from 'src/app/model/ArticleDisplay';
@Component({
  selector: 'app-list-post',
  templateUrl: './list-post.component.html',
  styleUrls: ['./list-post.component.css']
})
export class ListPostComponent implements OnInit {

  VALID_PERSONAL_NAVS: string[] = [
    'bai-viet',
    'chuyen-di',
    'tim-ban-dong-hanh'
  ];
  VALID_HOME_NAVS: string[] = [
    'de-xuat',
    'pho-bien',
    'moi-nhat',
    'theo-doi'
  ];

  articleDisplay = new ArticleDisplay();
  topics: Topic[] = [];
  isCheckedDict = {};
  pageIndex: 1;
  isLoading = true;
  isDisplayFilter = false;

  homeNav: string;
  personalNav: string;
  postFilter: PostFilter;

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

  constructor(private route: ActivatedRoute, private postService: PostService,
              private userService: UserService,
              private tripService: VirtualTripService) { }

  ngOnInit() {
    this.getTopics();
    this.setNavParams();
  }
  onScroll() {
    console.log('list-post-on-scrole');
    this.isLoading = true;

    // Continue loading data
    // this.getPost();
  }

  onButtonToggleChange(value: string) {
    this.postFilter.timePeriod = value;
  }

  submitFilter() {
    this.isDisplayFilter = false;
    this.getPosts();
  }

  onCheckboxToggleChange(topicId: string) {
    const index = this.postFilter.topics.indexOf(topicId);
    if (index === -1) {
      this.postFilter.topics.push(topicId);
    } else {
      this.postFilter.topics.splice(index, 1);
    }
  }

  setNavParams() {
    this.route.params.forEach(param => {
      // Get parameter from url
      this.homeNav = param['home-nav'];
      this.personalNav = param['personal-nav'];
      // this.homeNav = this.route.snapshot.paramMap.get('home-nav');
      // this.personalNav = this.route.snapshot.paramMap.get('personal-nav');

      // If home-nav is not valid
      if (this.homeNav && this.VALID_HOME_NAVS.indexOf(this.homeNav) === -1) {
        this.homeNav = 'de-xuat';
      }

      // If personal-nav is not valid
      if (this.personalNav && this.VALID_PERSONAL_NAVS.indexOf(this.personalNav) === -1) {
        this.personalNav = 'bai-viet';
      }

      // Init post filter
      this.initPostFilter();

      // Get posts
      this.getPosts();
    });
  }

  initPostFilter() {
    this.postFilter = new PostFilter();
    this.postFilter.timePeriod = 'all_time';
    this.postFilter.topics = [];
  }

  getTopics() {
    this.postService.getAllTopics().subscribe((res: any) => {
      this.topics = res;
      this.postFilter.topics = this.topics.map(t => t.id);
      this.topics.forEach(topic => {
        this.isCheckedDict[topic.id] = true;
      });
    }, (err: HttpErrorResponse) => {
      console.log(err);
    });
  }

  resetListPost() {
    this.articleDisplay.items = [];
    this.articleDisplay.typeArticle = null;
  }

  getPosts(): void {
    const postFilter: PostFilter = JSON.parse(JSON.stringify(this.postFilter));
    if (this.postFilter.topics.length === this.topics.length) {
      postFilter.topics = [];
    }

    if (this.homeNav || this.personalNav === 'bai-viet') {
      this.getArticles(postFilter);
    } else if (this.personalNav === 'chuyen-di') {
      this.getArticles(postFilter);
    } else if (this.personalNav === 'tim-ban-dong-hanh') {
    }
  }

  getArticles(postFilter: PostFilter): void {
    if (this.homeNav != null) {
      // Call api
      this.postService.getAllArticles(postFilter).subscribe((data: any) => {
        this.resetListPost();
        this.articleDisplay.typeArticle = 'article';
        data.forEach((article: any) => {
          if (article.post.isActive === true) {
            this.articleDisplay.items.push(article);
          }
        });
        this.isLoading = false;
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else if (this.personalNav != null) {
      if (this.personalNav === 'bai-viet') {
        // Call api
        const userId = this.getUserIdFromUrl();
        this.postService.getAllArticlesByUserId(userId, postFilter).subscribe((data: any) => {
          this.resetListPost();
          this.articleDisplay.typeArticle = 'article';
          data.forEach((article: any) => {
            if (article.post.isActive === true) {
              this.articleDisplay.items.push(article);
            }
          });
          this.isLoading = false;
        }, (err: HttpErrorResponse) => {
          console.log(err);
        });
      } else if (this.personalNav === 'chuyen-di') {
        this.tripService.getVirtualTrips().subscribe(data => {
          this.resetListPost();
          this.articleDisplay.typeArticle = 'virtual-trip';
          this.articleDisplay.items = data;
          this.isLoading = false;
        });
      }
    }
  }

  toggleFilter() {
    this.isDisplayFilter = !this.isDisplayFilter;
    console.log(this.postFilter);
  }

  gotoTop() {
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth'
    });
  }

  getUserIdFromUrl() {
    const url = window.location.href;
    const arr = url.split('/');
    return arr.length > 5 ? arr[4] : '';
  }
}
