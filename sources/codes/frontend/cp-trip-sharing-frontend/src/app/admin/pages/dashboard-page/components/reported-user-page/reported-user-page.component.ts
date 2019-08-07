import { Component, OnInit } from '@angular/core';
import { ReportedUser } from 'src/app/model/ReportedUser';
import { AdminService } from 'src/app/admin/services/admin-service/admin.service';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';

@Component({
  selector: 'app-reported-user-page',
  templateUrl: './reported-user-page.component.html',
  styleUrls: ['./reported-user-page.component.css']
})
export class ReportedUserPageComponent implements OnInit {
  reportedUsers: ReportedUser[];

  constructor(private adminService: AdminService,
              private errorHanlder: GlobalErrorHandler) {
    this.reportedUsers = [];
  }

  ngOnInit() {
    this.getReportedUsers();
  }

  getReportedUsers() {
    this.adminService.getReportedUsers().subscribe((res: ReportedUser[]) => {
      this.reportedUsers = res;
    }, this.errorHanlder.handleError);
  }

  resolve(reportedUser: ReportedUser) {
    this.adminService.resolveReportedUser(reportedUser).subscribe((res: ReportedUser) => {
      if (res == null) {
        reportedUser.isResolved = false;
      }
      reportedUser.isResolved = true;
    }, this.errorHanlder.handleError);
  }

}
