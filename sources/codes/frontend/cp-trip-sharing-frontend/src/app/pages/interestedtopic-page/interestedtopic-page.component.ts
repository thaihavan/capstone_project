import { Component, OnInit } from '@angular/core';
@Component({
  selector: 'app-interestedtopic-page',
  templateUrl: './interestedtopic-page.component.html',
  styleUrls: ['./interestedtopic-page.component.css']
})
export class InterestedtopicPageComponent implements OnInit {

   selectedTopic: Topic;
   listselectedTopic : Array<Topic> = [];
   listinterestedtopic: Topic[]=[
     {nameTopic:"Văn Hóa",urlImage: "https://gody.vn/public/v3/images/bg/br-register.jpg"},
     {nameTopic:"Văn Hóa",urlImage: "https://gody.vn/public/v3/images/bg/br-register.jpg"},
     {nameTopic:"Văn Hóa",urlImage: "https://gody.vn/public/v3/images/bg/br-register.jpg"},
     {nameTopic:"Văn Hóa",urlImage: "https://gody.vn/public/v3/images/bg/br-register.jpg"},
     {nameTopic:"Văn Hóa",urlImage: "https://gody.vn/public/v3/images/bg/br-register.jpg"},
     {nameTopic:"Văn Hóa",urlImage: "https://gody.vn/public/v3/images/bg/br-register.jpg"},
     {nameTopic:"Văn Hóa",urlImage: "https://gody.vn/public/v3/images/bg/br-register.jpg"},
     {nameTopic:"Văn Hóa",urlImage: "https://gody.vn/public/v3/images/bg/br-register.jpg"},
     {nameTopic:"Văn Hóa",urlImage: "https://gody.vn/public/v3/images/bg/br-register.jpg"},
     {nameTopic:"Văn Hóa",urlImage: "https://gody.vn/public/v3/images/bg/br-register.jpg"},
     {nameTopic:"Văn Hóa",urlImage: "https://gody.vn/public/v3/images/bg/br-register.jpg"},
     {nameTopic:"Văn Hóa",urlImage: "https://gody.vn/public/v3/images/bg/br-register.jpg"},
     {nameTopic:"Văn Hóa",urlImage: "https://gody.vn/public/v3/images/bg/br-register.jpg"},
     {nameTopic:"Văn Hóa",urlImage: "https://gody.vn/public/v3/images/bg/br-register.jpg"},
     {nameTopic:"Văn Hóa",urlImage: "https://gody.vn/public/v3/images/bg/br-register.jpg"},
     {nameTopic:"Văn Hóa",urlImage: "https://gody.vn/public/v3/images/bg/br-register.jpg"},
     {nameTopic:"Văn Hóa",urlImage: "https://gody.vn/public/v3/images/bg/br-register.jpg"},
     {nameTopic:"Văn Hóa",urlImage: "https://gody.vn/public/v3/images/bg/br-register.jpg"},
     {nameTopic:"Loại Khác",urlImage: "https://gody.vn/public/v3/images/bg/br-register.jpg"}
    ];

  constructor() { }

  ngOnInit() {
  }
  
  changeClass(topic : Topic): void{
    this.selectedTopic = topic;
    this.listselectedTopic.push(topic)
    console.log(this.listselectedTopic);
  }
}

export class Topic{
  nameTopic: string;
  urlImage: string;
}
