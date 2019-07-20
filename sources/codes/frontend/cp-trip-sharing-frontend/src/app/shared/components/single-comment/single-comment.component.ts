import { Component, OnInit, Input } from '@angular/core';
import { Comment } from 'src/app/model/Comment';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Like } from 'src/app/model/Like';
import { NotifyService } from 'src/app/core/services/notify-service/notify.service';
import { Notification } from 'src/app/model/Notification';
import { NotificationTemplates } from 'src/app/core/globals/NotificationTemplates';
import { HostGlobal } from 'src/app/core/global-variables';
import { Post } from 'src/app/model/Post';
import { MatDialog } from '@angular/material';
import { ReportPopupComponent } from '../report-popup/report-popup.component';
import { User } from 'src/app/model/User';

@Component({
  selector: 'app-single-comment',
  templateUrl: './single-comment.component.html',
  styleUrls: ['./single-comment.component.css']
})
export class SingleCommentComponent implements OnInit {
  @Input() comment: Comment;
  @Input() post: any;

  user: User;
  editComments = true;
  commentContent = '';
  liked = false;
  showRep = false;
  like: Like;
  checkRemoveComment = false;
  ngOnInit(): void {
  }

  constructor(private postService: PostService, private notifyService: NotifyService, private dialog: MatDialog) {
    this.like = new Like();
    this.user = JSON.parse(localStorage.getItem('User'));
  }

  showRepComment() {
    this.showRep = !this.showRep;
  }

  submitComment() {
    console.log('content: ' + this.commentContent);

    this.showRep = false;

    const comment = new Comment();
    comment.content = this.commentContent;
    comment.postId = this.comment.postId;
    comment.parentId = this.comment.id;
    this.postService.addComment(comment).subscribe((res: Comment) => {
      this.comment.childs.push(res);

      // Send notification
      this.sendCommentNotification();
    }, (error: HttpErrorResponse) => {
      console.log(error);
    });
    this.commentContent = '';
  }

  likeComment(liked: any, commetId: any) {
    this.like.objectId = commetId;
    this.like.objectType = 'comment';
    if (liked === false) {
      this.postService.likeAPost(this.like).subscribe((data: any) => {
        this.comment.likeCount += 1;
        this.comment.liked = true;

        // Send notification
        this.sendLikeCommentNotification();
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.postService.unlikeAPost(this.like).subscribe((data: any) => {
        this.comment.likeCount -= 1;
        this.comment.liked = false;
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }

  sendCommentNotification() {
    const notification = new Notification();
    notification.content = new NotificationTemplates()
      .getCommentedNotiTemplate(this.user.displayName, this.post.post.title);
    notification.displayImage = this.user.avatar;
    notification.receivers = [this.post.post.author.id];
    notification.url = HostGlobal.HOST_FRONTEND + '/bai-viet/' + this.comment.postId;

    this.notifyService.sendNotification(notification);
  }

  sendLikeCommentNotification() {
    const notification = new Notification();
    notification.content = new NotificationTemplates()
      .getLikeCommentNotiTemplate(this.user.displayName, this.post.post.title);
    notification.displayImage = this.user.avatar;
    notification.receivers = [this.post.post.author.id];
    notification.url = HostGlobal.HOST_FRONTEND + '/bai-viet/' + this.post.id;

    this.notifyService.sendNotification(notification);
  }

  reportComment() {
    this.openDialogMessageConfirm('Báo cáo bình luận');
  }

  openDialogMessageConfirm(title: string) {
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
  }

  editComment() {
    this.editComments = false;
  }

  cancelEdit() {
    this.editComments = true;
  }

  updateComment() {
    this.editComments = true;
    this.postService.updateComment(this.comment).subscribe((result: any) => {
      this.editComments = true;
    }, (err: HttpErrorResponse) => {
      console.log(err);
    });
  }

  removeComment() {
    this.postService.removeComment(this.comment.id, this.comment.authorId).subscribe((result: any) => {
      this.checkRemoveComment = true;
    });
  }
}
