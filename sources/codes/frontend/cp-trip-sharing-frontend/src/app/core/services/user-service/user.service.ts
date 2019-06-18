import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import {Account} from 'src/Model/Account';
import { User } from 'src/app/model/User';


const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
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
    return this.http.post<Account>(this.apiUrl + 'authenticate', account, httpOptions);
  }

  registerAccount(account: Account): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'register', account, httpOptions);
  }

  changePassword(account: Account): Observable<any> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.post<any>(this.apiUrl + 'changePassword', account, httpOption);
  }

  verifyEmail(token: string): Observable<any> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.post<any>(this.apiUrl + 'verify', null, httpOption);
  }

  addBlock(blocked: string): Observable<any> {
    return this.http.post<any>(this.apiUserService + 'addblock', blocked, httpOptions);
  }

  unBlock(blocked: string): Observable<any> {
    return this.http.delete<any>(this.apiUserService + 'unblock' + blocked);
  }

  bookMark(postId: string): Observable<any> {
    return this.http.post<any>(this.apiUserService + 'bookmark', postId, httpOptions);
  }

  deleteBookmark(postId: string): Observable<any> {
    return this.http.delete<any>(this.apiUserService + 'deletebookmark' + postId);
  }

  addFollow(following: string): Observable<any> {
    return this.http.post<any>(this.apiUserService + 'follow', following, httpOptions);
  }

  unFollow(following: string): Observable<any> {
    return this.http.delete<any>(this.apiUserService + 'unfollow?following=' + following);
  }

  getAllPhoto(): Observable<any> {
    return this.http.get<any>(this.apiUserService + 'allphoto');
  }

  addPhoto(url: string, date: Date): Observable<any> {
    const objectJson = '{"url":' + '"' + url + '"' + ',' + '"date":' + '"' + date + '"';
    return this.http.post<any>(this.apiUserService + 'addphoto', JSON.parse(objectJson), httpOptions);
  }

  registerUser(user: User): Observable<any> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.post<any>(this.apiUserService + 'Register', user, httpOptions);
  }

//   getAll(): Observable<any> {
//     const params: URLSearchParams = new URLSearchParams();
// •	return this.http.get<any>(this.apiUserService + 'All');
// •	}

  getUserById(userId: string): Observable<any> {
    const token = localStorage.getItem('Token');
    const httpOption = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.get<any>(this.apiUserService + 'User?userId=' + userId, httpOption);
  }

}
