import { Component, OnInit, Input, EventEmitter, Output, ElementRef, ViewChild } from '@angular/core';
import { User } from 'src/app/model/User';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Conversation } from 'src/app/model/Conversation';
import { ChatService } from 'src/app/core/services/chat-service/chat.service';
import { ChatMessage } from 'src/app/model/ChatMessage';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ImageUpload } from 'src/app/model/ImageUpload';
import { UploadImageService } from 'src/app/core/services/upload-image-service/upload-image.service';
import { OuterSubscriber } from 'rxjs/internal/OuterSubscriber';
import { HostGlobal } from 'src/app/core/global-variables';

@Component({
  selector: 'app-chat-popup',
  templateUrl: './chat-popup.component.html',
  styleUrls: ['./chat-popup.component.css']
})
export class ChatPopupComponent implements OnInit {
  @Input() conversation: Conversation;
  @Output() closed = new EventEmitter<boolean>();

  @ViewChild('chatContainer') chatContainer: ElementRef;

  user: User;
  hubConnection: HubConnection;

  screenHeight: number;
  chatContentHeight: number;

  inputMessage = '';

  scrollTop: number;

  constructor(private chatService: ChatService,
              private userService: UserService,
              private uploadImageService: UploadImageService) {
    this.user = JSON.parse(localStorage.getItem('User'));
  }

  ngOnInit() {
    if (this.conversation) {
      if (this.conversation.id && this.conversation.id != null) {
        this.initChatConnection();
      } else {
        // TODO
      }
    }
  }

  // tslint:disable-next-line: use-life-cycle-interface
  ngOnChanges() {
    if (this.conversation) {
      if (this.conversation.id && this.conversation.id != null) {
        this.getMessages();
      } else {
        // TODO
      }
    }
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
      if (convId === this.conversation.id) {
        if (!this.conversation.messages || this.conversation.messages == null) {
          this.conversation.messages = [];
        }
        this.conversation.messages.push(message);
        this.conversation.lastMessage = message;

        this.scrollTop = this.chatContainer.nativeElement.scrollHeight;
      }
    });
  }

  getMessages() {
    this.chatService.getAllMessages(this.conversation.id).subscribe((res: ChatMessage[]) => {
      this.conversation.messages = res;

      this.scrollTop = undefined;
      setTimeout(() => { this.scrollTop = this.chatContainer.nativeElement.scrollHeight; }, 0);

    }, (error: HttpErrorResponse) => {
      console.log(error);
    });
  }

  getProfileImage(userId: string) {
    const user = this.conversation.users.find(u => u.id === userId);
    if (user) {
      return user.profileImage;
    }
    return 'https://storage.googleapis.com/trip-sharing-final-image-bucket/image-default-user-avatar.png';
  }

  sendMessage(message) {
    if (message.trim() !== '') {
      this.hubConnection.invoke(
        'SendToConversation',
        this.user.id,
        this.conversation.id,
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
        this.conversation.id,
        this.user.id)
        .then(() => {
        })
        .catch((error) => {
          console.log(error);
        });
    }
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
      }, (error: HttpErrorResponse) => {
        console.log(error);
      });
    };
  }

  isImage(imgTag: string): boolean {
    return imgTag.startsWith('<img');
  }

  closeChatPopup() {
    this.closed.emit(true);
  }
}
