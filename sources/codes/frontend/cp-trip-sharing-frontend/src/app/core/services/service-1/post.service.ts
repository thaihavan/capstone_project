import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
export interface Post {
  user: string;
  time: string;
  title: string;
  image: string;
  description: string;
  likes: number;
  comments: number;
}
@Injectable({
  providedIn: 'root'
})
export class PostService {

constructor(private http: HttpClient) { }

getPosts(): Observable<Post[]> {
  return this.http.get<Post[]>('api/posts');
}
}
