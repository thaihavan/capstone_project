import { Component, OnInit } from '@angular/core';
import { Report } from 'src/app/model/Report';
import { AdminService } from 'src/app/admin/services/admin-service/admin.service';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';

@Component({
  selector: 'app-reported-comment-page',
  templateUrl: './reported-comment-page.component.html',
  styleUrls: ['./reported-comment-page.component.css']
})
export class ReportedCommentPageComponent implements OnInit {

  reportedComments: Report[];
  constructor(private adminService: AdminService,
              private errorHandler: GlobalErrorHandler) {
    this.reportedComments = [];
  }

  ngOnInit() {
    this.getReportedComments();
  }

  getReportedComments() {
    this.adminService.getReportedComments().subscribe((res: Report[]) => {
      this.reportedComments = res;
      console.log(this.reportedComments);
    }, this.errorHandler.handleError);
  }

}
