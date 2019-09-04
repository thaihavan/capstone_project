import { Component, OnInit, HostListener, ViewChild } from '@angular/core';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { Comment } from 'src/app/model/Comment';
import { Post } from 'src/app/model/Post';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { Like } from 'src/app/model/Like';
import { Bookmark } from 'src/app/model/Bookmark';
import { Title } from '@angular/platform-browser';
import { MatDialog } from '@angular/material';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';
import { NotifyService } from 'src/app/core/services/notify-service/notify.service';
import { User } from 'src/app/model/User';
import { FindingCompanionService } from 'src/app/core/services/post-service/finding-companion.service';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { ReportPopupComponent } from 'src/app/shared/components/report-popup/report-popup.component';
import { LoginPageComponent } from 'src/app/shared/components/login-page/login-page.component';
import { AlertifyService } from 'src/app/core/services/alertify-service/alertify.service';
import { CommentContainerComponent } from 'src/app/shared/components/comment-container/comment-container.component';

@Component({
  selector: 'app-detailpost-page',
  templateUrl: './detailpost-page.component.html',
  styleUrls: ['./detailpost-page.component.css']
})
export class DetailpostPageComponent implements OnInit {
  post: Post = new Post();
  user = new User();
  like: Like = new Like();
  bookmarkObject: Bookmark = new Bookmark();

  detailPost: any;
  postId: string;
  companionPostId: string;
  token: string;
  contributionPoint: string;

  bookmark = false;
  follow = false;
  isLoading = true;
  isNotFound = false;
  isLikeWaitingRespone = false;
  isShowMoreComment = false;

  typePost = '';
  authorId = '';
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
  constructor(
    private postService: PostService,
    private route: ActivatedRoute,
    private userService: UserService,
    private titleService: Title,
    public dialog: MatDialog,
    private notifyService: NotifyService,
    private postCopmanionService: FindingCompanionService,
    private errorHandler: GlobalErrorHandler,
    private alertify: AlertifyService,
    private router: Router
  ) {
    this.comments = [];
  }

  ngOnInit() {
    const articlePostId = this.route.snapshot.paramMap.get('articleId');
    const companionPostId = this.route.snapshot.paramMap.get('companionId');
    if (articlePostId !== null) {
      this.typePost = 'article';
      this.postId = articlePostId;
      this.loadArticlePostById(this.postId);
    } else {
      this.postId = companionPostId;
      this.loadCompanionPostById(this.postId);
    }
    this.token = localStorage.getItem('Token');
    this.user = JSON.parse(localStorage.getItem('User'));
  }

  // get article post
  loadArticlePostById(articleId: string) {
    this.postService.getArticleById(articleId).subscribe((data: any) => {
      if (data === null) {
        this.isNotFound = true;
      } else {
        this.detailPost = data;
        this.post = data.post;
        this.displayName = data.post.author.displayName;
        this.profileImage = data.post.author.profileImage;
        this.authorId = data.post.author.id;
        this.getContributionPoint(this.authorId);
        if (this.post.coverImage == null) {
          this.post.coverImage = '../../../assets/coverimg.jpg';
        }
        this.listLocation = data.destinations;
        if (this.profileImage == null) {
          this.profileImage = '../../../assets/img_avatar.png';
        }
        this.getStates();
        this.getCommentByPostId(this.post.id);
        this.titleService.setTitle(this.post.title);
      }
    }, this.errorHandler.handleError,
    () => {
      this.isLoading = false;
    });
  }

