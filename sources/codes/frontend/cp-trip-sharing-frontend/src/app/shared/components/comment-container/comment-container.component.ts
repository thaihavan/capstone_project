import { Component, OnInit, Input } from '@angular/core';
import { Comment} from 'src/app/model/Comment';

@Component({
  selector: 'app-comment-container',
  templateUrl: './comment-container.component.html',
  styleUrls: ['./comment-container.component.css']
})
export class CommentContainerComponent implements OnInit {

  @Input() comments: Comment[];

  constructor() { }

  ngOnInit() {
    console.log('Comments value in comment-container: ' + this.comments);
  }

}
