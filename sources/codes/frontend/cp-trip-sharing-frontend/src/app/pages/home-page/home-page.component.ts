import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {

  navLinks: any[];
  coverImage = '../../../assets/coverimg.jpg';
  activeLinkIndex = 0;

  constructor() {
    this.navLinks = [
      {
        label: 'Đề xuất',
        link: './for-you',
        index: 0
      },
      {
        label: 'Phổ biến',
        link: './popular',
        index: 1
      },
      {
        label: 'Mới nhất',
        link: './newest',
        index: 2
      },
      {
        label: 'Theo dõi',
        link: './followers',
        index: 3
      },
      {
        label: 'Chủ đề',
        link: './topics',
        index: 4
      }
    ];
   }

  ngOnInit() {
  }

}
