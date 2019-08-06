import { Component, OnInit, Inject } from '@angular/core';
// tslint:disable-next-line:max-line-length
import { CreateFindingCompanionsPostComponent } from 'src/app/pages/create-finding-companions-post/create-finding-companions-post.component';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AlertifyService } from 'src/app/core/services/alertify-service/alertify.service';

@Component({
  selector: 'app-dialog-step-finding-companions',
  templateUrl: './dialog-step-finding-companions.component.html',
  styleUrls: ['./dialog-step-finding-companions.component.css']
})
export class DialogStepFindingCompanionsComponent implements OnInit {
  scheduleTitle = '';
  scheduleNote = '';
  scheduleDate: Date;
  constructor(
    private dialogRef: MatDialogRef<CreateFindingCompanionsPostComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.scheduleTitle = this.data.scheduleTitle;
    this.scheduleNote = this.data.scheduleNote;
    this.scheduleDate = this.data.scheduleDate;
  }

  // input title has value
  onChange() {}

  // text are on change
  onChangeArea() {}

  // create step
  onCreate() {
    if (this.scheduleTitle === '' || this.scheduleNote === '' || this.scheduleDate === null) {
      this.alertify.error('Yêu cầu nhập đầy đủ thông tin');
      return;
    }
    this.data.scheduleTitle = this.scheduleTitle;
    this.data.scheduleNote = this.scheduleNote;
    this.data.scheduleDate = this.scheduleDate;
    this.dialogRef.close(this.data);
  }

  // colse dialog
  onClose() {
    this.dialogRef.close();
  }
}
