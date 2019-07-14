import { Component, OnInit } from '@angular/core';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { FormAddTopicsComponent } from './form-add-topics/form-add-topics.component';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';

@Component({
  selector: 'app-interested-topic-admins',
  templateUrl: './interested-topic-admins.component.html',
  styleUrls: ['./interested-topic-admins.component.css']
})
export class InterestedTopicAdminsComponent implements OnInit {
  listTopics: any[] = [];

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

  showPopupAddTopic() {
    this.openDialog();
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(FormAddTopicsComponent, {
      width: '500px',
      data: {
        toppics: [],
        destinations: [],
      }
    });
  }

  removeTopic(topicId: any) {
    debugger;
    this.postService.removeTopic(topicId).subscribe((result: any) => {
      this.openDialogMessageConfirm();
    }, (err: HttpErrorResponse) => {
      console.log(err);
    });
  }

  openDialogMessageConfirm() {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '320px',
      height: 'auto',
      position: {
        top: '20px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = 'Bạn đã xóa chủ đề thành công!';
    instance.message.url = '/dashboard/chu-de';
  }

}
