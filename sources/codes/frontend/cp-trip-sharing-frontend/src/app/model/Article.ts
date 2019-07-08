import { Post } from './Post';
import { ArticleDestinationItem } from './ArticleDestinationItem';

export class Article {
    id: string;
    topics: [];
    destinations: ArticleDestinationItem[];
    postId: string;
    coverImage: string;
    post: Post;
}
