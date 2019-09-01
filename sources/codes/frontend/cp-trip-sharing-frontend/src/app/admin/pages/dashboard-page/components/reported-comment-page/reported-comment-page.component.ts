import { Component, OnInit } from '@angular/core';
import { Report } from 'src/app/model/Report';
import { AdminService } from 'src/app/admin/services/admin-service/admin.service';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { MatDialog } from '@angular/material';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';
import { Comment } from 'src/app/model/Comment';

@Component({
  selector: 'app-reported-comment-page',
  templateUrl: './reported-comment-page.component.html',
  styleUrls: ['./reported-comment-page.component.css']
})
export class ReportedCommentPageComponent implements OnInit {

  reportedComments: Report[];
  constructor(private adminService: AdminService,
              private dialog: MatDialog,
              private errorHandler: GlobalErrorHandler) {
    this.reportedComments = [];
  }

  ngOnInit() {
    this.getReportedComments();
  }

  getReportedComments() {
    this.adminService.getReportedComments().subscribe((res: Report[]) => {
      this.reportedComments = res;
    }, this.errorHandler.handleError);
  }

  removeComment(comment: Comment) {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '500px',
      height: 'auto',
      position: {
        top: '20px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = `Bạn có chắc chắn muốn gỡ bỏ bình luận này không?`;
    instance.message.messageType = 'confirm';

    dialogRef.afterClosed().subscribe((res: string) => {
      if (res === 'continue') {
        comment.isActive = false;
        this.adminService.updateComment(comment).subscribe((result: any) => {
        }, this.errorHandler.handleError);
      }
    });
  }

  restoreComment(comment: Comment) {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '500px',
      height: 'auto',
      position: {
        top: '20px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = `Bạn có chắc chắn muốn khôi phục bình luận này không?`;
    instance.message.messageType = 'confirm';

    dialogRef.afterClosed().subscribe((res: string) => {
      if (res === 'continue') {
        comment.isActive = true;
        this.adminService.updateComment(comment).subscribe((result: any) => {
        }, this.errorHandler.handleError);
      }
    });
  }

}
