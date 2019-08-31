import { Component, OnInit } from '@angular/core';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { FormAddTopicsComponent } from './form-add-topics/form-add-topics.component';
import { Topic } from 'src/app/model/Topic';
import { DeleteConfirmPopupComponent } from 'src/app/shared/components/delete-confirm-popup/delete-confirm-popup.component';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';


@Component({
  selector: 'app-topic-page-admin',
  templateUrl: './topic-page-admin.component.html',
  styleUrls: ['./topic-page-admin.component.css']
})
export class TopicPageAdminComponent implements OnInit {
  listTopics: Topic[] = [];
  selectedTopics: string[] = [];

  constructor(private postService: PostService,
              public dialog: MatDialog,
              private errorHandler: GlobalErrorHandler) {
    this.getAllTopics();
  }

  ngOnInit() {
  }

  getAllTopics() {
    this.postService.getAllTopics().subscribe((topics: any) => {
      this.listTopics = topics;
    }, this.errorHandler.handleError);
  }

  onClickTopic(topic: Topic) {
    if (this.selectedTopics.indexOf(topic.id) === -1) {
      this.selectedTopics.push(topic.id);
    } else {
      const unselected = this.selectedTopics.indexOf(topic.id);
      this.selectedTopics.splice(unselected, 1);
    }
  }

  addTopic() {
    const dialogRef = this.dialog.open(FormAddTopicsComponent, {
      width: '500px',
      height: 'auto',
        position: {
          top: '20px'
        },
      data: {
        toppics: [],
        destinations: [],
      }
    });

    dialogRef.afterClosed().subscribe((res: any) => {
      if (res && res != null && res !== 'cancel') {
        this.listTopics.push(res);
      }
    });
  }

  UpdateTopic() {
    if (this.selectedTopics.length === 1) {
      const dialogRef = this.dialog.open(FormAddTopicsComponent, {
        width: '500px',
        height: 'auto',
        position: {
          top: '20px'
        },
        data: {
          toppics: [],
          destinations: [],
        },
      });

      let topic = this.listTopics.find(t => t.id === this.selectedTopics[0]);

      const instance = dialogRef.componentInstance;
      instance.topic = topic;

      dialogRef.afterClosed().subscribe((res: Topic) => {
        if (res && res != null) {
          topic = res;
        }
      });
    }
  }

  removeTopics() {
    if (this.selectedTopics.length > 0) {
      const dialogRef = this.dialog.open(MessagePopupComponent, {
        width: '500px',
        height: 'auto',
        position: {
          top: '20px'
        },
        disableClose: true
      });
      const instance = dialogRef.componentInstance;
      instance.message.messageText = `Bạn có chắc muốn xóa ${this.selectedTopics.length} chủ đề này không?`;
      instance.message.messageType = 'confirm';

      dialogRef.afterClosed().subscribe((res: string) => {
        if (res === 'continue') {
          if (this.selectedTopics.length > 0) {
            this.postService.removeTopics(this.selectedTopics).subscribe((result: any) => {
              this.listTopics = this.listTopics.filter(t => this.selectedTopics.find(x => x === t.id) == null);
              this.selectedTopics = [];
            }, this.errorHandler.handleError);
          }
        }
      });
    }

  }

  isSelected(topic: Topic) {
    return topic && this.selectedTopics.indexOf(topic.id) !== -1;
  }

}