  // get companionpost
  loadCompanionPostById(articleId: string) {
    this.token = localStorage.getItem('Token');
    this.postCopmanionService.getPost(articleId).subscribe(
      (data: any) => {
        if (data === null) {
          this.isNotFound = true;
        } else {
          this.detailPost = data;
          this.post = data.post;
          this.displayName = data.post.author.displayName;
          this.profileImage = data.post.author.profileImage;
          this.listLocation = data.destinations;
          this.authorId = this.post.author.id;
          this.getContributionPoint(this.authorId);
          if (this.profileImage == null) {
            this.profileImage = '../../../assets/img_avatar.png';
          }
          this.displayName = this.post.author.displayName;
          this.getCommentByPostId(this.post.id);
          this.getStates();
          this.titleService.setTitle(this.post.title);
        }
      },
      (error) => {
        this.isNotFound = true;
        this.isLoading = false;
      },
      () => {
        this.typePost = 'companion';
        this.isLoading = false;
      }
    );
  }

  getStates(): void {
    this.listUserIdFollowing = JSON.parse(
      localStorage.getItem('listUserIdFollowing')
    );
    if (this.listUserIdFollowing != null) {
      this.follow =
        this.listUserIdFollowing.indexOf(this.post.author.id) !== -1;
    }
    this.listPostIdBookMark = JSON.parse(
      localStorage.getItem('listPostIdBookmark')
    );
    if (this.listPostIdBookMark != null) {
      this.bookmark = this.listPostIdBookMark.indexOf(this.post.id) !== -1;
    }
  }

  // get author contribution point
  getContributionPoint(id) {
    this.userService.getContributionPoint(id).subscribe(
      res => {
        this.contributionPoint = res.contributionPoint;
      },
      error => {

      },
      () => {}
    );
  }

  getCommentByPostId(postId: string) {
    this.postService
      .getCommentByPost(postId, this.token)
      .subscribe((data: any) => {
        if (data != null) {
          this.comments = data;
          if (this.comments.length >= 3) {
            this.isShowMoreComment = true;
          }
        }
      });
  }

  submitComment() {
    const comment = new Comment();
    comment.content = this.commentContent;
    comment.postId = this.post.id;
    comment.parentId = null;
    if (comment.content.trim() !== '') {
    this.postService.addComment(comment).subscribe((res: Comment) => {
      this.commentContent = '';
      this.comments.push(res);
      // Send notification
      this.notifyService.sendCommentNotification(this.user, this.detailPost);
    }, this.errorHandler.handleError);
  }
}

  followPerson(authorId: any) {
    if (this.follow === false) {
      this.userService
        .addFollow(authorId, this.token)
        .subscribe((data: any) => {
          this.follow = true;
          this.listUserIdFollowing.push(authorId);
          localStorage.setItem(
            'listUserIdFollowing',
            JSON.stringify(this.listUserIdFollowing)
          );
        }, this.errorHandler.handleError);
    } else {
      this.userService.unFollow(authorId, this.token).subscribe((data: any) => {
        this.follow = false;
        const unfollow = this.listUserIdFollowing.indexOf(authorId);
        this.listUserIdFollowing.splice(unfollow, 1);
        localStorage.setItem(
          'listUserIdFollowing',
          JSON.stringify(this.listUserIdFollowing)
        );
      }, this.errorHandler.handleError);
    }
  }

  likePost(like: any) {
    if (this.isLikeWaitingRespone) {
      return;
    }
    if (this.user === null) {
      this.openDialogLoginForm();
      return;
    }
    this.isLikeWaitingRespone = true;
    this.like.objectId = this.detailPost.post.id;
    this.like.objectType = 'post';
    if (like === false) {
      this.postService.likeAPost(this.like).subscribe((data: any) => {
        this.detailPost.post.liked = true;
        this.detailPost.post.likeCount += 1;
        // Send notitication
        this.notifyService.sendLikeNotification(this.user, this.detailPost);
      }, this.errorHandler.handleError,
      () => {
        this.isLikeWaitingRespone = false;
      });
    } else {
      this.postService.unlikeAPost(this.like).subscribe((data: any) => {
        this.detailPost.post.liked = false;
        this.detailPost.post.likeCount -= 1;
      }, this.errorHandler.handleError,
      () => {
        this.isLikeWaitingRespone = false;
      });
    }
  }

