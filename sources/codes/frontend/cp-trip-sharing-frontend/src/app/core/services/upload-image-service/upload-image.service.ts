import { Injectable } from '@angular/core';
import { ImageUpload } from 'src/app/model/ImageUpload';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { HostGlobal } from '../../global-variables';
import { tap, map, catchError } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class UploadImageService {
  url =
    HostGlobal.HOST_POST_SERVICE + '/api/postservice/uploadfile/uploadimage';
  httpOption = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('Token')
    })
  };
  constructor(private http: HttpClient) { }
  uploadImage(Image: ImageUpload): Observable<ImageUpload> {
    return this.http.post<ImageUpload>(this.url, Image, this.httpOption);
  }
  uploadGoogleMapImage(url: string) {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        responseType: 'blod',
      })
    };
    this.http.get('https://cors-anywhere.herokuapp.com/' + url, httpOption)
      .pipe(
        tap( // Log the result or error
          data => console.log(data),
          error => console.log(error)
        )).subscribe();

    // this.http.get('https://cors-anywhere.herokuapp.com/' + url, { withCredentials: true, observe: 'response' })
    //   .pipe(
    //     map((resp: any) => {
    //       console.log('response', resp);
    //       return resp;

    //     }), catchError(error => {
    //       console.log('createOrder error', error);
    //       alert('Create Order Service returned an error. See server log for mote details');
    //       return throwError('createOrder: ' + error);
    //     })
    //   ).subscribe();
  }
}
