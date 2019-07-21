import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-header-admin',
  templateUrl: './header-admin.component.html',
  styleUrls: ['./header-admin.component.css']
})
export class HeaderAdminComponent implements OnInit {
  urlImgavatar = 'https://qph.fs.quoracdn.net/main-qimg-573142324088396d86586adb93f4c8c2';
  name = 'Admin';
  constructor(private userService: UserService) { }

  ngOnInit() {
  }

  gotoPersonalPage() {
    
  }

  signOut() {
    localStorage.clear();
    this.userService.Logout().subscribe((res: any) => {
      window.location.href = '';
    }, (error: HttpErrorResponse) => {
      console.log(error);
      window.location.href = '';
    });
  }

}
