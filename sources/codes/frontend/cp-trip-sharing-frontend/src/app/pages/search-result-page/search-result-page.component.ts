import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AngularWaitBarrier } from 'blocking-proxy/built/lib/angular_wait_barrier';
import { AlertifyService } from 'src/app/core/services/alertify-service/alertify.service';

@Component({
  selector: 'app-search-result-page',
  templateUrl: './search-result-page.component.html',
  styleUrls: ['./search-result-page.component.css']
})
export class SearchResultPageComponent implements OnInit {

  search = '';

  // 'text' or 'location'
  searchType: string;

  navLinks: any;
  routerLinkActive: string;

  selectedPath: string;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private alertify: AlertifyService) {
    this.searchType = this.route.snapshot.paramMap.get('searchType');
    this.search = this.getSearchParam();

    this.initNavLinks();

    this.routerLinkActive = `bai-viet`;
    this.selectedPath = this.getTabParam();
   }

  ngOnInit() {
  }

  initNavLinks() {
    this.navLinks = [
      {
        path: `bai-viet`,
        label: 'Cảm hứng du lịch'
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

    if (this.searchType === 'text') {
      this.navLinks.push(
        {
          path: `moi-nguoi`,
          label: 'Mọi người'
        });
    }
  }

  getSearchParam() {
    const urlSplit = window.location.href.split('/');

    if (urlSplit.length < 7) {
      return '';
    }

    return decodeURI(urlSplit[6]);
  }

  getTabParam() {
    const urlSplit = window.location.href.split('/');

    if (urlSplit.length < 6) {
      return '';
    }

    return decodeURI(urlSplit[5]);
  }

  changeSelectedLink(link: any) {
    this.selectedPath = link.path;
  }

  searchByText() {
    if (this.search && this.search.trim() !== '') {
      // this.router.navigate(['/search/text', this.selectedPath, this.search.trim()]);
      window.location.href = '/search/text/' + this.selectedPath + '/' + this.search.trim();
    }
  }

  searchByLocation(addressObject: any) {
    if (!addressObject) {
      this.alertify.error('Địa điểm không tồn tại!');
    }
    // this.router.navigate(['/search/location', this.selectedPath, addressObject.locationId]);
    window.location.href = '/search/location/' + this.selectedPath + '/' + addressObject.locationId;
  }

}
