import { Component, OnInit } from '@angular/core';
import { fakePostData, fakeUserData } from './raw';
import { NgxChartInterface } from 'src/app/model/NgxChartInterface';
import { StatisticsFilter } from 'src/app/model/StatisticsFilter';
import { AdminService } from 'src/app/admin/services/admin-service/admin.service';
import { ChartSingleModel } from 'src/app/model/ChartModel';
import { HttpErrorResponse } from '@angular/common/http';

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

  constructor(private adminService: AdminService) {
    this.initChartFilter();
    this.getChartData();
    this.initChartAttributes();
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
    this.userChartAttributes.xAxisTicks = [];
  }

  getChartData() {
    this.adminService.getPostStatistic(this.postFilter).subscribe((res: ChartSingleModel[]) => {
      this.postData = res;
    }, (error: HttpErrorResponse) => {
      console.log(error);
    });

    this.adminService.getUserStatistic(this.userFilter).subscribe((res: ChartSingleModel[]) => {
      this.userData = res;
    }, (error: HttpErrorResponse) => {
      console.log(error);
    });
  }

}
