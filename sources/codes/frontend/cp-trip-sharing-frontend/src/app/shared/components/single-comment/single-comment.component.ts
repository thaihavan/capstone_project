import { Component, OnInit, Input } from '@angular/core';
import { Comment } from 'src/app/model/Comment';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Like } from 'src/app/model/Like';

@Component({
  selector: 'app-single-comment',
  templateUrl: './single-comment.component.html',
  styleUrls: ['./single-comment.component.css']
})
export class SingleCommentComponent implements OnInit {
  @Input() comment: Comment;
  commentContent = '';
  liked = false;
  showRep = false;
  like: Like;
  ngOnInit(): void {
  }

  constructor(private postService: PostService) {
    this.like = new Like();
  }

  showRepComment() {
    this.showRep = !this.showRep;
  }

  submitComment() {
    console.log('content: ' + this.commentContent);

    this.showRep = false;

    const comment = new Comment();
    comment.content = this.commentContent;
    comment.postId = this.comment.postId;
    comment.parentId = this.comment.id;
    this.postService.addComment(comment).subscribe((res: Comment) => {
      this.comment.childs.push(res);

    }, (error: HttpErrorResponse) => {
      console.log(error);
    });
    this.commentContent = '';
  }

  likeComment(liked: any, commetId: any) {
    this.like.ObjectId = commetId;
    this.like.ObjectType = 'comment';
    if (!liked) {
      this.postService.likeAPost(this.like).subscribe((data: any) => {
        this.comment.likeCount += 1;
        this.comment.liked = true;
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.postService.unlikeAPost(this.like).subscribe((data: any) => {
        this.comment.likeCount -= 1;
        this.comment.liked = false;
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }
}
