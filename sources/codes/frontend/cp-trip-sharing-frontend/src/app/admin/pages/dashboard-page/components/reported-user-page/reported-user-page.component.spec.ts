import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportedUserPageComponent } from './reported-user-page.component';

describe('ReportedUserPageComponent', () => {
  let component: ReportedUserPageComponent;
  let fixture: ComponentFixture<ReportedUserPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportedUserPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportedUserPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
