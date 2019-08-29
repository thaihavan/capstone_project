import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';
import { Account } from 'src/app/model/Account';
import { Title } from '@angular/platform-browser';
import {
  FormGroup,
  FormControl,
  Validators,
  FormGroupDirective,
  NgForm,
  FormBuilder
} from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Router } from '@angular/router';

/** Error when invalid control is dirty, touched, or submitted. */
class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(
    control: FormControl | null,
    form: FormGroupDirective | NgForm | null
  ): boolean {
    const isSubmitted = form && form.submitted;
    return !!(
      control &&
      (control.invalid || form.hasError('isNotMatchPassword')) &&
      (control.dirty || control.touched || isSubmitted)
    );
  }
}

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit {
  email = '';
  password = '';
  rePassword = '';
  agreePolicy = true;
  pasHide = true;
  isLoading = false;
  repass = '';
  errorEmailHasUse = false;
  errorRegister = false;
  message: string;
  account: Account;
  form: FormGroup;
  matcher = new MyErrorStateMatcher();
  constructor(
    private dialog: MatDialog,
    private userService: UserService,
    private titleService: Title,
    private fb: FormBuilder,
    private router: Router
  ) {
    this.titleService.setTitle('Đăng ký');
    this.account = new Account();
    this.initForm();
  }

  ngOnInit() {}

  // initial form
  initForm() {
    this.form = this.fb.group(
      {
        email: new FormControl('', [
          Validators.compose([
            Validators.required,
            Validators.email,
            Validators.maxLength(255),
          ])
        ]),
        password: new FormControl('', [
          Validators.compose([
            Validators.required,
            Validators.minLength(6),
            Validators.maxLength(255),
            Validators.pattern('^[_A-z0-9]*$')
          ])
        ]),
        rePassword: new FormControl('', [
          Validators.compose([Validators.required])
        ]),
        checkbox: new FormControl('', [this.isAgreePolicy])
      },
      {
        validator: this.verifyPassword
      }
    );
  }

  // form check has validation error
  public hasError = (controlName: string, errorName: string) => {
    return this.form.controls[controlName].hasError(errorName);
  }

  // form password verify
  verifyPassword(form: FormGroup) {
    const condition =
      form.get('password').value !== form.get('rePassword').value;
    return condition ? { isNotMatchPassword: true } : null;
  }

  // form required agree policy
  isAgreePolicy(form: FormControl) {
    const condition =
      form.value !== true;
    return condition ? { isNotAgreePolicy: true } : null;
  }

  // on email change
  emailOnchage() {
    this.errorEmailHasUse = false;
  }

  onSubmit() {
      this.isLoading = true;
      this.account.email = this.form.value.email;
      this.account.password = this.form.value.password;
      this.userService.registerAccount(this.account).subscribe(
        (message: any) => {
        },
        (err: HttpErrorResponse) => {
          this.errorRegister = true;
          if (err.error.message === 'Email is in use') {
            this.message = 'Email đã được sử dụng';
          } else {
            this.message = 'Đăng kí thất bại!';
          }
          console.log(err);
          this.isLoading = false;
        },
        () => {
          this.errorEmailHasUse = false;
          this.errorRegister = false;
          this.openDialogMessageConfirm('success');
          setTimeout(() => {
            this.router.navigate(['/']);
            // window.location.href = '';
          }, 5000);
        }
      );
  }

  openDialogMessageConfirm(messageType: string) {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '500px',
      height: 'auto',
      position: {
        top: '20px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageType = messageType;
    instance.message.messageText =
      'Đăng kí thành công, vui lòng kiểm tra lại email!';
    instance.message.url = '/trang-chu';
  }
}
