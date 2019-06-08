import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-personal-page',
  templateUrl: './personal-page.component.html',
  styleUrls: ['./personal-page.component.css']
})
export class PersonalPageComponent implements OnInit {
  coverImage = '../../../assets/coverimg.jpg';
  avatar = '../../../assets/img_avatar.png';
  title = 'angular-material-tab-router';
  navLinks: any[];
  activeLinkIndex = 0;
  constructor(private router: Router) {
    this.navLinks = [
        {
            label: 'Bản tin hoạt động ',
            link: './register',
            index: 0
        }, {
            label: 'Chuyến đi',
            link: './virtual',
            index: 1
        }, {
            label: 'Tìm bạn đồng hành',
            link: './third',
            index: 2
        },
    ];
}
ngOnInit(): void {
  this.router.events.subscribe((res) => {
      this.activeLinkIndex = this.navLinks.indexOf(this.navLinks.find(tab => tab.link === '.' + this.router.url));
  });
}

}
