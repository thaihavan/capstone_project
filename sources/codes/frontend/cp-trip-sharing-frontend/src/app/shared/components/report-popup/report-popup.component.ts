import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { ReportType } from 'src/app/model/ReportType';
import { Report } from 'src/app/model/Report';
import { MatDialogRef } from '@angular/material';
import { PostService } from 'src/app/core/services/post-service/post.service';

@Component({
  selector: 'app-report-popup',
  templateUrl: './report-popup.component.html',
  styleUrls: ['./report-popup.component.css']
})
export class ReportPopupComponent implements OnInit {
  type: string;
  title: string;
  targetId: string;
  listReportTypes: ReportType[];
  selectedReportType: ReportType;

  showReasonOthers = false;
  reportContent: string;

  constructor(private userService: UserService,
              private postService: PostService,
              private errorHandler: GlobalErrorHandler,
              private dialogRef: MatDialogRef<ReportPopupComponent>) {
                this.listReportTypes = [];
                this.selectedReportType = null;
                this.reportContent = '';
              }

  ngOnInit() {
    switch (this.type) {
      case 'user':
        this.getReportUserTypes();
        break;
      case 'post':
        this.getReportPostTypes();
        break;
      case 'comment':
        this.getReportPostTypes();
        break;
    }
  }

  getReportUserTypes() {
    this.userService.getReportUserTypes().subscribe((res: ReportType[]) => {
      this.listReportTypes = res;
    }, this.errorHandler.handleError);
  }

  getReportPostTypes() {
    this.postService.getReportTypes().subscribe((res: ReportType[]) => {
      this.listReportTypes = res;
    }, this.errorHandler.handleError);
  }

  onClickReportType(reportType: ReportType) {
    this.selectedReportType = reportType;
    if (reportType.name === 'Lí do khác') {
      this.showReasonOthers = true;
    } else {
      this.showReasonOthers = false;
      this.reportContent = '';
    }
  }

  sendReport() {
    if (this.selectedReportType == null) {
      return;
    }

    switch (this.type) {
      case 'user':
        this.sendReportUser();
        break;
      case 'post':
        this.sendReportPost();
        break;
      case 'comment':
        this.sendReportComment();
        break;
    }

    this.dialogRef.close();
  }

  sendReportUser() {
    const reportedUser = new Report();
    reportedUser.reportTypeId = this.selectedReportType.id;
    reportedUser.content = this.reportContent.trim();
    reportedUser.targetId = this.targetId;

    this.userService.sendReportUser(reportedUser).subscribe((res: any) => {
    }, this.errorHandler.handleError);
  }

  sendReportPost() {
    const reportPost = new Report();
    reportPost.reportTypeId = this.selectedReportType.id;
    reportPost.content = this.reportContent.trim();
    reportPost.targetId = this.targetId;
    reportPost.targetType = 'post';

    this.postService.sendReport(reportPost).subscribe((res: any) => {
    }, this.errorHandler.handleError);
  }

  sendReportComment() {
    const reportComment = new Report();
    reportComment.reportTypeId = this.selectedReportType.id;
    reportComment.content = this.reportContent.trim();
    reportComment.targetId = this.targetId;
    reportComment.targetType = 'comment';

    this.postService.sendReport(reportComment).subscribe((res: any) => {
    }, this.errorHandler.handleError);
  }

}
