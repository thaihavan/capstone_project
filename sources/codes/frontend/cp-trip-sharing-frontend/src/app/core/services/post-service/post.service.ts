import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Article } from 'src/app/model/Article';
import { Observable } from 'rxjs';
import { HostGlobal } from 'src/app/core/global-variables';
import { Comment } from 'src/app/model/Comment';
import { Like } from 'src/app/model/Like';
import { PostFilter } from 'src/app/model/PostFilter';
import { Topic } from 'src/app/model/Topic';
import { Post } from 'src/app/model/Post';
import { ReportType } from 'src/app/model/ReportType';
import { Report } from 'src/app/model/Report';

@Injectable({
  providedIn: 'root'
})

export class PostService {
  baseUrl: string = null;
  constructor(private http: HttpClient) {
    this.baseUrl = HostGlobal.HOST_POST_SERVICE + '/api/postservice/article/';
  }

  getAllPosts(search: string, page: number): Observable<Post[]> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + sessionStorage.getItem('Token')
      })
    };
    return this.http.get<Post[]>(
      `${HostGlobal.HOST_POST_SERVICE}/api/postservice/post/all?page=${page}&search=${search}`,
      httpOption);
  }

  createPost(article: Article): Observable<Article> {
    const httpOptionAuth = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };

    return this.http.post<Article>(this.baseUrl + 'create', article, httpOptionAuth);
  }

  // getDetail(postId: string): Observable<any> {
  //   const baseUrl = HostGlobal.HOST_POST_SERVICE + '/api/postservice/post';
  //   return this.http.get(baseUrl + '?postId=' + postId);
  // }

  getAllArticles(postFilter: PostFilter, page: number): Observable<any> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };
    return this.http.post<Article>(this.baseUrl + 'all?page=' + page, postFilter, httpOption);
  }

  getPopularArticles(postFilter: PostFilter, page: number): Observable<any> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };
    return this.http.post<Article>(this.baseUrl + 'popular?page=' + page, postFilter, httpOption);
  }

  getRecommendArticles(postFilter: PostFilter, page: number): Observable<any> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.post<Article>(this.baseUrl + 'recommend?page=' + page, postFilter, httpOption);
  }

  getAllArticlesByUserId(userId: string, postFilter: PostFilter, page: number): Observable<any> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };
    return this.http.post<Article>(this.baseUrl + 'user?userId=' + userId + '&page=' + page, postFilter, httpOption);
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

  getAllTopics(): Observable<any> {
    return this.http.get(HostGlobal.HOST_POST_SERVICE + '/api/postservice/topic/all');
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

  updateArticle(article: Article) {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.post(this.baseUrl + 'update', article, httpOption);
  }

  getArticleById(articleId: string): Observable<Article> {
    const token = localStorage.getItem('Token');
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + token
      })
    };
    if (token != null) {
      return this.http.get<Article>(this.baseUrl + 'full?id=' + articleId, httpOption);
    } else {
      return this.http.get<Article>(this.baseUrl + 'full?id=' + articleId);
    }
  }

  removeArticle(articleId: any): Observable<any> {
    const httpOptionRemoveArticle = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    // tslint:disable-next-line:max-line-length
    return this.http.delete<any>(HostGlobal.HOST_POST_SERVICE + '/api/postservice/article/remove?articleId=' + articleId, httpOptionRemoveArticle);
  }

  addOrUpdateTopic(topic: Topic): Observable<any> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + sessionStorage.getItem('Token')
      })
    };
    return this.http.post<any>(HostGlobal.HOST_POST_SERVICE + '/api/postservice/topic/insert-or-update', topic, httpOption);
  }

  removeTopics(topicIds: string[]): Observable<any> {
    const httpOptionRemoveTopic = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + sessionStorage.getItem('Token')
      })
    };
    return this.http.post<any>(HostGlobal.HOST_POST_SERVICE + '/api/postservice/topic/delete', topicIds, httpOptionRemoveTopic);
  }

  updateComment(comment: Comment): Observable<any> {
    const url = HostGlobal.HOST_POST_SERVICE + '/api/postservice/comment/update';
    const httpOptionUpdateComment = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.put(url, comment, httpOptionUpdateComment);
  }

  removeComment(commentId: string, authorId: string): Observable<any> {
    const httpOptionRemoveComment = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.delete<any>(HostGlobal.HOST_POST_SERVICE + '/api/postservice/comment/delete?id=' + commentId
      + '&&authorId=' + authorId, httpOptionRemoveComment);
  }

  getReportTypes(): Observable<ReportType[]> {
    const httpOptionAuthen = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.get<ReportType[]>(`${HostGlobal.HOST_POST_SERVICE}/api/postservice/report/type`, httpOptionAuthen);
  }

  sendReport(report: Report): Observable<any> {
    const httpOptionAuthen = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.post(`${HostGlobal.HOST_POST_SERVICE}/api/postservice/report`, report, httpOptionAuthen);
  }
}
