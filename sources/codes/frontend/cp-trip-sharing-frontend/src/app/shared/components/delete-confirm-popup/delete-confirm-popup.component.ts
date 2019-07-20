import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-delete-confirm-popup',
  templateUrl: './delete-confirm-popup.component.html',
  styleUrls: ['./delete-confirm-popup.component.css']
})
export class DeleteConfirmPopupComponent implements OnInit {

  message: string;

  constructor(private dialogRef: MatDialogRef<DeleteConfirmPopupComponent>) { }

  ngOnInit() {
  }

  yes() {
    this.dialogRef.close('yes');
  }

  no() {
    this.dialogRef.close('no');
  }

}
