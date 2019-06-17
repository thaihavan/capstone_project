export class Post {
    id: string;
    title: string;
    description: string;
    content: string;
    image: string;
    isPublic: boolean;
    isActive: boolean;
    pubDate: string;
    postType: null;
    author: Account;
    time: string;
    likes: number;
    comments: number;
}
