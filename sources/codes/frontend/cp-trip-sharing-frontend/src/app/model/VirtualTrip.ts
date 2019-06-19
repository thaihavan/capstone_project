import { Post } from './Post';
import { LocationMarker } from './LocationMarker';

export class VirtualTrip {
    id: string;
    virtualTripItems: LocationMarker[];
    postId: string;
    coverImage: string;
    post: Post;
}
