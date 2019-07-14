import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-search-result-page',
  templateUrl: './search-result-page.component.html',
  styleUrls: ['./search-result-page.component.css']
})
export class SearchResultPageComponent implements OnInit {

  navLinks: any;
  routerLinkActive: string;

  constructor(private route: ActivatedRoute) {
    this.navLinks = [
      {
        path: 'moi-nguoi',
        label: 'Mọi người'
      },
      {
        path: 'bai-viet',
        label: 'Blogs'
      },
      {
        path: 'chuyen-di',
        label: 'Chuyến đi'
      },
      {
        path: 'tim-ban-dong-hanh',
        label: 'Tìm bạn đồng hành'
      }
    ];
    this.routerLinkActive = 'moi-nguoi';
   }

  ngOnInit() {
  }

}
