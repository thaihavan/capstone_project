import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchResultContainerComponent } from './search-result-container.component';

describe('SearchResultContainerComponent', () => {
  let component: SearchResultContainerComponent;
  let fixture: ComponentFixture<SearchResultContainerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchResultContainerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchResultContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
