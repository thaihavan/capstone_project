import { Component, OnInit, HostListener } from '@angular/core';

@Component({
  selector: 'app-chat-page',
  templateUrl: './chat-page.component.html',
  styleUrls: ['./chat-page.component.css']
})
export class ChatPageComponent implements OnInit {

  screenHeight: number;
  chatContentHeight: number;

  constructor() { }

  ngOnInit() {
    this.screenHeight = window.innerHeight;
    // headerHeight: 56
    // chatHeader: 64
    // chatFooter: 80
    this.chatContentHeight = this.screenHeight - 56 - 64 - 80 - 2;
  }

}
