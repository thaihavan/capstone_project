import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportedCommentPageComponent } from './reported-comment-page.component';

describe('ReportedCommentPageComponent', () => {
  let component: ReportedCommentPageComponent;
  let fixture: ComponentFixture<ReportedCommentPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportedCommentPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportedCommentPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
