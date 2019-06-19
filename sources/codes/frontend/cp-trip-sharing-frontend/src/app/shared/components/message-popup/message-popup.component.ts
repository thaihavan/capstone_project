import { Component, OnInit, Input, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Globals } from 'src/globals/globalvalues';
import { Message } from 'src/app/model/Message';
@Component({
  selector: 'app-message-popup',
  templateUrl: './message-popup.component.html',
  styleUrls: ['./message-popup.component.css']
})
export class MessagePopupComponent implements OnInit {
  message = new Message();
  constructor(public globals: Globals) {}

  ngOnInit() {
  }

  gotoHomePage() {
    window.location.href = this.globals.urllocal + this.message.url;
  }
}
