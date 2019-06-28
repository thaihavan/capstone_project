import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Bookmark } from 'src/app/model/Bookmark';

@Component({
  selector: 'app-list-bookmarks',
  templateUrl: './list-bookmarks.component.html',
  styleUrls: ['./list-bookmarks.component.css']
})
export class ListBookmarksComponent implements OnInit {
  listBookmark: Bookmark[] = [];
  constructor(private userService: UserService) {
    const token = localStorage.getItem('Token');
    if (token != null) {
      this.userService.getListBookmarksFromUserId(token).subscribe((result: any) => {
        this.listBookmark = result;
        console.log(this.listBookmark);
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }

  ngOnInit() {
  }

}