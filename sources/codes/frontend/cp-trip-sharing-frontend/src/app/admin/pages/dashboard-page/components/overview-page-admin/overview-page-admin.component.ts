import { Component, OnInit } from '@angular/core';
import { fakePostData, fakeUserData } from './raw';
import { NgxChartInterface } from 'src/app/model/NgxChartInterface';
import { StatisticsFilter } from 'src/app/model/StatisticsFilter';
import { AdminService } from 'src/app/admin/services/admin-service/admin.service';
import { ChartSingleModel } from 'src/app/model/ChartModel';
import { HttpErrorResponse } from '@angular/common/http';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';

@Component({
  selector: 'app-overview-page-admin',
  templateUrl: './overview-page-admin.component.html',
  styleUrls: ['./overview-page-admin.component.css']
})
export class OverviewPageAdminComponent implements OnInit {

  postData: any;
  userData: any;

  postChartAttributes: NgxChartInterface;
  userChartAttributes: NgxChartInterface;

  postFilter: StatisticsFilter;
  userFilter: StatisticsFilter;
  now: Date;

  constructor(private adminService: AdminService, private errorHandler: GlobalErrorHandler) {
    this.initChartFilter();
    this.initChartAttributes();
    this.getPostStatistic();
    this.getUserStatistic();
  }

  ngOnInit() {
  }

  initChartFilter() {
    this.now = new Date();

    this.postFilter = new StatisticsFilter();
    this.postFilter.to = new Date();
    this.postFilter.from = new Date();
    this.postFilter.from.setDate(this.now.getDate() - 30);

    this.userFilter = new StatisticsFilter();
    this.userFilter.to = new Date();
    this.userFilter.from = new Date();
    this.userFilter.from.setDate(this.now.getDate() - 30);
  }

  initChartAttributes() {
    this.postChartAttributes = new NgxChartInterface();
    this.postChartAttributes.view = [940, 360];

    this.userChartAttributes = new NgxChartInterface();
    this.userChartAttributes.view = [940, 360];
    this.userChartAttributes.showLegend = false;
    // this.userChartAttributes.xAxisTicks = [];
  }

  getPostStatistic() {
    this.adminService.getPostStatistic(this.postFilter).subscribe((res: ChartSingleModel[]) => {
      this.postData = res;
    }, this.errorHandler.handleError);
  }

  getUserStatistic() {
    this.adminService.getUserStatistic(this.userFilter).subscribe((res: ChartSingleModel[]) => {
      this.userData = res;
    }, this.errorHandler.handleError);
  }

}
