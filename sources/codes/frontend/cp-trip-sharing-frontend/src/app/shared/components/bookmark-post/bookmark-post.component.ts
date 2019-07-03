import { Component, OnInit, Input } from '@angular/core';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-bookmark-post',
  templateUrl: './bookmark-post.component.html',
  styleUrls: ['./bookmark-post.component.css']
})
export class BookmarkPostComponent implements OnInit {
  @Input() bookmark: any;
  listPostIdBookMark: string[] = [];
  constructor(private userService: UserService) { }

  ngOnInit() {
    if (this.bookmark.title == null) {
      this.bookmark.title = 'Không có title ';
    }
    if (this.bookmark.postType == null) {
      this.bookmark.postType = 'Không có postType';
    }
    if (this.bookmark.coverImage == null) {
      this.bookmark.coverImage = 'https://gody.vn/public/v3/images/bg/br-register.jpg';
    }
  }

  removeBookmark(postId: any) {
    this.listPostIdBookMark = JSON.parse(localStorage.getItem('listPostIdBookmark'));
    const token = localStorage.getItem('Token');
    if (this.listPostIdBookMark != null) {
      this.userService.deleteBookMark(postId, token).subscribe((data: any) => {
        const unbookmark = this.listPostIdBookMark.indexOf(postId);
        this.listPostIdBookMark.splice(unbookmark, 1);
        localStorage.setItem('listPostIdBookmark', JSON.stringify(this.listPostIdBookMark));
        window.location.href = '/user';
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });

    }

  }
}
