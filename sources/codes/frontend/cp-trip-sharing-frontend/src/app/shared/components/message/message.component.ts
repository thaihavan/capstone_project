import { Component, OnInit, Input } from '@angular/core';
import { Conversation } from 'src/app/model/Conversation';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit {
  @Input() conversations: Conversation[] = [];

  user: any;
  constructor() {
    this.user = JSON.parse(localStorage.getItem('User'));
  }

  ngOnInit() {
  }

}
