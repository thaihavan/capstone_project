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
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
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

  personalNav: string;
  postFilter: PostFilter;

  isScrollTopShow = false;
  topPosToStartShowing = 100;

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

  constructor(private route: ActivatedRoute, private postService: PostService,
              private userService: UserService,
              private tripService: VirtualTripService,
              private errorHandler: GlobalErrorHandler) { }

  ngOnInit() {
    this.getTopics();
    this.setNavParams();
  }

  onScroll() {
    this.isLoading = true;
    this.page++;
    console.log('page-' + this.page);
    // Continue loading data
    this.getPosts(false);
  }

  onButtonToggleChange(value: string) {
    this.postFilter.timePeriod = value;
  }

  submitFilter() {
    this.isDisplayFilter = false;
    this.getPosts(true);
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
      this.personalNav = param['personal-nav'];

      // If personal-nav is not valid
      if (this.personalNav && this.VALID_PERSONAL_NAVS.indexOf(this.personalNav) === -1) {
        this.personalNav = 'bai-viet';
      }

      // Init post filter
      this.initPostFilter();

      // Get posts
      this.getPosts(true);
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
    }, this.errorHandler.handleError);
  }

  resetListPost() {
    this.page = 1;
    this.articleDisplay.items = [];
    this.articleDisplay.typeArticle = null;
  }

  getPosts(isReset: boolean): void {
    const postFilter: PostFilter = JSON.parse(JSON.stringify(this.postFilter));
    if (this.postFilter.topics.length === this.topics.length) {
      postFilter.topics = [];
    }

    if (this.personalNav != null) {
      const userId = this.getUserIdFromUrl();
      if (isReset) {
        this.page = 1;
      }
      if (this.personalNav === 'bai-viet') {
        this.postService.getAllArticlesByUserId(userId, postFilter, this.page).subscribe((data: []) => {
          if (isReset) {
            this.resetListPost();
          }
          this.articleDisplay.typeArticle = 'article';
          this.articleDisplay.items.push(...data);
          this.isLoading = false;
        }, this.errorHandler.handleError);
      } else if (this.personalNav === 'chuyen-di') {
        this.tripService.getVirtualTripsByUser(userId, postFilter, this.page).subscribe((data: []) => {
          if (isReset) {
            this.resetListPost();
          }
          this.articleDisplay.typeArticle = 'virtual-trip';
          this.articleDisplay.items.push(...data);
          this.isLoading = false;
        }, this.errorHandler.handleError);
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
