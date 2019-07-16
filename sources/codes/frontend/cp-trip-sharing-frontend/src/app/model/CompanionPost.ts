import { Post } from './Post';
import { Schedule } from './Schedule';
import { ArticleDestinationItem } from './ArticleDestinationItem';

export class CompanionPost {
    constructor() {
        this.post = new Post();
        this.scheduleItems = [];
        this.destinations = [];
    }
    id: string;
    postId: string;
    from: Date;
    to: Date;
    conversationId: string;
    estimatedCost: number;
    estimatedCostItems: string[];
    maxMembers: number;
    minMembers: number;
    expiredDate: Date;
    scheduleItems: Schedule[];
    destinations: ArticleDestinationItem[];
    post: Post;
    requested: boolean;
}
