import { Component, OnInit, Input, ViewChildren, EventEmitter, Output } from '@angular/core';
import { PostSmallComponent } from '../post-small/post-small.component';

@Component({
  selector: 'app-list-post-horizontal',
  templateUrl: './list-post-horizontal.component.html',
  styleUrls: ['./list-post-horizontal.component.css']
})
export class ListPostHorizontalComponent implements OnInit {

  // article | virtual-trip | companion-post
  @Input() listType: string;
  @Input() posts: [];
  @Input() rowSize = 3;
  @Input() isLoading: boolean;
  @Output() checkFollowStages = new EventEmitter();
  @ViewChildren(PostSmallComponent) smallPost: PostSmallComponent[];
  rowSizeClass: string;
  isCallOnTimeFollow = true;
  constructor() { }
  ngOnInit() {
    this.rowSizeClass = `post-container-${this.rowSize}`;
  }
  getStagesEmit() {
    // console.log('View Childrens', this.smallPost);
    this.smallPost.forEach(element => {
      element.getStates();
    });
    // if (this.isCallOnTimeFollow) {
    this.checkFollowStages.emit();
    this.isCallOnTimeFollow = false;
    // }
  }
  getStages() {
    this.smallPost.forEach(element => {
      element.getStates();
    });
  }
}
