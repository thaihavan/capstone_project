import { Author } from './Author';

export class Comment {
    public id: string;
    public date: string;
    public likeCount: number;
    public content: string;
    public parentId: string;
    public postId: string;
    public childs: Comment[];
    public authorId: string;
    public author: Author;
    public liked: boolean;
    public isActive: boolean;

    constructor() {
        this.author = new Author();
    }
}
