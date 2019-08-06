import { Component, OnInit, Input } from '@angular/core';
import { Post } from 'src/app/model/Post';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';

@Component({
  selector: 'app-post-item',
  templateUrl: './post-item.component.html',
  styleUrls: ['./post-item.component.css']
})
export class PostItemComponent implements OnInit {

  @Input() post: Post;
  constructor(private userService: UserService,
              private errorHandler: GlobalErrorHandler) { }

  ngOnInit() {
    if (this.post.title == null) {
      this.post.title = 'Không có title ';
    }
    if (this.post.postType == null) {
      this.post.postType = 'Không có postType';
    }
    if (this.post.coverImage == null) {
      this.post.coverImage = 'https://gody.vn/public/v3/images/bg/br-register.jpg';
    }
  }

  goToPostDetail() {
    window.location.href = '/bai-viet/' + this.post.id;
  }
  gotoPersonalPage(userId: string) {
    window.location.href = `/user/${userId}`;
  }

}
