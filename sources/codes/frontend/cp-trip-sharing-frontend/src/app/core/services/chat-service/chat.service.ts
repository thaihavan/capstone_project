import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Conversation } from 'src/app/model/Conversation';
import { ChatMessage } from 'src/app/model/ChatMessage';
import { HostGlobal } from '../../global-variables';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { ChatUser } from 'src/app/model/ChatUser';

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
      .withUrl(HostGlobal.HOST_CHAT_SERVICE + '/chat?userId=' + this.user.id, {
        accessTokenFactory: () => localStorage.getItem('Token')
      })
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started!');
      })
      .catch(error => {
        console.log(error);
      });
  }

  getAllConversations(userId: string): Observable<Conversation[]> {
    return this.http.get<Conversation[]>(
      this.baseUrl + '/conversations?userId=' + userId
    );
  }

  getAllMessages(conversationId: string): Observable<ChatMessage[]> {
    return this.http.get<ChatMessage[]>(
      this.baseUrl + '/messages?conversationId=' + conversationId
    );
  }

  sendMessage(
    receiverId: string,
    message: ChatMessage
  ): Observable<ChatMessage> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.post<ChatMessage>(
      this.baseUrl + '/message?receiverId=' + receiverId,
      message,
      httpOption
    );
  }

  seen(conversationId: string): Observable<boolean> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.post<boolean>(
      this.baseUrl + '/seen?conversationId=' + conversationId,
      null,
      httpOption
    );
  }

  // get all members
  getMembers(conversationId): Observable<ChatUser[]> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    return this.http.get<ChatUser[]>(this.baseUrl + '/members?conversationId=' + conversationId, httpOption);
  }

  // add an user to group chat
  addUser(user): Observable<any> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    // tslint:disable-next-line:max-line-length
    return this.http.post<any>(this.baseUrl + '/add-user?conversationId=' + user.conversationId + '&userId=' + user.userId, user, httpOption);
  }

  leaveGroupChat(conversationId: string, userId: string): Observable<any> {
    const httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('Token')
      })
    };
    // tslint:disable-next-line:max-line-length
    return this.http.get<any>(this.baseUrl + '/remove-user?conversationId=' + conversationId + '&userId=' + userId, httpOption);
  }
}
