import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dialog-create-trip',
  templateUrl: './dialog-create-trip.component.html',
  styleUrls: ['./dialog-create-trip.component.css']
})
export class DialogCreateTripComponent implements OnInit {
  title: string;
  isPublic = true;
  note: string;
  isHasTitle = true;
  constructor(
    private dialogRef: MatDialogRef<DialogCreateTripComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private router: Router
  ) {}

  ngOnInit() {
    this.title = this.data.title;
    this.note = this.data.note;
  }

  // input title has value
  onChange() {
    if (this.title.trim() === '') {
      this.isHasTitle = true;
    } else {
      this.isHasTitle = false;
    }
  }

  // text are on change
  onChangeArea() {
    if (this.data.edit) {
      this.isHasTitle = false;
    }
  }

  // create virtual trip
  onCreate() {
    this.data.title = this.title;
    this.data.isPublic = this.isPublic;
    this.data.note = this.note;
    if (this.data.edit) {
      this.data.save = true;
    }
    this.dialogRef.close(this.data);
  }

  // close dialog redirect to home page
  onClose() {
    this.dialogRef.close();
    if (!this.data.edit) {
      this.router.navigate(['/trang-chu']);
    }
  }
}
