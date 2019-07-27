import { Component, OnInit, Input } from '@angular/core';
import { Notification } from 'src/app/model/Notification';
import { User } from 'src/app/model/User';

@Component({
  selector: 'app-list-notification',
  templateUrl: './list-notification.component.html',
  styleUrls: ['./list-notification.component.css']
})
export class ListNotificationComponent implements OnInit {
  @Input() notifications: Notification[] = [];

  user: User;
  listNotSeen: string[] = [];

  constructor() {
    this.user = JSON.parse(localStorage.getItem('User'));
  }

  ngOnInit() {
  }

  // tslint:disable-next-line: use-life-cycle-interface
  ngOnChanges() {
    this.getListNotSeen();
  }

  getListNotSeen() {
    for (const noti of this.notifications) {
      if (noti.seenIds.indexOf(this.user.id) === -1) {
        this.listNotSeen.push(noti.id);
        console.log(this.listNotSeen);
      } else {
        break;
      }
    }
  }

  isNotSeen(notificationId: string) {
    return this.listNotSeen.indexOf(notificationId) !== -1;
  }

}
