import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/admin/services/admin-service/admin.service';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { Report } from 'src/app/model/Report';
import { Post } from 'src/app/model/Post';

@Component({
  selector: 'app-reported-post-page',
  templateUrl: './reported-post-page.component.html',
  styleUrls: ['./reported-post-page.component.css']
})
export class ReportedPostPageComponent implements OnInit {

  reportedPosts: Report[];
  constructor(private adminService: AdminService,
              private errorHandler: GlobalErrorHandler) {
    this.reportedPosts = [];
  }

  ngOnInit() {
    this.getReportedPosts();
  }

  getReportedPosts() {
    this.adminService.getReportedPosts().subscribe((res: Report[]) => {
      this.reportedPosts = res;
      console.log(this.reportedPosts);
    }, this.errorHandler.handleError);
  }

  resolve(reportedPost: Report) {
    this.adminService.resolveReport(reportedPost).subscribe((res: Report) => {
      if (res == null) {
        reportedPost.isResolved = false;
      }
      reportedPost.isResolved = true;
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
