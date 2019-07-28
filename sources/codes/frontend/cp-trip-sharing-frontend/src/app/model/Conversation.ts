import { ChatUser } from './ChatUser';
import { ChatMessage } from './ChatMessage';
import { User } from './User';

export class Conversation {
    id: string;
    name: string;
    type: string;
    receivers: string[];
    lastMessage: ChatMessage;
    seenIds: string[];
    createdDate: Date;
    avatar: string;
    groupAdmin: string;

    users: ChatUser[];
    messages: ChatMessage[];
}
