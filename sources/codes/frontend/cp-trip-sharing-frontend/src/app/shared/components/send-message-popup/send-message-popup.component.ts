import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { User } from 'src/app/model/User';
import { ChatService } from 'src/app/core/services/chat-service/chat.service';
import { ChatMessage } from 'src/app/model/ChatMessage';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-send-message-popup',
  templateUrl: './send-message-popup.component.html',
  styleUrls: ['./send-message-popup.component.css']
})
export class SendMessagePopupComponent implements OnInit {

  receiver: User;
  message: string;
  errorMessage = '';

  constructor(
    private chatService: ChatService,
    private dialogRef: MatDialogRef<SendMessagePopupComponent>) { }

  ngOnInit() {
  }

  sendMessage() {
    console.log(this.message);
    if (!this.message || this.message == null || this.message.trim() === '') {
      this.errorMessage = 'Nội dung tin nhắn không được để trống.';
    } else {
      this.errorMessage = '';
      const chatMessage = new ChatMessage();
      chatMessage.content = this.message;
      this.chatService.sendMessage(this.receiver.id, chatMessage).subscribe((res) => {
        console.log(res);
        this.dialogRef.close();
      }, (error: HttpErrorResponse) => {
        console.log(error);
        this.errorMessage = 'Gửi thất bại.';
      });
    }
  }

  cancel() {
      this.dialogRef.close();
  }

}
