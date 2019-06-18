import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Globals } from 'src/globals/globalvalues';
import { Topic } from 'src/app/model/Topic';

@Component({
  selector: 'app-interested-toppic',
  templateUrl: './interested-toppic.component.html',
  styleUrls: ['./interested-toppic.component.css']
})
export class InterestedToppicComponent implements OnInit {

  selectedTopic: string[] = [];
  @Input() ListToppic;
  @Output() EventToppic: EventEmitter<string[]> = new EventEmitter();
  listselectedTopic: Array<Topic> = [];
  listinterestedtopic: Topic[] = [
    { topicId: 'topic-id-1', topicName: 'Văn Hóa', topicImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg' },
    { topicId: 'topic-id-2', topicName: 'Văn Hóa', topicImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg' },
    { topicId: 'topic-id-3', topicName: 'Văn Hóa', topicImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg' },
    { topicId: 'topic-id-4', topicName: 'Văn Hóa', topicImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg' },
    { topicId: 'topic-id-5', topicName: 'Văn Hóa', topicImage: 'https://gody.vn/public/v3/images/bg/br-register.jpg' }
  ];

  constructor(private globals: Globals) { }

  ngOnInit() {
  }

  onSelectToppic(topic: Topic): void {
    if (this.selectedTopic.indexOf(topic.topicId) === -1) {
      this.selectedTopic.push(topic.topicId);
    } else {
      const unselected = this.selectedTopic.indexOf(topic.topicId);
      this.selectedTopic.splice(unselected, 1);
    }
    this.EventToppic.emit(this.selectedTopic);
  }
  IsChecked(topic: Topic) {
    if (this.selectedTopic.indexOf(topic.topicId) !== -1) {
      return true;
    }
    return false;
  }

  gotoHomepage() {
    window.location.href = this.globals.urllocal;
  }

}

