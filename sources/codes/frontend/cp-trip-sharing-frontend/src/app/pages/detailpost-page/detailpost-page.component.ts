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
import { Title } from '@angular/platform-browser';
import { MatDialog } from '@angular/material';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';

@Component({
  selector: 'app-detailpost-page',
  templateUrl: './detailpost-page.component.html',
  styleUrls: ['./detailpost-page.component.css']
})
export class DetailpostPageComponent implements OnInit {
  post: Post = new Post();
  displayName = '';
  coverImg = '';
  article: Article;
  articleId: string;
  like: Like = new Like();
  bookmarkObject: Bookmark = new Bookmark();
  coverImage = '../../../assets/coverimg.jpg';
  avatar = '../../../assets/img_avatar.png';
  token: string;
  authorId: string;
  bookmark = false;
  commentContent = '';
  comments: Comment[];
  follow = false;
  followed = false;
  listPostIdBookMark: string[] = [];
  listUserIdFollowing: string[] = [];
  listLocation: any[] = [];
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
  constructor(private postService: PostService, private route: ActivatedRoute,
    private userService: UserService, private titleService: Title, public dialog: MatDialog) {
    this.comments = [];
    this.articleId = this.route.snapshot.paramMap.get('articleId');
    this.loadArticleByarticleId(this.articleId);
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
    this.token = localStorage.getItem('Token');
    this.postService.getArticleById(articleId).subscribe((data: any) => {
      this.article = data;
      this.post = data.post;
      if (this.post.coverImage != null) {
        this.coverImage = this.post.coverImage;
      }
      this.listLocation = data.destinations;
      this.authorId = this.post.author.id;
      if (this.post.author.profileImage != null) {
        this.coverImg = this.post.author.profileImage;
      }
      this.displayName = this.post.author.displayName;
      console.log(this.article);
      this.getCommentByPostId(this.post.id);
      this.checkBookMark(this.post.id);
      const user = JSON.parse(localStorage.getItem('User'));
      if (user.id === this.authorId) {
        this.followed = false;
        this.follow = false;
      } else {
        this.listUserIdFollowing = JSON.parse(localStorage.getItem('listUserIdFollowing'));
        if (this.listUserIdFollowing != null) {
          // tslint:disable-next-line:prefer-for-of
          for (let i = 0; i < this.listUserIdFollowing.length; i++) {
            if (this.authorId === this.listUserIdFollowing[i]) {
              this.followed = true;
              this.follow = false;
              break;
            } else {
              this.followed = false;
              this.follow = true;
            }
          }
        }
      }
      this.titleService.setTitle(this.post.title);
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

  followPerson(userId: any) {
    if (this.followed === false && this.follow === true) {
      this.userService.addFollow(userId, this.token).subscribe((data: any) => {
        this.followed = true;
        this.follow = false;
        this.listUserIdFollowing.push(userId);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.userService.unFollow(userId, this.token).subscribe((data: any) => {
        this.followed = false;
        this.follow = true;
        const unfollow = this.listUserIdFollowing.indexOf(userId);
        this.listUserIdFollowing.splice(unfollow, 1);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }

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

  gotoTopPage(el: HTMLElement) {
    el.scrollIntoView();
  }

  blockUserById(userId: any) {
    const token = localStorage.getItem('Token');
    if (token != null) {
      this.userService.addBlock(userId, token).subscribe((result: any) => {
        this.openDialogMessageConfirm();
      });
    }
  }

  openDialogMessageConfirm() {
    const user = JSON.parse(localStorage.getItem('User'));
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '380px',
      height: '200px',
      position: {
        top: '10px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = 'Chặn người dùng thành công!';
    instance.message.url = '/user/' + user.id + '/danh-sach-chan';
  }

  getShortDescription(htmlContent: any) {
    // Convert html string to DOM object
    const div = document.createElement('div');
    div.innerHTML = htmlContent;

    const pTags = div.getElementsByTagName('p');
    let pContent = '';
    for (let i = 0; i < pTags.length; i++) {
      pContent += pTags.item(i).innerText + ' ';

      if (pContent.length > 250) {
        break;
      }
    }

    if (pContent.length > 250) {
      pContent = pContent.substr(0, 250) + '...';
    }

    return pContent;
  }

  gotoPersonalPage(authorId: any) {
    window.location.href = '/user/' + authorId;
  }
}
