import { Component, OnInit, Output } from '@angular/core';
import { Globals } from 'src/globals/globalvalues';

@Component({
  selector: 'app-interestedtopic-page',
  templateUrl: './interestedtopic-page.component.html',
  styleUrls: ['./interestedtopic-page.component.css']
})
export class InterestedtopicPageComponent implements OnInit {
   selectedTopic: Topic;
   listselectedTopic: Array<Topic> = [];
   listinterestedtopic: Topic[] = [
     {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
     {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
     {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
     {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
     {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
     {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
     {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
     {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
     {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
     {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
     {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
     {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
     {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
     {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
     {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
     {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
     {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
     {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
     {nameTopic: 'Loại Khác', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'}
    ];

  constructor(private globals: Globals) { }

  ngOnInit() {
  }

  changeClass(topic: Topic): void {
    this.selectedTopic = topic;
    this.listselectedTopic.push(topic);
    console.log(this.listselectedTopic);
  }

  gotoHomepage() {
    window.location.href = this.globals.urllocal;
  }
}

export class Topic {
  nameTopic: string;
  urlImage: string;
}
