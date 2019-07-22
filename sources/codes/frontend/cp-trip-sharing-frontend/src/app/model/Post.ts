import { Author } from './Author';

export class Post {
    constructor() {
        this.author = new Author();
    }
    id: string;
    title: string;
    description: string;
    content: string;
    liked: boolean;
    coverImage: string;
    isPublic: boolean;
    isActive: boolean;
    pubDate: string;
    postType: null;
    author: Author;
    time: string;
    likes: number;
    comments: number;
    likeCount: number;
    commentCount: number;
}
