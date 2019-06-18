import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InitialUserInformationPageComponent } from './initial-user-information-page.component';

describe('InitialUserInformationPageComponent', () => {
  let component: InitialUserInformationPageComponent;
  let fixture: ComponentFixture<InitialUserInformationPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InitialUserInformationPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InitialUserInformationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
