import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListBookmarksComponent } from './list-bookmarks.component';

describe('ListBookmarksComponent', () => {
  let component: ListBookmarksComponent;
  let fixture: ComponentFixture<ListBookmarksComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListBookmarksComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListBookmarksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
