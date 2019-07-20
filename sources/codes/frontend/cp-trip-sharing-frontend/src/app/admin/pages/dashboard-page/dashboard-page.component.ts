import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard-page.component.html',
  styleUrls: ['./dashboard-page.component.css']
})
export class DashboardPageComponent implements OnInit {

  selectedTab = '';

  constructor(private router: Router) { }

  ngOnInit() {
  }
  gotoTopicManagement() {
    this.selectedTab = 'topic';
    this.router.navigate(['dashboard/chu-de']);
  }
}
