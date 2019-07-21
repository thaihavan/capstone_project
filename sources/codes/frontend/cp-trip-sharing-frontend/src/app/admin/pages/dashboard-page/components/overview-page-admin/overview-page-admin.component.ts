import { Component, OnInit } from '@angular/core';
import { fakePostData, fakeUserData } from './raw';
import { NgxChartInterface } from 'src/app/model/NgxChartInterface';

@Component({
  selector: 'app-overview-page-admin',
  templateUrl: './overview-page-admin.component.html',
  styleUrls: ['./overview-page-admin.component.css']
})
export class OverviewPageAdminComponent implements OnInit {

  postData: any;
  userData: any;

  chartAttributes: NgxChartInterface;

  constructor() {
    this.getPostData();
    this.initChartAttributes();
  }

  ngOnInit() {
  }

  initChartAttributes() {
    this.chartAttributes = new NgxChartInterface();
    this.chartAttributes.view = [940, 360];
    this.chartAttributes.xAxisTicks = ['25', 'Jul', '05'];
  }

  getPostData() {
    this.postData = fakePostData;
    this.userData = fakeUserData;
  }

  onSelect(event) {
    console.log(event);
  }

}
