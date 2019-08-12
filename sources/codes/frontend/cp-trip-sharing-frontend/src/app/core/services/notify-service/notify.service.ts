import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { HostGlobal } from '../../global-variables';
import { Observable } from 'rxjs';
import { Notification } from 'src/app/model/Notification';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { User } from 'src/app/model/User';
import { NotificationTemplates } from '../../globals/NotificationTemplates';
import { Post } from 'src/app/model/Post';
import { ChatUser } from 'src/app/model/ChatUser';

@Injectable({
  providedIn: 'root'
})
export class NotifyService {
  user: User;
  hubConnection: HubConnection;

  baseUrl = HostGlobal.HOST_NOTIFY_SERVICE + '/api/notificationservice/notification';

  constructor(private http: HttpClient) {
    this.user = JSON.parse(localStorage.getItem('User'));
    // this.initNotifyHubConnection();
  }

  initNotifyHubConnection() {
    // Init connection
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(
        HostGlobal.HOST_NOTIFY_SERVICE + '/notification?userId=' + this.user.id,
        {
          accessTokenFactory: () => localStorage.getItem('Token')
        }
      )
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('Notify connection started!');
      })
      .catch((error) => {
        console.log(error);
      });
  }

  sendNotification(notification: Notification) {
    this.hubConnection.invoke('SendNotification', notification)
      .then(() => { })
      .catch((error) => {
        console.log(error);
      });
  }

  SeenNotification(notificationId: string) {
    this.hubConnection.invoke('SeenNotification', notificationId, this.user.id)
      .then(() => { })
      .catch((error) => {
        console.log(error);
      });
  }

  getNotifications(): Observable<Notification[]> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.get<Notification[]>(this.baseUrl, httpOption);
  }

  /**
   * Define notification
   */

  /**
   * Like notification
   * param: from: User
   * param: post: any (Article or CompanionPost)
   */
  sendLikeNotification(from: User, post: any) {
    const notification = new Notification();
    notification.content = new NotificationTemplates()
    .getLikePostNotiTemplate(from.displayName, post.post.title);
    notification.displayImage = from.avatar;
    notification.receivers = [post.post.author.id];
    notification.url = HostGlobal.HOST_FRONTEND + '/bai-viet/' + post.id;
    notification.seenIds = [from.id];
    this.sendNotification(notification);
  }

  /**
   * Comment notification
   * param: from: User
   * param: post: any (Article or CompanionPost)
   */
  sendCommentNotification(from: User, post: any) {
    const notification = new Notification();
    notification.content = new NotificationTemplates()
      .getCommentedNotiTemplate(from.displayName, post.post.title);
    notification.displayImage = from.avatar;
    notification.receivers = [post.post.author.id];
    notification.url = HostGlobal.HOST_FRONTEND + '/bai-viet/' + post.id;
    notification.seenIds = [from.id];
    this.sendNotification(notification);
  }

  /**
   * Join request notification
   * param: from: User
   * param: post: any (Article or CompanionPost)
   */
  sendJoinRequestNotification(from: User, post: any) {
    const notification = new Notification();
    notification.content = new NotificationTemplates()
      .getNewJoinRequestNotiTemplate(from.displayName, post.post.title);
    notification.displayImage = from.avatar;
    notification.receivers = [post.post.author.id];
    notification.url = HostGlobal.HOST_FRONTEND + '/tim-ban-dong-hanh/' + post.id;
    notification.seenIds = [from.id];
    this.sendNotification(notification);
  }

  /**
   * Accept join request notification
   * param: from: User
   * param: post: any (Article or CompanionPost)
   */
  sendJoinRequestAcceptedNotification(from: User, post: any, toUserId: string) {
    const notification = new Notification();
    notification.content = new NotificationTemplates()
      .getJoinRequestAcceptedNotiTemplate(from.displayName, post.post.title);
    notification.displayImage = from.avatar;
    notification.receivers = [toUserId];
    notification.url = HostGlobal.HOST_FRONTEND + '/tim-ban-dong-hanh/' + post.id;
    notification.seenIds = [from.id];
    this.sendNotification(notification);
  }
}
