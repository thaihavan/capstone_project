import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  showformLogin = false;
  showformLoginFB = true;
  isRegisterForm = true;
  isLoginForm = false;
  isClickRegister = false;
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

  checkSigInOrSignUp(){
    if(this.isRegisterForm == true && this.isLoginForm == false ){
      this.isRegisterForm=false;
      this.isLoginForm = true;
    }
    else{
      this.isRegisterForm=true;
      this.isLoginForm = false;
    }
    
  }

  checkClickRegister(){
    this.isClickRegister = true;
    this.showformLogin = false;
    this.showformLoginFB = false;
    this.isRegisterForm = false;
    this.isLoginForm = false;
  }


}
