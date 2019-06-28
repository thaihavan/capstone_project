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

  articleDisplay = new ArticleDisplay();
  posts: Post[] = [];
  articles: Article[] = [];
  virtualTrips: VirtualTrip[] = [];
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
        this.homeNav = 'for-you';
      }

      // If personal-nav is not valid
      if (this.personalNav && this.VALID_PERSONAL_NAVS.indexOf(this.personalNav) === -1) {
        this.personalNav = 'articles';
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

  resetListPost(): void {
    this.posts = [];
    this.articles = [];
    this.virtualTrips = [];
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

  getPosts(): void {
    const postFilter: PostFilter = JSON.parse(JSON.stringify(this.postFilter));
    if (this.postFilter.topics.length === this.topics.length) {
      postFilter.topics = [];
    }

    if (this.homeNav || this.personalNav === 'articles') {
      this.resetListPost();
      this.getArticles(postFilter);
    } else if (this.personalNav === 'virtual-trips') {
      this.resetListPost();
      this.getArticles(postFilter);
    } else if (this.personalNav === 'companion-posts') {
      this.resetListPost();
    }
  }

  getArticles(postFilter: PostFilter): void {
    if (this.homeNav != null) {
      // Call api
      this.postService.getAllArticles(postFilter).subscribe((data: any) => {
        this.articleDisplay.typeArticle = 'article';
        this.articleDisplay.items = data;
        this.articleDisplay.items.forEach(post => {
          this.posts.push(post.post);
        });
        console.log(this.posts);
        this.isLoading = false;
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else if (this.personalNav === 'articles') {
      // Call api
      const account = JSON.parse(localStorage.getItem('Account'));
      this.postService.getAllArticlesByUserId(account.userId, postFilter).subscribe((data: any) => {
        this.resetListPost();
        this.articleDisplay.typeArticle = 'article';
        this.articleDisplay.items = data;
        this.articleDisplay.items.forEach(post => {
          this.posts.push(post.post);
        });
        this.isLoading = false;
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else if (this.personalNav === 'virtual-trips') {
        this.tripService.getVirtualTrips().subscribe(data => {
        this.resetListPost();
        this.articleDisplay.typeArticle = 'virtual-trips';
        this.articleDisplay.items = data;
        this.articleDisplay.items.forEach(post => {
          this.posts.push(post.post);
        });
        this.isLoading = false;
      });
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
}
