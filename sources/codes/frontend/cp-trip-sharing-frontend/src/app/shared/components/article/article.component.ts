import { Component, OnInit, Input } from '@angular/core';
import { Post } from 'src/app/model/Post';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css']
})
export class ArticleComponent implements OnInit {

  @Input() post: Post;
  name = 'PhongNV';
  constructor() { }

  ngOnInit() {
  }

}
