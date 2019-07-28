import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VirtualTrip } from 'src/app/model/VirtualTrip';
import { HostGlobal } from '../../global-variables';
import { PostFilter } from 'src/app/model/PostFilter';

@Injectable({
  providedIn: 'root'
})
export class VirtualTripService {
  baseUrl = HostGlobal.HOST_POST_SERVICE + '/api/postservice/virtualtrip';
  httpOption = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('Token')
    })
  };
  constructor(private http: HttpClient) {}
  createVirtualTrip(virtualTrip: VirtualTrip): Observable<VirtualTrip> {
    return this.http.post<VirtualTrip>(this.baseUrl + '/create', virtualTrip, this.httpOption);
  }

  updateVirtualTrip(virtualTrip: VirtualTrip): Observable<VirtualTrip> {
    return this.http.post<VirtualTrip>(this.baseUrl + '/update', virtualTrip, this.httpOption);
  }

  getDetailVtrip(id: string): Observable<VirtualTrip> {
    return this.http.get<VirtualTrip>(this.baseUrl + '?id=' + id, this.httpOption);
  }

  getVirtualTrips(postFilter: PostFilter, page: number): Observable<VirtualTrip[]> {
    return this.http.post<VirtualTrip[]>(this.baseUrl + '/all?page=' + page, postFilter, this.httpOption);
  }

  getVirtualTripsByUser(userId: string, postFilter: PostFilter, page: number): Observable<VirtualTrip[]> {
    return this.http.post<VirtualTrip[]>(this.baseUrl + '/user?userId=' + userId + '&page=' + page, postFilter, this.httpOption);
  }
}
