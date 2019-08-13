import { Component, OnInit, Input, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PopupMessage } from 'src/app/model/PopupMessage';
@Component({
  selector: 'app-message-popup',
  templateUrl: './message-popup.component.html',
  styleUrls: ['./message-popup.component.css']
})
export class MessagePopupComponent implements OnInit {
  message = new PopupMessage();

  title: string;
  constructor(private dialogRef: MatDialogRef<MessagePopupComponent>) { }

  ngOnInit() {
    switch (this.message.messageType) {
      case 'success':
        this.title = 'Thành công';
        break;
      case 'danger':
        this.title = 'Thất bại';
        break;
      case 'confirm':
        this.title = 'Xác nhận';
        break;
    }
  }

  continue() {
    if (this.message.messageType === 'confirm') {
      this.dialogRef.close('continue');
    } else {
      window.location.href = this.message.url;
    }
  }

  cancel() {
    if (this.message.messageType === 'confirm') {
      this.dialogRef.close('cancel');
    }
  }

}
