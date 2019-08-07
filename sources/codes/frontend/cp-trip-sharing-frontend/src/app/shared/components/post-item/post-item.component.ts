import { Component, OnInit, Input } from '@angular/core';
import { Post } from 'src/app/model/Post';
import { HostGlobal } from 'src/app/core/global-variables';

@Component({
  selector: 'app-post-item',
  templateUrl: './post-item.component.html',
  styleUrls: ['./post-item.component.css']
})
export class PostItemComponent implements OnInit {

  @Input() post: Post;
  constructor() { }

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
    switch (this.post.postType) {
      case 'Article':
        window.open(`${HostGlobal.HOST_FRONTEND}/bai-viet/${this.post.id}`, '_blank');
        break;
      case 'VirtualTrip':
        window.open(`${HostGlobal.HOST_FRONTEND}/chuyen-di/${this.post.id}`, '_blank');
        break;
      case 'CompanionPost':
        window.open(`${HostGlobal.HOST_FRONTEND}/tim-ban-dong-hanh/${this.post.id}`, '_blank');
        break;
    }

  }
  gotoPersonalPage(userId: string) {
    window.location.href = `/user/${userId}`;
  }

}
