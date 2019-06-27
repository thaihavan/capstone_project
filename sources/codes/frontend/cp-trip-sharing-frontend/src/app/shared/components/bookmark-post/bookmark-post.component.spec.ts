import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookmarkPostComponent } from './bookmark-post.component';

describe('BookmarkPostComponent', () => {
  let component: BookmarkPostComponent;
  let fixture: ComponentFixture<BookmarkPostComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookmarkPostComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookmarkPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
