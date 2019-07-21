import { Component, OnInit } from '@angular/core';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { FormAddTopicsComponent } from './form-add-topics/form-add-topics.component';
import { Topic } from 'src/app/model/Topic';
import { DeleteConfirmPopupComponent } from 'src/app/shared/components/delete-confirm-popup/delete-confirm-popup.component';


@Component({
  selector: 'app-topic-page-admin',
  templateUrl: './topic-page-admin.component.html',
  styleUrls: ['./topic-page-admin.component.css']
})
export class TopicPageAdminComponent implements OnInit {
  listTopics: Topic[] = [];
  selectedTopics: string[] = [];

  constructor(private postService: PostService, public dialog: MatDialog) {
    this.getAllTopics();
  }

  ngOnInit() {
  }

  getAllTopics() {
    this.postService.getAllTopics().subscribe((topics: any) => {
      this.listTopics = topics;
    }, (err: HttpErrorResponse) => {
      console.log(err);
    });
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
      width: '420px',
      data: {
        toppics: [],
        destinations: [],
      }
    });

    dialogRef.afterClosed().subscribe((res: Topic) => {
      if (res && res != null) {
        this.listTopics.push(res);
      }
    });
  }

  removeTopics() {

    if (this.selectedTopics.length > 0) {
      const dialogRef = this.dialog.open(DeleteConfirmPopupComponent, {
        width: '320px',
        height: 'auto',
        position: {
          top: '150px'
        },
        disableClose: true
      });
      const instance = dialogRef.componentInstance;
      instance.message = `Bạn có chắc muốn xóa ${this.selectedTopics.length} chủ đề này không?`;

      dialogRef.afterClosed().subscribe((res: string) => {
        if (res === 'yes') {
          if (this.selectedTopics.length > 0) {
            this.postService.removeTopics(this.selectedTopics).subscribe((result: any) => {
              this.listTopics = this.listTopics.filter(t => this.selectedTopics.find(x => x === t.id) == null);
            }, (err: HttpErrorResponse) => {
              console.log(err);
            });
          }
        }
      });
    }

  }

  isSelected(topic: Topic) {
    return topic && this.selectedTopics.indexOf(topic.id) !== -1;
  }

}
