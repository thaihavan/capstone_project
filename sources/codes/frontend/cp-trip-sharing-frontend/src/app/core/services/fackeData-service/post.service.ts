import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Post } from 'src/app/model/Post';
import { Account } from 'src/app/model/Account';

@Injectable({
  providedIn: 'root'
})
export class PostService {
post: Post;
posts: Post[] = [];
account: Account;
constructor(private http: HttpClient) {
}
fackeData() {
  this.post = new Post();
  this.post.title = 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Velit consectetur deserunt illo esse distinctio.';
  this.account = new Account();
  this.account.Username = 'PhongNV';
  this.post.author = this.account;
  this.post.comments = 123;
  this.post.likes = 123;
  this.post.image = 'luff.jpg';
  this.post.time = 'A month ago';
// tslint:disable-next-line:max-line-length
  this.post.description = 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Magnam omnis nihil, aliquam est, voluptates officiis iure soluta alias vel odit, placeat reiciendis ut libero! Quas aliquid natus cumque quae repellendus. Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa, excepturi. Doloremque, reprehenderit! Quos in maiores, soluta doloremque molestiae reiciendis libero expedita assumenda fuga quae. Consectetur id molestias itaque facere? Hic!';
  // tslint:disable-next-line:new-parens
  this.posts.push(this.post);
  this.posts.push(this.post);
}
getPosts(): Observable<Post[]> {
  // return this.http.get<Post[]>('api/posts');
  this.fackeData();
  return of(this.posts);
}

getDetailPost(): Observable<Post> {
  this.fackeData();
  return of(this.post);
}

}
