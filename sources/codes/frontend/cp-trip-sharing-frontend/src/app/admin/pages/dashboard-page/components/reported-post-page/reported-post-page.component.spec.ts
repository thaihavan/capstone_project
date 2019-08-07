import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportedPostPageComponent } from './reported-post-page.component';

describe('ReportedPostPageComponent', () => {
  let component: ReportedPostPageComponent;
  let fixture: ComponentFixture<ReportedPostPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportedPostPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportedPostPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
