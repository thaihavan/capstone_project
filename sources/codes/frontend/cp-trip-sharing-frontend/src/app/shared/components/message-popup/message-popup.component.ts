import { Component, OnInit, Input, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Globals } from 'src/globals/globalvalues';
import { PopupMessage } from 'src/app/model/PopupMessage';
@Component({
  selector: 'app-message-popup',
  templateUrl: './message-popup.component.html',
  styleUrls: ['./message-popup.component.css']
})
export class MessagePopupComponent implements OnInit {
  message = new PopupMessage();
  constructor(public globals: Globals) {}

  ngOnInit() {
  }

  gotoHomePage() {
    window.location.href = this.globals.urllocal + this.message.url;
  }
}
