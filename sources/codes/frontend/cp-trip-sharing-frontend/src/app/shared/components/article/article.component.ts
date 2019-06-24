import { Component, OnInit, Input } from '@angular/core';
import { Post } from 'src/app/model/Post';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { UserService } from 'src/app/core/services/user-service/user.service';
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
  token: string;
  follow = false;
  constructor(private postService: PostService, private userServiceL: UserService) {
    this.like = new Like();
    this.token = localStorage.getItem('Token');
  }

  ngOnInit() {
  }

  likePost(like: any) {
    this.like.ObjectId = this.post.id;
    this.like.ObjectType = 'post';
    if (like === false) {
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

  followPerson(userId: any) {
    debugger;
    this.follow = !this.follow;
    if (this.follow === true) {
      this.userServiceL.addFollow(userId, this.token).subscribe((data: any) => {
        console.log(data + 'fo');
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.userServiceL.unFollow(userId, this.token).subscribe((data: any) => {
        console.log(data + 'un');
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }
}
