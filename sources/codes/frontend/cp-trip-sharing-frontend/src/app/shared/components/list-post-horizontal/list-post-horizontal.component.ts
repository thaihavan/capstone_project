import { Component, OnInit, Input } from '@angular/core';

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
  rowSizeClass: string;
  constructor() { }
  ngOnInit() {
    this.rowSizeClass = `post-container-${this.rowSize}`;
  }
}
