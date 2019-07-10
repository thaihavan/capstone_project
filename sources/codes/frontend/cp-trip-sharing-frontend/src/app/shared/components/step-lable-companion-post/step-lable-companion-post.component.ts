import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-step-lable-companion-post',
  templateUrl: './step-lable-companion-post.component.html',
  styleUrls: ['./step-lable-companion-post.component.css']
})
export class StepLableCompanionPostComponent implements OnInit {
  @Input() item;
  @Input() index;
  @Input() isDisplayMore;
  @Output() update = new EventEmitter();
  @Output() delete = new EventEmitter();
  constructor() {}

  ngOnInit() {}
  isDelete() {
    this.delete.emit(this.index);
  }
  isUpdate() {
    this.update.emit(this.item);
  }
}
