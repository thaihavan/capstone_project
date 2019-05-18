import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OnlyForHomePageComponent } from './only-for-home-page.component';

describe('OnlyForHomePageComponent', () => {
  let component: OnlyForHomePageComponent;
  let fixture: ComponentFixture<OnlyForHomePageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OnlyForHomePageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OnlyForHomePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
