import { Component, OnInit } from '@angular/core';
import {MatDialog, MatDialogConfig} from '@angular/material';
import { InterestedtopicPageComponent } from '../interestedtopic-page/interestedtopic-page.component';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit {

  constructor(public dialog: MatDialog) { }

  ngOnInit() {
  }

  callInterestedtopicPage(): void {
    const dialogRef = this.dialog.open(InterestedtopicPageComponent, {
      height: 'auto',
      width: '60%'
    });
  }
}
