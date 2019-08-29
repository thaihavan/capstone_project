import { Post } from './Post';
import { LocationMarker } from './LocationMarker';

export class VirtualTrip {
    constructor() {
        this.post = new Post();
    }
    id: string;
    items: LocationMarker[];
    postId: string;
    post: Post;
}
