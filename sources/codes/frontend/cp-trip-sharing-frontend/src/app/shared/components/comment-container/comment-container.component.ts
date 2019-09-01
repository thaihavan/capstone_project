import { Component, OnInit, Input } from '@angular/core';
import { Comment} from 'src/app/model/Comment';
import { Post } from 'src/app/model/Post';

@Component({
  selector: 'app-comment-container',
  templateUrl: './comment-container.component.html',
  styleUrls: ['./comment-container.component.css']
})
export class CommentContainerComponent implements OnInit {

  @Input() comments: Comment[];
  @Input() post: any;

  constructor() { }

  ngOnInit() {
  }

}
