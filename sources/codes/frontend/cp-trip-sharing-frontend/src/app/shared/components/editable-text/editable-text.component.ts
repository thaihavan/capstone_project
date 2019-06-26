import { Component, OnInit, Input, Output, EventEmitter, HostListener, ElementRef, ViewChild, OnChanges } from '@angular/core';

@Component({
  selector: 'app-editable-text',
  templateUrl: './editable-text.component.html',
  styleUrls: ['./editable-text.component.css']
})
export class EditableTextComponent implements OnInit, OnChanges {

  originText: string;
  @Input() text: string;
  @Input() isInputText: boolean;
  @Output() updatedText = new EventEmitter();
  editAble = false;
  constructor(private elementRef: ElementRef) {}
  @HostListener('document:click', ['$event', '$event.target'])
    public onClick(event: MouseEvent, targetElement: HTMLElement): void {
        if (!targetElement) {
            return;
        }
        const clickedInside = this.elementRef.nativeElement.contains(targetElement);
        if (!clickedInside) {
            this.editAble = false;
            this.text = this.originText;
          }
    }
  ngOnInit() {
    this.originText = this.text;
  }
  ngOnChanges(): void {
    this.originText = this.text;
  }
  save() {
    this.editAble = false;
    this.originText = this.text;
    this.updatedText.emit(this.text);
  }
}
