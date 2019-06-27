import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { resource } from 'selenium-webdriver/http';

@Component({
  selector: 'app-bookmark-post',
  templateUrl: './bookmark-post.component.html',
  styleUrls: ['./bookmark-post.component.css']
})
export class BookmarkPostComponent implements OnInit {
  constructor(private userService: UserService) {
    const token = localStorage.getItem('Token');
    if (token != null) {
      this.userService.getListBookmarksFromUserId(token).subscribe((result: any) => {
        console.log(result + 'listbookmark' );
      });
    }
  }

  ngOnInit() {
  }

}
