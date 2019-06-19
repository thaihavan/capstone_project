import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Article } from 'src/app/model/Article';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  baseUrl = 'https://localhost:44352/api/postservice/article/';
  constructor(private http: HttpClient) { }
  createPost(article: Article): Observable<Article> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.post<Article>(this.baseUrl + 'create', article, httpOption);
  }

  getDetail(postId: string) {
    const baseUrl = 'https://localhost:44352/api/postservice/post';
    return this.http.get(baseUrl + '?postId=' + postId);
  }

  getAllPost() {
    return this.http.get(this.baseUrl + 'all');
  }
}
