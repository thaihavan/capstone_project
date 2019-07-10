import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleSmallComponent } from './article-small.component';

describe('ArticleSmallComponent', () => {
  let component: ArticleSmallComponent;
  let fixture: ComponentFixture<ArticleSmallComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArticleSmallComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticleSmallComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
