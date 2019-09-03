import { Component, OnInit, HostListener } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { PostFilter } from 'src/app/model/PostFilter';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { VirtualTripService } from 'src/app/core/services/post-service/virtual-trip.service';
import { FindingCompanionService } from 'src/app/core/services/post-service/finding-companion.service';
import { Article } from 'src/app/model/Article';
import { HttpErrorResponse } from '@angular/common/http';
import { VirtualTrip } from 'src/app/model/VirtualTrip';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { CompanionPost } from 'src/app/model/CompanionPost';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { User } from 'src/app/model/User';

@Component({
  selector: 'app-search-result-container',
  templateUrl: './search-result-container.component.html',
  styleUrls: ['./search-result-container.component.css']
})
export class SearchResultContainerComponent implements OnInit {

  searchType: string;
  search: string;
  tab: string;
  VALID_TABS = [
    'moi-nguoi',
    'bai-viet',
    'chuyen-di',
    'tim-ban-dong-hanh'
  ];

  showFilter = true;
  showTimePeriod = true;
  showTopic = true;

  isLoading = false;
  isScrollTopShow = false;
  topPosToStartShowing = 100;

  // list result
  posts: any[];
  users: User[];
  listType: string;
  postFilter: PostFilter;
  firstLoading = true;
  page = 1;

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

  constructor(private titleService: Title,
              private route: ActivatedRoute,
              private postService: PostService,
              private virtualTripService: VirtualTripService,
              private companionPostService: FindingCompanionService,
              private userService: UserService,
              private errorHandler: GlobalErrorHandler) {
    this.titleService.setTitle('Tìm kiếm');
  }

  ngOnInit() {

    this.searchType = this.getSearchTypeParam();

    this.route.params.forEach(param => {
      this.search = decodeURI(param.search ? param.search : '');
      this.tab = param.tab;
      if (this.VALID_TABS.indexOf(this.tab) === -1) {
        this.tab = 'bai-viet';
      }

      this.postFilter = this.initPostFilter();
      this.getSearchResult(this.postFilter, true);

    });
  }

  initPostFilter(): PostFilter {
    const postFilter = new PostFilter();
    postFilter.topics = [];
    postFilter.timePeriod = 'all_time';
    postFilter.search = '';
    postFilter.locationId = '';

    switch (this.searchType) {
      case 'text':
        postFilter.search = this.search;
        break;
      case 'location':
        postFilter.locationId = this.search;
        break;
    }

    return postFilter;
  }

  getSearchResult(postFilter: PostFilter, isReset: boolean) {
    if (isReset) {
      this.posts = [];
      this.users = [];
      this.page = 1;
    }
    switch (this.tab) {
      case 'moi-nguoi':
        this.showFilter = false;
        this.getUsers(this.search);
        break;
      case 'bai-viet':
        this.showFilter = true;
        this.listType = 'article';
        this.getArticles(postFilter);
        break;
      case 'chuyen-di':
        this.showFilter = true;
        this.listType = 'virtual-trip';
        this.getVirtualTrips(postFilter);
        break;
      case 'tim-ban-dong-hanh':
        this.showFilter = true;
        this.listType = 'companion-post';
        this.getCompanionPosts(postFilter);
        break;
    }
  }

  getUsers(search: string) {
    if (search === undefined || search == null) {
      search = '';
    }
    if (!this.firstLoading) {
      this.isLoading = true;
    }
    this.userService.getUsers(search, this.page).subscribe((res: User[]) => {
      // res = this.filterUserBlocker(res);
      this.users.push(...res);
    }, this.errorHandler.handleError,
    () => {
      this.isLoading = false;
      this.firstLoading = false;
    });
  }

  getArticles(postFilter: PostFilter) {
    this.postService.getAllArticles(postFilter, this.page).subscribe((res: Article[]) => {
      // res = this.filterPostBlocker(res);
      this.posts.push(...res);
      if (!this.firstLoading) {
        this.isLoading = true;
      }
    }, this.errorHandler.handleError,
    () => {
      this.isLoading = false;
      this.firstLoading = false;
    });
  }

  getVirtualTrips(postFilter: PostFilter) {
    this.virtualTripService.getVirtualTrips(postFilter, this.page).subscribe((res: VirtualTrip[]) => {
      // res = this.filterPostBlocker(res);
      this.posts.push(...res);
      if (!this.firstLoading) {
        this.isLoading = true;
      }
    }, this.errorHandler.handleError,
    () => {
      this.isLoading = false;
      this.firstLoading = false;
    });
  }

  getCompanionPosts(postFilter: PostFilter) {
    this.companionPostService.getCompanionPosts(postFilter, this.page).subscribe((res: CompanionPost[]) => {
      // res = this.filterPostBlocker(res);
      this.posts.push(...res);
      if (!this.firstLoading) {
        this.isLoading = true;
      }
    }, this.errorHandler.handleError,
    () => {
      this.isLoading = false;
      this.firstLoading = false;
    });
  }

  submitFilter(postFilter: PostFilter) {
    this.isLoading = false;
    this.firstLoading = true;
    this.postFilter = postFilter;
    switch (this.searchType) {
      case 'text':
        this.postFilter.search = this.search;
        break;
      case 'location':
        this.postFilter.locationId = this.search;
        break;
    }
    this.getSearchResult(this.postFilter, true);
  }

  getSearchTypeParam() {
    const urlSplit = window.location.href.split('/');

    if (urlSplit.length < 5) {
      return '';
    }

    return decodeURI(urlSplit[4]);
  }

  // filterPostBlocker(posts: any[]) {
  //   let listBlockers: any[] = JSON.parse(localStorage.getItem('listBlockers'));
  //   if (listBlockers == null) {
  //     listBlockers = [];
  //   }
  //   posts = posts.filter(p => listBlockers.find(b => b.id === p.post.author.id) == null);
  //   return posts;
  // }

  // filterUserBlocker(users: any[]) {
  //   let listBlockers: any[] = JSON.parse(localStorage.getItem('listBlockers'));
  //   if (listBlockers == null) {
  //     listBlockers = [];
  //   }
  //   users = users.filter(u => listBlockers.find(b => b.id === u.id) == null);
  //   return users;
  // }

  onScroll() {
    if (this.posts.length >= 12) {
      this.isLoading = true;
      this.page++;
      console.log('page-' + this.page);
      // Continue loading data
      this.getSearchResult(this.postFilter, false);
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
