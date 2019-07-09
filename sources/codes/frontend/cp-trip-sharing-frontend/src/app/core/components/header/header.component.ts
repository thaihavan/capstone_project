import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { LoginPageComponent } from 'src/app/pages/login-page/login-page.component';
import { NotifyService } from '../../services/notify-service/notify.service';
import { Notification } from 'src/app/model/Notification';
import { HttpErrorResponse } from '@angular/common/http';
import { Conversation } from 'src/app/model/Conversation';
import { ChatService } from '../../services/chat-service/chat.service';
import { ChatMessage } from 'src/app/model/ChatMessage';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  checkLogin: boolean;
  checkLogined: boolean;
  userId: string;
  urlImgavatar = 'https://qph.fs.quoracdn.net/main-qimg-573142324088396d86586adb93f4c8c2';
  numberNotification = 1;
  checkMessageNotification = false;
  checkNotification = false;

  notifications: Notification[] = [];
  conversations: Conversation[] = [];

  constructor(private dialog: MatDialog,
              private notifyService: NotifyService,
              private chatService: ChatService) {

  }

  ngOnInit() {
    if (localStorage.getItem('Token') != null) {
      this.checkLogin = false;
      this.checkLogined = true;
    } else {
      this.checkLogin = true;
      this.checkLogined = false;
    }

    this.getNotifications();
    this.getAllConversations();

    if (localStorage.getItem('User') != null) {
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
  }

  openDialogLoginForm() {
    const dialogRef = this.dialog.open(LoginPageComponent, {
      height: 'auto',
      width: '400px'
    });
  }

  signOut() {
    localStorage.clear();
    window.location.href = '/';
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
    this.checkNotification = true;
  }

  viewMessageNotification() {
    this.checkMessageNotification = true;
  }

  getNotifications() {
    this.notifyService.getNotifications().subscribe((res: Notification[]) => {
      console.log(res);
      this.notifications = res;
    }, (error: HttpErrorResponse) => {
      console.log(error);
    });
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
    }, (err: HttpErrorResponse) => {
      console.log(err);
    });
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
}
