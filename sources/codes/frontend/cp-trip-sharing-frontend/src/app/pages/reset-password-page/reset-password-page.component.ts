import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';
import { MatDialog, ErrorStateMatcher } from '@angular/material';
import { ResetPasswordModel } from 'src/app/model/ResetPasswordModel';
import { HttpErrorResponse } from '@angular/common/http';
import { Title } from '@angular/platform-browser';
import { FormGroup, FormControl, Validators, FormGroupDirective, NgForm } from '@angular/forms';

@Component({
  selector: 'app-reset-password-page',
  templateUrl: './reset-password-page.component.html',
  styleUrls: ['./reset-password-page.component.css']
})
export class ResetPasswordPageComponent implements OnInit {
  token: string = null;
  message: string = null;

  password = '';
  rePassword = '';

  pasHide = true;
  isLoading = false;
  hasErrorMessage = false;

  matcher = new MyErrorStateMatcher();
  resetPasswordModel: ResetPasswordModel;
  form = new FormGroup({
    password: new FormControl('', [
      Validators.compose([
        Validators.required,
        Validators.minLength(6),
        Validators.maxLength(255),
        Validators.pattern('^[_A-z0-9]*$')
      ])
    ]),
    rePassword: new FormControl('', [
      Validators.required,
    ])
  }, {
    validators: this.verifyPassword
  });
  constructor(private route: ActivatedRoute, private dialog: MatDialog, private userService: UserService,
              private titleService: Title) {
    this.titleService.setTitle('Đặt lại mật khẩu');
    this.resetPasswordModel = new ResetPasswordModel();
  }

  ngOnInit() {
    this.token = this.route.snapshot.paramMap.get('token');
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
    instance.message.messageText = 'Đổi mật khẩu thành công bạn có thể đăng nhập lại!';
    instance.message.url = '/trang-chu';
  }

  resetPassword() {
      this.isLoading = true;
      this.resetPasswordModel.NewPassword = this.password;
      this.userService.resetPassword(this.token, this.resetPasswordModel).subscribe((data: any) => {
        this.openDialogMessageConfirm('success');
      }, (err: HttpErrorResponse) => {
        this.isLoading = false;
        this.hasErrorMessage = true;
        this.message = 'Đặt lại mật khẩu thất bại!';
      },
      () => {
        this.isLoading = false;
      });
  }

  onSubmit() {
    this.resetPassword();
  }

}

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
