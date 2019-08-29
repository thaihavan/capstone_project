import { Component, OnInit, Input } from '@angular/core';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { Bookmark } from 'src/app/model/Bookmark';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
<<<<<<< Updated upstream
import { MessagePopupComponent } from '../message-popup/message-popup.component';
import { MatDialog } from '@angular/material';
=======
import { Router } from '@angular/router';
>>>>>>> Stashed changes

@Component({
  selector: 'app-bookmark-post',
  templateUrl: './bookmark-post.component.html',
  styleUrls: ['./bookmark-post.component.css']
})
export class BookmarkPostComponent implements OnInit {
  @Input() bookmark: Bookmark;
  listPostIdBookMark: string[] = [];
  checkRemoved = false;
  constructor(private userService: UserService,
              private errorHandler: GlobalErrorHandler,
<<<<<<< Updated upstream
              private dialog: MatDialog) { }
=======
              private router: Router) { }
>>>>>>> Stashed changes

  ngOnInit() {
  }

  removeBookmark(postId: any) {
    this.listPostIdBookMark = JSON.parse(localStorage.getItem('listPostIdBookmark'));
    const token = localStorage.getItem('Token');
    if (this.listPostIdBookMark != null) {
      const dialogRef = this.dialog.open(MessagePopupComponent, {
        width: '500px',
        height: 'auto',
        position: {
          top: '20px'
        },
        disableClose: true
      });
      const instance = dialogRef.componentInstance;
      instance.message.messageText = `Bạn có chắc chắn muốn xóa bài viết khỏi danh sách đánh đã dấu không?`;
      instance.message.messageType = 'confirm';
      dialogRef.afterClosed().subscribe((res: string) => {
        if (res === 'continue') {
          this.userService.deleteBookMark(postId, token).subscribe((data: any) => {
            const unbookmark = this.listPostIdBookMark.indexOf(postId);
            this.listPostIdBookMark.splice(unbookmark, 1);
            localStorage.setItem('listPostIdBookmark', JSON.stringify(this.listPostIdBookMark));
            this.checkRemoved = true;
          }, this.errorHandler.handleError);
        }
      });
    }
  }
  goToPostDetail(type) {
    if (type === 'Article') {
      this.router.navigate(['/bai-viet', this.bookmark.postId]);
    }
    if ( type === 'CompanionPost') {
      this.router.navigate(['/tim-ban-dong-hanh', this.bookmark.postId]);
    }
    // window.location.href = '/bai-viet/' + this.bookmark.postId;
  }
}
