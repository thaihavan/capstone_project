import { Component, OnInit, Input } from '@angular/core';
import { PostService } from 'src/app/core/services/fackeData-service/post.service';
import { Post } from 'src/app/model/Post';
import { ActivatedRoute } from '@angular/router';

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
  pageIndex: 1;
  isLoading = true;

  homeNav: string;
  personalNav: string;

  constructor(private route: ActivatedRoute, private postService: PostService) { }

  ngOnInit() {
    this.setNavParams();

    console.log('home-nav:', this.homeNav);
    console.log('personal-nav:', this.personalNav);

    this.getPost();
  }
  onScroll() {
    console.log('list-post-on-scrole');
    this.isLoading = true;

    // Continue loading data
    this.getPost();
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
    if (this.homeNav != null) {
      // Call api
      this.postService.getPosts().subscribe(posts => {
        this.posts.push(...posts);
        this.isLoading = false;
      });
    } else if (this.personalNav != null) {
      // Call api
      this.postService.getPosts().subscribe(posts => {
        this.posts.push(...posts);
        this.isLoading = false;
      });
    }
  }

}
