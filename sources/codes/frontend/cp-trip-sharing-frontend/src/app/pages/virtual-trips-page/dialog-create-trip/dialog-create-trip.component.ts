import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { VirtualTripsPageComponent } from '../virtual-trips-page.component';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dialog-create-trip',
  templateUrl: './dialog-create-trip.component.html',
  styleUrls: ['./dialog-create-trip.component.css']
})
export class DialogCreateTripComponent implements OnInit {
  title: string;
  isPublic: any;
  isHasTitle = true;
  constructor(
    private dialogRef: MatDialogRef<DialogCreateTripComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private router: Router
  ) {}

  ngOnInit() {
  }

  // input title has value
  onChange() {
    if (this.title === '') {
      this.isHasTitle = true;
    } else {
      this.isHasTitle = false;
    }
  }

  // create virtual trip
  onCreate() {
    this.data.title = this.title;
    this.data.isPublic = this.isPublic;
    this.dialogRef.close(this.data);
  }

  // close dialog redirect to home page
  onClose() {
    this.dialogRef.close();
    this.router.navigate(['/home']);
  }
}
