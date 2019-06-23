import { Component, OnInit, Input } from '@angular/core';
import { Post } from 'src/app/model/Post';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { Like } from 'src/app/model/Like';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css']
})
export class ArticleComponent implements OnInit {
  like: Like;
  @Input() post: Post;
  name = 'PhongNV';
  constructor(private postService: PostService) {
    this.like = new Like();
  }

  ngOnInit() {
  }

  likePost(like: any) {
    this.like.ObjectId = this.post.id;
    this.like.ObjectType = 'post';
    if (!like) {
      this.postService.likeAPost(this.like).subscribe((data: any) => {
        console.log(data);
        this.post.liked = true;
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.postService.unlikeAPost(this.like).subscribe((data: any) => {
        this.post.liked = false;
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }
}
