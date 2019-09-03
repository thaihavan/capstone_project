import { Component, OnInit, Input } from '@angular/core';
import { Post } from 'src/app/model/Post';
import { CompanionPost } from 'src/app/model/CompanionPost';
import { CompanionPostRequest } from 'src/app/model/CompanionPostRequest';
import { ChatService } from 'src/app/core/services/chat-service/chat.service';
import { ChatUser } from 'src/app/model/ChatUser';
import { AlertifyService } from 'src/app/core/services/alertify-service/alertify.service';
import { FindingCompanionService } from 'src/app/core/services/post-service/finding-companion.service';
import { MatDialog } from '@angular/material';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { LoginPageComponent } from '../login-page/login-page.component';
import { NotifyService } from 'src/app/core/services/notify-service/notify.service';
import { User } from 'src/app/model/User';
import { Router } from '@angular/router';

@Component({
  selector: 'app-detail-companion-post',
  templateUrl: './detail-companion-post.component.html',
  styleUrls: ['./detail-companion-post.component.css']
})
export class DetailCompanionPostComponent implements OnInit {
  post: Post = new Post();
  @Input() companionPost = new CompanionPost();
  statustRequest: StatustRequest = new StatustRequest();
  companionPostRequest: CompanionPostRequest;
  companionPostId: string;
  userListRequests: CompanionPostRequest[] = [];
  userListGroup: ChatUser[] = [];
  isAuthorPost = false;
  user: User;

  constructor(
    private chatService: ChatService,
    private alertify: AlertifyService,
    private postService: FindingCompanionService,
    private dialog: MatDialog,
    private errorHandler: GlobalErrorHandler,
    private notifyService: NotifyService,
    private router: Router
  ) {
    this.user = JSON.parse(localStorage.getItem('User'));
  }

  ngOnInit() {
    this.checkAuthorPost();
    if (this.isAuthorPost) {
      this.getAllRequests();
    }
    this.geGroupChatMembers();
  }

  // check author
  checkAuthorPost() {
    if (this.user === null) {
      return;
    }
    if (this.user.id === this.companionPost.post.author.id) {
      this.isAuthorPost = true;
    }
  }

  // for author post check if has user request
  checkHasRequest() {
    if (this.userListRequests.length === 0) {
      return false;
    }
    return true;
  }

  // for author check request from user accecpt or delete
  getAllRequests() {
    const currDate = new Date();
    const fromDate = new Date(this.companionPost.from);
    if (fromDate.getTime() > currDate.getTime()) {
      this.postService.getAllRequests(this.companionPost.id).subscribe(res => {
        this.userListRequests = res;
      });
    }
  }

  // get member in group chat
  geGroupChatMembers() {
    this.chatService.getMembers(this.companionPost.conversationId).subscribe(
      res => {
        this.userListGroup = res;
      },
      this.errorHandler.handleError,
      () => {
        const user = JSON.parse(localStorage.getItem('User'));
        if (user === null) {
          this.statustRequest.IsRequestJoin();
          // tslint:disable-next-line:no-shadowed-variable
          const currDate = new Date();
          // tslint:disable-next-line:no-shadowed-variable
          const fromDate = new Date(this.companionPost.from);
          if (fromDate.getTime() < currDate.getTime()) {
            this.statustRequest.IsExpired();
          }
          return;
        }
        const isJoined = this.userListGroup.find(u => u.id === user.id);
        if (this.isAuthorPost || isJoined) {
          this.statustRequest.IsJoined();
        } else {
          this.statustRequest.IsRequestJoin();
        }
        if (this.companionPost.requested) {
          this.statustRequest.IsWaiting();
        }
        const currDate = new Date();
        const fromDate = new Date(this.companionPost.from);
        if (fromDate.getTime() < currDate.getTime()) {
          this.statustRequest.IsExpired();
        }
      }
    );
  }

  // form author access a request join to group
  // tslint:disable-next-line:variable-name
  accessRequestJoin(user_id, index) {
    this.deleteRequest(index, true);
    const user = {
      conversationId: this.companionPost.conversationId,
      userId: user_id
    };
    this.chatService.addUser(user).subscribe(
      res => {
        this.userListGroup.push(res);
      },
      err => {
        this.alertify.error('Thêm thành viên lỗi');
      },
      () => {
        this.alertify.success('Đã thêm mới một thành viên');
        // Send notification
        this.notifyService.sendJoinRequestAcceptedNotification(
          this.user,
          this.companionPost,
          user_id
        );
      }
    );
  }

  // for author delete request soecify member
  deleteRequest(index, join) {
    this.postService
      .deleteRequest(this.userListRequests[index])
      .subscribe(res => {}, this.errorHandler.handleError, () => {
        if (!join) {
          this.alertify.success('Đã xoá yêu cầu');
        }
        this.userListRequests.splice(index, 1);
      });
  }

  // for member send request to join group companion
  sendRequest() {
    if (this.statustRequest.type === 'request') {
      this.companionPostRequest = new CompanionPostRequest();
      const user = JSON.parse(localStorage.getItem('User'));
      if (user === null) {
        const dialogRef = this.dialog.open(LoginPageComponent, {
          height: 'auto',
          width: '400px'
        });
        return;
      }
      this.companionPostRequest.userId = user.id;
      this.companionPostRequest.date = new Date();
      this.companionPostRequest.companionPostId = this.companionPost.id;
      this.postService
        .sendRequestJoinGroup(this.companionPostRequest)
        .subscribe(
          res => {
            this.companionPostRequest = res;
          },
          err => {
            this.alertify.error('Gửi yêu cầu lỗi!');
          },
          () => {
            this.statustRequest.IsWaiting();
            this.alertify.success('Gửi yêu câu thành công!');
            // Send notification
            this.notifyService.sendJoinRequestNotification(
              this.user,
              this.companionPost
            );
          }
        );
    } else {
      this.postService.cancleRequest(this.companionPost.id).subscribe(
        res => {},
        err => {
          this.alertify.error('Huỷ yêu cầu lỗi!');
        },
        () => {
          this.alertify.success('Huỷ yêu cầu thành công!');
          this.statustRequest.IsRequestJoin();
        }
      );
    }
  }

  // go to messages
  goToMessages() {
    const user = JSON.parse(localStorage.getItem('User'));
    if (user === null) {
      const dialogRef = this.dialog.open(LoginPageComponent, {
        height: 'auto',
        width: '400px'
      });
      return;
    }
    this.router.navigate(['/tin-nhan']);
    // window.location.href = 'tin-nhan';
  }
}
class StatustRequest {
  type: string;
  text: string;
  public IsRequestJoin() {
    this.type = 'request';
    this.text = 'Tham gia';
  }
  public IsWaiting() {
    this.type = 'waiting';
    this.text = 'Huỷ yêu cầu';
  }
  public IsJoined() {
    this.type = 'joined';
    this.text = 'Đã tham gia';
  }
  public IsExpired() {
    this.type = 'expired';
    this.text = 'Đã hết hạn';
  }
}
