import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListPostPageComponent } from './list-post-page.component';

describe('ListPostPageComponent', () => {
  let component: ListPostPageComponent;
  let fixture: ComponentFixture<ListPostPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListPostPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListPostPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
