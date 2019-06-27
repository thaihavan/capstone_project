import { Component, OnInit, Input } from '@angular/core';
import { Bookmark } from 'src/app/model/Bookmark';

@Component({
  selector: 'app-bookmark-post',
  templateUrl: './bookmark-post.component.html',
  styleUrls: ['./bookmark-post.component.css']
})
export class BookmarkPostComponent implements OnInit {
  @Input() bookmark: any;
  constructor() { }

  ngOnInit() {
    if (this.bookmark.title == null) {
      this.bookmark.title = 'Không có title nên để tạm như thế này nhé';
    }
    if (this.bookmark.postType == null) {
      this.bookmark.postType = 'Không có postType';
    }
    if (this.bookmark.coverImage == null) {
      this.bookmark.coverImage = 'https://gody.vn/public/v3/images/bg/br-register.jpg';
    }
    console.log(this.bookmark.title);
  }

}
