import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { HostGlobal } from '../../global-variables';
import { Observable } from 'rxjs';
import { Notification } from 'src/app/model/Notification';
import { HttpHeaders, HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class NotifyService {
  user: any;
  hubConnection: HubConnection;

  baseUrl = HostGlobal.HOST_NOTIFY_SERVICE + '/api/notificationservice/notification';

  constructor(private http: HttpClient) {
    this.user = JSON.parse(localStorage.getItem('User'));
    // this.initNotifyHubConnection(undefined);
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
    this.hubConnection.invoke('SeenNotification', notificationId)
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
}
