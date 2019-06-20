import { Component, OnInit, Input } from '@angular/core';
import { Comment} from 'src/app/model/Comment';

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

  constructor() { }

  showRepComment() {
    this.showRep = !this.showRep;
  }

  submitComment(commentId: string) {
    console.log('id: ' + commentId);
    console.log('content: ' + this.commentContent);

    this.showRep = false;

    this.commentContent = '';
  }
}
