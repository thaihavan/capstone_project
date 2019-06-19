import { Component, OnInit } from '@angular/core';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { Comment } from 'src/app/model/Comment';
import { Post } from 'src/app/model/Post';
import { ActivatedRoute } from '@angular/router';

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

  constructor(private postService: PostService, private route: ActivatedRoute) {
    this.post = new Post();
  }

  ngOnInit() {
    const postid: string = this.route.snapshot.queryParamMap.get('postId');
    this.loadDetaiPost(postid);
    this.getCommentByPostId(postid);
  }
  loadDetaiPost(postid: string) {
    this.postService.getDetail(postid).subscribe((data: any) => {
      this.post = data;
      console.log(data);
      this.comment.userDisplayName = 'Ha Van Thai';
      this.comment.userImageUrl = 'https://images.pexels.com/photos/414612/pexels-photo-414612.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500';
      this.comment.date = '5 phút trước';
      // tslint:disable-next-line:max-line-length
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

  getCommentByPostId(postId: string) {
    this.postService.getCommentByPost(postId).subscribe((data: any) => {
      if (data != null) {

      } else {

      }
    });
  }
}
