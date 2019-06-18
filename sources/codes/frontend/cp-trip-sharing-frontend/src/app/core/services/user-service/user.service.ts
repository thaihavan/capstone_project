import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Account } from 'src/app/model/Account';
import { User } from 'src/app/model/User';


const httpOption = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

const httpOptionAuthen = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + localStorage.getItem('Token')
  })
};

@Injectable({
  providedIn: 'root'
})
export class UserService {
  // apiUrl = 'https://localhost:44353/api/identity/account/';
  apiUrl = 'http://107.178.242.201/api/identity/account/';
  apiUserService = 'https://localhost:44351/api/userservice/';
  constructor(private http: HttpClient) { }

  getAccount(account: Account): Observable<Account> {
    return this.http.post<Account>(this.apiUrl + 'authenticate', account, httpOption);
  }

  registerAccount(account: Account): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'register', account, httpOption);
  }

  changePassword(account: Account): Observable<any> {
    const httpOptionAu = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.post<any>(this.apiUrl + 'changePassword', account, httpOptionAu);
  }

  verifyEmail(token: string): Observable<any> {
    const httpAuthen = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.post<any>(this.apiUrl + 'verify', null, httpAuthen);
  }

  addBlock(blocked: string): Observable<any> {
    return this.http.post<any>(this.apiUserService + 'user/addblock', blocked, httpOption);
  }

  unBlock(blocked: string): Observable<any> {
    return this.http.delete<any>(this.apiUserService + 'user/unblock' + blocked);
  }

  bookMark(postId: string): Observable<any> {
    return this.http.post<any>(this.apiUserService + 'user/bookmark', postId, httpOption);
  }

  deleteBookmark(postId: string): Observable<any> {
    return this.http.delete<any>(this.apiUserService + 'user/deletebookmark' + postId);
  }

  addFollow(following: string): Observable<any> {
    return this.http.post<any>(this.apiUserService + 'user/follow', following, httpOption);
  }

  unFollow(following: string): Observable<any> {
    return this.http.delete<any>(this.apiUserService + 'user/unfollow?following=' + following);
  }

  getAllPhoto(): Observable<any> {
    return this.http.get<any>(this.apiUserService + 'user/allphoto');
  }

  addPhoto(url: string, date: Date): Observable<any> {
    const objectJson = '{"url":' + '"' + url + '"' + ',' + '"date":' + '"' + date + '"';
    return this.http.post<any>(this.apiUserService + 'user/addphoto', JSON.parse(objectJson), httpOption);
  }

  registerUser(user: User): Observable<any> {
    return this.http.post<any>(this.apiUserService + 'user/register', user, httpOptionAuthen);
  }

//   getAll(): Observable<any> {
//     const params: URLSearchParams = new URLSearchParams();
// �	return this.http.get<any>(this.apiUserService + 'All');
// �	}

  getUserById(userId: string): Observable<any> {
    return this.http.get<any>(this.apiUserService + 'user?userId=' + userId, httpOptionAuthen);
  }

}
