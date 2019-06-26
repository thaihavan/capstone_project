import { Injectable } from '@angular/core';
import { ImageUpload } from 'src/app/model/ImageUpload';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UploadImageService {
  url = 'https://localhost:44352/api/postservice/uploadfile/uploadimage';
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
}
