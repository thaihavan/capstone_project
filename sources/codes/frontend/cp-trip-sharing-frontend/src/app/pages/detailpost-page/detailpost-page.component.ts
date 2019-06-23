import { Component, OnInit } from '@angular/core';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { Comment } from 'src/app/model/Comment';
import { Post } from 'src/app/model/Post';
import { ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

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

  constructor(private postService: PostService, private route: ActivatedRoute) {
    this.post = new Post();
    this.comments = [];
  }

  ngOnInit() {
    this.postId = this.route.snapshot.queryParamMap.get('postId');
    this.loadDetaiPost(this.postId);
    this.getCommentByPostId(this.postId);
    this.token = localStorage.getItem('Token');
  }

  loadDetaiPost(postid: string) {
    this.postService.getDetail(postid).subscribe((data: any) => {
      this.post = data;
      console.log(data);
    });
  }

  getCommentByPostId(postId: string) {
    if (this.token == null) {
      this.postService.getCommentByPost(postId).subscribe((data: any) => {
        if (data != null) {
          console.log('Comment: ' + data);
          console.log('Total comment: ', data.length);
          this.comments = data;
        } else {
          console.log('Can not get comments of this post.');
        }
      });
    } else {
      this.postService.getCommentByPostWithAuthen(postId, this.token).subscribe((data: any) => {
        if (data != null) {
          console.log('Comment: ' + data);
          console.log('Total comment: ', data.length);
          this.comments = data;
        } else {
          console.log('Can not get comments of this post.');
        }
      });
    }
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

}
