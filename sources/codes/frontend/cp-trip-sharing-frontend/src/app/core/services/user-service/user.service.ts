import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import {Account} from 'src/Model/Account';

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
}
