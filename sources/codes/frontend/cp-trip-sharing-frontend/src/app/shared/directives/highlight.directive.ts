import { Directive, Input, ElementRef, AfterViewInit } from '@angular/core';

@Directive({
  selector: '[appHighlight]'
})
export class HighlightDirective implements AfterViewInit {

  constructor(private el: ElementRef) {
   }
@Input('appHighlight') color: string;
@Input() size: string;
private highlight(color: string, size: string) {
  this.el.nativeElement.style.color = color;
  this.el.nativeElement.style.fontSize = size;
  this.el.nativeElement.style.fontWeight = 500;
}
ngAfterViewInit(): void {
  this.highlight(this.color, this.size);
}
}
