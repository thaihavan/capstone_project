import { Injectable } from '@angular/core';
import { HostGlobal } from '../../global-variables';
import { CompanionPost } from 'src/app/model/CompanionPost';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FindingCompanionService {
  user: any;
  baseUrl = HostGlobal.HOST_POST_SERVICE + '/api/postservice/companion/post/create';
  constructor(private http: HttpClient) {
    this.user = JSON.parse(localStorage.getItem('User'));
  }
  createPost(companionPost: CompanionPost): Observable<any> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.post( this.baseUrl, companionPost, httpOption);
  }
}
