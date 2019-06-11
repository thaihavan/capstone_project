import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InterestedtopicPageComponent } from './interestedtopic-page.component';

describe('InterestedtopicPageComponent', () => {
  let component: InterestedtopicPageComponent;
  let fixture: ComponentFixture<InterestedtopicPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InterestedtopicPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InterestedtopicPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
