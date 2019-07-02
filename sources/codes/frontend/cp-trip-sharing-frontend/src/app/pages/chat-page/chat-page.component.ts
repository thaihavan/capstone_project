import { Component, OnInit, HostListener } from '@angular/core';
import { ChatService } from 'src/app/core/services/chat-service/chat.service';
import { Account } from 'src/app/model/Account';
import { HttpErrorResponse } from '@angular/common/http';
import { Conversation } from 'src/app/model/Conversation';

@Component({
  selector: 'app-chat-page',
  templateUrl: './chat-page.component.html',
  styleUrls: ['./chat-page.component.css']
})
export class ChatPageComponent implements OnInit {

  screenHeight: number;
  chatContentHeight: number;

  listConversations: Conversation[];

  constructor(private chatService: ChatService) { }

  ngOnInit() {
    this.setHeight();
    this.getAllConversations();
  }

  setHeight() {
    this.screenHeight = window.innerHeight;
    // headerHeight: 56
    // chatHeader: 64
    // chatFooter: 80
    this.chatContentHeight = this.screenHeight - 56 - 64 - 80 - 2;
  }

  getAllConversations() {
    const account: Account = JSON.parse(localStorage.getItem('Account'));
    this.chatService.getAllConversations('account.id').subscribe((result: Conversation[]) => {
      this.listConversations = result;
    }, (err: HttpErrorResponse) => {
      console.log(err);
    });
  }

}
