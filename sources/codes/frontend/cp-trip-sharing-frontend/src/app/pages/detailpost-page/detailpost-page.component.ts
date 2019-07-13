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
import { NotifyService } from 'src/app/core/services/notify-service/notify.service';
import { Notification } from 'src/app/model/Notification';
import { NotificationTemplates } from 'src/app/core/globals/NotificationTemplates';
import { HostGlobal } from 'src/app/core/global-variables';

@Component({
  selector: 'app-detailpost-page',
  templateUrl: './detailpost-page.component.html',
  styleUrls: ['./detailpost-page.component.css']
})
export class DetailpostPageComponent implements OnInit {
  post: Post = new Post();
  user: any;
  authorId = '';
  article: Article = new Article();
  like: Like = new Like();
  bookmarkObject: Bookmark = new Bookmark();
  articleId: string;
  token: string;
  bookmark = false;
  follow = false;
  displayName = '';
  profileImage = '';
  commentContent = '';
  comments: Comment[];
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
              private userService: UserService, private titleService: Title,
              public dialog: MatDialog, private notifyService: NotifyService) {
    this.comments = [];
    this.user = JSON.parse(localStorage.getItem('User'));
    this.articleId = this.route.snapshot.paramMap.get('articleId');
    this.token = localStorage.getItem('Token');
    this.loadArticleByarticleId(this.articleId);
  }

  ngOnInit() { }

  loadArticleByarticleId(articleId: string) {
    this.postService.getArticleById(articleId).subscribe((data: any) => {
      this.article = data;
      this.post = data.post;
      this.displayName = data.post.author.displayName;
      this.profileImage = data.post.author.profileImage;
      this.authorId = data.post.author.id;
      if (this.post.coverImage == null) {
        this.post.coverImage = '../../../assets/coverimg.jpg';
      }
      this.listLocation = data.destinations;
      if (this.post.author.profileImage == null) {
        this.post.author.profileImage = '../../../assets/img_avatar.png';
      }
      this.getStates();
      this.getCommentByPostId(this.post.id);
      this.titleService.setTitle(this.post.title);
    });
  }

  getStates(): void {
    this.listUserIdFollowing = JSON.parse(localStorage.getItem('listUserIdFollowing'));
    if (this.listUserIdFollowing != null) {
      this.follow = this.listUserIdFollowing.indexOf(this.post.author.id) !== -1;
    }
    this.listPostIdBookMark = JSON.parse(localStorage.getItem('listPostIdBookmark'));
    if (this.listPostIdBookMark != null) {
      this.bookmark = this.listPostIdBookMark.indexOf(this.post.id) !== -1;
    }
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

  followPerson(authorId: any) {
    if (this.follow === false) {
      this.userService.addFollow(authorId, this.token).subscribe((data: any) => {
        this.follow = true;
        this.listUserIdFollowing.push(authorId);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.userService.unFollow(authorId, this.token).subscribe((data: any) => {
        this.follow = false;
        const unfollow = this.listUserIdFollowing.indexOf(authorId);
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
        // Send notitication
        this.sendLikeNotification();
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
        this.openDialogMessageConfirm('Bạn đã chặn người dùng thành công!', '/danh-sach-chan');
      });
    }
  }

  gotoPersonalPage(authorId: any) {
    window.location.href = '/user/' + authorId;
  }

  editpost() {
    window.location.href = '/chinh-sua-bai-viet/' + this.articleId;
  }

  removePost() {
    this.postService.removeArticle(this.articleId).subscribe((data: any) => {
      this.openDialogMessageConfirm('Bạn đã xóa bài viết thành công!', '');
    }, (err: HttpErrorResponse) => {
      console.log(err);
    });
  }

  openDialogMessageConfirm(message: string, url: string) {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '320px',
      height: 'auto',
      position: {
        top: '20px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = message;
    instance.message.url = '/user/' + this.user.id + url;
  }

  sendLikeNotification() {
    const notification = new Notification();
    notification.content = new NotificationTemplates()
      .getLikePostNotiTemplate(this.user.displayName, this.article.post.title);
    notification.displayImage = this.user.profileImage;
    notification.receivers = [this.article.post.author.id];
    notification.url = HostGlobal.HOST_FRONTEND + '/bai-viet/' + this.article.id;

    this.notifyService.sendNotification(notification);
  }

}
