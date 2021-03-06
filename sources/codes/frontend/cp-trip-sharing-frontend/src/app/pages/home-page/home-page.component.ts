import { Component, OnInit, ViewChildren } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Article } from 'src/app/model/Article';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { PostFilter } from 'src/app/model/PostFilter';
import { VirtualTrip } from 'src/app/model/VirtualTrip';
import { VirtualTripService } from 'src/app/core/services/post-service/virtual-trip.service';
import { FindingCompanionService } from 'src/app/core/services/post-service/finding-companion.service';
import { CompanionPost } from 'src/app/model/CompanionPost';
import { ArticleDestinationItem } from 'src/app/model/ArticleDestinationItem';
import { User } from 'src/app/model/User';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/core/services/alertify-service/alertify.service';
import { ListPostHorizontalComponent } from 'src/app/shared/components/list-post-horizontal/list-post-horizontal.component';
import { element } from '@angular/core/src/render3';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  coverImage = '../../../assets/cover-image.png';
  user: User;

  recommendedArticles: Article[] = [];
  isRecomendedLoading = true;

  popularArticles: Article[] = [];
  isPopularLoading = true;

  newestArticles: Article[] = [];
  isNewesLoading = true;

  virtualTrips: VirtualTrip[] = [];
  isVirtualLoading = true;

  companionPosts: CompanionPost[] = [];
  isCompanionLoading = true;

  @ViewChildren(ListPostHorizontalComponent) listPostHori: ListPostHorizontalComponent[];
  constructor(private titleService: Title,
              private postService: PostService,
              private virtualTripService: VirtualTripService,
              private companionPostService: FindingCompanionService,
              private errorHandler: GlobalErrorHandler,
              private router: Router,
              private alertify: AlertifyService) {
    this.titleService.setTitle('Trang chủ');
    this.user = JSON.parse(localStorage.getItem('User'));
  }

  ngOnInit() {
    if (this.user != null) {
      this.getRecommendArticles(undefined);
    }
    this.getNewestArticles(undefined);
    this.getPopularArticles(undefined);
    this.getVirtualTrips(undefined);
    this.getCompanionPosts(undefined);
  }

  getNewestArticles(postFilter: PostFilter): void {
    if (!postFilter) {
      postFilter = new PostFilter();
      postFilter.topics = [];
      postFilter.timePeriod = 'all_time';
    }
    this.postService.getAllArticles(postFilter, 1).subscribe((data: Article[]) => {
      this.newestArticles = data;
      // this.newestArticles = this.filterBlocker(this.newestArticles);
      if (this.newestArticles != null && this.newestArticles.length > 6) {
        this.newestArticles = this.newestArticles.slice(0, 6);
      }
    }, this.errorHandler.handleError,
    () => {
      this.isNewesLoading = false;
    });
  }

  getPopularArticles(postFilter: PostFilter): void {
    if (!postFilter) {
      postFilter = new PostFilter();
      postFilter.topics = [];
      postFilter.timePeriod = 'all_time';
    }
    this.postService.getPopularArticles(postFilter, 1).subscribe((data: Article[]) => {
      this.popularArticles = data;
      // this.popularArticles = this.filterBlocker(this.popularArticles);
      if (this.popularArticles != null && this.popularArticles.length > 6) {
        this.popularArticles = this.popularArticles.slice(0, 6);
      }
    }, this.errorHandler.handleError,
    () => {
      this.isPopularLoading = false;
    });
  }

  getRecommendArticles(postFilter: PostFilter): void {
    if (!postFilter) {
      postFilter = new PostFilter();
      postFilter.topics = [];
      postFilter.timePeriod = 'all_time';
    }
    this.postService.getRecommendArticles(postFilter, 1).subscribe((data: Article[]) => {
      this.recommendedArticles = data;
      // this.recommendedArticles = this.filterBlocker(this.recommendedArticles);
      if (this.recommendedArticles != null && this.recommendedArticles.length > 6) {
        this.recommendedArticles = this.recommendedArticles.slice(0, 6);
      }
    }, this.errorHandler.handleError,
    () => {
      this.isRecomendedLoading = false;
    });
  }

  getVirtualTrips(postFilter: PostFilter): void {
    if (!postFilter) {
      postFilter = new PostFilter();
      postFilter.topics = [];
      postFilter.timePeriod = 'all_time';
    }

    this.virtualTripService.getVirtualTrips(postFilter, 1).subscribe(data => {
      this.virtualTrips = data;
      // this.virtualTrips = this.filterBlocker(this.virtualTrips);
      if (this.virtualTrips != null && this.virtualTrips.length > 6) {
        this.virtualTrips = this.virtualTrips.slice(0, 6);
      }
    }, this.errorHandler.handleError,
    () => {
      this.isVirtualLoading = false;
    });
  }

  getCompanionPosts(postFilter: PostFilter): void {
    if (!postFilter) {
      postFilter = new PostFilter();
      postFilter.topics = [];
      postFilter.timePeriod = 'all_time';
    }

    this.companionPostService.getCompanionPosts(postFilter, 1).subscribe(data => {
      this.companionPosts = data;
      // this.companionPosts = this.filterBlocker(this.companionPosts);
      if (this.companionPosts != null && this.companionPosts.length > 6) {
        this.companionPosts = this.companionPosts.slice(0, 6);
      }
    }, this.errorHandler.handleError,
    () => {
      this.isCompanionLoading = false;
    });
  }

  // on google-map-search submit add address location.
  setAddress(addrObj) {
    if (!addrObj) {
      this.alertify.error('Địa điểm không tồn tại');
      return;
    }
    const searchDestination = new  ArticleDestinationItem();
    searchDestination.id = addrObj.locationId;
    searchDestination.name = addrObj.name;
    // this.router.navigate(['/search/location/bai-viet', addrObj.locationId ]);
    window.location.href = `/search/location/bai-viet/${addrObj.locationId}`;
  }

  checkFollowStages(event) {
    // tslint:disable-next-line:no-shadowed-variable
    console.log(event);
    console.log(this.listPostHori);
    this.listPostHori.forEach(listPost => {
      if (listPost !== event) {
        listPost.getStages();
      }
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
}
