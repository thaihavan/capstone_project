import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VirtualTrip } from 'src/app/model/VirtualTrip';
import { HostGlobal } from '../../global-variables';

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
  getVirtualTrips(): Observable<any> {
    return this.http.get<any>(this.baseUrl + '/all', this.httpOption);
  }
}
