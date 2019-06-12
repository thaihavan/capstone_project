import { Component, OnInit } from '@angular/core';
import { PostService, Post } from 'src/app/core/services/service-1/post.service';

@Component({
  selector: 'app-created-post',
  templateUrl: './created-post.component.html',
  styleUrls: ['./created-post.component.css']
})
export class CreatedPostComponent implements OnInit {
  posts: Post[] = [];
  pageIndex: 1;
  isLoading = true;
  constructor(private postService: PostService) {}

  ngOnInit() {
    this.postService.getPosts().subscribe(posts => (
      this.posts = posts,
      this.isLoading = false
    ));
  }
  onScroll() {
    this.isLoading = true;
    console.log('hello phong!');
    this.postService.getPosts().subscribe(posts => (this.posts.push(...posts),
    this.isLoading = false
    ));
  }
}
