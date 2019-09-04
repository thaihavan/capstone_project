import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/model/Post';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { AdminService } from 'src/app/admin/services/admin-service/admin.service';
import { MatDialog } from '@angular/material';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';

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
  isLoading = true;

  constructor(private postService: PostService,
              private adminService: AdminService,
              private dialog: MatDialog,
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
    this.isLoading = true;
    this.page = 1;
    this.posts = [];
    this.getPosts(this.search);
  }

  getPosts(search: string) {
    if (search === undefined || search == null) {
      search = '';
    }

    this.postService.getAllPosts(search, this.page).subscribe((res: Post[]) => {
      this.posts.push(...res);
      if (this.page * 12 > this.posts.length) {
        this.isLoading = false;
      }
    }, this.errorHandler.handleError);

  }

  removePost(post: Post) {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '500px',
      height: 'auto',
      position: {
        top: '20px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = `Bạn có chắc chắn muốn gỡ bỏ bài viết này không?`;
    instance.message.messageType = 'confirm';

    dialogRef.afterClosed().subscribe((res: string) => {
      if (res === 'continue') {
        post.isActive = false;
        this.updatePost(post);
      }
    });
  }

  restorePost(post: Post) {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '500px',
      height: 'auto',
      position: {
        top: '20px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = `Bạn có chắc chắn muốn khôi phục bài viết này không?`;
    instance.message.messageType = 'confirm';

    dialogRef.afterClosed().subscribe((res: string) => {
      if (res === 'continue') {
        post.isActive = true;
        this.updatePost(post);
      }
    });
  }

  updatePost(post: Post) {
    this.adminService.updatePost(post).subscribe((res: any) => {
    }, this.errorHandler.handleError);
  }
  onScroll() {
    if (this.posts.length >= 12) {
      this.page += 1;
      this.getPosts(this.search);
    }
  }
}
