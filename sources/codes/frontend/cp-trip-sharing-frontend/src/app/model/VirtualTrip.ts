import { Post } from './Post';
import { LocationMarker } from './LocationMarker';

export class VirtualTrip {
    id: string;
    items: LocationMarker[];
    postId: string;
    post: Post;
}
