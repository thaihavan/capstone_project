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
  follow: boolean;
  constructor(private postService: PostService, private userServiceL: UserService) {
    this.like = new Like();
  }

  ngOnInit() {
    this.token = localStorage.getItem('Token');
    this.userServiceL.getFollowed(this.post.author.id, this.token).subscribe((data: any) => {
      this.follow = data.followed;
    }, (err: HttpErrorResponse) => {
      console.log(err);
    });
  }

  likePost(like: any) {
    this.like.ObjectId = this.post.id;
    this.like.ObjectType = 'post';
    if (like === false) {
      this.postService.likeAPost(this.like).subscribe((data: any) => {
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
    if (this.follow === false) {
      this.userServiceL.addFollow(userId, this.token).subscribe((data: any) => {
        this.follow = true;
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.userServiceL.unFollow(userId, this.token).subscribe((data: any) => {
        this.follow = false;
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }
}