  bookmarkPost(postId: string) {
    if (this.user === null) {
      this.openDialogLoginForm();
      return;
    }
    if (this.bookmark === false) {
      this.bookmarkObject.postId = postId;
      this.userService
        .addBookMark(this.bookmarkObject, this.token)
        .subscribe((data: any) => {
          this.bookmark = true;
          this.listPostIdBookMark.push(postId);
          localStorage.setItem(
            'listPostIdBookmark',
            JSON.stringify(this.listPostIdBookMark)
          );
        }, this.errorHandler.handleError);
    } else {
      this.userService
        .deleteBookMark(postId, this.token)
        .subscribe((data: any) => {
          this.bookmark = false;
          const unbookmark = this.listPostIdBookMark.indexOf(postId);
          this.listPostIdBookMark.splice(unbookmark, 1);
          localStorage.setItem(
            'listPostIdBookmark',
            JSON.stringify(this.listPostIdBookMark)
          );
        }, this.errorHandler.handleError);
    }
  }

  commentPost(el: HTMLElement) {
    el.scrollIntoView();
  }

  gotoTopPage(el: HTMLElement) {
    el.scrollIntoView();
  }

  reportPost(postId: string) {
    if (this.user === null) {
      this.openDialogLoginForm();
      return;
    }
    this.openDialogReportPost('Báo cáo vi phạm', postId);
  }

  gotoPersonalPage(authorId: any) {
    this.router.navigate(['/user', authorId]);
    // window.location.href = '/user/' + authorId;
  }

  editpost() {
    if (this.typePost === 'article') {
      this.router.navigate(['/chinh-sua-bai-viet', this.postId]);
      // window.location.href = '/chinh-sua-bai-viet/' + this.postId;
    } else {
      this.router.navigate(['/chinh-sua-tim-ban-dong-hanh', this.postId]);
      // window.location.href = '/chinh-sua-tim-ban-dong-hanh/' + this.postId;
    }
  }

  removePost() {
    if (this.typePost === 'article') {
      this.postService.removeArticle(this.postId).subscribe(respone => { },
        (error) => {
          this.alertify.error('Lỗi xóa bài viết');
        },
        () => {
          // this.alertify.success('Xóa bài viết thành công');
          this.openDialogMessageConfirm('Bài viết đã được xóa!', '/bai-viet', 'success');
        }
        );
    } else {
      this.postCopmanionService.deletePost(this.postId).subscribe(respone => { },
        (error) => {
          this.alertify.error('Lỗi xóa bài viết');
        },
        () => {
          // this.alertify.success('Xóa bài viết thành công');
          this.openDialogMessageConfirm('Bài viết đã được xóa!', '/tim-ban-dong-hanh', 'success');
        }
      );
    }
  }

  openDialogReportPost(title: string, postId: string) {
    const dialogRef = this.dialog.open(ReportPopupComponent, {
      width: '400px',
      height: 'auto',
      position: {
        top: '10px'
      },
      disableClose: false
    });
    const instance = dialogRef.componentInstance;
    instance.title = title;
    instance.targetId = postId;
    instance.type = 'post';
  }

  openDialogMessageConfirm(message: string, url: string, messageType: string) {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '500px',
      height: 'auto',
      position: {
        top: '20px'
      },
      disableClose: messageType === 'success' ? true : false
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = message;
    instance.message.messageType = messageType;
    if (messageType === 'success') {
      instance.message.url = '/user/' + this.user.id + url;
    }
    dialogRef.afterClosed().subscribe(res => {
      if (res === 'continue') {
        this.removePost();
      }
    });
  }

  openDialogLoginForm() {
    if (this.user === null) {
      const dialogRef = this.dialog.open(LoginPageComponent, {
        height: 'auto',
        width: '400px'
      });
    }
  }
  showMoreComment() {
    this.isShowMoreComment = !this.isShowMoreComment;
  }
}
