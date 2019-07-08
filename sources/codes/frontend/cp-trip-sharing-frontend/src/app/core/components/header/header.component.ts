import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { LoginPageComponent } from 'src/app/pages/login-page/login-page.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  checkLogin: boolean;
  checkLogined: boolean;
  userId: string;
  urlImgavatar = 'https://qph.fs.quoracdn.net/main-qimg-573142324088396d86586adb93f4c8c2';
  numberNotification = 1;
  checkMessageNotification = false;
  checkNotification = false;
  constructor(private dialog: MatDialog) { }

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
    window.location.href = '/';
  }

  gotoPersonalPage() {
    const account = JSON.parse(localStorage.getItem('Account'));
    this.userId = account.userId;
    window.location.href = '/user/' + this.userId;
  }

  gotoBookmarkList() {
    const account = JSON.parse(localStorage.getItem('Account'));
    this.userId = account.userId;
    window.location.href = '/user/' + this.userId + '/da-danh-dau';
  }

  gotoBlockedList() {
    const account = JSON.parse(localStorage.getItem('Account'));
    this.userId = account.userId;
    window.location.href = '/user/' + this.userId + '/danh-sach-chan';
  }

  viewNotification() {
    this.checkNotification = true;
  }

  viewMessageNotification() {
    this.checkMessageNotification = true;
  }
}
