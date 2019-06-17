import { Component, OnInit, Input } from '@angular/core';
import { PostService } from 'src/app/core/services/fackeData-service/post.service';
import { Post } from 'src/app/model/Post';

@Component({
  selector: 'app-list-post',
  templateUrl: './list-post.component.html',
  styleUrls: ['./list-post.component.css']
})
export class ListPostComponent implements OnInit {

  posts: Post[] = [];
  pageIndex: 1;
  isLoading = true;
  constructor(private postService: PostService) {}

  ngOnInit() {
    this.postService.getPosts().subscribe(posts => {
      this.posts.push(...posts);
      this.isLoading = false;
    });
  }
  onScroll() {
    this.isLoading = true;
  }

}
