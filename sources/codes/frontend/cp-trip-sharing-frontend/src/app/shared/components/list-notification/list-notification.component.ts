import { Component, OnInit, Input } from '@angular/core';
import { Notification } from 'src/app/model/Notification';

@Component({
  selector: 'app-list-notification',
  templateUrl: './list-notification.component.html',
  styleUrls: ['./list-notification.component.css']
})
export class ListNotificationComponent implements OnInit {
  @Input() notifications: Notification[] = [];

  constructor() { }

  ngOnInit() {
  }

}
