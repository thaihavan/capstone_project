import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ChartSingleModel } from 'src/app/model/ChartModel';
import { HostGlobal } from 'src/app/core/global-variables';
import { StatisticsFilter } from 'src/app/model/StatisticsFilter';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { Report } from 'src/app/model/Report';
import { Post } from 'src/app/model/Post';

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
      Authorization: 'Bearer ' + sessionStorage.getItem('Token')
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

  banUser(userId: string): Observable<any> {
    const url = HostGlobal.HOST_USER_SERVICE + '/api/userservice/user/ban?userId=' + userId;
    return this.http.put(url, null, this.httpOptionAuthen);
  }

  unbanUser(userId: string): Observable<any> {
    const url = HostGlobal.HOST_USER_SERVICE + '/api/userservice/user/unban?userId=' + userId;
    return this.http.put(url, null, this.httpOptionAuthen);
  }

  updatePost(post: Post): Observable<any> {
    const url = HostGlobal.HOST_POST_SERVICE + '/api/postservice/post';
    return this.http.put(url, post, this.httpOptionAuthen);
  }

  getReportedUsers(): Observable<Report[]> {
    const url = HostGlobal.HOST_USER_SERVICE + '/api/userservice/user/reports';
    return this.http.get<Report[]>(url, this.httpOptionAuthen);
  }

  getReportedPosts(): Observable<Report[]> {
    const url = HostGlobal.HOST_POST_SERVICE + '/api/postservice/report/all?targetType=post';
    return this.http.get<Report[]>(url, this.httpOptionAuthen);
  }

  getReportedComments(): Observable<Report[]> {
    const url = HostGlobal.HOST_POST_SERVICE + '/api/postservice/report/all?targetType=comment';
    return this.http.get<Report[]>(url, this.httpOptionAuthen);
  }

  resolveReportedUser(reportedUser: Report): Observable<Report> {
    reportedUser.isResolved = true;
    const url = HostGlobal.HOST_USER_SERVICE + '/api/userservice/user/report';
    return this.http.put<Report>(url, reportedUser, this.httpOptionAuthen);
  }

  resolveReport(report: Report): Observable<Report> {
    report.isResolved = true;
    const url = HostGlobal.HOST_POST_SERVICE + '/api/postservice/report';
    return this.http.put<Report>(url, report, this.httpOptionAuthen);
  }
}
