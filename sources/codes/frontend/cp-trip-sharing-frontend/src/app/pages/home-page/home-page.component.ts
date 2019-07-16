import { Component, OnInit, HostListener, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { Article } from 'src/app/model/Article';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { PostFilter } from 'src/app/model/PostFilter';
import { HttpErrorResponse } from '@angular/common/http';
import { VirtualTrip } from 'src/app/model/VirtualTrip';
import { VirtualTripService } from 'src/app/core/services/post-service/virtual-trip.service';
import { FindingCompanionService } from 'src/app/core/services/post-service/finding-companion.service';
import { CompanionPost } from 'src/app/model/CompanionPost';
import { LocationMarker } from 'src/app/model/LocationMarker';
import { ArticleDestinationItem } from 'src/app/model/ArticleDestinationItem';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  coverImage = '../../../assets/cover-image.png';

  recommendedArticles: Article[] = [];
  popularArticles: Article[] = [];
  newestArticles: Article[] = [];

  virtualTrips: VirtualTrip[] = [];
  companionPosts: CompanionPost[] = [];

  constructor(private titleService: Title,
              private postService: PostService,
              private virtualTripService: VirtualTripService,
              private companionPostService: FindingCompanionService,
              private zone: NgZone) {
    this.titleService.setTitle('Trang chủ');
  }

  ngOnInit() {
    this.getArticles(undefined);
    this.getVirtualTrips(undefined);
    this.getCompanionPosts(undefined);
  }

  getArticles(postFilter: PostFilter): void {
    if (!postFilter) {
      postFilter = new PostFilter();
      postFilter.topics = [];
      postFilter.timePeriod = 'all_time';
    }
    this.postService.getAllArticles(postFilter).subscribe((data: Article[]) => {
      this.newestArticles = data;
      if (this.newestArticles != null && this.newestArticles.length > 6) {
        this.newestArticles = this.newestArticles.slice(0, 6);
      }
    }, (err: HttpErrorResponse) => {
      console.log(err);
    });
  }

  getVirtualTrips(postFilter: PostFilter): void {
    if (!postFilter) {
      postFilter = new PostFilter();
      postFilter.topics = [];
      postFilter.timePeriod = 'all_time';
    }

    this.virtualTripService.getVirtualTrips(postFilter).subscribe(data => {
      this.virtualTrips = data;
      if (this.virtualTrips != null && this.virtualTrips.length > 6) {
        this.virtualTrips = this.virtualTrips.slice(0, 6);
      }
    }, (err: HttpErrorResponse) => {
      console.log(err);
    });
  }

  getCompanionPosts(postFilter: PostFilter): void {
    if (!postFilter) {
      postFilter = new PostFilter();
      postFilter.topics = [];
      postFilter.timePeriod = 'all_time';
    }

    this.companionPostService.getCompanionPosts(postFilter).subscribe(data => {
      this.companionPosts = data;
      if (this.companionPosts != null && this.companionPosts.length > 6) {
        this.companionPosts = this.companionPosts.slice(0, 6);
      }
    }, (err: HttpErrorResponse) => {
      console.log(err);
    });
  }

   // on google-map-search submit add address location.
   setAddress(addrObj) {
    const searchDestination = new  ArticleDestinationItem();
    searchDestination.id = addrObj.locationId;
    searchDestination.name = addrObj.name;
  }
}
