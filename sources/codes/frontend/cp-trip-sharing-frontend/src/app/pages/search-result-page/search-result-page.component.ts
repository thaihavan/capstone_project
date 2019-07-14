import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AngularWaitBarrier } from 'blocking-proxy/built/lib/angular_wait_barrier';

@Component({
  selector: 'app-search-result-page',
  templateUrl: './search-result-page.component.html',
  styleUrls: ['./search-result-page.component.css']
})
export class SearchResultPageComponent implements OnInit {

  search = '';

  navLinks: any;
  routerLinkActive: string;

  selectedLink: any;

  constructor(private route: ActivatedRoute) {
    this.search = this.getSearchParam();
    this.navLinks = [
      {
        path: `moi-nguoi`,
        label: 'Mọi người'
      },
      {
        path: `bai-viet`,
        label: 'Blogs'
      },
      {
        path: `chuyen-di`,
        label: 'Chuyến đi'
      },
      {
        path: `tim-ban-dong-hanh`,
        label: 'Tìm bạn đồng hành'
      }
    ];
    this.routerLinkActive = `moi-nguoi`;
    this.selectedLink = {
      path: `moi-nguoi`,
      label: 'Mọi người'
    };
   }

  ngOnInit() {
  }

  getSearchParam() {
    const urlSplit = window.location.href.split('/');

    if (urlSplit.length < 6) {
      return '';
    }

    return decodeURI(urlSplit[5]);
  }

  changeSelectedLink(link: any) {
    this.selectedLink = link;
  }

  onSearchBtnClick() {
    if (this.search && this.search.trim() !== '') {
      window.location.href = '/search/' + this.selectedLink.path + '/' + this.search;
    }
  }

}
