import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { NotifyService } from '../../services/notify-service/notify.service';
import { Notification } from 'src/app/model/Notification';
import { Conversation } from 'src/app/model/Conversation';
import { ChatService } from '../../services/chat-service/chat.service';
import { ChatMessage } from 'src/app/model/ChatMessage';
import { User } from 'src/app/model/User';
import { Account } from 'src/app/model/Account';
import { GlobalErrorHandler } from '../../globals/GlobalErrorHandler';
import { ChangePasswordComponent } from 'src/app/shared/components/change-password/change-password.component';
import { LoginPageComponent } from 'src/app/shared/components/login-page/login-page.component';
import { UserService } from '../../services/user-service/user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  user: User;
  account: Account;
  checkLogin: boolean;
  userId: string;

  numNewMessages = 0;
  numNewNotifications = 0;

  notifications: Notification[] = [];
  conversations: Conversation[] = [];
  selectedConversation: Conversation;
  showChatPopup = false;

  search = '';

  constructor(private dialog: MatDialog,
              private notifyService: NotifyService,
              private userService: UserService,
              private chatService: ChatService,
              private errorHandler: GlobalErrorHandler) {

  }

  ngOnInit() {
    this.account = JSON.parse(localStorage.getItem('Account'));
    this.user = JSON.parse(localStorage.getItem('User'));

    if (this.account != null && this.user != null) {
      this.checkLogin = false;

      if (this.account.role === 'member') {

        this.getNotifications();
        this.getAllConversations();
        this.getFollowings();
        this.getListPostIdBookmark();
        this.getListBlockers();
        this.getListBlockeds();

        this.initSocketConnection();
      } else if (this.account.role === 'admin') {
        window.location.href = '/admin/dashboard';
      }
    } else {
      this.checkLogin = true;
    }
  }

  initSocketConnection() {
    this.notifyService.initNotifyHubConnection();
    this.chatService.initChatConnection();

    this.notifyService.hubConnection.on('clientNotificationListener', () => {
      console.log('New notification received!');
      this.getNotifications();
    });

    this.chatService.hubConnection.on('clientMessageListener', (convId: string, message: ChatMessage) => {
      console.log('New message received');
      this.getAllConversations();
    });
  }

  openDialogLoginForm() {
    if (this.checkLogin) {
      const dialogRef = this.dialog.open(LoginPageComponent, {
        height: 'auto',
        width: '400px'
      });
    }
  }

  signOut() {
    localStorage.clear();
    this.userService.Logout().subscribe((res) => {
      window.location.href = '/';
    }, this.errorHandler.handleError);
  }

  gotoPersonalPage() {
    const account = JSON.parse(localStorage.getItem('Account'));
    this.userId = account.userId;
    window.location.href = '/user/' + this.userId;
  }

  gotoBookmarkList() {
    const account = JSON.parse(localStorage.getItem('Account'));
    this.userId = account.userId;
    window.location.href = '/user/' + this.userId + '/da-danh-dau';
  }

  gotoBlockedList() {
    const account = JSON.parse(localStorage.getItem('Account'));
    this.userId = account.userId;
    window.location.href = '/user/' + this.userId + '/danh-sach-chan';
  }

  viewNotification() {
    this.numNewNotifications = 0;
    if (this.notifications != null && this.notifications.length > 0) {
      this.notifyService.SeenNotification(this.notifications[0].id);
    }
  }

  viewMessageNotification() {
    this.numNewMessages = 0;
  }

  getNotifications() {
    this.notifyService.getNotifications().subscribe((res: Notification[]) => {
      this.notifications = res;
      this.calcNumNewNotification();
    }, this.errorHandler.handleError);
  }

  getAllConversations() {
    const user = JSON.parse(localStorage.getItem('User'));
    if (user == null) {
      return;
    }

    this.chatService.getAllConversations(user.id).subscribe((result: Conversation[]) => {
      console.log(result);
      this.conversations = result;
      this.setConversationInfo(user);
      this.calcNumNewMessage();
    }, this.errorHandler.handleError);
  }

  setConversationInfo(user: any) {
    for (const c of this.conversations) {
      if (c.type === 'private') {
        for (const u of c.users) {
          if (u.id !== user.id) {
            c.avatar = u.profileImage;
            c.name = u.displayName;
            break;
          }
        }
      }
    }
  }

  selectConversation(conversation: Conversation) {
    this.selectedConversation = conversation;
    this.showChatPopup = true;
  }

  closeChatPopup() {
    this.showChatPopup = false;
  }

  calcNumNewMessage(): void {
    this.numNewMessages = 0;
    const account = JSON.parse(localStorage.getItem('Account'));
    this.conversations.forEach(c => {
      if (c.seenIds.indexOf(account.userId) === -1) {
        this.numNewMessages++;
      }
    });
  }

  calcNumNewNotification(): void {
    this.numNewNotifications = 0;
    const account = JSON.parse(localStorage.getItem('Account'));
    for (const n of this.notifications) {
      if (n.seenIds.indexOf(account.userId) === -1) {
        this.numNewNotifications++;
      } else {
        break;
      }
    }
  }

  onSearchBtnClick() {
    if (this.search && this.search.trim() !== '') {
      window.location.href = 'search/text/bai-viet/' + this.search;
    }
  }

  getFollowings() {
    this.userService.getAllFollowingId(this.account.userId).subscribe((result: any) => {
      localStorage.setItem('listUserIdFollowing', JSON.stringify(result));
    }, this.errorHandler.handleError);
  }

  getListPostIdBookmark() {
    this.userService.getListPostIdBookmarks(this.account.token).subscribe((result: any) => {
      localStorage.setItem('listPostIdBookmark', JSON.stringify(result));
    }, this.errorHandler.handleError);
  }

  getListBlockers() {
    this.userService.getBlockers().subscribe((res: User[]) => {
      localStorage.setItem('listBlockers', JSON.stringify(res));
    }, this.errorHandler.handleError);
  }

  getListBlockeds() {
    this.userService.getBlockeds().subscribe((res: User[]) => {
      localStorage.setItem('listBlockeds', JSON.stringify(res));
    }, this.errorHandler.handleError);
  }

  // dialog change password
  openDialogChangePassword() {
      const dialogRef = this.dialog.open(ChangePasswordComponent, {
        height: 'auto',
        width: '400px'
      });
  }
}
