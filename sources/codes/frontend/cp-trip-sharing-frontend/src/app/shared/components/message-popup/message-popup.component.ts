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
  @Input()
  typePopup = 'success';
  @Input() confirm: boolean;
  constructor() {}

  ngOnInit() {
    this.typePopup = this.message.messageType;
  }

  gotoHomePage() {
    window.location.href = this.message.url;
  }

}
