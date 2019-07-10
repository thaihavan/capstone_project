import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListPostHorizontalComponent } from './list-post-horizontal.component';

describe('ListPostHorizontalComponent', () => {
  let component: ListPostHorizontalComponent;
  let fixture: ComponentFixture<ListPostHorizontalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListPostHorizontalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListPostHorizontalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
