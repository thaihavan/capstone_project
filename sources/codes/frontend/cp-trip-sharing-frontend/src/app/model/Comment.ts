export class Comment {
    public id: string;
    public userDisplayName: string;
    public date: string;
    public userImageUrl: string;
    public numLikes: number;
    public content: string;
    public parentId: string;
    public postId: string;
    public childs: Comment[];
}
