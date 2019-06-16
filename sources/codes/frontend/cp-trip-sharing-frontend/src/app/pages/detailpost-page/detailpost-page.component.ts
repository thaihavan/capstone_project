import { Component, OnInit } from '@angular/core';
import { PostService } from 'src/app/core/services/service-1/post.service';
import {Post} from 'src/Model/Post';
import { Comment} from 'src/app/model/Comment';

@Component({
  selector: 'app-detailpost-page',
  templateUrl: './detailpost-page.component.html',
  styleUrls: ['./detailpost-page.component.css']
})
export class DetailpostPageComponent implements OnInit {
  post: Post;
  coverImage = '../../../assets/coverimg.jpg';
  avatar = '../../../assets/img_avatar.png';

  comment = new Comment();
  comments: Comment[];
  child1: Comment;
  child2: Comment;
  child3: Comment;

  constructor(private postService: PostService) {
    this.post = new Post();
  }

  ngOnInit() {
    this.postService.getDetailPost().subscribe(post => {
      this.post = post;

      this.comment.userDisplayName = 'Ha Van Thai';
      this.comment.userImageUrl = 'https://images.pexels.com/photos/414612/pexels-photo-414612.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500';
      this.comment.date = '5 phút trước';
      this.comment.content = 'Thật không dễ dàng để chọn ra những từ đẹp nhất trong tổng số khoảng 750 000 từ tiếng Anh. Với bài học này, chúng ta cùng tìm hiểu 12 từ được coi là đẹp nhất trong tiếng Anh ở khía cạnh nào đó.';
      this.comment.numLikes = 30;

      this.child1 = JSON.parse(JSON.stringify(this.comment));
      this.child2 = JSON.parse(JSON.stringify(this.comment));
      this.child3 = JSON.parse(JSON.stringify(this.comment));

      this.child1.childs = [this.child3];
      this.comment.childs = [this.child1, this.child2];

      this.comments = [this.comment];

      console.log('Comments value in detail-post after get result: ' + this.comments);

    });

    console.log('Comments value in detail-post: ' + this.comments);
  }

}
