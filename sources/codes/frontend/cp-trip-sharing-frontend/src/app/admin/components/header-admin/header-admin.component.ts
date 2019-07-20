import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-header-admin',
  templateUrl: './header-admin.component.html',
  styleUrls: ['./header-admin.component.css']
})
export class HeaderAdminComponent implements OnInit {
  urlImgavatar = 'https://qph.fs.quoracdn.net/main-qimg-573142324088396d86586adb93f4c8c2';
  name = 'Admin';
  constructor() { }

  ngOnInit() {
  }

  gotoPersonalPage() {
    
  }

  signOut() {

  }

}
