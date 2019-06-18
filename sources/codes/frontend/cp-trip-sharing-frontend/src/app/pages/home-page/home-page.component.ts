import { Component, OnInit, HostListener } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  activeLinkIndex = 0;
  isScrollTopShow = false;
  topPosToStartShowing = 100;

  componentRefer: any;

  navLinks: any[];
  coverImage = '../../../assets/coverimg.jpg';

  @HostListener('window:scroll') checkScroll() {
    const scrollPosition =
      window.pageYOffset ||
      document.documentElement.scrollTop ||
      document.body.scrollTop ||
      0;
    if (scrollPosition >= this.topPosToStartShowing) {
      this.isScrollTopShow = true;
    } else {
      this.isScrollTopShow = false;
    }
  }

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

  onActivate(componentRef) {
    this.componentRefer = componentRef;
  }

  onScroll() {
    this.componentRefer.onScroll();
  }

  gotoTop() {
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth'
    });
  }

}
