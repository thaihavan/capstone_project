import { Component, OnInit, HostListener } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  activeLinkIndex = 0;

  navLinks: any[];
  coverImage = '../../../assets/coverimg.jpg';

  constructor(private router: Router) {
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
        link: './follower',
        index: 3
      },
      {
        label: 'Chủ đề',
        link: './topic',
        index: 4
      }
    ];
  }

  ngOnInit() {
    this.router.events.subscribe(res => {
      this.activeLinkIndex = this.navLinks.indexOf(
        this.navLinks.find(tab => tab.link === '.' + this.router.url)
      );
    });
  }

}
