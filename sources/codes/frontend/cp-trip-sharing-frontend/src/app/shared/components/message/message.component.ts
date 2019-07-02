import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit {
  urlImage = 'https://gody.vn/public/v3/images/bg/br-register.jpg';
  username = 'Tran Van Phong';
  subContent = 'Xin chào! Chuyến đi vui vẻ chứ!';
  time = '9:30';
  constructor() { }

  ngOnInit() {
  }

}
