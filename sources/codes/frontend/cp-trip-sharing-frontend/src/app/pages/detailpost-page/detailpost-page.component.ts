import { Component, OnInit, HostListener } from '@angular/core';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { Comment } from 'src/app/model/Comment';
import { Post } from 'src/app/model/Post';
import { ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { Article } from 'src/app/model/Article';
import { Like } from 'src/app/model/Like';
import { Bookmark } from 'src/app/model/Bookmark';

@Component({
  selector: 'app-detailpost-page',
  templateUrl: './detailpost-page.component.html',
  styleUrls: ['./detailpost-page.component.css']
})
export class DetailpostPageComponent implements OnInit {
  post: Post;
  article: Article;
  articleId: string;
  like: Like = new Like();
  bookmarkObject: Bookmark = new Bookmark();
  coverImage = '../../../assets/coverimg.jpg';
  avatar = '../../../assets/img_avatar.png';
  token: string;
  postId: string;
  bookmark = false;
  commentContent = '';
  comments: Comment[];
  follow = false;
  followed = false;
  listPostIdBookMark: string[] = [];
  isScrollTopShow = false;
  topPosToStartShowing = 300;
  @HostListener('window:scroll') checkScroll() {
    const scrollPosition =
      window.pageYOffset ||
      document.documentElement.scrollTop ||
      document.body.scrollTop ||
      0;
    if (scrollPosition >= this.topPosToStartShowing) {
      this.isScrollTopShow = true;
    } else {
      this.isScrollTopShow = false;
    }
  }
  constructor(private postService: PostService, private route: ActivatedRoute, private userService: UserService) {
    this.post = new Post();
    this.comments = [];
    this.articleId = this.route.snapshot.paramMap.get('articleId');
    this.loadArticleByarticleId(this.articleId);
    this.token = localStorage.getItem('Token');
  }

  ngOnInit() { }

  checkBookMark(postId: any) {
    this.listPostIdBookMark = JSON.parse(localStorage.getItem('listPostIdBookmark'));
    if (this.listPostIdBookMark != null) {
      // tslint:disable-next-line:prefer-for-of
      for (let i = 0; i < this.listPostIdBookMark.length; i++) {
        if (postId === this.listPostIdBookMark[i]) {
          this.bookmark = true;
          break;
        }
      }
    }
  }
  loadArticleByarticleId(articleId: string) {
    this.postService.getArticleById(articleId).subscribe((data: any) => {
      this.article = data;
      this.post = data.post;
      console.log(this.article);
      this.getCommentByPostId(this.post.id);
      this.checkBookMark(this.post.id);
      // this.listUserIdFollowing = JSON.parse(localStorage.getItem('listUserIdFollowing'));
      // if (this.listUserIdFollowing != null) {
      //   // tslint:disable-next-line:prefer-for-of
      //   for (let i = 0; i < this.listUserIdFollowing.length; i++) {
      //     if (data.authorId === this.listUserIdFollowing[i]) {
      //       this.followed = true;
      //       this.follow = false;
      //       break;
      //     } else {
      //       this.followed = false;
      //       this.follow = true;
      //     }
      //   }
      // }
    });
  }

  getCommentByPostId(postId: string) {
    this.postService.getCommentByPost(postId, this.token).subscribe((data: any) => {
      if (data != null) {
        console.log('Comment: ' + data);
        console.log('Total comment: ', data.length);
        this.comments = data;
      } else {
        console.log('Can not get comments of this post.');
      }
    });
  }

  submitComment() {
    const comment = new Comment();
    comment.content = this.commentContent;
    comment.postId = this.post.id;
    comment.parentId = null;

    this.postService.addComment(comment).subscribe((res: Comment) => {
      console.log('add comment res: ' + res);
      this.comments.push(res);
    }, (error: HttpErrorResponse) => {
      console.log(error);
    });
  }

  // followPerson(userId: any) {
  //   if (this.followed === false && this.follow === true) {
  //     this.userService.addFollow(userId, this.token).subscribe((data: any) => {
  //       this.followed = true;
  //       this.follow = false;
  //       this.listUserIdFollowing.push(userId);
  //       localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
  //     }, (err: HttpErrorResponse) => {
  //       console.log(err);
  //     });
  //   } else {
  //     this.userService.unFollow(userId, this.token).subscribe((data: any) => {
  //       this.followed = false;
  //       this.follow = true;
  //       const unfollow = this.listUserIdFollowing.indexOf(userId);
  //       this.listUserIdFollowing.splice(unfollow, 1);
  //       localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
  //     }, (err: HttpErrorResponse) => {
  //       console.log(err);
  //     });
  //   }
  // }

  likePost(like: any) {
    this.like.ObjectId = this.article.post.id;
    this.like.ObjectType = 'post';
    if (like === false) {
      this.postService.likeAPost(this.like).subscribe((data: any) => {
        this.article.post.liked = true;
        this.article.post.likeCount += 1;
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.postService.unlikeAPost(this.like).subscribe((data: any) => {
        this.article.post.liked = false;
        this.article.post.likeCount -= 1;
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }

  bookmarkPost(postId: any, title: any, imgCover: any, postType: any) {
    if (this.bookmark === false) {
      this.bookmarkObject.CoverImage = imgCover;
      this.bookmarkObject.PostId = postId;
      this.bookmarkObject.PostType = postType;
      this.bookmarkObject.Title = title;
      this.userService.addBookMark(this.bookmarkObject, this.token).subscribe((data: any) => {
        this.bookmark = true;
        this.listPostIdBookMark.push(postId);
        localStorage.setItem('listPostIdBookmark', JSON.stringify(this.listPostIdBookMark));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.userService.deleteBookMark(postId, this.token).subscribe((data: any) => {
        this.bookmark = false;
        const unbookmark = this.listPostIdBookMark.indexOf(postId);
        this.listPostIdBookMark.splice(unbookmark, 1);
        localStorage.setItem('listPostIdBookmark', JSON.stringify(this.listPostIdBookMark));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }

  commentPost(el: HTMLElement) {
    el.scrollIntoView();
  }
}
