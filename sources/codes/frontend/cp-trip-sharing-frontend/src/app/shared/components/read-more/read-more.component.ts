import { Component, OnInit, Input, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-read-more',
  templateUrl: './read-more.component.html',
  styleUrls: ['./read-more.component.css'],
  animations: [
    trigger('showAnimation', [
      // ...
      state('show-more', style({
        overflow: 'visible',
        height: '100%'
      })),
      state('show-less', style({
        height: '50px',
        overflow: 'hidden'
      })),
      transition('* => *', [
        animate('1s')
      ]),
    ]),
  ],
})
export class ReadMoreComponent implements OnInit, OnChanges {

  @Input() text: string;
  @Input() isEditable: boolean;
  @Output() saveText = new EventEmitter();
  isShowLess = false;
  constructor() { }

  ngOnInit() {
    if (this.text.length >= 60) {
      this.isShowLess = true;
    }
  }
  ngOnChanges(changes: SimpleChanges) {
    if (this.text.length >= 60) {
      this.isShowLess = true;
    }
  }
  updateText(text) {
    this.text = text;
    this.saveText.emit(text);
  }

}
