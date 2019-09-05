import { Component, OnInit, Input, HostListener } from '@angular/core';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { Post } from 'src/app/model/Post';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { Article } from 'src/app/model/Article';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { VirtualTrip } from 'src/app/model/VirtualTrip';
import { PostFilter } from 'src/app/model/PostFilter';
import { Topic } from 'src/app/model/Topic';
import { VirtualTripService } from 'src/app/core/services/post-service/virtual-trip.service';
import { ArticleDisplay } from 'src/app/model/ArticleDisplay';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { FindingCompanionService } from 'src/app/core/services/post-service/finding-companion.service';
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

  articleDisplay = new ArticleDisplay();
  topics: Topic[] = [];
  isCheckedDict = {};
  pageIndex: 1;
  isLoading = true;
  isDisplayFilter = false;
  isPreLoading = true;
  personalNav: string;

  isScrollTopShow = false;
  topPosToStartShowing = 100;

  page = 1;
  showTimePeriod = true;
  showTopic = true;

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
              private tripService: VirtualTripService,
              private companionPostService: FindingCompanionService,
              private errorHandler: GlobalErrorHandler,
              private router: Router) { }

  ngOnInit() {
    this.setNavParams();
    this.router.events.subscribe(res => {
      this.isPreLoading = true;
    });
  }

  onScroll() {
    this.isLoading = true;
    this.page++;
    console.log('page-' + this.page);
    // Continue loading data
    this.getPosts(false, undefined);
  }

  submitFilter(postFilter: PostFilter) {
    this.isDisplayFilter = false;
    this.getPosts(true, postFilter);
  }

  setNavParams() {
    this.route.params.forEach(param => {
      // Get parameter from url
      this.personalNav = param['personal-nav'];

      // If personal-nav is not valid
      if (this.personalNav && this.VALID_PERSONAL_NAVS.indexOf(this.personalNav) === -1) {
        this.personalNav = 'bai-viet';
      }

      // Get posts
      this.getPosts(true, undefined);
    });
  }

  resetListPost() {
    this.page = 1;
    this.articleDisplay.items = [];
    this.articleDisplay.typeArticle = null;
  }

  getPosts(isReset: boolean, postFilter: PostFilter): void {
    if (!postFilter) {
      postFilter = new PostFilter();
      postFilter.topics = [];
      postFilter.timePeriod = 'all_time';

      this.isLoading = true;
    }

    if (this.personalNav != null) {
      const userId = this.getUserIdFromUrl();
      if (isReset) {
        this.page = 1;
        this.resetListPost();
      }
      if (this.personalNav === 'bai-viet') {
        this.postService.getAllArticlesByUserId(userId, postFilter, this.page).subscribe((data: []) => {
          this.articleDisplay.typeArticle = 'article';
          this.articleDisplay.items.push(...data);
          this.isLoading = false;
        }, this.errorHandler.handleError , () => {
          this.isPreLoading = false;
        });
      } else if (this.personalNav === 'chuyen-di') {
        this.showTopic = false;
        this.tripService.getVirtualTripsByUser(userId, postFilter, this.page).subscribe((data: []) => {
          this.articleDisplay.typeArticle = 'virtual-trip';
          this.articleDisplay.items.push(...data);
          this.isLoading = false;
        }, this.errorHandler.handleError, () => {
          this.isPreLoading = false;
        });
      } else if (this.personalNav === 'tim-ban-dong-hanh') {
        this.showTopic = false;
        this.companionPostService.getCompanionPostsByUser(userId, postFilter, this.page).subscribe((data: []) => {
          this.articleDisplay.typeArticle = 'companion-post';
          this.articleDisplay.items.push(...data);
          this.isLoading = false;
        }, this.errorHandler.handleError,  () => {
          this.isPreLoading = false;
        });
      }
    }
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
