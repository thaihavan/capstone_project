import { Component, OnInit, HostListener } from '@angular/core';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { Comment } from 'src/app/model/Comment';
import { Post } from 'src/app/model/Post';
import { ActivatedRoute } from '@angular/router';
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

  bookmark = false;
  follow = false;

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
  constructor(private postService: PostService, private route: ActivatedRoute,
              private userService: UserService, private titleService: Title,
              public dialog: MatDialog, private notifyService: NotifyService,
              private postCopmanionService: FindingCompanionService,
              private errorHandler: GlobalErrorHandler) {
    this.comments = [];
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
  }

  ngOnInit() {
    this.user = JSON.parse(localStorage.getItem('User'));
   }

  // get article post
  loadArticlePostById(articleId: string) {
    this.postService.getArticleById(articleId).subscribe((data: any) => {
      this.detailPost = data;
      this.post = data.post;
      this.displayName = data.post.author.displayName;
      this.profileImage = data.post.author.profileImage;
      this.authorId = data.post.author.id;
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
    },
    this.errorHandler.handleError);
  }

  // get companionpost
  loadCompanionPostById(articleId: string) {
    this.token = localStorage.getItem('Token');
    this.postCopmanionService.getPost(articleId).subscribe(
      (data: any) => {
        this.detailPost = data;
        this.post = data.post;
        this.displayName = data.post.author.displayName;
        this.profileImage = data.post.author.profileImage;
        this.listLocation = data.destinations;
        this.authorId = this.post.author.id;
        if (this.profileImage == null) {
          this.profileImage = '../../../assets/img_avatar.png';
        }
        this.displayName = this.post.author.displayName;
        this.getCommentByPostId(this.post.id);
        this.getStates();
        this.titleService.setTitle(this.post.title);
      },
      this.errorHandler.handleError,
      () => {
        this.typePost = 'companion';
      }
    );
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
      // Send notification
      this.notifyService.sendCommentNotification(this.user, this.detailPost);
    }, this.errorHandler.handleError);
  }

  followPerson(authorId: any) {
    if (this.follow === false) {
      this.userService.addFollow(authorId, this.token).subscribe((data: any) => {
        this.follow = true;
        this.listUserIdFollowing.push(authorId);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, this.errorHandler.handleError);
    } else {
      this.userService.unFollow(authorId, this.token).subscribe((data: any) => {
        this.follow = false;
        const unfollow = this.listUserIdFollowing.indexOf(authorId);
        this.listUserIdFollowing.splice(unfollow, 1);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, this.errorHandler.handleError);
    }
  }

  likePost(like: any) {
    this.like.objectId = this.detailPost.post.id;
    this.like.objectType = 'post';
    if (like === false) {
      this.postService.likeAPost(this.like).subscribe((data: any) => {
        this.detailPost.post.liked = true;
        this.detailPost.post.likeCount += 1;
        // Send notitication
        this.notifyService.sendLikeNotification(this.user, this.detailPost);
      }, this.errorHandler.handleError);
    } else {
      this.postService.unlikeAPost(this.like).subscribe((data: any) => {
        this.detailPost.post.liked = false;
        this.detailPost.post.likeCount -= 1;
      }, this.errorHandler.handleError);
    }
  }

  bookmarkPost(postId: any, title: any, imgCover: any, postType: any) {
    if (this.bookmark === false) {
      this.bookmarkObject.coverImage = imgCover;
      this.bookmarkObject.postId = postId;
      this.bookmarkObject.postType = postType;
      this.bookmarkObject.title = title;
      this.userService.addBookMark(this.bookmarkObject, this.token).subscribe((data: any) => {
        this.bookmark = true;
        this.listPostIdBookMark.push(postId);
        localStorage.setItem('listPostIdBookmark', JSON.stringify(this.listPostIdBookMark));
      }, this.errorHandler.handleError);
    } else {
      this.userService.deleteBookMark(postId, this.token).subscribe((data: any) => {
        this.bookmark = false;
        const unbookmark = this.listPostIdBookMark.indexOf(postId);
        this.listPostIdBookMark.splice(unbookmark, 1);
        localStorage.setItem('listPostIdBookmark', JSON.stringify(this.listPostIdBookMark));
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
    this.openDialogReportPost('Báo cáo vi phạm', postId);
  }

  gotoPersonalPage(authorId: any) {
    window.location.href = '/user/' + authorId;
  }

  editpost() {
    window.location.href = '/chinh-sua-bai-viet/' + this.postId;
  }

  removePost() {
    this.postService.removeArticle(this.postId).subscribe((data: any) => {
      this.openDialogMessageConfirm('Bạn đã xóa bài viết thành công!', '', 'success');
    }, this.errorHandler.handleError);
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
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = message;
    instance.message.messageType = messageType;
    instance.message.url = '/user/' + this.user.id + url;
  }

}
