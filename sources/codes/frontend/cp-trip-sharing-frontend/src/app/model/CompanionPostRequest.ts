import { Author } from './Author';

export class CompanionPostRequest {
    id: string;
    userId: string;
    companionPostId: string;
    date: Date;
    user: Author;
}
