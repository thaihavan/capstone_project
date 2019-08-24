import { Injectable } from '@angular/core';
import { ImageUpload } from 'src/app/model/ImageUpload';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, observable } from 'rxjs';
import { HostGlobal } from '../../global-variables';
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
  constructor(private http: HttpClient) {}
  uploadImage(Image: ImageUpload): Observable<ImageUpload> {
    return this.http.post<ImageUpload>(this.url, Image, this.httpOption);
  }
   uploadGoogleMapImage(url: string): Observable<Blob> {
    const proxyurl = 'https://cors-anywhere.herokuapp.com/';
    return this.http.get(proxyurl + url, { responseType: 'blob'});
  }
}
