import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { ReportType } from 'src/app/model/ReportType';
import { ReportedUser } from 'src/app/model/ReportedUser';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-report-popup',
  templateUrl: './report-popup.component.html',
  styleUrls: ['./report-popup.component.css']
})
export class ReportPopupComponent implements OnInit {
  title: string;
  userId: string;
  listReportTypes: ReportType[];
  selectedReportType: ReportType;

  showReasonOthers = false;
  reportContent: string;

  constructor(private userService: UserService,
              private errorHandler: GlobalErrorHandler,
              private dialogRef: MatDialogRef<ReportPopupComponent>) {
                this.listReportTypes = [];
                this.selectedReportType = null;
                this.reportContent = '';
              }

  ngOnInit() {
    this.getReportUserTypes();
  }

  getReportUserTypes() {
    this.userService.getReportUserTypes().subscribe((res: ReportType[]) => {
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

    const reportedUser = new ReportedUser();
    reportedUser.reportTypeId = this.selectedReportType.id;
    reportedUser.content = this.reportContent.trim();
    reportedUser.userId = this.userId;

    this.userService.sendReportUser(reportedUser).subscribe((res: any) => {
      console.log(res);
    }, this.errorHandler.handleError);

    this.dialogRef.close();
  }

}
