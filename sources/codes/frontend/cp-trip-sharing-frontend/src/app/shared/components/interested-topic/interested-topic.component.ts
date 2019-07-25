import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Topic } from 'src/app/model/Topic';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-interested-topic',
  templateUrl: './interested-topic.component.html',
  styleUrls: ['./interested-topic.component.css']
})
export class InterestedTopicComponent implements OnInit {

  selectedTopic: string[] = [];
  @Input() listTopicIdSelected: string[] = [];
  @Input() listTopics: Topic[];
  @Output() EventTopic: EventEmitter<string[]> = new EventEmitter();
  listselectedTopic: Array<Topic> = [];

  constructor(private postService: PostService) { }

  ngOnInit() {
    this.getAllTopics();
  }

  onSelectTopic(topicId: any): void {
    if (this.selectedTopic.indexOf(topicId) === -1) {
      this.selectedTopic.push(topicId);
    } else {
      const unselected = this.selectedTopic.indexOf(topicId);
      this.selectedTopic.splice(unselected, 1);
    }
    this.EventTopic.emit(this.selectedTopic);
  }
  IsChecked(topicId: any) {
    if (this.selectedTopic.indexOf(topicId) !== -1) {
      return true;
    }
    return false;
  }

  gotoHomepage() {
    window.location.href = '';
  }

  getAllTopics() {
    this.postService.getAllTopics().subscribe((topics: Topic[]) => {
      this.listTopics = topics;
    }, (err: HttpErrorResponse) => {
      console.log(err);
    },
    () => {
      if (this.listTopicIdSelected !== undefined) {
        this.listTopicIdSelected.forEach(topicId => {
          this.onSelectTopic(topicId);
          this.IsChecked(topicId);
        });
      }
    });
  }

}

