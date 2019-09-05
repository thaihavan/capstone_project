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
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { MessagePopupComponent } from '../message-popup/message-popup.component';

@Component({
  selector: 'app-single-comment',
  templateUrl: './single-comment.component.html',
  styleUrls: ['./single-comment.component.css']
})
export class SingleCommentComponent implements OnInit {
  @Input() comment: Comment;
  @Input() post: any;

  user: User;
  editComments = false;
  commentContent = '';
  liked = false;
  showRep = false;
  isLikeWaitingRespone = false;
  like: Like;
  checkRemoveComment = false;
  ngOnInit(): void {
  }

  constructor(private postService: PostService,
              private notifyService: NotifyService,
              private dialog: MatDialog,
              private errorHandler: GlobalErrorHandler) {
    this.like = new Like();
    this.user = JSON.parse(localStorage.getItem('User'));
  }

  showRepComment() {
    this.showRep = !this.showRep;
  }

  submitComment() {
    const comment = new Comment();
    comment.content = this.commentContent;
    comment.postId = this.comment.postId;
    comment.parentId = this.comment.id;
    if (comment.content.trim() !== '') {
    this.postService.addComment(comment).subscribe((res: Comment) => {
      this.comment.childs.unshift(res);

      // Send notification
      this.sendCommentNotification();
    }, this.errorHandler.handleError);
    this.commentContent = '';
    this.showRep = false;
   }
  }

  likeComment(liked: any, commetId: any) {
    if (this.isLikeWaitingRespone) {
      return;
    }
    this.isLikeWaitingRespone = true;
    this.like.objectId = commetId;
    this.like.objectType = 'comment';
    if (liked === false) {
      this.postService.likeAPost(this.like).subscribe((data: any) => {
        this.comment.likeCount += 1;
        this.comment.liked = true;
        // Send notification
        this.sendLikeCommentNotification();
      }, (error) => {
        this.isLikeWaitingRespone = false;
      },
      () => {
        this.isLikeWaitingRespone = false;
      });
    } else {
      this.postService.unlikeAPost(this.like).subscribe((data: any) => {
        this.comment.likeCount -= 1;
        this.comment.liked = false;
      }, (error) => {
        this.isLikeWaitingRespone = false;
      },
      () => {
        this.isLikeWaitingRespone = false;
      });
    }
  }

  sendCommentNotification() {
    const notification = new Notification();
    notification.content = new NotificationTemplates()
      .getCommentedNotiTemplate(this.user.displayName, this.post.post.title);
    notification.displayImage = this.user.avatar;
    notification.receivers = [this.post.post.author.id];
    if (this.post.post.postType === 'Article') {
      notification.url = HostGlobal.HOST_FRONTEND + '/bai-viet/' + this.comment.postId;
    } else {
      notification.url = HostGlobal.HOST_FRONTEND + '/tim-ban-dong-hanh/' + this.comment.postId;
    }
    // notification.url = HostGlobal.HOST_FRONTEND + '/bai-viet/' + this.comment.postId;
    notification.seenIds = [this.user.id];
    this.notifyService.sendNotification(notification);
  }

  sendLikeCommentNotification() {
    const notification = new Notification();
    notification.content = new NotificationTemplates()
      .getLikeCommentNotiTemplate(this.user.displayName, this.post.post.title);
    notification.displayImage = this.user.avatar;
    notification.receivers = [this.post.post.author.id];
    if (this.post.post.postType === 'Article') {
      notification.url = HostGlobal.HOST_FRONTEND + '/bai-viet/' + this.post.id;
    } else {
      notification.url = HostGlobal.HOST_FRONTEND + '/tim-ban-dong-hanh/' + this.post.id;
    }
    // notification.url = HostGlobal.HOST_FRONTEND + '/bai-viet/' + this.post.id;
    notification.seenIds = [this.user.id];
    this.notifyService.sendNotification(notification);
  }

  reportComment() {
    this.openDialogMessageConfirm('Báo cáo bình luận', this.comment.id);
  }

  openDialogMessageConfirm(title: string, commentId: string) {
    const dialogRef = this.dialog.open(ReportPopupComponent, {
      width: '500px',
      height: 'auto',
      position: {
        top: '10px'
      },
      disableClose: false
    });
    const instance = dialogRef.componentInstance;
    instance.title = title;
    instance.targetId = commentId;
    instance.type = 'comment';
  }

  editComment() {
    this.editComments = true;
  }

  cancelEdit() {
    this.editComments = false;
  }

  cancelRepComment() {
    this.showRep = false;
  }

  updateComment(txtcomment: string) {
    if (txtcomment.trim() !== '') {
      this.comment.content = txtcomment;
      this.postService.updateComment(this.comment).subscribe((result: any) => {
      this.editComments = false;
    }, this.errorHandler.handleError);
   }
  }

  removeComment() {
      const dialogRef = this.dialog.open(MessagePopupComponent, {
        width: '500px',
        height: 'auto',
        position: {
          top: '20px'
        },
        disableClose: true
      });
      const instance = dialogRef.componentInstance;
      instance.message.messageText = `Bạn có chắc chắn muốn xóa bình luận này không?`;
      instance.message.messageType = 'confirm';

      dialogRef.afterClosed().subscribe((res: string) => {
        if (res === 'continue') {
          this.postService.removeComment(this.comment.id, this.comment.authorId).subscribe((result: any) => {
            this.checkRemoveComment = true;
          }, this.errorHandler.handleError);
        }
      });
    }
}
