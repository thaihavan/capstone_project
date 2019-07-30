import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  FormControl,
  Validators,
  NgForm,
  FormGroupDirective
} from '@angular/forms';
import { MatDialog, ErrorStateMatcher, MatDialogRef } from '@angular/material';
import { Title } from '@angular/platform-browser';
import { Account } from 'src/app/model/Account';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { ChangePassword } from 'src/app/model/ChangePassword';
import { HttpErrorResponse } from '@angular/common/http';
import { AlertifyService } from 'src/app/core/services/alertify-service/alertify.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  password = '';
  newPassword = '';
  rePassword = '';

  pasHide = true;
  isLoading = false;
  errorChangePass = false;

  changePasswordObject = new ChangePassword();
  message: string;
  form: FormGroup;
  matcher = new MyErrorStateMatcher();
  newOldPassMatcher = new NewPassEqualOldPassMatcher();
  constructor(
    private userService: UserService,
    private titleService: Title,
    private fb: FormBuilder,
    private alertify: AlertifyService,
    private dialogRef: MatDialogRef<ChangePasswordComponent>,
  ) {
    this.titleService.setTitle('Đăng ký');
    this.initForm();
  }

  ngOnInit() {}

  // initial form
  initForm() {
    this.form = this.fb.group(
      {
        password: new FormControl('', [
          Validators.compose([
            Validators.required,
            Validators.minLength(6),
            Validators.maxLength(255),
            Validators.pattern('^[_A-z0-9]*$')
          ])
        ]),
        newPassword: new FormControl('', [
          Validators.compose([
            Validators.required,
            Validators.minLength(6),
            Validators.maxLength(255),
            Validators.pattern('^[_A-z0-9]*$')
          ])
        ]),
        rePassword: new FormControl('', [
          Validators.compose([Validators.required])
        ])
      },
      {
        validators: [this.verifyPassword, this.verifyNewOldPassword]
      }
    );
  }

  // form check has validation error
  public hasError = (controlName: string, errorName: string) => {
    return this.form.controls[controlName].hasError(errorName);
  }

  // form password verify
  verifyNewOldPassword(form: FormGroup) {
    const condition =
      form.get('password').value === form.get('newPassword').value;
    return condition ? { isNewPassEqualOldPass: true } : null;
  }

  // form password verify
  verifyPassword(form: FormGroup) {
    const condition =
      form.get('newPassword').value !== form.get('rePassword').value;
    return condition ? { isNotMatchPassword: true } : null;
  }

  onSubmit() {
    this.isLoading = true;
    this.changePasswordObject.CurrentPassword = this.password;
    this.changePasswordObject.NewPassword = this.newPassword;
    this.userService.changePassword(this.changePasswordObject).subscribe(
      (message: any) => {
        // this.message = 'Đổi mật khẩu thành công';
      },
      (err: HttpErrorResponse) => {
        this.isLoading = false;
        this.errorChangePass = true;
        this.message = 'Đổi mật khẩu thất bại!';
        this.alertify.error('Đổi mật khẩu thất bại!');
      },
      () => {
        this.alertify.success('Đổi mật khẩu thành công');
        this.dialogRef.close();
      }
    );
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

class NewPassEqualOldPassMatcher implements ErrorStateMatcher {
  isErrorState(
    control: FormControl | null,
    form: FormGroupDirective | NgForm | null
  ): boolean {
    const isSubmitted = form && form.submitted;
    return !!(
      control &&
      (control.invalid || form.hasError('isNewPassEqualOldPass')) &&
      (control.dirty || control.touched || isSubmitted)
    );
  }
}
