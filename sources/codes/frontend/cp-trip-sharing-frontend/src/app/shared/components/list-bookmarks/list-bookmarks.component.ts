import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { Bookmark } from 'src/app/model/Bookmark';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';

@Component({
  selector: 'app-list-bookmarks',
  templateUrl: './list-bookmarks.component.html',
  styleUrls: ['./list-bookmarks.component.css']
})
export class ListBookmarksComponent implements OnInit {
  listBookmark: Bookmark[] = [];
  constructor(private userService: UserService,
              private errorHandler: GlobalErrorHandler) {
    const token = localStorage.getItem('Token');
    if (token != null) {
      this.userService.getListBookmarksFromUserId(token).subscribe((result: any) => {
        this.listBookmark = result;
      }, this.errorHandler.handleError);
    }
  }

  ngOnInit() {
  }

}
