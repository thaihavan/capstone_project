import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Globals } from 'src/globals/globalvalues';

@Component({
  selector: 'app-interested-toppic',
  templateUrl: './interested-toppic.component.html',
  styleUrls: ['./interested-toppic.component.css']
})
export class InterestedToppicComponent implements OnInit {

  selectedTopic: Topic[] = [];
  @Input() ListToppic;
  @Output() EventToppic: EventEmitter<any> = new EventEmitter();
  listselectedTopic: Array<Topic> = [];
  listinterestedtopic: Topic[] = [
    {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
    {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
    {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
    {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
    {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
    {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
    {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
    {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
    {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
    {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
    {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
    {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
    {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
    {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
    {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
    {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
    {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
    {nameTopic: 'Văn Hóa', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'},
    {nameTopic: 'Loại Khác', urlImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg'}
   ];

 constructor(private globals: Globals) { }

 ngOnInit() {
 }

 onSelectToppic(topic: Topic): void {
   if (this.selectedTopic.indexOf(topic) !== -1) {
     this.selectedTopic = this.selectedTopic.filter(item => item !== topic);
   } else {
     this.selectedTopic.push(topic);
   }
   this.EventToppic.emit(this.selectedTopic);
 }
 IsChecked(topic) {
   if (this.selectedTopic.indexOf(topic) !== -1) {
     return true;
   }
   return false;
 }

 gotoHomepage() {
   window.location.href = this.globals.urllocal;
 }

}
export class Topic {
  nameTopic: string;
  urlImage: string;
}
