import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Article } from 'src/app/model/Article';
import { Observable } from 'rxjs';
import { HostGlobal } from 'src/app/core/global-variables';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  baseUrl: string = null;
  constructor(private http: HttpClient) {
    this.baseUrl = HostGlobal.HOST_POST_SERVICE + '/api/postservice/article/';
  }
  createPost(article: Article): Observable<Article> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.post<Article>(this.baseUrl + 'create', article, httpOption);
  }

  getDetail(postId: string): Observable<any> {
    const baseUrl = HostGlobal.HOST_POST_SERVICE + '/api/postservice/post';
    return this.http.get(baseUrl + '?postId=' + postId);
  }

  getAllPost(): Observable<any> {
    console.log(this.baseUrl);
    return this.http.get(this.baseUrl + 'all');
  }

  getCommentByPost(postId: string): Observable<any> {
    const url = HostGlobal.HOST_POST_SERVICE + '/api/postservice/comment/all';
    return this.http.get(url + '?id=' + postId);
  }


  getAllTopics(): Observable<any> {
    return this.http.get(HostGlobal.HOST_POST_SERVICE + '/api/postservice/topic/all');
  }
}
