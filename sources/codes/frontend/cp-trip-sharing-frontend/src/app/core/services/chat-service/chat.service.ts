import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Conversation } from 'src/app/model/Conversation';
import { ChatMessage } from 'src/app/model/ChatMessage';
import { HostGlobal } from '../../global-variables';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  baseUrl = HostGlobal.HOST_CHAT_SERVICE + 'sth';

  constructor() { }

  getAllConversations(userId: string): Observable<Conversation[]> {
    const fakeConversation1 = new Conversation();
    fakeConversation1.id = 'id1';
    fakeConversation1.type = 'non_group';
    fakeConversation1.receivers = ['user1', 'user2'];

    const fakeConversation2 = new Conversation();
    fakeConversation2.id = 'id2';
    fakeConversation2.type = 'non_group';
    fakeConversation2.receivers = ['user1', 'user3'];

    const fakeList = [fakeConversation1, fakeConversation2];

    return of(fakeList);
  }

  getAllMessages(conversationId: string): Observable<ChatMessage[]> {
    return null;
  }
}
