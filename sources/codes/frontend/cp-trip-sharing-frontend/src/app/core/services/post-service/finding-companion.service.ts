import { Injectable } from '@angular/core';
import { HostGlobal } from '../../global-variables';
import { CompanionPost } from 'src/app/model/CompanionPost';
import { Observable, Observer } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Comment } from 'src/app/model/Comment';
import { Like } from 'src/app/model/Like';
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

  getCompanionPosts(postFilter: PostFilter): Observable<CompanionPost[]> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<CompanionPost[]>(this.baseUrl + '/post/all', postFilter, httpOption);
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

  getCommentByPost(postId: string, token: string): Observable<any> {
    const url = HostGlobal.HOST_POST_SERVICE + '/api/postservice/comment/all';
    if (token != null) {
      const httpOptionAuth = {
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
          Authorization: 'Bearer ' + token
        })
      };
      return this.http.get(url + '?id=' + postId, httpOptionAuth);
    } else {
      return this.http.get(url + '?id=' + postId);
    }
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

  addComment(comment: Comment): Observable<any> {
    const httpOptionAuth = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    const url = HostGlobal.HOST_POST_SERVICE + '/api/postservice/comment/add';
    return this.http.post<Comment>(url, comment, httpOptionAuth);
  }

  likeAPost(likeObject: Like): Observable<any> {
    const httpOptionLike = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.post<any>(HostGlobal.HOST_POST_SERVICE + '/api/postservice/like/like', likeObject, httpOptionLike);
  }

  unlikeAPost(likeObject: Like): Observable<any> {
    const httpOptionUnLike = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      }),
      body: likeObject
    };
    return this.http.delete<any>(HostGlobal.HOST_POST_SERVICE + '/api/postservice/like/unlike', httpOptionUnLike);
  }
}
