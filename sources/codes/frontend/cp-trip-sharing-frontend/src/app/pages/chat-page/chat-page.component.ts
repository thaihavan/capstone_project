import { Component, OnInit, HostListener, ViewChild, ElementRef, ViewEncapsulation } from '@angular/core';
import { ChatService } from 'src/app/core/services/chat-service/chat.service';
import { Account } from 'src/app/model/Account';
import { HttpErrorResponse } from '@angular/common/http';
import { Conversation } from 'src/app/model/Conversation';
import { Title } from '@angular/platform-browser';
import { User } from 'src/app/model/User';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { HostGlobal } from 'src/app/core/global-variables';
import { ChatMessage } from 'src/app/model/ChatMessage';
import { ChatUser } from 'src/app/model/ChatUser';
import { UploadImageService } from 'src/app/core/services/upload-image-service/upload-image.service';
import { ImageUpload } from 'src/app/model/ImageUpload';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { MatDialog } from '@angular/material';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';

@Component({
  selector: 'app-chat-page',
  templateUrl: './chat-page.component.html',
  styleUrls: ['./chat-page.component.css']
})
export class ChatPageComponent implements OnInit {

  @ViewChild('chatContainer') chatContainer: ElementRef;

  user: User;
  hubConnection: HubConnection;

  screenHeight: number;
  chatContentHeight: number;

  listConversations: Conversation[];

  selectedConversation: Conversation;
  selectedUser: User;
  inputMessage = '';

  scrollTop: number;

  constructor(private chatService: ChatService,
              private titleService: Title,
              private uploadImageService: UploadImageService,
              private userService: UserService,
              private dialog: MatDialog,
              private errorHandler: GlobalErrorHandler) {
    this.titleService.setTitle('Tin nhắn');
  }

  ngOnInit() {
    this.setHeight();

    this.user = JSON.parse(localStorage.getItem('User'));
    this.selectedConversation = new Conversation();

    this.initChatConnection();

    this.getAllConversations();

  }

  setHeight() {
    this.screenHeight = window.innerHeight;
    // headerHeight: 56
    // chatHeader: 64
    // chatFooter: 80
    this.chatContentHeight = this.screenHeight - 56 - 64 - 80 - 2;

  }

  initChatConnection() {
    // Init connection
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(
        HostGlobal.HOST_CHAT_SERVICE + '/chat?userId=' + this.user.id,
        {
          accessTokenFactory: () => localStorage.getItem('Token')
        }
      )
      .build();

    this.hubConnection
      .start()
      .then(() => {
        // console.log('Connection started!');
      })
      .catch((error) => {
        console.log(error);
      });

    // Listening
    this.hubConnection.on('clientMessageListener', (convId: string, message: ChatMessage) => {
      const conversation = this.listConversations.find(c => c.id === convId);
      if (conversation && conversation != null) {
        if (!conversation.messages || conversation.messages == null) {
          conversation.messages = [];
        }
        conversation.messages.push(message);
        conversation.lastMessage = message;
        this.scrollTop = this.chatContainer.nativeElement.scrollHeight;
      }
    });
  }

  getAllConversations() {
    this.chatService.getAllConversations(this.user.id).subscribe((result: Conversation[]) => {
      this.listConversations = result;
      if (this.listConversations && this.listConversations.length > 0) {
        this.selectedConversation = this.listConversations[0];
        this.onClickUserItem(this.selectedConversation);
      }
      this.setConversationInfo();
    }, this.errorHandler.handleError);
  }

  setConversationInfo() {
    for (const c of this.listConversations) {
      if (c.type === 'private') {
        for (const u of c.users) {
          if (u.id !== this.user.id) {
            c.avatar = u.profileImage;
            c.name = u.displayName;
            break;
          }
        }
      }
    }
  }

  onClickUserItem(conv: Conversation) {
    this.selectedConversation = conv;
    this.chatService.getAllMessages(conv.id).subscribe((res: ChatMessage[]) => {
      this.selectedConversation.messages = res;

      this.scrollTop = undefined;
      setTimeout(() => { this.scrollTop = this.chatContainer.nativeElement.scrollHeight; }, 100);

      if (conv.type === 'private') {
        const chatUser = this.selectedConversation.users.find(u => u.id !== this.user.id);
        this.userService.getUserById(chatUser.id).subscribe((user: User) => {
          this.selectedUser = user;
        }, this.errorHandler.handleError);
      }

    }, this.errorHandler.handleError);
  }

  getProfileImage(userId: string) {
    const user = this.selectedConversation.users.find(u => u.id === userId);
    if (user) {
      return user.profileImage;
    }
    return 'https://storage.googleapis.com/trip-sharing-cp-image-bucket/image-default-user-avatar.png';
  }

  sendMessage(message: string) {
    if (message.trim() !== '') {
      this.hubConnection.invoke(
        'SendToConversation',
        this.user.id,
        this.selectedConversation.id,
        message)
        .then(() => {
          this.inputMessage = '';
        })
        .catch((error) => {
          console.log(error);
        });
    }
  }

  seenConversation() {
    if (this.inputMessage.trim() === '') {
      this.hubConnection.invoke(
        'SeenConversation',
        this.selectedConversation.id,
        this.user.id)
        .then(() => {
        })
        .catch((error) => {
          console.log(error);
        });
    }
  }

  seen() {
    this.chatService.seen(this.selectedConversation.id).subscribe((res: boolean) => {
    }, this.errorHandler.handleError);
  }

  processImage(imageInput: any) {
    const imageUpload = new ImageUpload();

    const file: File = imageInput.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      imageUpload.image = reader.result.toString().split(',')[1];
      imageUpload.type = file.type;

      this.uploadImageService.uploadImage(imageUpload).subscribe((res: any) => {
        this.sendMessage(`<img width="100%" src='${res.image}'>`);
      }, this.errorHandler.handleError);
    };
  }

  isImage(imgTag: string): boolean {
    return imgTag.startsWith('<img');
  }

  leaveGroupChat(conversationId: string, userId: string) {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '500px',
      height: 'auto',
      position: {
        top: '20px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = 'Bạn có chắc chắn muốn rời khỏi nhóm không?';
    instance.message.messageType = 'confirm';

    dialogRef.afterClosed().subscribe((result: string) => {
      if (result === 'continue') {
        this.chatService.leaveGroupChat(conversationId, userId).subscribe((res) => {
          window.location.reload();
        }, this.errorHandler.handleError);
      }
    });
  }

  gotoUserPage(user: ChatUser) {
    window.location.href = `/user/${user.id}`;
  }

  removeMember(selectedConversation: Conversation, user: ChatUser) {
    if (selectedConversation.groupAdmin === this.user.id) {
      const dialogRef = this.dialog.open(MessagePopupComponent, {
        width: '500px',
        height: 'auto',
        position: {
          top: '20px'
        },
        disableClose: true
      });
      const instance = dialogRef.componentInstance;
      instance.message.messageText = 'Bạn có chắc chắn muốn xóa thành viên này khỏi nhóm không?';
      instance.message.messageType = 'confirm';

      dialogRef.afterClosed().subscribe((result: string) => {
        if (result === 'continue') {
          this.chatService.leaveGroupChat(selectedConversation.id, user.id).subscribe((res) => {
            selectedConversation.users = selectedConversation.users.filter(u => u.id !== user.id);
          }, this.errorHandler.handleError);
        }
      });
    }
  }

}
