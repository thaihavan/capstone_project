import { Component, OnInit } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<LoginPageComponent>) { }

  ngOnInit() {
  }
  callRegisterPage() : void{
    this.dialogRef.close();
  }

  forgotPassword() : void{
    this.dialogRef.close();
  }
}
