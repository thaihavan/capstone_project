import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ResetPasswordModel } from 'src/app/model/ResetPasswordModel';
import { Account } from 'src/app/model/Account';
import { User } from 'src/app/model/User';
import { ChangePassword } from 'src/app/model/ChangePassword';
import { HostGlobal } from 'src/app/core/global-variables';

const httpOption = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

const httpOptionAuthen = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'Bearer ' + localStorage.getItem('Token')
  })
};

@Injectable({
  providedIn: 'root'
})
export class UserService {
  apiUrl: string = null;
  apiUserService: string = null;
  constructor(private http: HttpClient) {
    this.apiUrl = HostGlobal.HOST_IDENTITY_PROVIDER + '/api/identity/account/';
    this.apiUserService = HostGlobal.HOST_USER_SERVICE + '/api/userservice/';
  }

  getAccount(account: Account): Observable<Account> {
    return this.http.post<Account>(this.apiUrl + 'authenticate', account, httpOption);
  }

  registerAccount(account: Account): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'register', account, httpOption);
  }

  forgotPassword(account: Account): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'forgotpassword', account, httpOption);
  }

  changePassword(changePasswordObject: ChangePassword): Observable<any> {
    const httpOptionAu = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.post<any>(this.apiUrl + 'changePassword', changePasswordObject, httpOptionAu);
  }

  resetPassword(token: string, newpassword: ResetPasswordModel): Observable<any> {
    const httpOptionAu = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.post<any>(this.apiUrl + 'resetpassword', newpassword, httpOptionAu);
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

  // addBlock(blocked: string): Observable<any> {
  //   return this.http.post<any>(this.apiUserService + 'user/addblock', blocked, httpOption);
  // }

  // unBlock(blocked: string): Observable<any> {
  //   return this.http.delete<any>(this.apiUserService + 'user/unblock' + blocked);
  // }
  getListPostIdBookmarks(token: string): Observable<any> {
    const httpAuthen = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.get<any>(this.apiUserService + 'bookmark/bookmarkPostId', httpAuthen);
  }

  getListBookmarksFromUserId(token: string): Observable<any> {
    const httpAuthen = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.get<any>(this.apiUserService + 'bookmark/bookmark', httpAuthen);
  }

  addBookMark(bookmark: any, token: string): Observable<any> {
    const httpAuthen = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.post<any>(this.apiUserService + 'bookmark/bookmark', bookmark, httpAuthen);
  }

  deleteBookMark(postId: string, token: string): Observable<any> {
    const httpAuthen = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      }),
      body: null
    };
    return this.http.delete<any>(this.apiUserService + 'bookmark/bookmark?postId=' + postId, httpAuthen);
  }

  addFollow(following: string, token: string): Observable<any> {
    const httpAuthen = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.post<any>(this.apiUserService + 'follow/follow?following=' + following, null, httpAuthen);
  }

  unFollow(following: string, token: string): Observable<any> {
    const httpAuthen = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      }),
      body: null
    };
    return this.http.delete<any>(this.apiUserService + 'follow/unfollow?following=' + following, httpAuthen);
  }

  getFollowed(authorId: string, token: string) {
    const httpAuthen = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.get<any>(this.apiUserService + 'follow/followed?following=' + authorId, httpAuthen);
  }

  getAllFollower(userId: any): Observable<any> {
    return this.http.get<any>(this.apiUserService + 'follow/follower?userId=' + userId);
  }

  getAllFollowing(userId: any): Observable<any> {
    return this.http.get<any>(this.apiUserService + 'follow/following?userId=' + userId);
  }

  getAllFollowingId(token: any): Observable<any> {
    const httpAuthen = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.get<any>(this.apiUserService + 'follow/followingids', httpAuthen);
  }

  // getAllPhoto(): Observable<any> {
  //   return this.http.get<any>(this.apiUserService + 'user/allphoto');
  // }

  // addPhoto(url: string, date: Date): Observable<any> {
  //   const objectJson = '{"url":' + '"' + url + '"' + ',' + '"date":' + '"' + date + '"';
  //   return this.http.post<any>(this.apiUserService + 'user/addphoto', JSON.parse(objectJson), httpOption);
  // }

  registerUser(user: User): Observable<any> {
    return this.http.post<any>(this.apiUserService + 'user/register', user, httpOptionAuthen);
  }

  updateUser(user: User): Observable<any> {
    return this.http.post<any>(this.apiUserService + 'user/update', user, httpOptionAuthen);
  }

  //   getAll(): Observable<any> {
  //     const params: URLSearchParams = new URLSearchParams();
  // �	return this.http.get<any>(this.apiUserService + 'All');
  // �	}

  getUserById(userId: string): Observable<any> {
    return this.http.get<any>(this.apiUserService + 'user?userId=' + userId, httpOptionAuthen);
  }
}
