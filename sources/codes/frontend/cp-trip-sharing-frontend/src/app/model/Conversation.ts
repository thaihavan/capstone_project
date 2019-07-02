import { ChatUser } from './ChatUser';
import { ChatMessage } from './ChatMessage';

export class Conversation {
    id: string;
    type: string;
    receivers: string[];

    listUsers: ChatUser[];
    listMessages: ChatMessage[];
}