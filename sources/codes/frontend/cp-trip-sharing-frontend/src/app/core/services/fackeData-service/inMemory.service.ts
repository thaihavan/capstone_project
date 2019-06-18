import { Injectable } from '@angular/core';
import { InMemoryDbService } from 'angular-in-memory-web-api';

@Injectable({
  providedIn: 'root'
})
export class InMemoryService implements InMemoryDbService {
  createDb() {
    const posts = [
        {
          user: 'PhongNV',
          time: 'A month ago',
          title: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Velit consectetur deserunt illo esse distinctio.',
          image: 'luff.jpg',
          // tslint:disable-next-line:max-line-length
          description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Magnam omnis nihil, aliquam est, voluptates officiis iure soluta alias vel odit, placeat reiciendis ut libero! Quas aliquid natus cumque quae repellendus. Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa, excepturi. Doloremque, reprehenderit! Quos in maiores, soluta doloremque molestiae reiciendis libero expedita assumenda fuga quae. Consectetur id molestias itaque facere? Hic!',
          likes: '123',
          comments: '123'
      },
      {
          user: 'PhongNV',
          time: 'A month ago',
          title: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Velit consectetur deserunt illo esse distinctio.',
          image: 'luff.jpg',
          // tslint:disable-next-line:max-line-length
          description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Magnam omnis nihil, aliquam est, voluptates officiis iure soluta alias vel odit, placeat reiciendis ut libero! Quas aliquid natus cumque quae repellendus. Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa, excepturi. Doloremque, reprehenderit! Quos in maiores, soluta doloremque molestiae reiciendis libero expedita assumenda fuga quae. Consectetur id molestias itaque facere? Hic!',
          likes: '123',
          comments: '123'
      }
    ];
    return {posts};
  }

constructor() { }

}
