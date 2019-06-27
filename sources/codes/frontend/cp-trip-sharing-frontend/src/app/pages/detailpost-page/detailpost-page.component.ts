import { Component, OnInit } from '@angular/core';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { Comment } from 'src/app/model/Comment';
import { Post } from 'src/app/model/Post';
import { ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { UserService } from 'src/app/core/services/user-service/user.service';

@Component({
  selector: 'app-detailpost-page',
  templateUrl: './detailpost-page.component.html',
  styleUrls: ['./detailpost-page.component.css']
})
export class DetailpostPageComponent implements OnInit {
  post: Post;
  postId: string;
  coverImage = '../../../assets/coverimg.jpg';
  avatar = '../../../assets/img_avatar.png';
  token: string;
  commentContent = '';
  comments: Comment[];
  follow = false;
  followed = false;
  listUserIdFollowing: string[] = [];

  constructor(private postService: PostService, private route: ActivatedRoute, private userService: UserService) {
    this.post = new Post();
    this.comments = [];
    this.postId = this.route.snapshot.queryParamMap.get('postId');
    this.loadDetaiPost(this.postId);
    this.token = localStorage.getItem('Token');
    this.getCommentByPostId(this.postId);
  }

  ngOnInit() { }

  loadDetaiPost(postid: string) {
    this.postService.getDetail(postid).subscribe((data: any) => {
      this.post = data;
      this.listUserIdFollowing = JSON.parse(localStorage.getItem('listUserIdFollowing'));
      if (this.listUserIdFollowing != null) {
        // tslint:disable-next-line:prefer-for-of
        for (let i = 0; i < this.listUserIdFollowing.length; i++) {
          if (data.authorId === this.listUserIdFollowing[i]) {
            this.followed = true;
            this.follow = false;
            break;
          } else {
            this.followed = false;
            this.follow = true;
          }
        }
      }
    });
  }

  getCommentByPostId(postId: string) {
      this.postService.getCommentByPost(postId, this.token).subscribe((data: any) => {
        if (data != null) {
          console.log('Comment: ' + data);
          console.log('Total comment: ', data.length);
          this.comments = data;
        } else {
          console.log('Can not get comments of this post.');
        }
      });
  }

  submitComment() {
    const comment = new Comment();
    comment.content = this.commentContent;
    comment.postId = this.postId;
    comment.parentId = null;

    this.postService.addComment(comment).subscribe((res: Comment) => {
      console.log('add comment res: ' + res);
      this.comments.push(res);
    }, (error: HttpErrorResponse) => {
      console.log(error);
    });
  }

  followPerson(userId: any) {
    if (this.followed === false && this.follow === true) {
      this.userService.addFollow(userId, this.token).subscribe((data: any) => {
        this.followed = true;
        this.follow = false;
        this.listUserIdFollowing.push(userId);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.userService.unFollow(userId, this.token).subscribe((data: any) => {
        this.followed = false;
        this.follow = true;
        const unfollow = this.listUserIdFollowing.indexOf(userId);
        this.listUserIdFollowing.splice(unfollow, 1);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }
}
