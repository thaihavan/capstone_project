import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { LoginPageComponent } from 'src/app/pages/login-page/login-page.component';
import { Globals } from 'src/globals/globalvalues';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  checkLogin: boolean;
  checkLogined: boolean;
  userId: string;
  urlImgavatar = 'https://gody.vn/public/v3/images/bg/br-register.jpg';
  constructor(private globals: Globals, private dialog: MatDialog) { }

  ngOnInit() {
    if (localStorage.getItem('Token') != null) {
      this.checkLogin = false;
      this.checkLogined = true;
    } else {
      this.checkLogin = true;
      this.checkLogined = false;
    }
  }

  openDialogLoginForm() {
    const dialogRef = this.dialog.open(LoginPageComponent, {
      height: 'auto',
      width: '400px'
    });
  }

  signOut() {
    localStorage.clear();
    window.location.href = window.location.href = this.globals.urllocal;
  }

  gotoPersonalPage() {
    const account = JSON.parse(localStorage.getItem('Account'));
    this.userId = account.userId;
    window.location.href = '/personal/article?userId=' + this.userId;
  }

  gotoBookmarkList() {
    window.location.href = '/personal/bookmarks';
  }

  gotoBlockedList() {
    window.location.href = '/personal/blocked';
  }
}
