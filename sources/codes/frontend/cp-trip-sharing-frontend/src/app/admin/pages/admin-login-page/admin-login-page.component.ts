import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user-service/user.service';

@Component({
  selector: 'app-admin-login-page',
  templateUrl: './admin-login-page.component.html',
  styleUrls: ['./admin-login-page.component.css']
})
export class AdminLoginPageComponent implements OnInit {

  constructor(private userService: UserService) { }

  ngOnInit() {
  }

}
