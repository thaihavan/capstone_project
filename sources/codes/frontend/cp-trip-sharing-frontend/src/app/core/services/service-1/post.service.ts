import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { post } from 'selenium-webdriver/http';
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
posts: Post[] = [
  {
    user: 'PhongNV',
    time: 'A month ago',
    title: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Velit consectetur deserunt illo esse distinctio.',
    image: 'luff.jpg',
    // tslint:disable-next-line:max-line-length
    description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Magnam omnis nihil, aliquam est, voluptates officiis iure soluta alias vel odit, placeat reiciendis ut libero! Quas aliquid natus cumque quae repellendus. Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa, excepturi. Doloremque, reprehenderit! Quos in maiores, soluta doloremque molestiae reiciendis libero expedita assumenda fuga quae. Consectetur id molestias itaque facere? Hic!',
    likes: 123,
    comments: 123
},
{
    user: 'PhongNV',
    time: 'A month ago',
    title: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Velit consectetur deserunt illo esse distinctio.',
    image: 'luff.jpg',
    // tslint:disable-next-line:max-line-length
    description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Magnam omnis nihil, aliquam est, voluptates officiis iure soluta alias vel odit, placeat reiciendis ut libero! Quas aliquid natus cumque quae repellendus. Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa, excepturi. Doloremque, reprehenderit! Quos in maiores, soluta doloremque molestiae reiciendis libero expedita assumenda fuga quae. Consectetur id molestias itaque facere? Hic!',
    likes: 123,
    comments: 123
}
];

post: Post = {
  user: 'PhongNV',
  time: 'A month ago',
  title: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Velit consectetur deserunt illo esse distinctio.',
  image: 'luff.jpg',
  // tslint:disable-next-line:max-line-length
  description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Magnam omnis nihil, aliquam est, voluptates officiis iure soluta alias vel odit, placeat reiciendis ut libero! Quas aliquid natus cumque quae repellendus. Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa, excepturi. Doloremque, reprehenderit! Quos in maiores, soluta doloremque molestiae reiciendis libero expedita assumenda fuga quae. Consectetur id molestias itaque facere? Hic! Lorem ipsum dolor sit amet consectetur adipisicing elit. Magnam omnis nihil, aliquam est, voluptates officiis iure soluta alias vel odit, placeat reiciendis ut libero! Quas aliquid natus cumque quae repellendus. Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa, excepturi. Doloremque, reprehenderit! Quos in maiores, soluta doloremque molestiae reiciendis libero expedita assumenda fuga quae. Consectetur id molestias itaque facere? Hic!',
  likes: 123,
  comments: 123
};
getPosts(): Observable<Post[]> {
  // return this.http.get<Post[]>('api/posts');
  return of(this.posts);
}

getDetailPost(): Observable<Post> {
  return of(this.post);
}

}
