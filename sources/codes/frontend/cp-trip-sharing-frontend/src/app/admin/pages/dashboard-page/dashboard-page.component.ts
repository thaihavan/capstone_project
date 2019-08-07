import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Account } from 'src/app/model/Account';

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard-page.component.html',
  styleUrls: ['./dashboard-page.component.css']
})
export class DashboardPageComponent implements OnInit {

  VALID_TABS: string[];
  selectedTab = '';
  tabTitle = '';

  account: Account;


  constructor(private router: Router) {
    this.account = JSON.parse(sessionStorage.getItem('Account'));

    if (this.account == null || this.account.role !== 'admin') {
      window.location.href = '/admin/login';
    }
  }

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
        case 'nguoi-dung-vi-pham':
          this.tabTitle = 'Người dùng vi phạm';
          break;
        case 'bai-viet-vi-pham':
          this.tabTitle = 'Bài viết vi phạm';
          break;
        case 'binh-luan-vi-pham':
          this.tabTitle = 'Bình luận vi phạm';
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
    if (urlSplit.length < 6) {
      return '';
    }
    return urlSplit[5];
  }

  gotoOverview() {
    this.selectedTab = 'tong-quan';
    this.tabTitle = 'Tổng quan';
    this.router.navigate(['dashboard/tong-quan']);
  }

  gotoPostManagement() {
    this.selectedTab = 'bai-viet';
    this.tabTitle = 'Bài viết';
    this.router.navigate(['admin/dashboard/bai-viet']);
  }

  gotoUserManagement() {
    this.selectedTab = 'nguoi-dung';
    this.tabTitle = 'Người dùng';
    this.router.navigate(['admin/dashboard/nguoi-dung']);
  }

  gotoTopicManagement() {
    this.selectedTab = 'chu-de';
    this.tabTitle = 'Chủ đề';
    this.router.navigate(['admin/dashboard/chu-de']);
  }

  gotoReportedUser() {
    this.selectedTab = 'nguoi-dung-vi-pham';
    this.tabTitle = 'Nguời dùng vi phạm';
    this.router.navigate(['admin/dashboard/nguoi-dung-vi-pham']);
  }

  gotoReportedPost() {
    this.selectedTab = 'bai-viet-vi-pham';
    this.tabTitle = 'Bài viết vi phạm';
    this.router.navigate(['admin/dashboard/bai-viet-vi-pham']);
  }

  gotoReportedComment() {
    this.selectedTab = 'binh-luan-vi-pham';
    this.tabTitle = 'Bình luận vi phạm';
    this.router.navigate(['admin/dashboard/binh-luan-vi-pham']);
  }
}
