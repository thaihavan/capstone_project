import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  showformLogin = false;
  showformLoginFB = true;

  constructor() { }

  ngOnInit() {
  }
  changePage(){
    if(this.showformLogin == true && this.showformLoginFB == false ){
      this.showformLogin=false;
      this.showformLoginFB = true;
    }
    else{
      this.showformLogin=true;
      this.showformLoginFB = false;
    }
    
  }


}
