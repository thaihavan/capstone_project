import { Component, OnInit, HostListener, ViewChild, ElementRef } from '@angular/core';
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

@Component({
  selector: 'app-chat-page',
  templateUrl: './chat-page.component.html',
  styleUrls: ['./chat-page.component.css']
})
export class ChatPageComponent implements OnInit {

  @ViewChild('chatContainer') chatContainer: ElementRef;

  user: any;
  hubConnection: HubConnection;

  screenHeight: number;
  chatContentHeight: number;

  listConversations: Conversation[];

  selectedConversation: Conversation;
  inputMessage = '';

  constructor(private chatService: ChatService,
              private titleService: Title,
              private uploadImageService: UploadImageService) {
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

  goToBottom() {
    this.chatContainer.nativeElement.scrollTop = this.chatContainer.nativeElement.scrollHeight;
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
        conversation.messages.push(message);
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
    }, (err: HttpErrorResponse) => {
      console.log(err);
    });
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
    }, (error: HttpErrorResponse) => {
      console.log(error);
    });
  }

  getProfileImage(userId: string) {
    const user = this.selectedConversation.users.find(u => u.id === userId);
    if (user) {
      return user.profileImage;
    }
    return '';
  }

  sendMessage() {
    if (this.inputMessage.trim() !== '') {
      this.hubConnection.invoke(
        'SendToConversation',
        this.user.id,
        this.selectedConversation.id,
        this.inputMessage)
        .then(() => {
          const messageObject = new ChatMessage();
          messageObject.content = this.inputMessage;
          messageObject.fromUserId = this.user.id;
          messageObject.conversationId = this.selectedConversation.id;
          messageObject.time = new Date();
          if (!this.selectedConversation.messages || this.selectedConversation.messages == null) {
            this.selectedConversation.messages = [];
          }

          this.selectedConversation.messages.push(messageObject);

          this.selectedConversation.lastMessage = messageObject;
          this.inputMessage = '';
        })
        .catch((error) => {
          console.log(error);
        });
    }
  }

  seenConversation() {
    if (this.inputMessage.trim() !== '') {
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
    }, (error: HttpErrorResponse) => {
      console.log(error);
    });
  }

  sendImage(imgTag: string) {
    this.hubConnection.invoke(
      'SendToConversation',
      this.user.id,
      this.selectedConversation.id,
      imgTag)
      .then(() => {
        const messageObject = new ChatMessage();
        messageObject.content = imgTag;
        messageObject.fromUserId = this.user.id;
        messageObject.conversationId = this.selectedConversation.id;
        messageObject.time = new Date();
        if (!this.selectedConversation.messages || this.selectedConversation.messages == null) {
          this.selectedConversation.messages = [];
        }
        this.selectedConversation.messages.push(messageObject);
        this.selectedConversation.lastMessage = messageObject;
      })
      .catch((error) => {
        console.log(error);
      });
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
        this.sendImage(`<img src='${res.image}'>`);
      }, (error: HttpErrorResponse) => {
        console.log(error);
      });
    };
  }

  isImage(imgTag: string): boolean {
    return imgTag.startsWith('<img');
  }
}
