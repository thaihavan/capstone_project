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

@Component({
  selector: 'app-search-result-container',
  templateUrl: './search-result-container.component.html',
  styleUrls: ['./search-result-container.component.css']
})
export class SearchResultContainerComponent implements OnInit {

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
  posts: any;
  users: [];
  listType: string;

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
              private userService: UserService) {
  }

  ngOnInit() {
    this.route.params.forEach(param => {
      this.search = decodeURI(param.search ? param.search : '');
      this.tab = param.tab;
      if (this.VALID_TABS.indexOf(this.tab) === -1) {
        this.tab = 'moi-nguoi';
      }
      this.getSearchResult(this.initPostFilter());
      });
  }

  initPostFilter(): PostFilter {
    const postFilter = new PostFilter();
    postFilter.topics = [];
    postFilter.timePeriod = 'all_time';
    postFilter.search = this.search;

    return postFilter;
  }

  getSearchResult(postFilter: PostFilter) {
    this.posts = [];
    this.users = [];
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

    this.userService.getUsers(search).subscribe((res: any) => {
      this.users = res;
    }, (error: HttpErrorResponse) => {
      console.log(error);
    });
  }

  getArticles(postFilter: PostFilter) {
    this.postService.getAllArticles(postFilter).subscribe((res: Article[]) => {
      this.posts = res;
    }, (error: HttpErrorResponse) => {
      console.log(error);
    });
  }

  getVirtualTrips(postFilter: PostFilter) {
    this.virtualTripService.getVirtualTrips(postFilter).subscribe((res: VirtualTrip[]) => {
      this.posts = res;
    }, (error: HttpErrorResponse) => {
      console.log(error);
    });
  }

  getCompanionPosts(postFilter: PostFilter) {
    this.companionPostService.getCompanionPosts(postFilter).subscribe((res: CompanionPost[]) => {
      this.posts = res;
    }, (error: HttpErrorResponse) => {
      console.log(error);
    });
  }

  submitFilter(postFilter: PostFilter) {
    postFilter.search = this.search;
    switch (this.tab) {
      case 'bai-viet':
        this.getArticles(postFilter);
        break;
      case 'chuyen-di':
        break;
      case 'tim-ban-dong-hanh':
        break;
    }
  }

  onScroll() {
    this.isLoading = true;

    // Continue loading data
    // this.getPost();
  }

  gotoTop() {
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth'
    });
  }

}
