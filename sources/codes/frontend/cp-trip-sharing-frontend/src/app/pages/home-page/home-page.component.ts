import { Component, OnInit, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  activeLinkIndex = 0;

  navLinks: any[];
  coverImage = '../../../assets/coverimg.jpg';

  constructor(private router: Router, private titleService: Title) {
    this.titleService.setTitle('Trang chủ');
    this.navLinks = [
      {
        label: 'Đề xuất',
        link: './de-xuat',
        index: 0
      },
      {
        label: 'Phổ biến',
        link: './pho-bien',
        index: 1
      },
      {
        label: 'Mới nhất',
        link: './moi-nhat',
        index: 2
      },
      {
        label: 'Theo dõi',
        link: './theo-doi',
        index: 3
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
