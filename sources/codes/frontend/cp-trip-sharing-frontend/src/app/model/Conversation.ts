import { ChatUser } from './ChatUser';
import { ChatMessage } from './ChatMessage';

export class Conversation {
    id: string;
    name: string;
    type: string;
    receivers: string[];
    lastMessage: string;

    users: ChatUser[];
    messages: ChatMessage[];
    avatar: string;
}
