import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Conversation } from 'src/app/model/Conversation';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit {
  @Input() conversations: Conversation[] = [];
  @Output() selectConversation = new EventEmitter<Conversation>();

  user: any;
  constructor() {
    this.user = JSON.parse(localStorage.getItem('User'));
  }

  ngOnInit() {
  }

  isImage(imgTag: string): boolean {
    return imgTag.startsWith('<img');
  }

  onClickConversation(conversation: Conversation) {
    this.selectConversation.emit(conversation);
  }

}
