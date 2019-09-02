import { Component, OnInit, HostListener } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { PostFilter } from 'src/app/model/PostFilter';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { VirtualTripService } from 'src/app/core/services/post-service/virtual-trip.service';
import { Article } from 'src/app/model/Article';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { FindingCompanionService } from 'src/app/core/services/post-service/finding-companion.service';

@Component({
  selector: 'app-list-post-page',
  templateUrl: './list-post-page.component.html',
  styleUrls: ['./list-post-page.component.css']
})
export class ListPostPageComponent implements OnInit {
  constructor(private titleService: Title,
              private route: ActivatedRoute,
              private postService: PostService,
              private virtualTripService: VirtualTripService,
              private companionPostService: FindingCompanionService,
              private errorHandler: GlobalErrorHandler) {
  }
  coverImage = '../../../assets/cover-image.png';
  homeNav = '';
  listType = '';
  title = '';
  posts = [];

  showTimePeriod = true;
  showTopic = true;

  firstLoading = true;
  isLoading = false;
  isScrollTopShow = false;
  topPosToStartShowing = 100;
  postFilter: PostFilter;
  page = 1;

  VALID_HOME_NAVS: string[] = [
    'de-xuat',
    'pho-bien',
    'moi-nhat',
    'bai-viet',
    'chuyen-di',
    'tim-ban-dong-hanh'
  ];

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
  ngOnInit() {
    this.route.params.subscribe(routeParams => {
      this.homeNav = routeParams['home-nav'];
      this.setTitle();
      this.posts = [];
      this.page = 1;
      this.getPosts(undefined);
    });
    // this.homeNav = this.route.snapshot.paramMap.get('home-nav');
    // this.setTitle();
    // this.getPosts(undefined);
  }

  setTitle() {
    switch (this.homeNav) {
      case '':
        this.title = 'Trang chủ';
        break;
      case 'de-xuat':
        this.title = 'Đề xuất';
        this.listType = 'article';
        break;
      case 'pho-bien':
        this.title = 'Phổ biến';
        this.listType = 'article';
        break;
      case 'moi-nhat':
        this.title = 'Mới nhất';
        this.listType = 'article';
        break;
      case 'bai-viet':
        this.title = 'Bài viết';
        this.listType = 'article';
        break;
      case 'chuyen-di':
        this.title = 'Chuyến đi';
        this.listType = 'virtual-trip';
        this.showTopic = false;
        break;
      case 'tim-ban-dong-hanh':
        this.title = 'Tìm bạn đồng hành';
        this.listType = 'companion-post';
        this.showTopic = false;
        break;
    }
    this.titleService.setTitle(this.title);
  }

  getPosts(postFilter: PostFilter) {
    this.firstLoading = true;
    switch (this.homeNav) {
      case '':
        break;
      case 'de-xuat':
        this.getRecommendArticles(postFilter);
        break;
      case 'pho-bien':
        this.getPopularArticles(postFilter);
        break;
      case 'moi-nhat':
        this.getNewestArticles(postFilter);
        break;
      case 'chuyen-di':
        this.getVirtualTrips(postFilter);
        break;
      case 'tim-ban-dong-hanh':
        this.getCompanionPosts(postFilter);
        break;
    }
  }

  submitFilter(postFilter: PostFilter) {
    this.firstLoading = true;
    this.postFilter = postFilter;
    this.posts = [];
    this.getPosts(this.postFilter);
  }

  getNewestArticles(postFilter: PostFilter) {
    if (!postFilter) {
      postFilter = new PostFilter();
      postFilter.topics = [];
      postFilter.timePeriod = 'all_time';
    }
    if (!this.firstLoading) {
      this.isLoading = true;
    }
    this.postService.getAllArticles(postFilter, this.page).subscribe((data: Article[]) => {
      // data = this.filterBlocker(data);
      this.posts.push(...data);
    },
    this.errorHandler.handleError,
    () => {
      this.isLoading = false;
      this.firstLoading = false;
    });
  }

  getPopularArticles(postFilter: PostFilter): void {
    if (!postFilter) {
      postFilter = new PostFilter();
      postFilter.topics = [];
      postFilter.timePeriod = 'all_time';
    }
    if (!this.firstLoading) {
      this.isLoading = true;
    }
    this.postService.getPopularArticles(postFilter, this.page).subscribe((data: Article[]) => {
      // data = this.filterBlocker(data);
      this.posts.push(...data);
    }, this.errorHandler.handleError,
    () => {
      this.isLoading = false;
      this.firstLoading = false;
    });
  }

  getRecommendArticles(postFilter: PostFilter): void {
    if (!postFilter) {
      postFilter = new PostFilter();
      postFilter.topics = [];
      postFilter.timePeriod = 'all_time';
    }
    if (!this.firstLoading) {
      this.isLoading = true;
    }
    this.postService.getRecommendArticles(postFilter, this.page).subscribe((data: Article[]) => {
      // data = this.filterBlocker(data);
      this.posts.push(...data);
    }, this.errorHandler.handleError,
    () => {
      this.isLoading = false;
      this.firstLoading = false;
    });
  }

  getVirtualTrips(postFilter: PostFilter): void {
    if (!postFilter) {
      postFilter = new PostFilter();
      postFilter.topics = [];
      postFilter.timePeriod = 'all_time';

      this.isLoading = true;
    }
    if (!this.firstLoading) {
      this.isLoading = true;
    }
    this.virtualTripService.getVirtualTrips(postFilter, this.page).subscribe(data => {
      // data = this.filterBlocker(data);
      this.posts.push(...data);
    }, this.errorHandler.handleError,
    () => {
      this.isLoading = false;
      this.firstLoading = false;
    });
  }

  getCompanionPosts(postFilter: PostFilter): void {
    if (!postFilter) {
      postFilter = new PostFilter();
      postFilter.topics = [];
      postFilter.timePeriod = 'all_time';
    }
    if (!this.firstLoading) {
      this.isLoading = true;
    }
    this.companionPostService.getCompanionPosts(postFilter, this.page).subscribe(data => {
      // data = this.filterBlocker(data);
      this.posts.push(...data);
    }, this.errorHandler.handleError,
    () => {
      this.isLoading = false;
      this.firstLoading = false;
    });
  }

  // filterBlocker(posts: any[]) {
  //   let listBlockers: any[] = JSON.parse(localStorage.getItem('listBlockers'));
  //   if (listBlockers == null) {
  //     listBlockers = [];
  //   }
  //   posts = posts.filter(p => listBlockers.find(u => u.id === p.post.author.id) == null);
  //   return posts;
  // }

  onScroll() {
    if (this.posts.length >= 12) {
      this.isLoading = true;
      this.page++;
      console.log('page-' + this.page);
      // Continue loading data
      this.getPosts(this.postFilter);
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
