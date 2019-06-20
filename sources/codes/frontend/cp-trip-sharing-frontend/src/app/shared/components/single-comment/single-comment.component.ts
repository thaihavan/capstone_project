import { Component, OnInit, Input } from '@angular/core';
import { Comment} from 'src/app/model/Comment';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-single-comment',
  templateUrl: './single-comment.component.html',
  styleUrls: ['./single-comment.component.css']
})
export class SingleCommentComponent implements OnInit {
  @Input() comment: Comment;
  commentContent = '';

  showRep = false;

  ngOnInit(): void {
  }

  constructor(private postService: PostService) { }

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
}
