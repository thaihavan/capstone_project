import { Component, OnInit } from '@angular/core';
import { PostService } from 'src/app/core/services/service-1/post.service';
import {Post} from 'src/Model/Post'

@Component({
  selector: 'app-detailpost-page',
  templateUrl: './detailpost-page.component.html',
  styleUrls: ['./detailpost-page.component.css']
})
export class DetailpostPageComponent implements OnInit {
  post: Post;
  coverImage = '../../../assets/coverimg.jpg';
  avatar = '../../../assets/img_avatar.png';
  constructor(private postService: PostService) {
    this.post = new Post();
  }

  ngOnInit() {
    this.postService.getDetailPost().subscribe(post => {
      this.post = post;
    });
  }

}
