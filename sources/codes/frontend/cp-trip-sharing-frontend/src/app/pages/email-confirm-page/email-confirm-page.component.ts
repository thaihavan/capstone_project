import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-email-confirm-page',
  templateUrl: './email-confirm-page.component.html',
  styleUrls: ['./email-confirm-page.component.css']
})
export class EmailConfirmPageComponent implements OnInit {

  token: string = null;

  constructor(private route: ActivatedRoute, private userService: UserService) {

  }

  ngOnInit() {
    this.token = this.route.snapshot.paramMap.get('token');
    this.userService.verifyEmail(this.token).subscribe(() => {
      // TODO:
      // Display popup xác nhận email thành công
      // redirect sang hompage sau 3s
      window.location.href = '';
    }, (err: HttpErrorResponse) => {
      // TODO: 
      // Display popup xác nhận email thất bại.
      // redirect sang hompage sau 3s
    });
  }

}
