import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TopicPageAdminComponent } from './topic-page-admin.component';

describe('TopicPageAdminComponent', () => {
  let component: TopicPageAdminComponent;
  let fixture: ComponentFixture<TopicPageAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TopicPageAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TopicPageAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
