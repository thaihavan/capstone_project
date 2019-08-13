import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/model/User';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { HostGlobal } from 'src/app/core/global-variables';
import { AdminService } from 'src/app/admin/services/admin-service/admin.service';
import { MatDialog } from '@angular/material';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';

@Component({
  selector: 'app-user-page-admin',
  templateUrl: './user-page-admin.component.html',
  styleUrls: ['./user-page-admin.component.css']
})
export class UserPageAdminComponent implements OnInit {

  search: string;
  searchType: string;
  users: User[];
  page: number;

  constructor(private userService: UserService,
              private adminService: AdminService,
              private dialog: MatDialog,
              private errorHandler: GlobalErrorHandler) {
    this.searchType = 'text';
    this.search = '';
    this.users = [];
    this.page = 1;
  }

  ngOnInit() {
    this.getUsers(undefined);
  }

  searchByText() {
    this.users = [];
    this.getUsers(this.search);
  }

  getUsers(search: string) {
    if (search === undefined || search == null) {
      search = '';
    }

    this.userService.getUsers(search, this.page).subscribe((res: []) => {
      this.users.push(...res);
    }, this.errorHandler.handleError);
  }

  goToUserPage(user: User) {
    window.open(`${HostGlobal.HOST_FRONTEND}/user/${user.id}`, '_blank');
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
