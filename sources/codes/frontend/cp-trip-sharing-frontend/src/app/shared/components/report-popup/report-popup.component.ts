import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { ReportType } from 'src/app/model/ReportType';
import { ReportedUser } from 'src/app/model/ReportedUser';
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
        break;
      case 'comment':
        break;
    }

    this.dialogRef.close();
  }

  sendReportUser() {
    const reportedUser = new ReportedUser();
    reportedUser.reportTypeId = this.selectedReportType.id;
    reportedUser.content = this.reportContent.trim();
    reportedUser.userId = this.targetId;

    this.userService.sendReportUser(reportedUser).subscribe((res: any) => {
      console.log(res);
    }, this.errorHandler.handleError);
  }

}
