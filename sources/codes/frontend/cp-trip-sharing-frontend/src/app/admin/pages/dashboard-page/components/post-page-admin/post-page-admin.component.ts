import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/model/Post';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { AdminService } from 'src/app/admin/services/admin-service/admin.service';

@Component({
  selector: 'app-post-page-admin',
  templateUrl: './post-page-admin.component.html',
  styleUrls: ['./post-page-admin.component.css']
})
export class PostPageAdminComponent implements OnInit {

  search: string;
  searchType: string;
  posts: Post[];
  page: number;

  constructor(private postService: PostService,
              private adminService: AdminService,
              private errorHandler: GlobalErrorHandler) {
    this.searchType = 'text';
    this.search = '';
    this.posts = [];
    this.page = 1;
  }

  ngOnInit() {
    this.getPosts(this.search);
  }

  searchByText() {
    this.posts = [];
    this.getPosts(this.search);
  }

  getPosts(search: string) {
    if (search === undefined || search == null) {
      search = '';
    }

    this.postService.getAllPosts(search, this.page).subscribe((res: Post[]) => {
      this.posts.push(...res);
    }, this.errorHandler.handleError);

  }

  removePost(post: Post) {
    post.isActive = false;
    this.updatePost(post);
  }

  restorePost(post: Post) {
    post.isActive = true;
    this.updatePost(post);
  }

  updatePost(post: Post) {
    this.adminService.updatePost(post).subscribe((res: any) => {
    }, this.errorHandler.handleError);
  }

}
