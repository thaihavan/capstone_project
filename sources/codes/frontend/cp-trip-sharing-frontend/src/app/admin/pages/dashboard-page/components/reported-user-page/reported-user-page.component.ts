import { Component, OnInit } from '@angular/core';
import { Report } from 'src/app/model/Report';
import { AdminService } from 'src/app/admin/services/admin-service/admin.service';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { User } from 'src/app/model/User';
import { MatDialog } from '@angular/material';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';

@Component({
  selector: 'app-reported-user-page',
  templateUrl: './reported-user-page.component.html',
  styleUrls: ['./reported-user-page.component.css']
})
export class ReportedUserPageComponent implements OnInit {
  reportedUsers: Report[];

  constructor(private adminService: AdminService,
              private dialog: MatDialog,
              private errorHandler: GlobalErrorHandler) {
    this.reportedUsers = [];
  }

  ngOnInit() {
    this.getReportedUsers();
  }

  getReportedUsers() {
    this.adminService.getReportedUsers().subscribe((res: Report[]) => {
      this.reportedUsers = res;
    }, this.errorHandler.handleError);
  }

  resolve(reportedUser: Report) {
    this.adminService.resolveReportedUser(reportedUser).subscribe((res: Report) => {
      if (res == null) {
        reportedUser.isResolved = false;
      }
      reportedUser.isResolved = true;
    }, this.errorHandler.handleError);
  }

  banUser(user: User) {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '500px',
      height: 'auto',
      position: {
        top: '20px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = `Bạn có chắc muốn đình chỉ người dùng này không?`;
    instance.message.messageType = 'confirm';

    dialogRef.afterClosed().subscribe((res: string) => {
      if (res === 'continue') {
        this.adminService.banUser(user.id).subscribe((result: any) => {
          user.active = false;
        }, this.errorHandler.handleError);
      }
    });
  }

  unbanUser(user: User) {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '500px',
      height: 'auto',
      position: {
        top: '20px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = `Bạn có chắc muốn bỏ đình chỉ người dùng này không?`;
    instance.message.messageType = 'confirm';

    dialogRef.afterClosed().subscribe((res: string) => {
      if (res === 'continue') {
        this.adminService.unbanUser(user.id).subscribe((result: any) => {
          user.active = true;
        }, this.errorHandler.handleError);
      }
    });
  }

}
