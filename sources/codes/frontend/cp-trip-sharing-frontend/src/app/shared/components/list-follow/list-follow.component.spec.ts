import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListFollowComponent } from './list-follow.component';

describe('ListFollowComponent', () => {
  let component: ListFollowComponent;
  let fixture: ComponentFixture<ListFollowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListFollowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListFollowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
