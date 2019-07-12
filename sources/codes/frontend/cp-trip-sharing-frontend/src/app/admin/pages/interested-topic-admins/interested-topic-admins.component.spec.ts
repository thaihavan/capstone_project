import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InterestedTopicAdminsComponent } from './interested-topic-admins.component';

describe('InterestedTopicAdminsComponent', () => {
  let component: InterestedTopicAdminsComponent;
  let fixture: ComponentFixture<InterestedTopicAdminsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InterestedTopicAdminsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InterestedTopicAdminsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
