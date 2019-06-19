import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Globals } from 'src/globals/globalvalues';
import { Topic } from 'src/app/model/Topic';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-interested-toppic',
  templateUrl: './interested-toppic.component.html',
  styleUrls: ['./interested-toppic.component.css']
})
export class InterestedTopicComponent implements OnInit {

  selectedTopic: string[] = [];
  @Input() listTopics: Topic[];
  @Output() EventTopic: EventEmitter<string[]> = new EventEmitter();
  listselectedTopic: Array<Topic> = [];

  constructor(private globals: Globals, private postService: PostService) { }

  ngOnInit() {
    this.getAllTopics();
  }

  onSelectTopic(topic: Topic): void {
    if (this.selectedTopic.indexOf(topic.id) === -1) {
      this.selectedTopic.push(topic.id);
    } else {
      const unselected = this.selectedTopic.indexOf(topic.id);
      this.selectedTopic.splice(unselected, 1);
    }
    this.EventTopic.emit(this.selectedTopic);
  }
  IsChecked(topic: Topic) {
    if (this.selectedTopic.indexOf(topic.id) !== -1) {
      return true;
    }
    return false;
  }

  gotoHomepage() {
    window.location.href = this.globals.urllocal;
  }

  getAllTopics() {
    this.postService.getAllTopics().subscribe((topics: Topic[]) => {
      this.listTopics = topics;
    }, (err: HttpErrorResponse) => {
      console.log(err);
    });
  }

}

