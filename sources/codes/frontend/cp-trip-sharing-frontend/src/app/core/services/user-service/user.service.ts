import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ResetPasswordModel } from 'src/app/model/ResetPasswordModel';
import { Account } from 'src/app/model/Account';
import { User } from 'src/app/model/User';
import { ChangePassword } from 'src/app/model/ChangePassword';
import { HostGlobal } from 'src/app/core/global-variables';
import { Bookmark } from 'src/app/model/Bookmark';
import { ReportType } from 'src/app/model/ReportType';
import { Report } from 'src/app/model/Report';

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
  apiPostService: string = null;
  constructor(private http: HttpClient) {
    this.apiUrl = HostGlobal.HOST_IDENTITY_PROVIDER + '/api/identity/account/';
    this.apiUserService = HostGlobal.HOST_USER_SERVICE + '/api/userservice/';
    this.apiPostService = HostGlobal.HOST_POST_SERVICE + '/api/postservice/';
  }

  getAccount(account: Account): Observable<Account> {
    return this.http.post<Account>(this.apiUrl + 'authenticate', account, httpOption);
  }

  Logout(): Observable<any> {
    return this.http.post(this.apiUrl + 'logout', null, httpOptionAuthen);
  }

  loginWithgoogle(token: string): Observable<Account> {
    return this.http.get<Account>(this.apiUrl + 'authenticate/google?token=' + token);
  }

  loginWithFacebook(token: string): Observable<Account> {
    return this.http.get<Account>(this.apiUrl + 'authenticate/facebook?token=' + token);
  }

  registerAccount(account: Account): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'register', account, httpOption);
  }

  forgotPassword(email: any): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'forgotpassword?email=' + email, null, httpOption);
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

  addBlock(userId: string, token: any): Observable<any> {
    const httpAuthen = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.post<any>(this.apiUserService + 'block/addblock?blocked=' + userId, null, httpAuthen);
  }

  unBlock(userId: string, token: any): Observable<any> {
    const httpAuthen = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      }),
      body: null
    };
    return this.http.delete<any>(this.apiUserService + 'block/unblock?blocked=' + userId , httpAuthen);
  }

  getBlockeds(): Observable<any> {
    const httpAuthen = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.get<any>(this.apiUserService + 'block/blocked', httpAuthen);
  }

  getBlockers(): Observable<User[]> {
    const httpAuthen = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.get<User[]>(this.apiUserService + 'block/blocker', httpAuthen);
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

  getAllFollowingId(userId: string): Observable<any> {
    return this.http.get<any>(this.apiUserService + 'follow/followingids?userId=' + userId);
  }

  getListPostIdBookmarks(token: string): Observable<any> {
    const httpAuthen = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.get<any>(this.apiPostService + 'bookmark/bookmarkPostId', httpAuthen);
  }

  getListBookmarksFromUserId(token: string): Observable<any> {
    const httpAuthen = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.get<any>(this.apiPostService + 'bookmark/bookmark', httpAuthen);
  }

  addBookMark(bookmark: any, token: string): Observable<any> {
    const httpAuthen = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.post<any>(this.apiPostService + 'bookmark/bookmark', bookmark, httpAuthen);
  }

  deleteBookMark(postId: string, token: string): Observable<any> {
    const httpAuthen = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      }),
      body: null
    };
    return this.http.delete<any>(this.apiPostService + 'bookmark/bookmark?postId=' + postId, httpAuthen);
  }

  registerUser(user: User): Observable<any> {
    return this.http.post<any>(this.apiUserService + 'user/register', user, httpOptionAuthen);
  }

  updateUser(user): Observable<any> {
    return this.http.post<any>(this.apiUserService + 'user/update', user, httpOptionAuthen);
  }

  getUserById(userId: string): Observable<any> {
    return this.http.get<any>(this.apiUserService + 'user?userId=' + userId, httpOption);
  }

  getUsers(search: string, page: number): Observable<any> {
    return this.http.get<any>(`${this.apiUserService}user/all?page=${page}&search=${search}`, httpOption);
  }

  getReportUserTypes(): Observable<ReportType[]> {
    const url = `${this.apiUserService}user/report/type`;
    return this.http.get<ReportType[]>(url, httpOptionAuthen);
  }

  sendReportUser(reportUser: Report): Observable<any> {
    const url = `${this.apiUserService}user/report`;
    return this.http.post(url, reportUser, httpOptionAuthen);
  }

  checkValidateUserName(username): Observable<any> {
    const url = `${this.apiUserService}user/check-username`;
    return this.http.get(url + '?username=' + username, httpOptionAuthen);
  }

  getContributionPoint(userId): Observable<any> {
    const url = `${this.apiUserService}user/contribution`;
    return this.http.get(url + '?userId=' + userId, httpOptionAuthen);
  }
}
