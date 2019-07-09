import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Conversation } from 'src/app/model/Conversation';
import { ChatMessage } from 'src/app/model/ChatMessage';
import { HostGlobal } from '../../global-variables';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  user: any;
  hubConnection: HubConnection;

  baseUrl = HostGlobal.HOST_CHAT_SERVICE + '/api/chatservice/chat';

  constructor(private http: HttpClient) {
    this.user = JSON.parse(localStorage.getItem('User'));
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
        console.log('Connection started!');
      })
      .catch((error) => {
        console.log(error);
      });
  }

  getAllConversations(userId: string): Observable<Conversation[]> {
    return this.http.get<Conversation[]>(this.baseUrl + '/conversations?userId=' + userId);
  }

  getAllMessages(conversationId: string): Observable<ChatMessage[]> {
    return this.http.get<ChatMessage[]>(this.baseUrl + '/messages?conversationId=' + conversationId);
  }

  sendMessage(receiverId: string, message: ChatMessage): Observable<ChatMessage> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.post<ChatMessage>(this.baseUrl + '/message?receiverId=' + receiverId, message, httpOption);
  }
}
