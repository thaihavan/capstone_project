import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ForgotpasswordPageComponent } from './forgotpassword-page.component';

describe('ForgotpasswordPageComponent', () => {
  let component: ForgotpasswordPageComponent;
  let fixture: ComponentFixture<ForgotpasswordPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ForgotpasswordPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ForgotpasswordPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
