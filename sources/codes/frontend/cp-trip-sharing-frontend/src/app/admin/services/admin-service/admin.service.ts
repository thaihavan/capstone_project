import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ChartSingleModel } from 'src/app/model/ChartModel';
import { HostGlobal } from 'src/app/core/global-variables';
import { StatisticsFilter } from 'src/app/model/StatisticsFilter';
import { UserService } from 'src/app/core/services/user-service/user.service';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  httpOption = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  httpOptionAuthen = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('Token')
    })
  };

  constructor(private http: HttpClient) { }

  getUserStatistic(filter: StatisticsFilter): Observable<ChartSingleModel[]> {
    const url = HostGlobal.HOST_USER_SERVICE + '/api/userservice/user/statistics';
    return this.http.post<ChartSingleModel[]>(url, filter, this.httpOptionAuthen);
  }

  getPostStatistic(filter: StatisticsFilter): Observable<ChartSingleModel[]> {
    const url = HostGlobal.HOST_POST_SERVICE + '/api/postservice/post/statistics';
    return this.http.post<ChartSingleModel[]>(url, filter, this.httpOptionAuthen);
  }
}
