import { Component, OnInit, Input } from '@angular/core';
import { Post } from 'src/app/model/Post';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { Like } from 'src/app/model/Like';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css']
})
export class ArticleComponent implements OnInit {
  like: Like;
  liked = false;
  @Input() post: Post;
  name = 'PhongNV';
  constructor(private postService: PostService) {
    this.like = new Like();
  }

  ngOnInit() {
  }

  likePost() {
    this.liked = !this.liked;
    console.log(this.liked);
    this.like.ObjectId = this.post.id;
    this.like.ObjectType = 'post';
    if (this.liked) {
      this.postService.likeAPost(this.like).subscribe((data: any) => {
        console.log(data);
        this.liked = true;
      });
    } else {
      this.postService.unlikeAPost(this.like).subscribe((data: any) => {
        this.liked = false;
      });
    }
  }
}
