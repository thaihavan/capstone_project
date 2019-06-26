import { Component, OnInit, Input } from '@angular/core';
import { Post } from 'src/app/model/Post';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { Like } from 'src/app/model/Like';
import { HttpErrorResponse } from '@angular/common/http';
import { User } from 'src/app/model/User';

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
  checkUser = true;
  user: any;
  userId: string;
  listUserIdFollowing: string[] = [];
  constructor(private postService: PostService, private userService: UserService) {
    this.like = new Like();
    this.token = localStorage.getItem('Token');
  }

  ngOnInit() {
    this.user = JSON.parse(localStorage.getItem('Account'));
    if (this.user.userId === this.post.author.id) {
      this.checkUser = false;
    }
    this.listUserIdFollowing = JSON.parse(localStorage.getItem('listUserIdFollowing'));
    if (this.listUserIdFollowing != null) {
      // tslint:disable-next-line:prefer-for-of
      for (let i = 0; i < this.listUserIdFollowing.length; i++) {
        if (this.post.author.id === this.listUserIdFollowing[i]) {
          this.follow = true;
          break;
        }
      }
    }
  }

  likePost(like: any) {
    this.like.ObjectId = this.post.id;
    this.like.ObjectType = 'post';
    if (like === false) {
      this.postService.likeAPost(this.like).subscribe((data: any) => {
        this.post.liked = true;
        this.post.likeCount += 1;
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.postService.unlikeAPost(this.like).subscribe((data: any) => {
        this.post.liked = false;
        this.post.likeCount -= 1;
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }

  followPerson(userId: any) {
    if (this.follow === false) {
      this.userService.addFollow(userId, this.token).subscribe((data: any) => {
        this.follow = true;
        this.listUserIdFollowing.push(userId);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.userService.unFollow(userId, this.token).subscribe((data: any) => {
        this.follow = false;
        const unfollow = this.listUserIdFollowing.indexOf(userId);
        this.listUserIdFollowing.splice(unfollow, 1);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }
}
