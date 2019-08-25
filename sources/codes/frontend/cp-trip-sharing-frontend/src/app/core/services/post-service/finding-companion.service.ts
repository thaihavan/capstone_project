import { Injectable } from '@angular/core';
import { HostGlobal } from '../../global-variables';
import { CompanionPost } from 'src/app/model/CompanionPost';
import { Observable, Observer } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CompanionPostRequest } from 'src/app/model/CompanionPostRequest';
import { PostFilter } from 'src/app/model/PostFilter';

@Injectable({
  providedIn: 'root'
})
export class FindingCompanionService {
  user: any;
  baseUrl = HostGlobal.HOST_POST_SERVICE + '/api/postservice/companion';

  constructor(private http: HttpClient) {
    this.user = JSON.parse(localStorage.getItem('User'));
  }

  getCompanionPosts(postFilter: PostFilter, page: number): Observable<CompanionPost[]> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<CompanionPost[]>(this.baseUrl + '/post/all?page=' + page, postFilter, httpOption);
  }

  getCompanionPostsByUser(userId: string, postFilter: PostFilter, page: number): Observable<CompanionPost[]> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<CompanionPost[]>(this.baseUrl + '/post/user?userId=' + userId + '&page=' + page, postFilter, httpOption);
  }

  createPost(companionPost: CompanionPost): Observable<any> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.post( this.baseUrl + '/post/create', companionPost, httpOption);
  }

  getPost(id): Observable<CompanionPost> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.get<CompanionPost>(this.baseUrl + '/post?id=' + id, httpOption);
  }

  updatePost(companionPost: CompanionPost): any {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.post(this.baseUrl + '/post/update', companionPost, httpOption);
  }

  deletePost(id): Observable<any> {
    const httpOptionRemoveArticle = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    // tslint:disable-next-line:max-line-length
    return this.http.delete<any>(HostGlobal.HOST_POST_SERVICE + '/api/postservice/post/remove?postId=' + id, httpOptionRemoveArticle);
  }

  // get all request from members
  getAllRequests(postId: string): Observable<CompanionPostRequest[]> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.get<CompanionPostRequest[]>(this.baseUrl + '/post/requests?companionPostId=' + postId, httpOption);
  }

  // delete request from member
  deleteRequest(request: CompanionPostRequest): Observable<any> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      }),
      body: request
    };
    return this.http.delete(this.baseUrl + '/post/request', httpOption );
  }

  // member send request to join gruop
  sendRequestJoinGroup(companionPostRequest: CompanionPostRequest): Observable<any> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.post<any>(this.baseUrl + '/post/join', companionPostRequest, httpOption );
  }

  // member cancle a request has been sent
  cancleRequest(pId: string): Observable<any> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      }),
    };
    return this.http.delete(this.baseUrl + '/post/request/cancel?postId=' + pId, httpOption);
  }

}
