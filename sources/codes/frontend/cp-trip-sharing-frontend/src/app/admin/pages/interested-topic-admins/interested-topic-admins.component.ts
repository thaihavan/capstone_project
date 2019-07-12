import { Component, OnInit } from '@angular/core';
import { PostService } from 'src/app/core/services/post-service/post.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { FormAddTopicsComponent } from './form-add-topics/form-add-topics.component';

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


}
