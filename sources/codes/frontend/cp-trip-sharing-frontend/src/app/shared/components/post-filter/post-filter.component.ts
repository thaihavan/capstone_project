import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { PostFilter } from 'src/app/model/PostFilter';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { Topic } from 'src/app/model/Topic';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-post-filter',
  templateUrl: './post-filter.component.html',
  styleUrls: ['./post-filter.component.css']
})
export class PostFilterComponent implements OnInit {
  @Input() showTimePeriod = true;
  @Input() showTopic = true;
  @Output() filtered = new EventEmitter<PostFilter>();

  postFilter: PostFilter;
  isDisplayFilter = false;
  topics: Topic[] = [];
  isCheckedDict = {};

  constructor(private postService: PostService) { }

  ngOnInit() {
    this.initPostFilter();
    this.getTopics();
  }

  initPostFilter() {
    this.postFilter = new PostFilter();
    this.postFilter.timePeriod = 'all_time';
    this.postFilter.topics = [];
  }

  onButtonToggleChange(value: string) {
    this.postFilter.timePeriod = value;
  }

  onCheckboxToggleChange(topicId: string) {
    const index = this.postFilter.topics.indexOf(topicId);
    if (index === -1) {
      this.postFilter.topics.push(topicId);
    } else {
      this.postFilter.topics.splice(index, 1);
    }
  }

  getTopics() {
    this.postService.getAllTopics().subscribe((res: any) => {
      this.topics = res;
      this.postFilter.topics = this.topics.map(t => t.id);
      this.topics.forEach(topic => {
        this.isCheckedDict[topic.id] = true;
      });
    }, (err: HttpErrorResponse) => {
      console.log(err);
    });
  }

  toggleFilter() {
    this.isDisplayFilter = !this.isDisplayFilter;
    console.log(this.postFilter);
  }

  submitFilter() {
    this.isDisplayFilter = false;
    this.filtered.emit(this.postFilter);
  }

}
