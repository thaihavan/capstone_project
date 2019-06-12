import { Component, OnInit } from '@angular/core';
@Component({
  selector: 'app-interestedtopic-page',
  templateUrl: './interestedtopic-page.component.html',
  styleUrls: ['./interestedtopic-page.component.css']
})
export class InterestedtopicPageComponent implements OnInit {
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
     {nameTopic:"Văn Hóa",urlImage: "https://gody.vn/public/v3/images/bg/br-register.jpg"}
    ];

    nameclass:string ="eachtopic";
  constructor() { }

  ngOnInit() {
  }
  
  changeClass(): void{
    this.nameclass="changeClass";
  }
}

export class Topic{
  nameTopic: string;
  urlImage: string;
}
