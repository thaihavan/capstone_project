import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard-page.component.html',
  styleUrls: ['./dashboard-page.component.css']
})
export class DashboardPageComponent implements OnInit {

  VALID_TABS: string[];
  selectedTab = '';
  tabTitle = '';


  constructor(private router: Router) { }

  ngOnInit() {
    this.selectedTab = this.getTabFromUrl();

    this.setTabTitle();

  }

  setTabTitle() {
    if (this.selectedTab.trim() === '') {
      this.selectedTab = 'tong-quan';
      this.tabTitle = 'Tổng quan';
    } else {
      switch (this.selectedTab.trim()) {
        case 'tong-quan':
          this.tabTitle = 'Tổng quan';
          break;
        case 'bai-viet':
          this.tabTitle = 'Bài viết';
          break;
        case 'nguoi-dung':
          this.tabTitle = 'Người dùng';
          break;
        case 'chu-de':
          this.tabTitle = 'Chủ đề';
          break;
      }
    }
  }

  initValidTabs() {
    this.VALID_TABS = [
      'tong-quan',
      'bai-viet',
      'nguoi-dung',
      'chu-de',
      'bao-cao'
    ];
  }

  getTabFromUrl() {
    const urlSplit = window.location.href.split('/');
    if (urlSplit.length < 5) {
      return '';
    }
    return urlSplit[4];
  }

  gotoOverview() {
    this.selectedTab = 'tong-quan';
    this.tabTitle = 'Tổng quan';
    this.router.navigate(['dashboard/tong-quan']);
  }

  gotoPostManagement() {
    this.selectedTab = 'bai-viet';
    this.tabTitle = 'Bài viết';
    this.router.navigate(['dashboard/bai-viet']);
  }

  gotoUserManagement() {
    this.selectedTab = 'nguoi-dung';
    this.tabTitle = 'Người dùng';
    this.router.navigate(['dashboard/nguoi-dung']);
  }

  gotoTopicManagement() {
    this.selectedTab = 'chu-de';
    this.tabTitle = 'Chủ đề';
    this.router.navigate(['dashboard/chu-de']);
  }
}
